using System.Numerics;
using MiNET.BlockEntities;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Items
{
	public abstract class ItemHeadBase : ItemBlock
	{
		public ItemHeadBase()
		{

		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			if (base.PlaceBlock(world, player, targetCoordinates, face, faceCoords))
			{
				var skullBlockEntity = new SkullBlockEntity
				{
					Coordinates = GetNewCoordinatesFromFace(targetCoordinates, face),
					Rotation = (byte) player.KnownPosition.GetDirection16()
				};

				world.SetBlockEntity(skullBlockEntity);

				return true;
			}

			return false;
		}
	}
}