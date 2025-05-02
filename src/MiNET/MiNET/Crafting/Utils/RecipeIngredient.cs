using System.Linq;
using MiNET.Items;
using MiNET.Net;

namespace MiNET.Crafting
{
	public abstract class RecipeIngredient : IPacketDataObject
	{
		public abstract RecipeIngredientType Type { get; }

		public virtual int Count { get; set; }

		public void Write(Packet packet)
		{
			packet.Write((byte) Type);

			WriteData(packet);

			packet.WriteSignedVarInt(Count);
		}

		protected virtual void WriteData(Packet packet) { }

		public static RecipeIngredient Read(Packet packet)
		{
			var type = (RecipeIngredientType) packet.ReadByte();

			RecipeIngredient ingredient = type switch
			{
				RecipeIngredientType.StringIdMeta => RecipeItemIngredient.ReadData(packet),
				RecipeIngredientType.Tag => RecipeTagIngredient.ReadData(packet),
				_ => new RecipeAirIngredient()
			};

			ingredient.Count = packet.ReadSignedVarInt();

			return ingredient;
		}

		public abstract bool ValidateItem(Item item);
	}

	public class RecipeAirIngredient : RecipeIngredient
	{
		public override RecipeIngredientType Type => RecipeIngredientType.Air;

		public override bool ValidateItem(Item item)
		{
			return true;
		}

		public override string ToString()
		{
			return Count > 0 ? $"Air(count: {Count})" : "Air()";
		}
	}

	public class RecipeItemIngredient : RecipeIngredient
	{
		public override RecipeIngredientType Type => RecipeIngredientType.StringIdMeta;

		public Item Item { get; set; }

		public override int Count { get => Item.Count; set => Item.Count = (byte) value; }

		public RecipeItemIngredient(Item item)
		{
			Item = item;
		}

		protected RecipeItemIngredient()
		{

		}

		protected override void WriteData(Packet packet)
		{
			packet.Write(Item.Id);
			packet.Write(Item is ItemBlock itemBlock ? itemBlock.Block.Data : Item.Metadata);
		}

		internal static RecipeIngredient ReadData(Packet packet)
		{
			return new RecipeItemIngredient(ItemFactory.GetItem(
				packet.ReadString(),
				packet.ReadShort()));
		}

		public override bool ValidateItem(Item item)
		{
			if (item.Id == Item.Id
				&& (item.Metadata == Item.Metadata || Item.Metadata == short.MaxValue)
				&& item.Count >= Item.Count)
			{
				return true;
			}

			return Item is ItemBlock originItemBlock
				&& item is ItemBlock itemBlock
				&& !originItemBlock.Block.Equals(itemBlock.Block);
		}

		public override string ToString()
		{
			return $"Item(id: {Item.Id}, metadata: {Item.Metadata}, count: {Count})";
		}
	}

	public class RecipeTagIngredient : RecipeIngredient
	{
		public override RecipeIngredientType Type => RecipeIngredientType.Tag;

		public string Tag { get; set; }

		public RecipeTagIngredient(string tag, int count = 1)
		{
			Tag = tag;
			Count = count;
		}

		protected RecipeTagIngredient()
		{

		}

		protected override void WriteData(Packet packet)
		{
			packet.Write(Tag);
		}

		internal static RecipeIngredient ReadData(Packet packet)
		{
			return new RecipeTagIngredient()
			{
				Tag = packet.ReadString()
			};
		}

		public override bool ValidateItem(Item item)
		{
			return item.Count >= Count
				&& ItemFactory.ItemTags[Tag].Contains(item.Id);
		}

		public override string ToString()
		{
			return $"Tag(tag: {Tag} count: {Count})";
		}
	}
}
