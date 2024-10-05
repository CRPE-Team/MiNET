using System;
using System.Numerics;
using MiNET.BlockEntities;
using MiNET.Blocks.States;
using MiNET.Items;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class HangingSignBase : SignBase
	{
		public abstract bool AttachedBit { get; set; }

		public abstract OldFacingDirection4 FacingDirection { get; set; }

		public abstract int GroundSignDirection { get; set; }

		public abstract bool Hanging { get; set; }

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			var groundSignDirection = player.KnownPosition.GetOppositeDirection16();

			if (face == BlockFace.Down)
			{
				var targetBlock = world.GetBlock(targetCoordinates);

				if (targetBlock.IsSolid && !targetBlock.IsTransparent
					|| targetBlock is SlabBase slab && slab.VerticalHalf == VerticalHalf.Bottom
					|| targetBlock is StairsBase stairs && !stairs.UpsideDownBit)
				{
					if (player.IsSneaking)
					{
						AttachedBit = true;
					}
				}
				else if (targetBlock is EndRod endRod && (endRod.FacingDirection == OldFacingDirection3.Down || endRod.FacingDirection == OldFacingDirection3.Up)
					|| targetBlock is Chain chain && chain.PillarAxis == PillarAxis.Y
					|| targetBlock is HangingSignBase)
				{
					if (targetBlock is not HangingSignBase || player.IsSneaking || groundSignDirection % 4 != 0)
					{
						AttachedBit = true;
					}
				}
				else
				{
					return true;
				}

				Hanging = true;
			}
			else if (face == BlockFace.Up)
			{
				var direction = (int) player.KnownPosition.GetDirection() % 2;
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

			if (AttachedBit)
			{
				GroundSignDirection = groundSignDirection;
			}
			else if (Hanging)
			{
				FacingDirection = player.KnownPosition.GetDirection();
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

		private OldFacingDirection4 GetXDirection(float headYaw)
		{
			return Math.Abs(headYaw) <= 90 ? OldFacingDirection4.North : OldFacingDirection4.South;
		}

		private OldFacingDirection4 GetZDirection(float headYaw)
		{
			return headYaw > 0 ? OldFacingDirection4.East : OldFacingDirection4.West;
		}
	}
}