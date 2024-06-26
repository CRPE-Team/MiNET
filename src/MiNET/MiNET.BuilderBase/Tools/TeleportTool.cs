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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2017 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System.Numerics;
using fNbt;
using log4net;
using MiNET.BlockEntities;
using MiNET.Blocks;
using MiNET.Items;
using MiNET.Utils;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.BuilderBase.Tools
{
	public class TeleportTool : ItemCompass
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof (DistanceWand));

		private NbtCompound _extraData;

		public override NbtCompound ExtraData
		{
			get
			{
				UpdateExtraData();
				return _extraData;
			}
			set { _extraData = value; }
		}

		private void UpdateExtraData()
		{
			_extraData = new NbtCompound
			{
				{
					new NbtCompound("display")
					{
						new NbtString("Name", ChatFormatting.Reset + ChatColors.Yellow + $"Teleport tool"),
						new NbtList("Lore")
						{
							new NbtString(ChatFormatting.Reset + ChatFormatting.Italic + ChatColors.White + $"Left click to teleport."),
							new NbtString(ChatFormatting.Reset + ChatFormatting.Italic + ChatColors.White + $"Right teleport but stay on same Y."),
						}
					}
				}
			};
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates blockCoordinates, BlockFace face, Vector3 faceCoords)
		{
			Teleport(player, true);
			return true;
		}

		public override void UseItem(Level world, Player player, BlockCoordinates blockCoordinates)
		{
			Teleport(player, true);
		}

		public override bool Animate(Level world, Player player)
		{
			Teleport(player, false);
			return true;
		}

		public override bool BreakBlock(Level world, Player player, Block block, BlockEntity blockEntity)
		{
			Teleport(player, false);
			return false; // Will revert block break;
		}

		private void Teleport(Player player, bool stayOnY)
		{
			var target = new EditHelper(player.Level, player).GetBlockInLineOfSight(player.Level, player.KnownPosition, returnLastAir: true, limitHeight: false);
			if (target == null)
			{
				player.SendMessage("No position in range");
				return;
			}

			var pos = target.Coordinates;
			var known = player.KnownPosition;
			var newPosition = new PlayerLocation(pos.X, stayOnY ? known.Y - 1.62f : pos.Y + 1.62f, pos.Z, known.HeadYaw, known.Yaw, known.Pitch);
			player.SendMessage($"Wrooom to {(BlockCoordinates) newPosition}");
			player.Teleport(newPosition);
		}
	}
}