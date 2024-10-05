﻿#region LICENSE

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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System;
using System.Collections.Generic;
using System.Numerics;
using fNbt.Serialization;
using Newtonsoft.Json;

namespace MiNET.Utils.Vectors
{
	public struct BlockCoordinates : IEquatable<BlockCoordinates>
	{
		public int X, Y, Z;

		public BlockCoordinates(int value)
		{
			X = Y = Z = value;
		}

		public BlockCoordinates(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public BlockCoordinates(BlockCoordinates v)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
		}

		public BlockCoordinates(PlayerLocation location)
		{
			X = (int) Math.Floor(location.X);
			Y = (int) Math.Floor(location.Y);
			Z = (int) Math.Floor(location.Z);
		}

		public BlockCoordinates(Vector3 location)
		{
			X = (int) Math.Floor(location.X);
			Y = (int) Math.Floor(location.Y);
			Z = (int) Math.Floor(location.Z);
		}


		/// <summary>
		///     Calculates the distance between two BlockCoordinates objects.
		/// </summary>
		public double DistanceTo(BlockCoordinates other)
		{
			return Math.Sqrt(Square(other.X - X) +
							Square(other.Y - Y) +
							Square(other.Z - Z));
		}

		/// <summary>
		///     Calculates the square of a num.
		/// </summary>
		private int Square(int num)
		{
			return num * num;
		}

		public BlockCoordinates Abs()
		{
			return new BlockCoordinates(
				Math.Abs(X),
				Math.Abs(Y),
				Math.Abs(Z)
			);
		}

		/// <summary>
		///     Finds the distance of this Coordinate3D from BlockCoordinates.Zero
		/// </summary>
		[JsonIgnore, NbtIgnore]
		public double Distance
		{
			get { return DistanceTo(Zero); }
		}

		public static BlockCoordinates Min(BlockCoordinates value1, BlockCoordinates value2)
		{
			return new BlockCoordinates(
				Math.Min(value1.X, value2.X),
				Math.Min(value1.Y, value2.Y),
				Math.Min(value1.Z, value2.Z)
			);
		}

		public static BlockCoordinates Max(BlockCoordinates value1, BlockCoordinates value2)
		{
			return new BlockCoordinates(
				Math.Max(value1.X, value2.X),
				Math.Max(value1.Y, value2.Y),
				Math.Max(value1.Z, value2.Z)
			);
		}

		public static bool operator !=(BlockCoordinates a, BlockCoordinates b)
		{
			return !a.Equals(b);
		}

		public static bool operator ==(BlockCoordinates a, BlockCoordinates b)
		{
			return a.Equals(b);
		}

		public static BlockCoordinates operator +(BlockCoordinates a, Direction direction)
		{
			return direction switch
			{
				Direction.North => a.BlockNorth(),
				Direction.South => a.BlockSouth(),
				Direction.East => a.BlockEast(),
				Direction.West => a.BlockWest(),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		}

		public static BlockCoordinates operator -(BlockCoordinates a, Direction direction)
		{
			return a + direction.Opposite();
		}

		public static BlockCoordinates operator +(BlockCoordinates a, BlockFace face)
		{
			return face switch
			{
				BlockFace.Down => a.BlockDown(),
				BlockFace.Up => a.BlockUp(),
				BlockFace.North => a.BlockNorth(),
				BlockFace.South => a.BlockSouth(),
				BlockFace.East => a.BlockEast(),
				BlockFace.West => a.BlockWest(),
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}

		public static BlockCoordinates operator -(BlockCoordinates a, BlockFace face)
		{
			return a + face.Opposite();
		}

		public static BlockCoordinates operator +(BlockCoordinates a, BlockCoordinates b)
		{
			return new BlockCoordinates(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static BlockCoordinates operator -(BlockCoordinates a, BlockCoordinates b)
		{
			return new BlockCoordinates(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static BlockCoordinates operator -(BlockCoordinates a)
		{
			return new BlockCoordinates(-a.X, -a.Y, -a.Z);
		}

		public static BlockCoordinates operator *(BlockCoordinates a, BlockCoordinates b)
		{
			return new BlockCoordinates(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}

		public static BlockCoordinates operator /(BlockCoordinates a, BlockCoordinates b)
		{
			return new BlockCoordinates(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
		}

		public static BlockCoordinates operator %(BlockCoordinates a, BlockCoordinates b)
		{
			return new BlockCoordinates(a.X % b.X, a.Y % b.Y, a.Z % b.Z);
		}

		public static BlockCoordinates operator +(BlockCoordinates a, int b)
		{
			return new BlockCoordinates(a.X + b, a.Y + b, a.Z + b);
		}

		public static BlockCoordinates operator -(BlockCoordinates a, int b)
		{
			return new BlockCoordinates(a.X - b, a.Y - b, a.Z - b);
		}

		public static BlockCoordinates operator *(BlockCoordinates a, int b)
		{
			return new BlockCoordinates(a.X * b, a.Y * b, a.Z * b);
		}

		public static BlockCoordinates operator /(BlockCoordinates a, int b)
		{
			return new BlockCoordinates(a.X / b, a.Y / b, a.Z / b);
		}

		public static BlockCoordinates operator %(BlockCoordinates a, int b)
		{
			return new BlockCoordinates(a.X % b, a.Y % b, a.Z % b);
		}

		public static BlockCoordinates operator +(int a, BlockCoordinates b)
		{
			return new BlockCoordinates(a + b.X, a + b.Y, a + b.Z);
		}

		public static BlockCoordinates operator -(int a, BlockCoordinates b)
		{
			return new BlockCoordinates(a - b.X, a - b.Y, a - b.Z);
		}

		public static BlockCoordinates operator *(int a, BlockCoordinates b)
		{
			return new BlockCoordinates(a * b.X, a * b.Y, a * b.Z);
		}

		public static BlockCoordinates operator /(int a, BlockCoordinates b)
		{
			return new BlockCoordinates(a / b.X, a / b.Y, a / b.Z);
		}

		public static BlockCoordinates operator %(int a, BlockCoordinates b)
		{
			return new BlockCoordinates(a % b.X, a % b.Y, a % b.Z);
		}

		public static explicit operator BlockCoordinates(ChunkCoordinates a)
		{
			return new BlockCoordinates(a.X << 4, 0, a.Z << 4);
		}

		public static implicit operator BlockCoordinates(Vector3 a)
		{
			return new BlockCoordinates(a);
		}

		public static explicit operator BlockCoordinates(PlayerLocation a)
		{
			return new BlockCoordinates(a);
		}

		public static implicit operator Vector3(BlockCoordinates a)
		{
			return new Vector3(a.X, a.Y, a.Z);
		}

		public static readonly BlockCoordinates Zero = new BlockCoordinates(0);
		public static readonly BlockCoordinates One = new BlockCoordinates(1);

		public static readonly BlockCoordinates Up = new BlockCoordinates(0, 1, 0);
		public static readonly BlockCoordinates Down = new BlockCoordinates(0, -1, 0);
		public static readonly BlockCoordinates East = new BlockCoordinates(1, 0, 0);
		public static readonly BlockCoordinates West = new BlockCoordinates(-1, 0, 0);
		public static readonly BlockCoordinates North = new BlockCoordinates(0, 0, -1);
		public static readonly BlockCoordinates South = new BlockCoordinates(0, 0, 1);

		// not sure these are useful. Three is no "left" or "right" here :-(
		public static readonly BlockCoordinates Left = new BlockCoordinates(-1, 0, 0);
		public static readonly BlockCoordinates Right = new BlockCoordinates(1, 0, 0);
		public static readonly BlockCoordinates Backwards = new BlockCoordinates(0, 0, -1);
		public static readonly BlockCoordinates Forwards = new BlockCoordinates(0, 0, 1);

		public IEnumerable<BlockCoordinates> Get2dAroundCoordinates()
		{
			yield return BlockEast();
			yield return BlockWest();
			yield return BlockNorth();
			yield return BlockSouth();
		}

		public IEnumerable<BlockCoordinates> Get3dAroundCoordinates()
		{
			yield return BlockUp();
			yield return BlockDown();
			yield return BlockEast();
			yield return BlockWest();
			yield return BlockNorth();
			yield return BlockSouth();
		}

		public BlockCoordinates BlockUp()
		{
			return this + Up;
		}

		public BlockCoordinates BlockDown()
		{
			return this + Down;
		}

		public BlockCoordinates BlockEast()
		{
			return this + East;
		}

		public BlockCoordinates BlockWest()
		{
			return this + West;
		}

		public BlockCoordinates BlockNorth()
		{
			return this + North;
		}

		public BlockCoordinates BlockSouth()
		{
			return this + South;
		}

		public BlockCoordinates BlockNorthEast()
		{
			return this + North + East;
		}

		public BlockCoordinates BlockNorthWest()
		{
			return this + North + West;
		}

		public BlockCoordinates BlockSouthEast()
		{
			return this + South + East;
		}

		public BlockCoordinates BlockSouthWest()
		{
			return this + South + West;
		}

		public BlockCoordinates GetNext(BlockFace face)
		{
			return face switch
			{
				BlockFace.Down => BlockDown(),
				BlockFace.Up => BlockUp(),
				BlockFace.North => BlockNorth(),
				BlockFace.South => BlockSouth(),
				BlockFace.West => BlockWest(),
				BlockFace.East => BlockEast(),
			};
		}

		public bool Equals(BlockCoordinates other)
		{
			return X == other.X && Y == other.Y && Z == other.Z;
		}

		public override bool Equals(object obj)
		{
			return obj is BlockCoordinates other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y, Z);
		}

		public override string ToString()
		{
			return $"X={X}, Y={Y}, Z={Z}";
		}
	}
}