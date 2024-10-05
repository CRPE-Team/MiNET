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
using System.Linq;
using System.Threading;
using fNbt;
using fNbt.Serialization;
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

	[JsonObject(MemberSerialization.OptIn)]
	public interface IBlockStateContainer : IEquatable<IBlockStateContainer>
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

		public bool Equals(IBlockStateContainer obj)
		{
			return Id == obj?.Id && _states.SequenceEqual(obj.States);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj is not IBlockStateContainer other) return false;
			return Equals(other);
		}

		public override int GetHashCode()
		{
			var hash = new HashCode();
			hash.Add(Id);
			foreach (var state in States)
			{
				hash.Add(state);
			}

			return hash.ToHashCode();
		}

		public override string ToString()
		{
			return $"{nameof(Id)}: {Id}, {nameof(Data)}: {Data}, {nameof(RuntimeId)}: {RuntimeId}, {nameof(States)} {{ {string.Join(';', States)} }}";
		}
	}

	public class BlockStateContainer : IBlockStateContainer
	{
		private static readonly Dictionary<string, IBlockStateContainer> _defaultStates = new();

		private IBlockStateContainer _cache;
		private bool _changed = false;
		private bool _factoryState = false;

		[NbtProperty("name")]
		public virtual string Id { get; }
		public int RuntimeId => GetPaletteContainer()?.RuntimeId ?? -1;
		public short Data => GetPaletteContainer()?.Data ?? 0;

		public bool IsValidStates => GetPaletteContainer() != null;

		public IEnumerable<IBlockState> States => GetStates();

		public byte[] StatesCacheNbt => GetPaletteContainer()?.StatesCacheNbt;

		[NbtProperty("states")]
		public NbtCompound StatesNbt => GetPaletteContainer()?.StatesNbt;

		public BlockStateContainer()
		{
			if (!_defaultStates.TryGetValue(Id, out _cache))
			{
				BlockFactory.BlockStates.TryGetValue(this, out _cache);
				_defaultStates.TryAdd(Id, _cache);
			}
		}

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
			return [];
		}

		public bool Equals(IBlockStateContainer obj)
		{
			return Id == obj?.Id && GetStates().SequenceEqual(obj.States);
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj is not IBlockStateContainer other) return false;
			return Equals(other);
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

	public class BlockStateInt : IBlockState, ICloneable, IEquatable<BlockStateInt>
	{
		private int _value;

		public int Type { get; } = 3;
		public virtual string Name { get; set; }

		public virtual int Value
		{
			get => _value; 
			set
			{
				ValidateValue(value);
				_value = value;
			}
		}

		public object GetValue() => _value;

		public bool Equals(BlockStateInt other)
		{
			return Name == other.Name && Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!obj.GetType().IsAssignableTo(GetType())) return false;
			return Equals((BlockStateInt) obj);
		}

		public override int GetHashCode()
		{
			return Value;
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public static bool operator ==(BlockStateInt x, BlockStateInt y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(BlockStateInt x, BlockStateInt y)
		{
			return !x.Equals(y);
		}

		protected virtual void ValidateValue(int value)
		{

		}

		protected void ThrowArgumentException(int value)
		{
			throw new ArgumentOutOfRangeException(Name, value, null);
		}
	}

	public class BlockStateByte : IBlockState, ICloneable, IEquatable<BlockStateByte>
	{
		public int Type { get; } = 1;
		public virtual string Name { get; set; }
		public virtual byte Value { get; set; }

		public object GetValue() => Value;

		public bool Equals(BlockStateByte other)
		{
			return Name == other.Name && Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!obj.GetType().IsAssignableTo(GetType())) return false;
			return Equals((BlockStateByte) obj);
		}

		public override int GetHashCode()
		{
			return Value;
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public static bool operator ==(BlockStateByte x, BlockStateByte y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(BlockStateByte x, BlockStateByte y)
		{
			return !x.Equals(y);
		}
	}

	public class BlockStateString : IBlockState, ICloneable, IEquatable<BlockStateString>
	{
		public int Type { get; } = 8;
		public virtual string Name { get; set; }
		public virtual string Value { get; set; }

		public object GetValue() => Value;

		public bool Equals(BlockStateString other)
		{
			return Name == other.Name && Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (!obj.GetType().IsAssignableTo(GetType())) return false;
			return Equals((BlockStateString) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}";
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public static bool operator ==(BlockStateString x, BlockStateString y)
		{
			return x.Equals(y);
		}

		public static bool operator !=(BlockStateString x, BlockStateString y)
		{
			return !x.Equals(y);
		}
	}
}