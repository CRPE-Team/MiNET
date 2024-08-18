namespace MiNET.Net
{
	public partial class McpeDisconnect
	{
		public string message;
		public string filteredMessage;

		partial void AfterEncode()
		{
			var skipMessage = message == null && filteredMessage == null;
			Write(skipMessage);
			if (!skipMessage)
			{
				Write(message);
				Write(filteredMessage);
			}

		}

		partial void AfterDecode()
		{
			var skipMessage = ReadBool();
			if (!skipMessage)
			{
				message = ReadString();
				filteredMessage = ReadString();
			}
		}
	}
}
