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
using log4net;
using MiNET.Net;
using MiNET.Utils.Vectors;

namespace MiNET.Utils
{
	public class MapInfo : IPacketDataObject, ICloneable
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
					if (trackedObject is EntityMapTrackedObject entity)
					{
						packet.Write(0);
						packet.WriteSignedVarLong(entity.EntityId);
					}
					else if (trackedObject is BlockMapTrackedObject block)
					{
						packet.Write(1);
						packet.Write(block.Coordinates);
					}
				}

				var count = Decorators.Length;
				packet.WriteLength(count);
				foreach (var decorator in Decorators)
				{
					if (decorator is EntityMapDecorator entity)
					{
						packet.WriteSignedVarLong(entity.EntityId);
					}
					else if (decorator is BlockMapDecorator block)
					{
						packet.Write(block.Coordinates);
					}
				}

				packet.WriteLength(count);
				foreach (var decorator in Decorators)
				{
					packet.Write(decorator.Icon);
					packet.Write(decorator.Rotation);
					packet.Write(decorator.X);
					packet.Write(decorator.Z);
					packet.Write(decorator.Label);
					packet.WriteUnsignedVarInt(decorator.Color);
				}
			}

			if ((UpdateType & MapUpdateFlagTexture) != 0)
			{
				packet.WriteSignedVarInt(Col);
				packet.WriteSignedVarInt(Row);

				packet.WriteSignedVarInt(XOffset);
				packet.WriteSignedVarInt(ZOffset);

				packet.WriteUnsignedVarInt((uint) (Col * Row));
				var i = 0;
				for (int col = 0; col < Col; col++)
				{
					for (int row = 0; row < Row; row++)
					{
						var r = Data[i++];
						var g = Data[i++];
						var b = Data[i++];
						var a = Data[i++];
						var color = BitConverter.ToUInt32(new byte[] { r, g, b, 0xff }, 0);
						packet.WriteUnsignedVarInt(color);
					}
				}
			}
		}

		public static MapInfo Read(Packet packet)
		{
			var map = new MapInfo();

			map.MapId = packet.ReadSignedVarLong();
			map.UpdateType = (byte) packet.ReadUnsignedVarInt();
			packet.ReadByte(); // Dimension (waste)
			packet.ReadBool(); // Locked (waste)

			if ((map.UpdateType & MapUpdateFlagInitialisation) == MapUpdateFlagInitialisation)
			{
				// Entities
				var count = packet.ReadLength();
				for (int i = 0; i < count - 1; i++) // This is some weird shit vanilla is doing with counting.
				{
					var eid = packet.ReadSignedVarLong();
				}
			}

			if ((map.UpdateType & MapUpdateFlagTexture) == MapUpdateFlagTexture || (map.UpdateType & MapUpdateFlagDecoration) == MapUpdateFlagDecoration)
			{
				map.Scale = packet.ReadByte();
				//Log.Warn($"Reading scale {map.Scale}");
			}

			if ((map.UpdateType & MapUpdateFlagDecoration) == MapUpdateFlagDecoration)
			{
				// Decorations
				//Log.Warn("Got decoration update, reading it");

				try
				{
					var entityCount = packet.ReadLength();
					for (int i = 0; i < entityCount; i++)
					{
						var type = packet.ReadInt();
						if (type == 0)
						{
							// entity
							var q = packet.ReadSignedVarLong();
						}
						else if (type == 1)
						{
							// block
							var b = packet.ReadBlockCoordinates();
						}
					}

					var count = packet.ReadLength();
					map.Decorators = new MapDecorator[count];
					for (int i = 0; i < count; i++)
					{
						MapDecorator decorator = new MapDecorator();
						decorator.Icon = packet.ReadByte();
						decorator.Rotation = packet.ReadByte();
						decorator.X = packet.ReadByte();
						decorator.Z = packet.ReadByte();
						decorator.Label = packet.ReadString();
						decorator.Color = packet.ReadUnsignedVarInt();
						map.Decorators[i] = decorator;
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

			//map.MapId = packet.ReadLong();
			//var readBytes = packet.ReadBytes(3);
			////Log.Warn($"{HexDump(readBytes)}");
			//map.UpdateType = packet.ReadByte(); //
			//var bytes = packet.ReadBytes(6);
			////Log.Warn($"{HexDump(bytes)}");

			//map.Direction = packet.ReadByte(); //
			//map.X = packet.ReadByte(); //
			//map.Z = packet.ReadByte(); //

			//if (map.UpdateType == 0x06)
			//{
			//	// Full map
			//	try
			//	{
			//		if (bytes[4] == 1)
			//		{
			//			map.Col = packet.ReadInt();
			//			map.Row = packet.ReadInt(); //

			//			map.XOffset = packet.ReadInt(); //
			//			map.ZOffset = packet.ReadInt(); //

			//			map.Data = packet.ReadBytes(map.Col*map.Row*4);
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

		public override string ToString()
		{
			return $"MapId: {MapId}, UpdateType: {UpdateType}, X: {X}, Z: {Z}, Col: {Col}, Row: {Row}, X-offset: {XOffset}, Z-offset: {ZOffset}, Data: {Data?.Length}";
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}

	public class MapDecorator
	{
		protected int Type { get; set; }
		public byte Rotation { get; set; }
		public byte Icon { get; set; }
		public byte X { get; set; }
		public byte Z { get; set; }
		public string Label { get; set; }
		public uint Color { get; set; }
	}

	public class BlockMapDecorator : MapDecorator
	{
		public BlockCoordinates Coordinates { get; set; }

		public BlockMapDecorator()
		{
			Type = 1;
		}
	}

	public class EntityMapDecorator : MapDecorator
	{
		public long EntityId { get; set; }

		public EntityMapDecorator()
		{
			Type = 0;
		}
	}

	public class MapTrackedObject
	{
		protected int Type { get; set; }
	}

	public class EntityMapTrackedObject : MapTrackedObject
	{
		public long EntityId { get; set; }

		public EntityMapTrackedObject()
		{
			Type = 0;
		}
	}

	public class BlockMapTrackedObject : MapTrackedObject
	{
		public BlockCoordinates Coordinates { get; set; }

		public BlockMapTrackedObject()
		{
			Type = 1;
		}
	}
}