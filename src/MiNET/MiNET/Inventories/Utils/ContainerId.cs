using System;

namespace MiNET.Inventories
{
	public enum ContainerId
	{
		Unknown = -1,

		AnvilInput = 0,
		AnvilMaterial = 1,
		AnvilResultPreview = 2,
		SmithingTableInput = 3,
		SmithingTableMaterial = 4,
		SmithingTableResultPreview = 5,
		Armor = 6,
		LevelEntity = 7,
		BeaconPayment = 8,
		BrewingStandInput = 9,
		BrewingStandResult = 10,
		BrewingStandFuel = 11,
		CombinedHotbarAndInventory = 12,
		CraftingInput = 13,
		CraftingOutputPreview = 14,
		RecipeConstruction = 15,
		RecipeNature = 16,
		RecipeItems = 17,
		RecipeSearch = 18,
		RecipeSearchBar = 19,
		RecipeEquipment = 20,
		RecipeBook = 21,
		EnchantingInput = 22,
		EnchantingMaterial = 23,
		FurnaceFuel = 24,
		FurnaceIngredient = 25,
		FurnaceResult = 26,
		HorseEquip = 27,
		Hotbar = 28,
		Inventory = 29,
		ShulkerBox = 30,
		TradeIngredient1 = 31,
		TradeIngredient2 = 32,
		TradeResultPreview = 33,
		Offhand = 34,
		CompoundCreatorInput = 35,
		CompoundCreatorOutputPreview = 36,
		ElementConstructorOutputPreview = 37,
		MaterialReducerInput = 38,
		MaterialReducerOutput = 39,
		LabTableInput = 40,
		LoomInput = 41,
		LoomDye = 42,
		LoomMaterial = 43,
		LoomResultPreview = 44,
		BlastFurnaceIngredient = 45,
		SmokerIngredient = 46,
		Trade2Ingredient1 = 47,
		Trade2Ingredient2 = 48,
		Trade2ResultPreview = 49,
		GrindstoneInput = 50,
		GrindstoneAdditional = 51,
		GrindstoneResultPreview = 52,
		StonecutterInput = 53,
		StonecutterResultPreview = 54,
		CartographyInput = 55,
		CartographyAdditional = 56,
		CartographyResultPreview = 57,
		Barrel = 58,
		Cursor = 59,
		CreatedOutput = 60,
		SmithingTableTemplate = 61,
		Crafter = 62,
		Dynamic = 63
	}

	public static class WindowIdExtensions
	{
		public static WindowId ToWindowId(this ContainerId containerId)
		{
			return containerId.ToWindowId(WindowId.None);
		}

		public static WindowId ToWindowId(this ContainerId containerId, WindowId currentWindowId)
		{
			switch (containerId)
			{
				case ContainerId.Armor:
					return WindowId.Armor;

				case ContainerId.Hotbar:
				case ContainerId.Inventory:
				case ContainerId.CombinedHotbarAndInventory:
					return WindowId.Inventory;

				case ContainerId.Offhand:
					return WindowId.Offhand;

				case ContainerId.AnvilInput:
				case ContainerId.AnvilMaterial:
				case ContainerId.BeaconPayment:
				case ContainerId.CartographyAdditional:
				case ContainerId.CartographyInput:
				case ContainerId.CompoundCreatorInput:
				case ContainerId.CraftingInput:
				case ContainerId.CreatedOutput:
				case ContainerId.Cursor:
				case ContainerId.EnchantingInput:
				case ContainerId.EnchantingMaterial:
				case ContainerId.GrindstoneAdditional:
				case ContainerId.GrindstoneInput:
				case ContainerId.LabTableInput:
				case ContainerId.LoomDye:
				case ContainerId.LoomInput:
				case ContainerId.LoomMaterial:
				case ContainerId.MaterialReducerInput:
				case ContainerId.MaterialReducerOutput:
				case ContainerId.SmithingTableInput:
				case ContainerId.SmithingTableMaterial:
				case ContainerId.SmithingTableTemplate:
				case ContainerId.StonecutterInput:
				case ContainerId.Trade2Ingredient1:
				case ContainerId.Trade2Ingredient2:
				case ContainerId.TradeIngredient1:
				case ContainerId.TradeIngredient2:
					return WindowId.UI;

				case ContainerId.Barrel:
				case ContainerId.BlastFurnaceIngredient:
				case ContainerId.BrewingStandFuel:
				case ContainerId.BrewingStandInput:
				case ContainerId.BrewingStandResult:
				case ContainerId.FurnaceFuel:
				case ContainerId.FurnaceIngredient:
				case ContainerId.FurnaceResult:
				case ContainerId.HorseEquip:
				case ContainerId.LevelEntity: //chest
				case ContainerId.ShulkerBox:
				case ContainerId.SmokerIngredient:
					return currentWindowId;

				default:
					throw new Exception($"Unexpected container ID {containerId}");
			};
		}
	}
}
