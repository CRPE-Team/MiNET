using MiNET.Items;

namespace MiNET.BlockEntities
{
	public class CauldronBlockEntity : BlockEntity
	{
		public int? CustomColor { get; set; }
		public Item[] Items { get; set; }
		public short PotionId { get; set; } = -1;
		public short PotionType { get; set; } = -1;

		public CauldronBlockEntity() : base(BlockEntityIds.Cauldron)
		{
			Items = [ new ItemAir() ];
		}
	}
}