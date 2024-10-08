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
using System.Collections.Generic;
using fNbt;
using fNbt.Serialization;
using log4net;
using MiNET.Items;
using MiNET.Net;
using MiNET.Utils.Nbt;
using MiNET.Utils.Vectors;
using MiNET.Worlds;

namespace MiNET.BlockEntities
{
	[NbtObject]
	public abstract class BlockEntity : ICloneable
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(BlockEntity));

		[NbtProperty("id")]
		public string Id { get; }

		public string CustomName { get; set; }

		[NbtProperty("isMovable")]
		public bool IsMovable { get; set; }

		[NbtFlatProperty(typeof(NbtLowerCaseNamingStrategy))]
		public BlockCoordinates Coordinates { get; set; }

		[NbtIgnore]
		public bool UpdatesOnTick { get; set; }

		public BlockEntity(string id)
		{
			Id = id;
		}

		public virtual NbtCompound GetCompound()
		{
			return NbtConvert.ToNbt<NbtCompound>(this);
		}

		public virtual void SetCompound(NbtCompound compound)
		{
			NbtConvert.FromNbt(this, compound);
		}

		public virtual void OnTick(Level level)
		{
		}

		public virtual void SendData(Player player)
		{
			var tag = GetCompound();
			var nbt = new Nbt() { NbtFile = new NbtFile(tag) { Flavor = NbtFlavor.Bedrock } };

			if (Log.IsDebugEnabled) Log.Debug($"Nbt: {nbt.NbtFile.RootTag}");

			var entityData = McpeBlockEntityData.CreateObject();
			entityData.namedtag = nbt;
			entityData.coordinates = Coordinates;
			player.SendPacket(entityData);
		}

		public virtual void SendData(Level level)
		{
			var tag = GetCompound();
			var nbt = new Nbt() { NbtFile = new NbtFile(tag) { Flavor = NbtFlavor.Bedrock } };

			if (Log.IsDebugEnabled) Log.Debug($"Nbt: {nbt.NbtFile.RootTag}");

			var entityData = McpeBlockEntityData.CreateObject();
			entityData.namedtag = nbt;
			entityData.coordinates = Coordinates;
			level.RelayBroadcast(entityData);
		}

		public virtual void RemoveBlockEntity(Level level)
		{

		}

		public virtual List<Item> GetDrops()
		{
			return [];
		}

		public virtual object Clone()
		{
			// Slow, but common solution. Recommended to implement real clone.

			var type = GetType();
			var clone = Activator.CreateInstance(type);

			var settings = new NbtSerializerSettings() { Flavor = NbtFlavor.BedrockNoVarInt };
			return NbtConvert.FromNbt(clone, NbtConvert.ToNbt(this, settings), settings);
		}
	}
}