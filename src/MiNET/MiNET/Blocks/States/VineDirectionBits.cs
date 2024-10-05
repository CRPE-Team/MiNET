using System;

namespace MiNET.Blocks.States
{
	public partial class VineDirectionBits
	{
		internal VineDirectionBits() { }

		private VineDirectionBits(int value)
		{
			Value = value;
		}

		public static readonly VineDirectionBits None = new VineDirectionBits(0);

		/// <summary>
		/// <inheritdoc cref="VineDirectionBit.South"/>
		/// </summary>
		public static readonly VineDirectionBit South = VineDirectionBit.South;

		/// <summary>
		/// <inheritdoc cref="VineDirectionBit.West"/>
		/// </summary>
		public static readonly VineDirectionBit West = VineDirectionBit.West;

		/// <summary>
		/// <inheritdoc cref="VineDirectionBit.North"/>
		/// </summary>
		public static readonly VineDirectionBit North = VineDirectionBit.North;

		/// <summary>
		/// <inheritdoc cref="VineDirectionBit.East"/>
		/// </summary>
		public static readonly VineDirectionBit East = VineDirectionBit.East;

		public bool HasSide(VineDirectionBit side)
		{
			var sideBit = 1 << side.Value;

			return (Value & sideBit) == sideBit;
		}

		public VineDirectionBits WithSide(VineDirectionBit side)
		{
			return this + side;
		}

		public VineDirectionBits WithoutSide(VineDirectionBit side)
		{
			return this - side;
		}

		public static VineDirectionBits operator +(VineDirectionBits x, VineDirectionBit y)
		{
			return new VineDirectionBits(x.Value | (1 << y.Value));
		}

		public static VineDirectionBits operator -(VineDirectionBits x, VineDirectionBit y)
		{
			return new VineDirectionBits(x.Value & ~(1 << y.Value));
		}

		public static implicit operator VineDirectionBits(VineDirectionBit side)
		{
			return new VineDirectionBits(1 << side.Value);
		}

		public static bool operator ==(VineDirectionBits x, VineDirectionBits y)
		{
			return x.Value == y.Value;
		}

		public static bool operator !=(VineDirectionBits x, VineDirectionBits y)
		{
			return x.Value != y.Value;
		}

		public struct VineDirectionBit
		{
			/// <summary>0</summary>
			private const byte SouthBit = 0;
			/// <summary>1</summary>
			private const byte WestBit = 1;
			/// <summary>2</summary>
			private const byte NorthBit = 2;
			/// <summary>3</summary>
			private const byte EastBit = 3;

			public int Value { get; set; }

			private VineDirectionBit(int value)
			{
				Value = value;
			}

			/// <summary>
			/// Value = <inheritdoc cref="SouthBit"/>
			/// </summary>
			public static readonly VineDirectionBit South = new VineDirectionBit(SouthBit);

			/// <summary>
			/// Value = <inheritdoc cref="WestBit"/>
			/// </summary>
			public static readonly VineDirectionBit West = new VineDirectionBit(WestBit);

			/// <summary>
			/// Value = <inheritdoc cref="NorthBit"/>
			/// </summary>
			public static readonly VineDirectionBit North = new VineDirectionBit(NorthBit);

			/// <summary>
			/// Value = <inheritdoc cref="EastBit"/>
			/// </summary>
			public static readonly VineDirectionBit East = new VineDirectionBit(EastBit);


			public static implicit operator VineDirectionBit(MiNET.Utils.Direction direction)
			{
				return direction switch
				{
					MiNET.Utils.Direction.South => South,
					MiNET.Utils.Direction.West => West,
					MiNET.Utils.Direction.North => North,
					MiNET.Utils.Direction.East => East,
					_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
				};
			}

			public static implicit operator VineDirectionBit(MiNET.BlockFace face)
			{
				return face switch
				{
					MiNET.BlockFace.South => South,
					MiNET.BlockFace.West => West,
					MiNET.BlockFace.North => North,
					MiNET.BlockFace.East => East,
					_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
				};
			}
		}
	}
}
