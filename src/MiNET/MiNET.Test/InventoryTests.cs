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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2019 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;
using MiNET.Crafting;
using MiNET.Inventories;
using MiNET.Items;

namespace MiNET.Test
{
	[TestClass]
	public class InventoryTests
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(GeneralTests));

		[TestMethod]
		public void RemoveItemsFromInventoryTest()
		{
			var inventory = new PlayerInventory(new Player(null, null));
			inventory.Slots[0] = new ItemCobblestone {Count = 64};
			inventory.Slots[1] = new ItemStone {Count = 64};
			inventory.Slots[2] = new ItemStone {Count = 64};
			inventory.Slots[3] = new ItemStone {Count = 64};

			inventory.RemoveItems(BlockFactory.GetIdByType<Stone>(), 2);

			Assert.AreEqual(64, inventory.Slots[0].Count);
			Assert.AreEqual(62, inventory.Slots[1].Count);
			Assert.AreEqual(64, inventory.Slots[2].Count);
			Assert.AreEqual(64, inventory.Slots[3].Count);

			inventory.RemoveItems(BlockFactory.GetIdByType<Stone>(), 64);

			Assert.AreEqual(64, inventory.Slots[0].Count);
			Assert.AreEqual(0, inventory.Slots[1].Count);
			Assert.AreEqual(62, inventory.Slots[2].Count);
			Assert.AreEqual(64, inventory.Slots[3].Count);
		}

		[TestMethod]
		public void CreateiveInventoryTest()
		{
			var items = InventoryUtils.Content;
		}

		[TestMethod]
		public void CraftingTest()
		{
			var recipes = RecipeManager.Recipes;
		}
	}
}