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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Numerics;
using fNbt.Serialization;
using log4net;
using MiNET.Blocks;
using MiNET.Utils.Vectors;
using MiNET.Worlds;
using Newtonsoft.Json;

namespace MiNET.Items
{
	/// <summary>
	///     Generic Item that will simply place the block on use. No interaction or other use supported by the block.
	/// </summary>
	public abstract class ItemBlock<TBlock> : ItemBlock where TBlock : Block, new()
	{
		[JsonIgnore]
		public new TBlock Block { get => (TBlock) base.Block; }

		public ItemBlock() : this(new TBlock())
		{

		}

		protected ItemBlock(TBlock block) : base(block)
		{

		}
	}

	/// <summary>
	///     Generic Item that will simply place the block on use. No interaction or other use supported by the block.
	/// </summary>
	public class ItemBlock : Item
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(ItemBlock));

		[JsonIgnore]
		[NbtProperty]
		public virtual Block Block { get; protected set; }

		public override int BlockRuntimeId => Block?.RuntimeId ?? Block.UnknownRuntimeId;

		public override bool Edu { get => base.Edu || (Block?.Edu ?? false); protected set => base.Edu = value; }

		protected ItemBlock()
		{
		}

		internal ItemBlock(Block block)
		{
			Block = block ?? throw new ArgumentNullException(nameof(block));

			Id ??= block.Id;
	
			FuelEfficiency = block.FuelEfficiency;
			Edu = block.Edu;
		}

		public override Item GetSmelt(string block)
		{
			return Block.GetSmelt(block) ?? base.GetSmelt(block);
		}

		public override bool PlaceBlock(Level world, Player player, BlockCoordinates targetCoordinates, BlockFace face, Vector3 faceCoords)
		{
			Block currentBlock = world.GetBlock(targetCoordinates);
			Block newBlock = BlockFactory.GetBlockById(Block.Id);
			newBlock.Coordinates = currentBlock.IsReplaceable ? targetCoordinates : GetNewCoordinatesFromFace(targetCoordinates, face);

			newBlock.SetStates(Block);

			if (!newBlock.CanPlace(world, player, targetCoordinates, face))
			{
				return false;
			}

			// TODO - invert logic
			if (newBlock.PlaceBlock(world, player, targetCoordinates, face, faceCoords))
			{
				return false;
			}

			world.SetBlock(newBlock);

			if (player.GameMode == GameMode.Survival && newBlock is not Air)
			{
				var itemInHand = player.Inventory.GetItemInHand();
				itemInHand.Count--;
				player.Inventory.SetInventorySlot(player.Inventory.InHandSlot, itemInHand);
			}

			// TODO - should move to the Block
			world.BroadcastSound(newBlock.Coordinates, LevelSoundEventType.Place, newBlock.RuntimeId);

			return true;
		}

		public override object Clone()
		{
			var item = (ItemBlock) base.Clone();

			item.Block = Block?.Clone() as Block;

			return item;
		}

		public override string ToString()
		{
			return $"{GetType().Name}(Id={Id}, Meta={Metadata}, UniqueId={UniqueId}) {{Block={Block?.GetType().Name}}} Count={Count}, NBT={ExtraData}";
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), Block.GetHashCode());
		}

		internal void SetBlock(Block block)
		{
			Block = block;
		}

		protected override bool Equals(Item other)
		{
			return other is ItemBlock otherItemBlock
				&& base.Equals(other)
				&& Block.Equals(otherItemBlock.Block);
		}
	}
}