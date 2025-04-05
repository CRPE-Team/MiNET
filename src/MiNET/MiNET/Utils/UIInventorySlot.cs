namespace MiNET.Utils
{
	public enum UIInventorySlot : byte
	{
		Cursor = 0,

		AnvilInput = 1,
		AnvilMaterial = 2,

		StoneCutterInput = 3,

		Trade2Ingredient1 = 4,
		Trade2Ingredient2 = 5,

		TradeIngredient1 = 6,
		TradeIngredient2 = 7,

		MaterialReducerInput = 8,

		LoomInput = 9,
		LoomDye = 10,
		LoomMaterial = 11,

		CartographyInput = 12,
		CartographyAdditional = 13,

		EnchantingInput = 14,
		EnchantingMaterial = 15,

		GrindstoneInput = 16,
		GrindstoneAdditional = 17,

		CompoundCreatorInput1 = 18,
		CompoundCreatorInput2,
		CompoundCreatorInput3,
		CompoundCreatorInput4,
		CompoundCreatorInput5,
		CompoundCreatorInput6,
		CompoundCreatorInput7,
		CompoundCreatorInput8,
		CompoundCreatorInput9,

		BeaconPayment = 27,

		Crafting2x2Input1 = 28,
		Crafting2x2Input2,
		Crafting2x2Input3,
		Crafting2x2Input4,

		Crafting3x3Input1 = 32,
		Crafting3x3Input2,
		Crafting3x3Input3,
		Crafting3x3Input4,
		Crafting3x3Input5,
		Crafting3x3Input6,
		Crafting3x3Input7,
		Crafting3x3Input8,
		Crafting3x3Input9,

		MaterialReducerOutput1 = 41,
		MaterialReducerOutput2,
		MaterialReducerOutput3,
		MaterialReducerOutput4,
		MaterialReducerOutput5,
		MaterialReducerOutput6,
		MaterialReducerOutput7,
		MaterialReducerOutput8,
		MaterialReducerOutput9,

		CreatedItemOutput = 50,

		SmithingTableInput = 51,
		SmithingTableMaterial = 52,
		SmithingTableTemplate = 53,

		SlotsCount
	}

	public class UIInventorySlots
	{
		public static readonly UIInventorySlot Cursor = 
			UIInventorySlot.Cursor;

		public static readonly UIInventorySlot[] Anvil = 
		[
			UIInventorySlot.AnvilInput, 
			UIInventorySlot.AnvilMaterial
		];

		public static readonly UIInventorySlot StoneCutterInput = 
			UIInventorySlot.StoneCutterInput;

		public static readonly UIInventorySlot[] Trade2Ingredient = 
		[
			UIInventorySlot.Trade2Ingredient1, 
			UIInventorySlot.Trade2Ingredient2
		];

		public static readonly UIInventorySlot[] TradeIngredient = 
		[
			UIInventorySlot.TradeIngredient1, 
			UIInventorySlot.TradeIngredient2
		];

		public static readonly UIInventorySlot MaterialReducerInput = 
			UIInventorySlot.MaterialReducerInput;

		public static readonly UIInventorySlot[] Loom = 
		[
			UIInventorySlot.LoomInput, 
			UIInventorySlot.LoomDye, 
			UIInventorySlot.LoomMaterial
		];

		public static readonly UIInventorySlot[] CartographyTable = 
		[
			UIInventorySlot.CartographyInput, 
			UIInventorySlot.CartographyAdditional
		];

		public static readonly UIInventorySlot[] EnchantingTable = 
		[
			UIInventorySlot.EnchantingInput, 
			UIInventorySlot.EnchantingMaterial
		];

		public static readonly UIInventorySlot[] Grindstone = 
		[
			UIInventorySlot.GrindstoneInput, 
			UIInventorySlot.GrindstoneAdditional
		];

		public static readonly UIInventorySlot[] CompoundCreatorInput = 
		[
			UIInventorySlot.CompoundCreatorInput1,
			UIInventorySlot.CompoundCreatorInput2,
			UIInventorySlot.CompoundCreatorInput3,
			UIInventorySlot.CompoundCreatorInput4,
			UIInventorySlot.CompoundCreatorInput5,
			UIInventorySlot.CompoundCreatorInput6,
			UIInventorySlot.CompoundCreatorInput7,
			UIInventorySlot.CompoundCreatorInput8,
			UIInventorySlot.CompoundCreatorInput9
		];

		public static readonly UIInventorySlot BeaconPayment = UIInventorySlot.BeaconPayment;

		public static readonly UIInventorySlot[] Crafting2x2Input = 
		[
			UIInventorySlot.Crafting2x2Input1,
			UIInventorySlot.Crafting2x2Input2,
			UIInventorySlot.Crafting2x2Input3,
			UIInventorySlot.Crafting2x2Input4
		];

		public static readonly UIInventorySlot[] Crafting3x3Input =
		[
			UIInventorySlot.Crafting3x3Input1,
			UIInventorySlot.Crafting3x3Input2,
			UIInventorySlot.Crafting3x3Input3,
			UIInventorySlot.Crafting3x3Input4,
			UIInventorySlot.Crafting3x3Input5,
			UIInventorySlot.Crafting3x3Input6,
			UIInventorySlot.Crafting3x3Input7,
			UIInventorySlot.Crafting3x3Input8,
			UIInventorySlot.Crafting3x3Input9
		];

		public static readonly UIInventorySlot[] CraftingInput =
		[
			..Crafting2x2Input,
			..Crafting3x3Input
		];

		public static readonly UIInventorySlot[] MaterialReducerOutput =
		[
			UIInventorySlot.MaterialReducerOutput1,
			UIInventorySlot.MaterialReducerOutput2,
			UIInventorySlot.MaterialReducerOutput3,
			UIInventorySlot.MaterialReducerOutput4,
			UIInventorySlot.MaterialReducerOutput5,
			UIInventorySlot.MaterialReducerOutput6,
			UIInventorySlot.MaterialReducerOutput7,
			UIInventorySlot.MaterialReducerOutput8,
			UIInventorySlot.MaterialReducerOutput9
		];

		public static readonly UIInventorySlot CreatedItemOutput = UIInventorySlot.CreatedItemOutput;

		public static readonly UIInventorySlot[] SmithingTable = 
		[
			UIInventorySlot.SmithingTableInput,
			UIInventorySlot.SmithingTableMaterial,
			UIInventorySlot.SmithingTableTemplate
		];

		public static byte? GetSlotByContainerId(ContainerId containerId, byte relativeSlot = 0)
		{
			UIInventorySlot? slot = containerId switch
			{
				ContainerId.AnvilInput => UIInventorySlot.AnvilInput,
				ContainerId.AnvilMaterial => UIInventorySlot.AnvilMaterial,
				ContainerId.AnvilResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.SmithingTableInput => UIInventorySlot.SmithingTableInput,
				ContainerId.SmithingTableMaterial => UIInventorySlot.SmithingTableMaterial,
				ContainerId.SmithingTableResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.BeaconPayment => BeaconPayment,
				ContainerId.CraftingInput => CraftingInput[relativeSlot],
				ContainerId.CraftingOutputPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.EnchantingInput => UIInventorySlot.EnchantingInput,
				ContainerId.EnchantingMaterial => UIInventorySlot.EnchantingMaterial,
				ContainerId.TradeIngredient1 => UIInventorySlot.TradeIngredient1,
				ContainerId.TradeIngredient2 => UIInventorySlot.TradeIngredient2,
				ContainerId.CompoundCreatorInput => CompoundCreatorInput[relativeSlot],
				ContainerId.CompoundCreatorOutputPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.ElementConstructorOutputPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.MaterialReducerInput => UIInventorySlot.MaterialReducerInput,
				ContainerId.MaterialReducerOutput => MaterialReducerOutput[relativeSlot],
				ContainerId.LoomInput => UIInventorySlot.LoomInput,
				ContainerId.LoomDye => UIInventorySlot.LoomDye,
				ContainerId.LoomMaterial => UIInventorySlot.LoomMaterial,
				ContainerId.LoomResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.Trade2Ingredient1 => UIInventorySlot.Trade2Ingredient1,
				ContainerId.Trade2Ingredient2 => UIInventorySlot.Trade2Ingredient2,
				ContainerId.Trade2ResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.GrindstoneInput => UIInventorySlot.GrindstoneInput,
				ContainerId.GrindstoneAdditional => UIInventorySlot.GrindstoneAdditional,
				ContainerId.GrindstoneResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.StonecutterInput => UIInventorySlot.StoneCutterInput,
				ContainerId.StonecutterResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.CartographyInput => UIInventorySlot.CartographyInput,
				ContainerId.CartographyAdditional => UIInventorySlot.CartographyAdditional,
				ContainerId.CartographyResultPreview => UIInventorySlot.CreatedItemOutput,
				ContainerId.Cursor => UIInventorySlot.Cursor,
				ContainerId.CreatedOutput => UIInventorySlot.CreatedItemOutput,
				ContainerId.SmithingTableTemplate => UIInventorySlot.SmithingTableTemplate,

				_ => null
			};

			return (byte?)slot;
		}
	}
}
