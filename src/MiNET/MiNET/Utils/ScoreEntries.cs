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
using System.Linq;
using MiNET.Entities;
using MiNET.Net;
using Org.BouncyCastle.Ocsp;

namespace MiNET.Utils
{
	public class ScoreEntries : List<ScoreEntry>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.Write((byte) (this.FirstOrDefault() is ScoreEntryRemove ? McpeSetScore.Types.Remove : McpeSetScore.Types.Change));
			packet.WriteUnsignedVarInt((uint) Count);
			foreach (var entry in this)
			{
				entry.Write(packet);
			}
		}	

		public static ScoreEntries Read(Packet packet)
		{
			var list = new ScoreEntries();
			var type = packet.ReadByte();
			var length = packet.ReadUnsignedVarInt();
			for (var i = 0; i < length; ++i)
			{
				var entry = ScoreEntry.Read(packet, type);
				if (entry == null) continue;

				list.Add(entry);
			}

			return list;
		}
	}

	public abstract class ScoreEntry : IPacketDataObject
	{
		public long Id { get; set; }

		public string ObjectiveName { get; set; }

		public uint Score { get; set; }

		public void Write(Packet packet)
		{
			packet.WriteSignedVarLong(Id);
			packet.Write(ObjectiveName);
			packet.Write(Score);

			WriteData(packet);
		}

		protected virtual void WriteData(Packet packet) { }

		public static ScoreEntry Read(Packet packet, byte type)
		{
			var entryId = packet.ReadSignedVarLong();
			var entryObjectiveName = packet.ReadString();
			var entryScore = packet.ReadUint();

			ScoreEntry entry = type switch
			{
				(int) McpeSetScore.Types.Remove => ScoreEntryRemove.ReadData(packet),

				_ => (McpeSetScore.ChangeTypes) packet.ReadByte() switch
				{
					McpeSetScore.ChangeTypes.Player => ScoreEntryChangePlayer.ReadData(packet),
					McpeSetScore.ChangeTypes.Entity => ScoreEntryChangeEntity.ReadData(packet),
					McpeSetScore.ChangeTypes.FakePlayer => ScoreEntryChangeFakePlayer.ReadData(packet)
				}
			};

			if (entry == null) return null;

			entry.Id = entryId;
			entry.ObjectiveName = entryObjectiveName;
			entry.Score = entryScore;

			return entry;
		}
	}

	public class ScoreEntryRemove : ScoreEntry
	{
		internal static ScoreEntryRemove ReadData(Packet packet)
		{
			return new ScoreEntryRemove();
		}
	}

	public abstract class ScoreEntryChange : ScoreEntry
	{
	}

	public class ScoreEntryChangePlayer : ScoreEntryChange
	{
		public long EntityId { get; set; }

		protected override void WriteData(Packet packet)
		{
			packet.Write((byte) McpeSetScore.ChangeTypes.Player);
			packet.WriteSignedVarLong(EntityId);
		}

		internal static ScoreEntryChangePlayer ReadData(Packet packet)
		{
			return new ScoreEntryChangePlayer 
			{ 
				EntityId = packet.ReadSignedVarLong() 
			};
		}
	}

	public class ScoreEntryChangeEntity : ScoreEntryChange
	{
		public long EntityId { get; set; }

		protected override void WriteData(Packet packet)
		{
			packet.Write((byte) McpeSetScore.ChangeTypes.Entity);
			packet.WriteSignedVarLong(EntityId);
		}

		internal static ScoreEntryChangeEntity ReadData(Packet packet)
		{
			return new ScoreEntryChangeEntity
			{
				EntityId = packet.ReadSignedVarLong()
			};
		}
	}

	public class ScoreEntryChangeFakePlayer : ScoreEntryChange
	{
		public string CustomName { get; set; }

		protected override void WriteData(Packet packet)
		{
			packet.Write((byte) McpeSetScore.ChangeTypes.FakePlayer);
			packet.Write(CustomName);
		}

		internal static ScoreEntryChangeFakePlayer ReadData(Packet packet)
		{
			return new ScoreEntryChangeFakePlayer 
			{ 
				CustomName = packet.ReadString() 
			};
		}
	}

	public class ScoreboardIdentityEntries : List<ScoreboardIdentityEntry>, IPacketDataObject
	{
		public void Write(Packet packet)
		{
			packet.Write((byte) (this.FirstOrDefault() is ScoreboardClearIdentityEntry ? McpeSetScoreboardIdentity.Operations.ClearIdentity : McpeSetScoreboardIdentity.Operations.RegisterIdentity));
			packet.WriteUnsignedVarInt((uint) Count);
			foreach (var entry in this)
			{
				entry.Write(packet);
			}
		}

		public static ScoreboardIdentityEntries Read(Packet packet)
		{
			var list = new ScoreboardIdentityEntries();

			var type = (McpeSetScoreboardIdentity.Operations) packet.ReadByte();
			var length = packet.ReadUnsignedVarInt();
			for (var i = 0; i < length; ++i)
			{
				list.Add(ScoreboardIdentityEntry.Read(packet, type));
			}

			return list;
		}
	}

	public abstract class ScoreboardIdentityEntry : IPacketDataObject
	{
		public long Id { get; set; }

		public void Write(Packet packet)
		{
			packet.WriteSignedVarLong(Id);

			WriteData(packet);
		}

		protected virtual void WriteData(Packet packet) { }

		public static ScoreboardIdentityEntry Read(Packet packet, McpeSetScoreboardIdentity.Operations type)
		{
			var scoreboardId = packet.ReadSignedVarLong();

			return type switch
			{
				McpeSetScoreboardIdentity.Operations.RegisterIdentity => ScoreboardRegisterIdentityEntry.ReadData(packet, scoreboardId),
				McpeSetScoreboardIdentity.Operations.ClearIdentity => ScoreboardClearIdentityEntry.ReadData(packet, scoreboardId)
			};

			// https://github.com/pmmp/PocketMine-MP/commit/39808dd94f4f2d1716eca31cb5a1cfe9000b6c38#diff-041914be0a0493190a4911ae5c4ac502R62
		}
	}

	public class ScoreboardRegisterIdentityEntry : ScoreboardIdentityEntry
	{
		public long EntityId { get; set; }

		protected override void WriteData(Packet packet)
		{
			packet.WriteSignedVarLong(EntityId);
		}

		internal static ScoreboardRegisterIdentityEntry ReadData(Packet packet, long scoreboardId)
		{
			return new ScoreboardRegisterIdentityEntry() 
			{ 
				Id = scoreboardId, 
				EntityId = packet.ReadSignedVarLong() 
			};
		}
	}

	public class ScoreboardClearIdentityEntry : ScoreboardIdentityEntry
	{
		internal static ScoreboardClearIdentityEntry ReadData(Packet packet, long scoreboardId)
		{
			return new ScoreboardClearIdentityEntry() 
			{ 
				Id = scoreboardId
			};
		}
	}
}