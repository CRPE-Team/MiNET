using fNbt.Serialization;

namespace MiNET.Blocks.Utils
{
	public class BlockProperties
	{
		public BlockProperties(float blastResistance, float brightness, int igniteOdds, int burnOdds, float friction, float hardness, float opacity)
		{
			BlastResistance = blastResistance;
			Brightness = brightness;
			IgniteOdds = igniteOdds;
			BurnOdds = burnOdds;
			Friction = friction;
			Hardness = hardness;
			Opacity = opacity;
		}

		[NbtProperty("blastResistance")]
		public float BlastResistance { get; }

		[NbtProperty("brightness")]
        public float Brightness { get; }

		[NbtProperty("flameEncouragement")]
        public int IgniteOdds { get; }

		[NbtProperty("flammability")]
		public int BurnOdds { get; }

		[NbtProperty("friction")]
		public float Friction { get; }

		[NbtProperty("hardness")]
        public float Hardness { get; }

		[NbtProperty("opacity")]
        public float Opacity { get; }
	}
}
