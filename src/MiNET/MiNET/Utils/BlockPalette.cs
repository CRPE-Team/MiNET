#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE.
// The License is based on the Mozilla Public License Version 1.1, but Sections 14
// and 15 have been added to cover use of software over a computer network and
// provide for limited attribution for the Original Developer. In addition, Exhibit A has
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using fNbt;
using MiNET.Blocks;
using Newtonsoft.Json;

namespace MiNET.Utils
{
	public class BlockPalette : List<IBlockStateContainer>
	{
		public static int Version => 17694723;

		public static BlockPalette FromJson(string json)
		{
			var pallet = new BlockPalette();

			dynamic result = JsonConvert.DeserializeObject<dynamic>(json);
			int runtimeId = 0;
			foreach (dynamic obj in result)
			{
				var record = new PaletteBlockStateContainer();
				record.Id = obj.Id;
				record.Data = obj.Data;
				record.RuntimeId = runtimeId++;

				foreach (dynamic stateObj in obj.States)
				{
					switch ((int) stateObj.Type)
					{
						case 1:
						{
							record.AddState(new BlockStateByte()
							{
								Name = stateObj.Name,
								Value = stateObj.Value
							});
							break;
						}
						case 3:
						{
							record.AddState(new BlockStateInt()
							{
								Name = stateObj.Name,
								Value = stateObj.Value
							});
							break;
						}
						case 8:
						{
							record.AddState(new BlockStateString()
							{
								Name = stateObj.Name,
								Value = stateObj.Value
							});
							break;
						}
					}
				}

				pallet.Add(record);
			}

			return pallet;
		}
	}

	public class BlockStateContainerEqualityComparer : IEqualityComparer<IBlockStateContainer>
	{
		public bool Equals(IBlockStateContainer x, IBlockStateContainer y)
		{
			if (ReferenceEquals(x, y)) return true;

			bool result = x.Id == y.Id;
			if (!result) return false;

			var xStates = new HashSet<IBlockState>(x.States);
			var yStates = new HashSet<IBlockState>(y.States);

			yStates.IntersectWith(xStates);
			result = yStates.Count == xStates.Count;

			return result;
		}

		public int GetHashCode([DisallowNull] IBlockStateContainer obj)
		{
			return obj.GetHashCode();
		}
	}

	[JsonObject(MemberSerialization.OptIn)]
	public interface IBlockStateContainer
	{
		[JsonProperty]
		public int RuntimeId { get; }
		[JsonProperty]
		public string Id { get; }
		[JsonProperty]
		public short Data { get; }

		[JsonProperty]
		public IEnumerable<IBlockState> States { get; }

		public byte[] StatesCacheNbt { get; }
		public NbtCompound StatesNbt { get; }
	}


	public class PaletteBlockStateContainer : IBlockStateContainer
	{
		private readonly List<IBlockState> _states;

		public int RuntimeId { get; set; }
		public string Id { get; set; }
		public short Data { get; set; }
		public IEnumerable<IBlockState> States => _states;

		public byte[] StatesCacheNbt { get; set; }
		public NbtCompound StatesNbt { get; set; }

		public PaletteBlockStateContainer()
		{
			_states = new List<IBlockState>();
		}

		public PaletteBlockStateContainer(string id, IEnumerable<IBlockState> states)
		{
			Id = id;
			_states = states.ToList();
		}

		public void AddState(IBlockState state)
		{
			_states.Add(state);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!obj.GetType().IsAssignableTo(typeof(IBlockStateContainer))) return false;
			return new BlockStateContainerEqualityComparer().Equals(this, (IBlockStateContainer) obj);
		}

		public override int GetHashCode()
		{
			var hash = new HashCode();
			hash.Add(Id);
			foreach (var state in States)
			{
				hash.Add(state);
			}

			int hashCode = hash.ToHashCode();
			return hashCode;
		}

		public override string ToString()
		{
			return $"{nameof(Id)}: {Id}, {nameof(Data)}: {Data}, {nameof(RuntimeId)}: {RuntimeId}, {nameof(States)} {{ {string.Join(';', States)} }}";
		}
	}

	public class BlockStateContainer : IBlockStateContainer
	{
		private IBlockStateContainer _cache;
		private bool _changed = true;
		private bool _factoryState = false;

		public virtual string Id { get; }
		public int RuntimeId => GetPaletteContainer()?.RuntimeId ?? -1;
		public short Data => GetPaletteContainer()?.Data ?? 0;

		public bool IsValidStates => GetPaletteContainer() != null;

		public IEnumerable<IBlockState> States => GetStates();

		public byte[] StatesCacheNbt => GetPaletteContainer()?.StatesCacheNbt;
		public NbtCompound StatesNbt => GetPaletteContainer()?.StatesNbt;

		private IBlockStateContainer GetPaletteContainer()
		{
			if (_changed)
			{
				if (_factoryState)
				{
					SpinWait spinWait = default;
					while (_factoryState)
					{
						spinWait.SpinOnce();
					}

					return _cache;
				}

				_factoryState = true;
				BlockFactory.BlockStates.TryGetValue(this, out _cache);

				_factoryState = false;
				_changed = false;
			}

			return _cache;
		}

		protected void NotifyStateUpdate(BlockStateString state, string value)
		{
			state.Value = value;
			_changed = true;
		}

		protected void NotifyStateUpdate(BlockStateInt state, int value)
		{
			state.Value = value;
			_changed = true;
		}

		protected void NotifyStateUpdate(BlockStateByte state, byte value)
		{
			state.Value = value;
			_changed = true;
		}

		protected void NotifyStateUpdate(BlockStateByte state, bool value)
		{
			state.Value = Convert.ToByte(value);
			_changed = true;
		}

		public void SetStates(IBlockStateContainer blockstate)
		{
			SetStates(blockstate.States);
		}

		public virtual void SetStates(IEnumerable<IBlockState> states)
		{

		}

		protected virtual IEnumerable<IBlockState> GetStates()
		{
			return Array.Empty<IBlockState>();
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!obj.GetType().IsAssignableTo(typeof(IBlockStateContainer))) return false;
			return new BlockStateContainerEqualityComparer().Equals(this, (IBlockStateContainer) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id);
		}

		public override string ToString()
		{
			return $"{nameof(Id)}: {Id}, {nameof(Data)}: {Data}, {nameof(RuntimeId)}: {RuntimeId}, {nameof(States)} {{ {string.Join(';', States)} }}";
		}
	}

	public class ItemPickInstance
	{
		public string Id { get; set; } = null;
		public short Metadata { get; set; } = -1;
		public bool WantNbt { get; set; } = false;
	}

	public interface IBlockState
	{
		public string Name { get; set; }

		public object GetValue();
	}

	public class BlockStateInt : IBlockState
	{
		public int Type { get; } = 3;
		public string Name { get; set; }
		public int Value { get; set; }

		public object GetValue() => Value;

		protected bool Equals(BlockStateInt other)
		{
			return Name == other.Name && Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((BlockStateInt) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GetType().Name, Name, Value);
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
		}
	}

	public class BlockStateByte : IBlockState
	{
		public int Type { get; } = 1;
		public string Name { get; set; }
		public byte Value { get; set; }

		public object GetValue() => Value;

		protected bool Equals(BlockStateByte other)
		{
			return Name == other.Name && Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((BlockStateByte) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GetType().Name, Name, Value);
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
		}
	}

	public class BlockStateString : IBlockState
	{
		public int Type { get; } = 8;
		public string Name { get; set; }
		public string Value { get; set; }

		public object GetValue() => Value;

		protected bool Equals(BlockStateString other)
		{
			return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (obj.GetType() != this.GetType())
				return false;
			return Equals((BlockStateString) obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(GetType().Name, Name, Value.ToLowerInvariant());
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
		}
	}
}