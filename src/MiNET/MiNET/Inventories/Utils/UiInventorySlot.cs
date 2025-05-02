namespace MiNET.Inventories
{
	public enum UiInventorySlot : byte
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
		public static readonly UiInventorySlot Cursor = 
			UiInventorySlot.Cursor;

		public static readonly UiInventorySlot[] Anvil = 
		[
			UiInventorySlot.AnvilInput, 
			UiInventorySlot.AnvilMaterial
		];

		public static readonly UiInventorySlot StoneCutterInput = 
			UiInventorySlot.StoneCutterInput;

		public static readonly UiInventorySlot[] Trade2Ingredient = 
		[
			UiInventorySlot.Trade2Ingredient1, 
			UiInventorySlot.Trade2Ingredient2
		];

		public static readonly UiInventorySlot[] TradeIngredient = 
		[
			UiInventorySlot.TradeIngredient1, 
			UiInventorySlot.TradeIngredient2
		];

		public static readonly UiInventorySlot MaterialReducerInput = 
			UiInventorySlot.MaterialReducerInput;

		public static readonly UiInventorySlot[] Loom = 
		[
			UiInventorySlot.LoomInput, 
			UiInventorySlot.LoomDye, 
			UiInventorySlot.LoomMaterial
		];

		public static readonly UiInventorySlot[] CartographyTable = 
		[
			UiInventorySlot.CartographyInput, 
			UiInventorySlot.CartographyAdditional
		];

		public static readonly UiInventorySlot[] EnchantingTable = 
		[
			UiInventorySlot.EnchantingInput, 
			UiInventorySlot.EnchantingMaterial
		];

		public static readonly UiInventorySlot[] Grindstone = 
		[
			UiInventorySlot.GrindstoneInput, 
			UiInventorySlot.GrindstoneAdditional
		];

		public static readonly UiInventorySlot[] CompoundCreatorInput = 
		[
			UiInventorySlot.CompoundCreatorInput1,
			UiInventorySlot.CompoundCreatorInput2,
			UiInventorySlot.CompoundCreatorInput3,
			UiInventorySlot.CompoundCreatorInput4,
			UiInventorySlot.CompoundCreatorInput5,
			UiInventorySlot.CompoundCreatorInput6,
			UiInventorySlot.CompoundCreatorInput7,
			UiInventorySlot.CompoundCreatorInput8,
			UiInventorySlot.CompoundCreatorInput9
		];

		public static readonly UiInventorySlot BeaconPayment = UiInventorySlot.BeaconPayment;

		public static readonly UiInventorySlot[] Crafting2x2Input = 
		[
			UiInventorySlot.Crafting2x2Input1,
			UiInventorySlot.Crafting2x2Input2,
			UiInventorySlot.Crafting2x2Input3,
			UiInventorySlot.Crafting2x2Input4
		];

		public static readonly UiInventorySlot[] Crafting3x3Input =
		[
			UiInventorySlot.Crafting3x3Input1,
			UiInventorySlot.Crafting3x3Input2,
			UiInventorySlot.Crafting3x3Input3,
			UiInventorySlot.Crafting3x3Input4,
			UiInventorySlot.Crafting3x3Input5,
			UiInventorySlot.Crafting3x3Input6,
			UiInventorySlot.Crafting3x3Input7,
			UiInventorySlot.Crafting3x3Input8,
			UiInventorySlot.Crafting3x3Input9
		];

		public static readonly UiInventorySlot[] CraftingInput =
		[
			..Crafting2x2Input,
			..Crafting3x3Input
		];

		public static readonly UiInventorySlot[] MaterialReducerOutput =
		[
			UiInventorySlot.MaterialReducerOutput1,
			UiInventorySlot.MaterialReducerOutput2,
			UiInventorySlot.MaterialReducerOutput3,
			UiInventorySlot.MaterialReducerOutput4,
			UiInventorySlot.MaterialReducerOutput5,
			UiInventorySlot.MaterialReducerOutput6,
			UiInventorySlot.MaterialReducerOutput7,
			UiInventorySlot.MaterialReducerOutput8,
			UiInventorySlot.MaterialReducerOutput9
		];

		public static readonly UiInventorySlot CreatedItemOutput = UiInventorySlot.CreatedItemOutput;

		public static readonly UiInventorySlot[] SmithingTable = 
		[
			UiInventorySlot.SmithingTableInput,
			UiInventorySlot.SmithingTableMaterial,
			UiInventorySlot.SmithingTableTemplate
		];

		public static byte? GetSlotByContainerId(ContainerId containerId, byte relativeSlot = 0)
		{
			UiInventorySlot? slot = containerId switch
			{
				ContainerId.AnvilInput => UiInventorySlot.AnvilInput,
				ContainerId.AnvilMaterial => UiInventorySlot.AnvilMaterial,
				ContainerId.AnvilResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.SmithingTableInput => UiInventorySlot.SmithingTableInput,
				ContainerId.SmithingTableMaterial => UiInventorySlot.SmithingTableMaterial,
				ContainerId.SmithingTableResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.BeaconPayment => BeaconPayment,
				ContainerId.CraftingInput => CraftingInput[relativeSlot],
				ContainerId.CraftingOutputPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.EnchantingInput => UiInventorySlot.EnchantingInput,
				ContainerId.EnchantingMaterial => UiInventorySlot.EnchantingMaterial,
				ContainerId.TradeIngredient1 => UiInventorySlot.TradeIngredient1,
				ContainerId.TradeIngredient2 => UiInventorySlot.TradeIngredient2,
				ContainerId.CompoundCreatorInput => CompoundCreatorInput[relativeSlot],
				ContainerId.CompoundCreatorOutputPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.ElementConstructorOutputPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.MaterialReducerInput => UiInventorySlot.MaterialReducerInput,
				ContainerId.MaterialReducerOutput => MaterialReducerOutput[relativeSlot],
				ContainerId.LoomInput => UiInventorySlot.LoomInput,
				ContainerId.LoomDye => UiInventorySlot.LoomDye,
				ContainerId.LoomMaterial => UiInventorySlot.LoomMaterial,
				ContainerId.LoomResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.Trade2Ingredient1 => UiInventorySlot.Trade2Ingredient1,
				ContainerId.Trade2Ingredient2 => UiInventorySlot.Trade2Ingredient2,
				ContainerId.Trade2ResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.GrindstoneInput => UiInventorySlot.GrindstoneInput,
				ContainerId.GrindstoneAdditional => UiInventorySlot.GrindstoneAdditional,
				ContainerId.GrindstoneResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.StonecutterInput => UiInventorySlot.StoneCutterInput,
				ContainerId.StonecutterResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.CartographyInput => UiInventorySlot.CartographyInput,
				ContainerId.CartographyAdditional => UiInventorySlot.CartographyAdditional,
				ContainerId.CartographyResultPreview => UiInventorySlot.CreatedItemOutput,
				ContainerId.Cursor => UiInventorySlot.Cursor,
				ContainerId.CreatedOutput => UiInventorySlot.CreatedItemOutput,
				ContainerId.SmithingTableTemplate => UiInventorySlot.SmithingTableTemplate,

				_ => null
			};

			return (byte?)slot;
		}
	}
}
