﻿using System;
using System.Numerics;
using MiNET.BlockEntities;
using MiNET.Items;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class HangingSignBase : SignBase
	{
		public virtual bool AttachedBit { get; set; }

		public virtual int FacingDirection { get; set; }

		public virtual int GroundSignDirection { get; set; }

		public virtual bool Hanging { get; set; }

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			
			if (face == BlockFace.Down)
			{
				
			}
			else if (face == BlockFace.Up)
			{
				var direction = player.GetDirection() % 2;
				var faceX = new[] { BlockFace.West, BlockFace.East };
				var faceZ = new[] { BlockFace.South, BlockFace.North };

				var current = direction == 1 ? faceX : faceZ;
				var other = direction == 0 ? faceX : faceZ;

				var canPlace = PlaceNotHanging(world, Coordinates.GetNext(current[0]), player, current[0])
					|| PlaceNotHanging(world, Coordinates.GetNext(current[1]), player, current[1])
					|| PlaceNotHanging(world, Coordinates.GetNext(other[0]), player, other[0])
					|| PlaceNotHanging(world, Coordinates.GetNext(other[1]), player, other[1]);

				if (!canPlace) return true;
			}
			else
			{
				if (!PlaceNotHanging(world, targetCoordinates, player, face)) return true;

			}

			var blockEntity = new HangingSignBlockEntity() { Coordinates = Coordinates };
			world.SetBlockEntity(blockEntity);

			return base.PlaceBlock(world, player, targetCoordinates, face, faceCoords);
		}

		public override Item GetItem(Level world, bool blockItem = false)
		{
			return ItemFactory.GetItem(Id);
		}

		private bool PlaceNotHanging(Level world, BlockCoordinates targetCoordinates, Player player, BlockFace face)
		{
			var targetBlock = world.GetBlock(targetCoordinates);
			if (!targetBlock.IsSolid && targetBlock.IsTransparent) return false;

			FacingDirection = face == BlockFace.West || face == BlockFace.East
				? GetXDirection(player.KnownPosition.HeadYaw)
				: GetZDirection(player.KnownPosition.HeadYaw);

			return true;
		}

		private int GetXDirection(float headYaw)
		{
			return Math.Abs(headYaw) <= 90 ? 2 : 3;
		}

		private int GetZDirection(float headYaw)
		{
			return headYaw > 0 ? 5 : 4;
		}
	}
}