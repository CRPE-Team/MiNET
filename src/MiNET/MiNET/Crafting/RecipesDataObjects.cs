using System.Collections.Generic;
using MiNET.Inventories;

namespace MiNET.Crafting
{
	internal class RecipeDataBase
	{
		public string Block { get; set; }

		public ExternalDataItem[] Output { get; set; }

		public int Priority { get; set; }

		public string[] Shape { get; set; }

		public ExternalDataItem[] UnlockingIngredients { get; set; }
	}

	internal class ShapelessRecipeData : RecipeDataBase
	{
		public ExternalDataItem[] Input { get; set; }
	}

	internal class ShapedRecipeData : RecipeDataBase
	{
		public Dictionary<string, ExternalDataItem> Input { get; set; }
	}

	internal class SmeltingRecipeData
	{
		public string Block { get; set; }

		public ExternalDataItem Input { get; set; }

		public ExternalDataItem Output { get; set; }
	}
}
