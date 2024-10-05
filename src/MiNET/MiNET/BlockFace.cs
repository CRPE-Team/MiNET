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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System;
using MiNET.Utils;

namespace MiNET
{
	public enum BlockFace
	{
		Down = 0,
		Up = 1,
		North = 2, 
		South = 3,
		West = 4,
		East = 5,
		None = 255
	}

	public enum BlockAxis
	{
		X,
		Y,
		Z
	}

	public static class BlockFaceExtensions
	{
		public static BlockFace Opposite(this BlockFace face)
		{
			return face switch
			{
				BlockFace.Down => BlockFace.Up,
				BlockFace.Up => BlockFace.Down,
				BlockFace.South => BlockFace.North,
				BlockFace.West => BlockFace.East,
				BlockFace.North => BlockFace.South,
				BlockFace.East => BlockFace.West,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}

		public static Direction ToDirection(this BlockFace face)
		{
			return face switch
			{
				BlockFace.South => Direction.South,
				BlockFace.West => Direction.West,
				BlockFace.North => Direction.North,
				BlockFace.East => Direction.East,
				_ => throw new ArgumentOutOfRangeException(nameof(face), face, null)
			};
		}
	}
}