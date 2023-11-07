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

using System;
using LibNoise.Combiner;
using log4net;
using MiNET.Blocks;
using MiNET.Entities;
using MiNET.Net;
using MiNET.Utils.Vectors;

namespace MiNET.Utils
{
	public class MapInfo : ICloneable, IPacketDataObject
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(MapInfo));

		private const int MapUpdateFlagTexture = 0x02;
		private const int MapUpdateFlagDecoration = 0x04;
		private const int MapUpdateFlagInitialisation = 0x08;

		public long MapId { get; set; }
		public byte UpdateType { get; set; }
		public BlockCoordinates Origin { get; set; } = new BlockCoordinates();
		public MapDecorator[] Decorators { get; set; } = new MapDecorator[0];
		public MapTrackedObject[] TrackedObjects { get; set; } = new MapTrackedObject[0];
		public byte X { get; set; }
		public byte Z { get; set; }
		public int Scale { get; set; }
		public int Col { get; set; }
		public int Row { get; set; }
		public int XOffset { get; set; }
		public int ZOffset { get; set; }
		public byte[] Data { get; set; }

		public override string ToString()
		{
			return $"MapId: {MapId}, UpdateType: {UpdateType}, X: {X}, Z: {Z}, Col: {Col}, Row: {Row}, X-offset: {XOffset}, Z-offset: {ZOffset}, Data: {Data?.Length}";
		}

		public object Clone()
		{
			return MemberwiseClone();
		}

		public void Write(Packet packet)
		{
			packet.WriteSignedVarLong(MapId);
			packet.WriteUnsignedVarInt((uint) UpdateType);
			packet.Write((byte) 0); // dimension
			packet.Write(false); // Locked
			packet.Write(Origin);

			if ((UpdateType & MapUpdateFlagInitialisation) != 0)
			{
				packet.WriteUnsignedVarInt(0);
				//packet.WriteSignedVarLong(MapId);
			}

			if ((UpdateType & (MapUpdateFlagInitialisation | MapUpdateFlagDecoration | MapUpdateFlagTexture)) != 0)
			{
				packet.Write((byte) Scale);
			}

			if ((UpdateType & MapUpdateFlagDecoration) != 0)
			{
				var countTrackedObj = TrackedObjects.Length;

				packet.WriteUnsignedVarInt((uint) countTrackedObj);
				foreach (var trackedObject in TrackedObjects)
				{
					trackedObject.Write(packet);
				}

				var count = Decorators.Length;

				packet.WriteUnsignedVarInt((uint) count);
				foreach (var decorator in Decorators)
				{
					decorator.WriteAdditionalData(packet);
				}

				packet.WriteUnsignedVarInt((uint) count);
				foreach (var decorator in Decorators)
				{
					decorator.Write(packet);
				}
			}

			if ((UpdateType & MapUpdateFlagTexture) != 0)
			{
				packet.WriteSignedVarInt(Col);
				packet.WriteSignedVarInt(Row);

				packet.WriteSignedVarInt(XOffset);
				packet.WriteSignedVarInt(ZOffset);

				packet.WriteUnsignedVarInt((uint) (Col * Row));
				int i = 0;
				for (int col = 0; col < Col; col++)
				{
					for (int row = 0; row < Row; row++)
					{
						byte r = Data[i++];
						byte g = Data[i++];
						byte b = Data[i++];
						byte a = Data[i++];
						uint color = BitConverter.ToUInt32(new byte[] { r, g, b, 0xff }, 0);
						packet.WriteUnsignedVarInt(color);
					}
				}
			}
		}

		public static MapInfo Read(Packet packet)
		{
			MapInfo map = new MapInfo();

			map.MapId = packet.ReadSignedVarLong();
			map.UpdateType = (byte) packet.ReadUnsignedVarInt();
			packet.ReadByte(); // Dimension (waste)
			packet.ReadBool(); // Locked (waste)

			if ((map.UpdateType & MapUpdateFlagInitialisation) == MapUpdateFlagInitialisation)
			{
				// Entities
				var count = packet.ReadUnsignedVarInt();
				for (int i = 0; i < count - 1; i++) // This is some weird shit vanilla is doing with counting.
				{
					var eid = packet.ReadSignedVarLong();
				}
			}

			if ((map.UpdateType & MapUpdateFlagTexture) == MapUpdateFlagTexture || (map.UpdateType & MapUpdateFlagDecoration) == MapUpdateFlagDecoration)
			{
				map.Scale = packet.ReadByte();
				//Log.Warn($"packet.Reading scale {map.Scale}");
			}

			if ((map.UpdateType & MapUpdateFlagDecoration) == MapUpdateFlagDecoration)
			{
				// Decorations
				//Log.Warn("Got decoration update, reading it");

				try
				{
					var entityCount = packet.ReadUnsignedVarInt();
					for (int i = 0; i < entityCount; i++)
					{
						var type = packet.ReadInt();
						if (type == 0)
						{
							var entity = EntityMapDecorator.ReadAdditionalData(packet);
						}
						else if (type == 1)
						{
							var block = BlockMapDecorator.ReadAdditionalData(packet);
						}
					}

					var count = packet.ReadUnsignedVarInt();
					map.Decorators = new MapDecorator[count];
					for (int i = 0; i < count; i++)
					{
						map.Decorators[i] = MapDecorator.Read(packet);
					}
				}
				catch (Exception e)
				{
					Log.Error($"Error while reading decorations for map={map}", e);
				}
			}

			if ((map.UpdateType & MapUpdateFlagTexture) == MapUpdateFlagTexture)
			{
				// Full map
				try
				{
					map.Col = packet.ReadSignedVarInt();
					map.Row = packet.ReadSignedVarInt(); //

					map.XOffset = packet.ReadSignedVarInt(); //
					map.ZOffset = packet.ReadSignedVarInt(); //
					packet.ReadUnsignedVarInt(); // size
					for (int col = 0; col < map.Col; col++)
					{
						for (int row = 0; row < map.Row; row++)
						{
							packet.ReadUnsignedVarInt();
						}
					}
				}
				catch (Exception e)
				{
					Log.Error($"Errror while reading map data for map={map}", e);
				}
			}

			//else
			//{
			//	Log.Warn($"Unknown map-type 0x{map.UpdateType:X2}");
			//}

			//map.MapId = ReadLong();
			//var readBytes = ReadBytes(3);
			////Log.Warn($"{HexDump(readBytes)}");
			//map.UpdateType = ReadByte(); //
			//var bytes = ReadBytes(6);
			////Log.Warn($"{HexDump(bytes)}");

			//map.Direction = ReadByte(); //
			//map.X = ReadByte(); //
			//map.Z = ReadByte(); //

			//if (map.UpdateType == 0x06)
			//{
			//	// Full map
			//	try
			//	{
			//		if (bytes[4] == 1)
			//		{
			//			map.Col = ReadInt();
			//			map.Row = ReadInt(); //

			//			map.XOffset = ReadInt(); //
			//			map.ZOffset = ReadInt(); //

			//			map.Data = ReadBytes(map.Col*map.Row*4);
			//		}
			//	}
			//	catch (Exception e)
			//	{
			//		Log.Error($"Errror while reading map data for map={map}", e);
			//	}
			//}
			//else if (map.UpdateType == 0x04)
			//{
			//	// Map update
			//}
			//else
			//{
			//	Log.Warn($"Unknown map-type 0x{map.UpdateType:X2}");
			//}

			return map;
		}
	}

	public class MapDecorator : IPacketDataObject
	{
		protected int Type { get; set; }
		public byte Rotation { get; set; }
		public byte Icon { get; set; }
		public byte X { get; set; }
		public byte Z { get; set; }
		public string Label { get; set; }
		public uint Color { get; set; }

		public void Write(Packet packet)
		{
			packet.Write((byte) Icon);
			packet.Write((byte) Rotation);
			packet.Write((byte) X);
			packet.Write((byte) Z);
			packet.Write(Label);
			packet.WriteUnsignedVarInt(Color);
		}

		internal virtual void WriteAdditionalData(Packet packet) { }

		public static MapDecorator Read(Packet packet)
		{
			var decorator = new MapDecorator();

			decorator.Icon = packet.ReadByte();
			decorator.Rotation = packet.ReadByte();
			decorator.X = packet.ReadByte();
			decorator.Z = packet.ReadByte();
			decorator.Label = packet.ReadString();
			decorator.Color = packet.ReadUnsignedVarInt();

			return decorator;
		}
	}

	public class BlockMapDecorator : MapDecorator
	{
		public BlockCoordinates Coordinates { get; set; }

		public BlockMapDecorator()
		{
			Type = 1;
		}

		internal override void WriteAdditionalData(Packet packet)
		{
			packet.Write(Coordinates);
		}

		internal static BlockMapDecorator ReadAdditionalData(Packet packet)
		{
			return new BlockMapDecorator()
			{
				Coordinates = packet.ReadBlockCoordinates()
			};
		}
	}

	public class EntityMapDecorator : MapDecorator
	{
		public long EntityId { get; set; }

		public EntityMapDecorator()
		{
			Type = 0;
		}

		internal override void WriteAdditionalData(Packet packet)
		{
			packet.WriteSignedVarLong(EntityId);
		}

		internal static EntityMapDecorator ReadAdditionalData(Packet packet)
		{
			return new EntityMapDecorator()
			{
				EntityId = packet.ReadSignedVarLong()
			};
		}
	}

	public class MapTrackedObject : IPacketDataObject
	{
		protected int Type { get; set; }

		public virtual void Write(Packet packet) { }
	}

	public class EntityMapTrackedObject : MapTrackedObject
	{
		public long EntityId { get; set; }

		public EntityMapTrackedObject()
		{
			Type = 0;
		}

		public override void Write(Packet packet)
		{
			packet.Write(0);
			packet.WriteSignedVarLong(EntityId);
		}
	}

	public class BlockMapTrackedObject : MapTrackedObject
	{
		public BlockCoordinates Coordinates { get; set; }

		public BlockMapTrackedObject()
		{
			Type = 1;
		}

		public override void Write(Packet packet)
		{
			packet.Write(1);
			packet.Write(Coordinates);
		}
	}
}