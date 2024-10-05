using System;

namespace MiNET.Blocks.States
{
	public partial class WeirdoDirection
	{
		/// <summary>0</summary>
		private const int EastValue = 0;
		/// <summary>1</summary>
		private const int WestValue = 1;
		/// <summary>2</summary>
		private const int SouthValue = 2;
		/// <summary>3</summary>
		private const int NorthValue = 3;

		internal WeirdoDirection() { }

		private WeirdoDirection(int value)
		{
			Value = value;
		}

		/// <summary>
		/// Value = <inheritdoc cref="SouthValue"/>
		/// </summary>
		public static readonly WeirdoDirection South = new WeirdoDirection(SouthValue);

		/// <summary>
		/// Value = <inheritdoc cref="WestValue"/>
		/// </summary>
		public static readonly WeirdoDirection West = new WeirdoDirection(WestValue);

		/// <summary>
		/// Value = <inheritdoc cref="NorthValue"/>
		/// </summary>
		public static readonly WeirdoDirection North = new WeirdoDirection(NorthValue);

		/// <summary>
		/// Value = <inheritdoc cref="EastValue"/>
		/// </summary>
		public static readonly WeirdoDirection East = new WeirdoDirection(EastValue);

		public static implicit operator WeirdoDirection(MiNET.Utils.Direction direction)
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

		public static implicit operator WeirdoDirection(MiNET.BlockFace face)
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

		public static implicit operator MiNET.BlockFace(WeirdoDirection direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.BlockFace.South,
				WestValue => MiNET.BlockFace.West,
				NorthValue => MiNET.BlockFace.North,
				EastValue => MiNET.BlockFace.East,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		public static implicit operator MiNET.Utils.Direction(WeirdoDirection direction)
		{
			return direction.Value switch
			{
				SouthValue => MiNET.Utils.Direction.South,
				WestValue => MiNET.Utils.Direction.West,
				NorthValue => MiNET.Utils.Direction.North,
				EastValue => MiNET.Utils.Direction.East,
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}
	}
}
