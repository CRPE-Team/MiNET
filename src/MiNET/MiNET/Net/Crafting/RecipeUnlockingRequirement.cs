namespace MiNET.Net.Crafting
{
	public class RecipeUnlockingRequirement : IPacketDataObject
	{
		public RecipeIngredient[] UnlockingIngredients { get; set; }

		public void Write(Packet packet)
		{
			var unlockingContext = UnlockingIngredients == null;

			packet.Write(unlockingContext);

			if (!unlockingContext)
			{
				packet.Write(UnlockingIngredients);
			}
		}

		public static RecipeUnlockingRequirement Read(Packet packet)
		{
			var requirement = new RecipeUnlockingRequirement();

			if (!packet.ReadBool())
			{
				var count = packet.ReadLength();

				requirement.UnlockingIngredients = new RecipeIngredient[count];
				for (var i = 0; i < count; i++)
				{
					requirement.UnlockingIngredients[i] = RecipeIngredient.Read(packet);
				}
			}

			return requirement;
		}
	}
}
