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
using System.Numerics;
using log4net;
using MiNET.BlockEntities;
using MiNET.Items;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public partial class Chalkboard : Block
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Chalkboard));

		public short Size { get; set; }

		public Chalkboard() : base()
		{
			Edu = true;
			IsTransparent = true;
			IsSolid = false;
			BlastResistance = 5;
			Hardness = 1;
		}

		protected override bool CanPlace(Level world, Player player, BlockCoordinates blockCoordinates, BlockCoordinates targetCoordinates, BlockFace face)
		{
			return world.GetBlock(blockCoordinates).IsReplaceable;
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			if (Size == 0)
			{
				Size = (byte) ((int) (Math.Floor((player.KnownPosition.Yaw + 180) * 16 / 360) + 0.5) & 0x0f);

				var block = new Chalkboard
				{
					Coordinates = Coordinates,
					Size = Size
				};
				world.SetBlock(block);

				var blockEntity = new ChalkboardBlockEntity
				{
					BaseCoordinates = Coordinates,
					Coordinates = Coordinates,
					Owner = player.EntityId,
					Size = Size,
					OnGround = true,
					Text = string.Empty
				};

				world.SetBlockEntity(blockEntity);
			}
			else if (Size == 1)
			{
				Size = (byte) ((int) (Math.Floor((player.KnownPosition.Yaw + 180) * 4 / 360) + 0.5) & 0x0f);

				var current = Coordinates;

				for (int x = 0; x < 2; x++)
				{
					var block = new Chalkboard
					{
						Coordinates = current + GetDirCoord() * x,
						Size = Size
					};
					world.SetBlock(block);
					var blockEntity = new ChalkboardBlockEntity
					{
						BaseCoordinates = Coordinates,
						Coordinates = current + GetDirCoord() * x,
						Owner = player.EntityId,
						Size = Size,
						OnGround = true,
					};
					world.SetBlockEntity(blockEntity);
				}
			}
			else if (Size == 2)
			{
				Size = (byte) ((int) (Math.Floor((player.KnownPosition.Yaw + 180) * 4 / 360) + 0.5) & 0x0f);

				for (int y = 0; y < 2; y++)
				{
					var current = Coordinates + BlockCoordinates.Up * y;

					for (int x = -1; x < 2; x++)
					{
						var block = new Chalkboard
						{
							Coordinates = current + GetDirCoord() * x,
							Size = Size
						};
						world.SetBlock(block);
						var blockEntity = new ChalkboardBlockEntity
						{
							BaseCoordinates = Coordinates,
							Coordinates = current + GetDirCoord() * x,
							Owner = player.EntityId,
							Size = Size,
							OnGround = true,
						};
						world.SetBlockEntity(blockEntity);
					}
				}
			}

			return true;
		}

		public override void BreakBlock(Level world, BlockFace face, bool silent = false)
		{
			var blockEntity = world.GetBlockEntity(Coordinates) as ChalkboardBlockEntity;
			if (blockEntity == null)
			{
				Log.Warn($"Found no block entity at {Coordinates}");
				return;
			}
			var baseCoord = blockEntity.BaseCoordinates;
			var baseBlockEntity = world.GetBlockEntity(baseCoord) as ChalkboardBlockEntity;
			if (baseBlockEntity == null)
			{
				Log.Warn($"Found no base block entity at {baseCoord}");
				return;
			}

			int size = baseBlockEntity.Size ?? 0;

			if (size == 0)
			{
				world.SetAir(Coordinates);
				world.RemoveBlockEntity(Coordinates);
			}
			else if (size == 1)
			{
				var current = baseCoord;

				for (int x = 0; x < 2; x++)
				{
					world.SetAir(current + GetDirCoord() * x);
					world.RemoveBlockEntity(current + GetDirCoord() * x);
				}
			}
			else if (size == 2)
			{
				for (int y = 0; y < 2; y++)
				{
					var current = baseCoord + BlockCoordinates.Up * y;
					for (int x = -1; x < 2; x++)
					{
						world.SetAir(current + GetDirCoord() * x);
						world.RemoveBlockEntity(current + GetDirCoord() * x);
					}
				}
			}
		}

		public override bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			return true;
		}

		public override Item[] GetDrops(Level world, Item tool)
		{
			return new[] { new ItemOakSign() };
		}

		private BlockCoordinates GetDirCoord()
		{
			BlockCoordinates direction = new BlockCoordinates();
			switch (Size & 0x07)
			{
				case 0:
					direction = Level.West;
					break; // West
				case 1:
					direction = Level.South;
					break; // South
				case 2:
					direction = Level.East;
					break; // East
				case 3:
					direction = Level.North;
					break; // North 
			}

			return direction;
		}
	}
}