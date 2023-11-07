using System.Collections.Generic;
using static MiNET.Net.Experiments;

namespace MiNET.Net
{
	public class Experiments : List<Experiments.Experiment>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.Write(Count);

			foreach (var experiment in this)
			{
				experiment.Write(packet);
			}
		}

		public static Experiments Read(Packet packet)
		{
			var experiments = new Experiments();
			var count = packet.ReadInt();

			for (int i = 0; i < count; i++)
			{
				experiments.Add(Experiment.Read(packet));
			}

			return experiments;
		}

		public class Experiment : IPacketDataObject
		{
			public string Name { get; }
			public bool Enabled { get; }

			public Experiment(string name, bool enabled)
			{
				Name = name;
				Enabled = enabled;
			}

			public void Write(Packet packet)
			{
				packet.Write(Name);
				packet.Write(Enabled);
			}

			public static Experiment Read(Packet packet)
			{
				var experimentName = packet.ReadString();
				var enabled = packet.ReadBool();
				return new Experiment(experimentName, enabled);
			}
		}
	}
}