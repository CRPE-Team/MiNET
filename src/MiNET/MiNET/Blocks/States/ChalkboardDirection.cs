using MiNET.Utils;

namespace MiNET.Blocks.States
{
	public class ChalkboardDirection : BlockStateInt
	{
		public override string Name => "direction";

		public const int MaxValue = 15;

		public static int[] Values()
		{
			return [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
		}


		protected override void ValidateValue(int value)
		{
			if (value < 0 || value > MaxValue)
			{
				ThrowArgumentException(value);
			}
		}
	}
}
