using System.Linq;
using System.Numerics;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.Blocks
{
	public abstract class SignBase : Block
	{
		protected SignBase() : base()
		{
			IsTransparent = true;
			IsSolid = false;
			BlastResistance = 5;
			Hardness = 1;

			IsFlammable = true; // Only in PE!!
		}

		public override Item GetItem(Level world, bool blockItem = false)
		{
			switch (this)
			{
				case StandingSign:
				case WallSign:
					return new ItemOakSign();
				case DarkoakStandingSign:
				case DarkoakWallSign:
					return new ItemDarkOakSign();
			}

			var idSplit = Id.Split('_');
			var itemId = $"{string.Join('_', idSplit.Take(idSplit.Length - 2))}_{idSplit.Last()}";

			return ItemFactory.GetItem(itemId);
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			// TODO: check a clicked sign side for changing a specific side text
			if (player != null)
			{
				OpenSign(player);
			}

			return base.PlaceBlock(world, player, targetCoordinates, face, faceCoords);
		}

		public override bool Interact(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoord)
		{
			if (player.Inventory.GetItemInHand() is ItemSignBase)
			{
				return false;
			}

			OpenSign(player);

			return true;
		}

		public void OpenSign(Player player, bool front = true)
		{
			var packet = McpeOpenSign.CreateObject();
			packet.coordinates = Coordinates;
			packet.front = front;

			player.SendPacket(packet);
		}
	}
}
