using fNbt.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;
using MiNET.Items;

namespace MiNET.Test
{
	[TestClass]
	public class NbtTests
	{
		[TestMethod]
		public void ItemConvertTest()
		{
			var item = new ItemPrismarineBrickSlab();
			item.Block.VerticalHalf = "top";

			var tag = NbtConvert.ToNbt(item);

			Assert.IsNotNull(tag);

			var newItem = NbtConvert.FromNbt<Item>(tag);

			Assert.AreEqual(item, newItem);
		}

		[TestMethod]
		public void BlockConvertTest()
		{
			var block = new PrismarineBrickSlab();
			block.VerticalHalf = "top";

			var tag = NbtConvert.ToNbt(block);

			Assert.IsNotNull(tag);

			var newBlock = NbtConvert.FromNbt<Block>(tag);

			Assert.AreEqual(block, newBlock);
		}
	}
}
