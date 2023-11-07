namespace MiNET.Net;

public class EducationUriResource : IPacketDataObject
{
	public string ButtonName { get; set; }
	public string LinkUri { get; set; }

	public EducationUriResource()
	{
		
	}

	public EducationUriResource(string buttonName, string linkUri)
	{
		ButtonName = buttonName;
		LinkUri = linkUri;
	}

	public void Write(Packet packet)
	{
		packet.Write(ButtonName);
		packet.Write(LinkUri);
	}

	public static EducationUriResource Read(Packet packet)
	{
		var name = packet.ReadString();
		var uri = packet.ReadString();

		return new EducationUriResource(name, uri);
	}
}