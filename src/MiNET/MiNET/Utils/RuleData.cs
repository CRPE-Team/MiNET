#region LICENSE

// The contents of this file are subject to the Common Public Attribution
// License Version 1.0. (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// https://github.com/NiclasOlofsson/MiNET/blob/master/LICENSE. 
// The License is based on the Mozilla Public License Version 1.1, but Sections 14 
// and 15 have been added to cover use of software over a computer network and 
// provide for limited attribution for the Original Developer. In addition, Exhibit A has 
// been modified to be consistent with Exhibit B.
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for
// the specific language governing rights and limitations under the License.
// 
// The Original Code is MiNET.
// 
// The Original Developer is the Initial Developer.  The Initial Developer of
// the Original Code is Niclas Olofsson.
// 
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2018 Niclas Olofsson. 
// All Rights Reserved.

#endregion

using System.Collections.Generic;
using MiNET.Net;

namespace MiNET.Utils
{
	public class Rules : List<RuleData>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.Write(Count); // LE
			foreach (var rule in this)
			{
				rule.Write(packet);
			}
		}

		public static Rules Read(Packet packet)
		{
			var rules = new Rules();

			var count = packet.ReadInt(); // LE
			for (int i = 0; i < count; i++)
			{
				rules.Add(RuleData.Read(packet));
			}

			return rules;
		}
	}

	public class RuleData : IPacketDataObject
	{
		public string Name { get; set; }
		public bool Unknown1 { get; set; }
		public bool Unknown2 { get; set; }

		public override string ToString()
		{
			return $"Name: {Name}, Unknown1: {Unknown1}, Unknown2: {Unknown2}";
		}

		public void Write(Packet packet)
		{
			packet.Write(Name);
			packet.Write(Unknown1);
			packet.Write(Unknown2);
		}

		public static RuleData Read(Packet packet)
		{
			return new RuleData()
			{
				Name = packet.ReadString(),
				Unknown1 = packet.ReadBool(),
				Unknown2 = packet.ReadBool()
			};
		}
	}
}