using fNbt.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;
using MiNET.Blocks.States;
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
			item.Block.VerticalHalf = VerticalHalf.Top;

			var tag = NbtConvert.ToNbt(item);

			Assert.IsNotNull(tag);

			var newItem = NbtConvert.FromNbt<Item>(tag);

			Assert.AreEqual(item, newItem);
		}

		[TestMethod]
		public void BlockConvertTest()
		{
			var block = new Bell();
			block.Attachment = Attachment.Side;
			block.Direction = 2;
			block.ToggleBit = true;

			var tag = NbtConvert.ToNbt(block);

			Assert.IsNotNull(tag);

			var newBlock = NbtConvert.FromNbt<Block>(tag);

			Assert.AreEqual(block, newBlock);
		}
	}
}
