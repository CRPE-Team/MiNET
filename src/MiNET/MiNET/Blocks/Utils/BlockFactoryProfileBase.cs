using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using MiNET.Utils;
using MiNET.Utils.Nbt;

namespace MiNET.Blocks.Utils
{
	public abstract class BlockFactoryProfileBase : IBlockFactoryProfile
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(BlockFactoryProfileBase));

		private bool _loaded = false;

		public abstract bool BlockRuntimeIdsAreHashes { get; }


		public Dictionary<string, IBlockStateContainer> MetaBlockNameToState { get; private set; } = new Dictionary<string, IBlockStateContainer>();
		public Dictionary<string, Type> IdToType { get; private set; } = new Dictionary<string, Type>();
		public Dictionary<Type, string> TypeToId { get; private set; } = new Dictionary<Type, string>();
		public Dictionary<string, Func<Block>> IdToFactory { get; private set; } = new Dictionary<string, Func<Block>>();
		public Dictionary<string, string> BlockIdToItemId { get; private set; }
		public Dictionary<string, string> ItemIdToBlockId { get; private set; }
		public Dictionary<string, BlockProperties> IdToProperties { get; private set; }

		public abstract IBlockPalette BlockPalette { get; }
		public HashSet<string> Ids { get; private set; } = new HashSet<string>();
		public HashSet<IBlockStateContainer> BlockStates { get; protected set; }


		public virtual bool Load()
		{
			if (_loaded) return false;

			var assembly = Assembly.GetAssembly(typeof(Block));

			IdToProperties = ResourceUtil.ReadResource<Dictionary<string, BlockProperties>>("block_properties_table.json", typeof(BlockFactory), "Data");
			BlockIdToItemId = ResourceUtil.ReadResource<Dictionary<string, string>>("block_id_to_item_id_map.json", typeof(BlockFactory), "Data");
			ItemIdToBlockId = BlockIdToItemId.ToDictionary(pair => pair.Value, pair => pair.Key);

			OnLoad();

			var visitedContainers = new HashSet<IBlockStateContainer>();
			var blockMapEntry = new List<R12ToCurrentBlockMapEntry>();

			using (var stream = assembly.GetManifestResourceStream(typeof(BlockFactory).Namespace + ".Data.r12_to_current_block_map.bin"))
			{
				while (stream.Position < stream.Length)
				{
					var length = VarInt.ReadUInt32(stream);
					byte[] bytes = new byte[length];
					stream.Read(bytes, 0, bytes.Length);

					string stringId = Encoding.UTF8.GetString(bytes);

					bytes = new byte[2];
					stream.Read(bytes, 0, bytes.Length);
					var meta = BitConverter.ToInt16(bytes);

					var compound = NbtExtensions.ReadNbtCompound(stream);

					var state = BlockUtils.GetBlockStateContainer(compound);

					if (!visitedContainers.TryGetValue(state, out _))
					{
						blockMapEntry.Add(new R12ToCurrentBlockMapEntry(stringId, meta, state));
						visitedContainers.Add(state);
					}
				}
			}

			var idToStatesMap = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

			foreach (var state in BlockPalette)
			{
				if (!idToStatesMap.TryGetValue(state.Id, out var candidates))
				{
					idToStatesMap.Add(state.Id, candidates = new List<int>());
				}

				candidates.Add(state.RuntimeId);
			}

			foreach (var entry in blockMapEntry)
			{
				var data = entry.Meta;

				var mappedState = entry.State;
				var mappedName = entry.State.Id;

				if (!idToStatesMap.TryGetValue(mappedName, out var matching))
				{
					continue;
				}

				foreach (var match in matching)
				{
					var networkState = BlockPalette[match];

					var thisStates = new HashSet<IBlockState>(mappedState.States);
					var otherStates = new HashSet<IBlockState>(networkState.States);

					otherStates.IntersectWith(thisStates);

					if (otherStates.Count == thisStates.Count)
					{
						((PaletteBlockStateContainer) BlockPalette[match]).Data = data;

						var id = BlockIdToItemId.GetValueOrDefault(mappedName, mappedName);

						break;
					}
				}
			}

			foreach (PaletteBlockStateContainer record in BlockPalette)
			{
				MetaBlockNameToState.TryAdd(BlockUtils.GetMetaBlockName(record.Id, record.Data), record);
			}

			BlockStates = new HashSet<IBlockStateContainer>(BlockPalette);

			(IdToType, TypeToId) = BuildIdTypeMapPair();
			IdToFactory = BuildIdToFactory();

			_loaded = true;
			return true;
		}

		protected abstract void OnLoad();

		public abstract string GetIdByRuntimeId(int id);
		public abstract bool IsTransparent(int runtimeId);

		public string GetBlockIdFromItemId(string id)
		{
			return ItemIdToBlockId.GetValueOrDefault(id);
		}

		public string GetIdByType<T>(bool withRoot = true) where T : Block
		{
			return GetIdByType(typeof(T), withRoot);
		}

		public string GetIdByType(Type type, bool withRoot = true)
		{
			return withRoot
				? TypeToId.GetValueOrDefault(type)
				: TypeToId.GetValueOrDefault(type).Replace("minecraft:", "");
		}

		public T GetBlockById<T>(string id) where T : Block
		{
			return (T) GetBlockById(id);
		}

		public Block GetBlockById(string id, short metadata)
		{
			var block = GetBlockById(id);

			if (!MetaBlockNameToState.TryGetValue(BlockUtils.GetMetaBlockName(id, metadata), out var map))
			{
				return block;
			}

			block.SetStates(map.States);

			return block;
		}

		public Block GetBlockById(string id)
		{
			if (string.IsNullOrEmpty(id)) return null;

			return IdToFactory.GetValueOrDefault(id)?.Invoke();
		}

		public Block GetBlockByRuntimeId(int runtimeId)
		{
			var blockState = BlockPalette.GetContainer(runtimeId);
			if (blockState == null)
			{
				return null;
			}

			var block = GetBlockById(blockState.Id);
			if (block != null)
			{
				block.SetStates(blockState.States);
			}

			return block;
		}

		public bool IsBlock<T>(int runtimeId) where T : Block
		{
			return IsBlock(runtimeId, typeof(T));
		}

		public bool IsBlock<T>(string id) where T : Block
		{
			return IsBlock(id, typeof(T));
		}

		public bool IsBlock(string id, Type blockType)
		{
			if (string.IsNullOrEmpty(id)) return false;

			var type = IdToType.GetValueOrDefault(id);

			return type?.IsAssignableTo(blockType) ?? false;
		}

		public bool IsBlock(int runtimeId, Type blockType)
		{
			var blockState = BlockPalette.GetContainer(runtimeId);
			if (blockState == null) return false;

			return IsBlock(blockState.Id, blockType);
		}

		public IBlockStateContainer GetStateContainer(int runtimeId)
		{
			return BlockPalette.GetContainer(runtimeId);
		}

		public bool GetStateContainer(IBlockStateContainer container, out IBlockStateContainer palette)
		{
			return BlockStates.TryGetValue(container, out palette);
		}


		private (Dictionary<string, Type>, Dictionary<Type, string>) BuildIdTypeMapPair()
		{
			var idToType = new Dictionary<string, Type>(Ids.Count);
			var typeToId = new Dictionary<Type, string>(Ids.Count);

			var blockTypes = typeof(BlockFactory).Assembly.GetTypes().Where(type => type.IsAssignableTo(typeof(Block)) && !type.IsAbstract);

			foreach (var type in blockTypes)
			{
				if (type == typeof(Block))
					continue;

				var block = (Block) Activator.CreateInstance(type);

				if (string.IsNullOrEmpty(block.Id))
				{
					Log.Error($"Detected block without id [{type}]");
					continue;
				}

				idToType[block.Id] = type;
				typeToId[type] = block.Id;
			}

			return (idToType, typeToId);
		}

		private Dictionary<string, Func<Block>> BuildIdToFactory()
		{
			var idToFactory = new Dictionary<string, Func<Block>>(Ids.Count);

			foreach (var pair in IdToType)
			{
				// faster then Activator.CreateInstance
				var constructorExpression = Expression.New(pair.Value);
				var lambdaExpression = Expression.Lambda<Func<Block>>(constructorExpression);
				var createFunc = lambdaExpression.Compile();

				idToFactory.Add(pair.Key, createFunc);
			}

			return idToFactory;
		}
	}
}
