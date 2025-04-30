namespace MiNET.Net
{
	public partial class McpeSetHud
	{
		public int[] hudElements;
		public int hudVisibility;

		partial void AfterEncode()
		{
			WriteLength(hudElements.Length);
			for (var i = 0; i < hudElements.Length; i++)
			{
				WriteVarInt(hudElements[i]);
			}

			WriteVarInt(hudVisibility);
		}

		partial void AfterDecode()
		{
			hudElements = new int[ReadLength()];
			for (var i = 0; i < hudElements.Length; i++)
			{
				hudElements[i] = ReadVarInt();
			}

			hudVisibility = ReadVarInt();
		}

		public override void Reset()
		{
			hudElements = default;
			hudVisibility = default;

			base.Reset();
		}
	}
}
