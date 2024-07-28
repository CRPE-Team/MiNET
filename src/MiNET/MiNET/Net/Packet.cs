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
// All portions of the code written by Niclas Olofsson are Copyright (c) 2014-2020 Niclas Olofsson.
// All Rights Reserved.

#endregion

using System;
using System.Buffers.Binary;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
using fNbt;
using log4net;
using log4net.Appender;
using Microsoft.IO;
using MiNET.Items;
using MiNET.Net.Crafting;
using MiNET.Net.RakNet;
using MiNET.Utils;
using MiNET.Utils.IO;
using MiNET.Utils.Metadata;
using MiNET.Utils.Nbt;
using MiNET.Utils.Skins;
using MiNET.Utils.Vectors;
using Newtonsoft.Json;

namespace MiNET.Net
{
	public abstract partial class Packet
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Packet));

		private byte[] _encodedMessage;

		[JsonIgnore] public ReliabilityHeader ReliabilityHeader = new ReliabilityHeader();

		[JsonIgnore] public bool ForceClear;
		[JsonIgnore] public bool NoBatch { get; set; }

		[JsonIgnore] public int Id;
		[JsonIgnore] public bool IsMcpe;

		protected MemoryStreamReader _reader; // new construct for reading
		protected private Stream _buffer;
		private BinaryWriter _writer;

		[JsonIgnore] public ReadOnlyMemory<byte> Bytes { get; private set; }
		[JsonIgnore] public Stopwatch Timer { get; } = Stopwatch.StartNew();

		public Packet()
		{
			Timer.Start();
		}
		
		public void Write(sbyte value)
		{
			_writer.Write(value);
		}

		public sbyte ReadSByte()
		{
			return (sbyte)_reader.ReadByte();
		}
		
		public void Write(byte value)
		{
			_writer.Write(value);
		}

		public byte ReadByte()
		{
			return (byte) _reader.ReadByte();
		}

		public void Write(bool value)
		{
			Write((byte) (value ? 1 : 0));
		}

		public bool ReadBool()
		{
			return _reader.ReadByte() != 0;
		}

		public void Write(Memory<byte> value)
		{
			Write((ReadOnlyMemory<byte>) value);
		}

		public void Write(ReadOnlyMemory<byte> value)
		{
			if (value.IsEmpty)
			{
				Log.Warn("Trying to write empty Memory<byte>");
				return;
			}
			_writer.Write(value.Span);
		}

		public void Write(byte[] value)
		{
			if (value == null)
			{
				Log.Warn("Trying to write null byte[]");
				return;
			}

			_writer.Write(value);
		}

		public ReadOnlyMemory<byte> Slice(int count)
		{
			return _reader.Read(count);
		}

		public ReadOnlyMemory<byte> ReadReadOnlyMemory(int count, bool slurp = false)
		{
			if (!slurp && count == 0) return Memory<byte>.Empty;

			if (count == 0)
			{
				count = (int) (_reader.Length - _reader.Position);
			}

			ReadOnlyMemory<byte> readBytes = _reader.Read(count);
			if (readBytes.Length != count) throw new ArgumentOutOfRangeException($"Expected {count} bytes, only read {readBytes.Length}.");
			return readBytes;
		}

		public byte[] ReadBytes(int count, bool slurp = false)
		{
			if (!slurp && count == 0) return new byte[0];

			if (count == 0)
			{
				count = (int) (_reader.Length - _reader.Position);
			}

			ReadOnlyMemory<byte> readBytes = _reader.Read(count);
			if (readBytes.Length != count) throw new ArgumentOutOfRangeException($"Expected {count} bytes, only read {readBytes.Length}.");
			return readBytes.ToArray(); //TODO: Replace with ReadOnlyMemory<byte> return
		}

		public void WriteByteArray(byte[] value)
		{
			if (value == null || value.Length == 0)
			{
				WriteLength(0);
				return;
			}

			WriteLength(value.Length);
			_writer.Write(value, 0, value.Length);
		}

		public byte[] ReadByteArray(bool slurp = false)
		{
			var len = ReadLength();
			var bytes = ReadBytes(len, slurp);
			return bytes;
		}

		public void Write(ulong[] value)
		{
			if (value == null || value.Length == 0)
			{
				WriteLength(0);
				return;
			}

			WriteLength(value.Length);

			for (int i = 0; i < value.Length; i++)
			{
				ulong val = value[i];
				Write(val);
			}
		}

		public ulong[] ReadUlongs(bool slurp = false)
		{
			var len = ReadLength();
			var ulongs = new ulong[len];
			for (int i = 0; i < ulongs.Length; i++)
			{
				ulongs[i] = ReadUlong();
			}
			return ulongs;
		}

		public void Write(short value, bool bigEndian = false)
		{
			if (bigEndian) _writer.Write(BinaryPrimitives.ReverseEndianness(value));
			else _writer.Write(value);
		}

		public short ReadShort(bool bigEndian = false)
		{
			if (_reader.Position == _reader.Length) return 0;

			if (bigEndian) return BinaryPrimitives.ReverseEndianness(_reader.ReadInt16());

			return _reader.ReadInt16();
		}

		public void Write(ushort value, bool bigEndian = false)
		{
			if (bigEndian) _writer.Write(BinaryPrimitives.ReverseEndianness(value));
			else _writer.Write(value);
		}

		public ushort ReadUshort(bool bigEndian = false)
		{
			if (_reader.Position == _reader.Length) return 0;

			if (bigEndian) return BinaryPrimitives.ReverseEndianness(_reader.ReadUInt16());

			return _reader.ReadUInt16();
		}

		public void WriteBe(short value)
		{
			_writer.Write(BinaryPrimitives.ReverseEndianness(value));
		}

		public short ReadShortBe()
		{
			if (_reader.Position == _reader.Length) return 0;

			return BinaryPrimitives.ReverseEndianness(_reader.ReadInt16());
		}

		public void Write(Int24 value)
		{
			_writer.Write(value.GetBytes());
		}

		public Int24 ReadLittle()
		{
			return new Int24(_reader.Read(3).Span);
		}

		public void Write(int value, bool bigEndian = false)
		{
			if (bigEndian) _writer.Write(BinaryPrimitives.ReverseEndianness(value));
			else _writer.Write(value);
		}

		public int ReadInt(bool bigEndian = false)
		{
			if (bigEndian) return BinaryPrimitives.ReverseEndianness(_reader.ReadInt32());

			return _reader.ReadInt32();
		}

		public void WriteBe(int value)
		{
			_writer.Write(BinaryPrimitives.ReverseEndianness(value));
		}

		public int ReadIntBe()
		{
			return BinaryPrimitives.ReverseEndianness(_reader.ReadInt32());
		}

		public void Write(uint value)
		{
			_writer.Write(value);
		}

		public uint ReadUint()
		{
			return _reader.ReadUInt32();
		}


		public void WriteVarInt(int value)
		{
			VarInt.WriteInt32(_buffer, value);
		}

		public int ReadVarInt()
		{
			return VarInt.ReadInt32(_reader);
		}

		public void WriteSignedVarInt(int value)
		{
			VarInt.WriteSInt32(_buffer, value);
		}

		public int ReadSignedVarInt()
		{
			return VarInt.ReadSInt32(_reader);
		}

		public void WriteUnsignedVarInt(uint value)
		{
			VarInt.WriteUInt32(_buffer, value);
		}

		public uint ReadUnsignedVarInt()
		{
			return VarInt.ReadUInt32(_reader);
		}

		public int ReadLength()
		{
			return (int) VarInt.ReadUInt32(_reader);
		}

		public void WriteLength(int value)
		{
			VarInt.WriteUInt32(_buffer, (uint) value);
		}

		public void WriteVarLong(long value)
		{
			VarInt.WriteInt64(_buffer, value);
		}

		public long ReadVarLong()
		{
			return VarInt.ReadInt64(_reader);
		}

		public void WriteEntityId(long value)
		{
			WriteSignedVarLong(value);
		}

		public void WriteSignedVarLong(long value)
		{
			VarInt.WriteSInt64(_buffer, value);
		}

		public long ReadSignedVarLong()
		{
			return VarInt.ReadSInt64(_reader);
		}

		public void WriteRuntimeEntityId(long value)
		{
			WriteUnsignedVarLong(value);
		}

		public long ReadRuntimeEntityId()
		{
			return ReadUnsignedVarLong();
		}

		public void WriteUnsignedVarLong(long value)
		{
			// Need to fix this to ulong later
			VarInt.WriteUInt64(_buffer, (ulong) value);
		}

		public long ReadUnsignedVarLong()
		{
			// Need to fix this to ulong later
			return (long) VarInt.ReadUInt64(_reader);
		}

		public void Write(long value)
		{
			_writer.Write(BinaryPrimitives.ReverseEndianness(value));
		}

		public long ReadLong()
		{
			return BinaryPrimitives.ReverseEndianness(_reader.ReadInt64());
		}

		public void Write(ulong value)
		{
			_writer.Write(value);
		}

		public ulong ReadUlong()
		{
			return _reader.ReadUInt64();
		}

		public void Write(float value)
		{
			_writer.Write(value);

			//byte[] bytes = BitConverter.GetBytes(value);
			//_writer.Write(bytes[3]);
			//_writer.Write(bytes[2]);
			//_writer.Write(bytes[1]);
			//_writer.Write(bytes[0]);
		}

		public float ReadFloat()
		{
			//byte[] buffer = _reader.ReadBytes(4);
			//return BitConverter.ToSingle(new[] {buffer[3], buffer[2], buffer[1], buffer[0]}, 0);
			return _reader.ReadSingle();
		}

		public void Write(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				WriteLength(0);
				return;
			}

			byte[] bytes = Encoding.UTF8.GetBytes(value);

			WriteLength(bytes.Length);
			Write(bytes);
		}

		public string ReadString()
		{
			if (_reader.Position == _reader.Length) return string.Empty;
			int len = ReadLength();
			if (len <= 0) return string.Empty;
			return Encoding.UTF8.GetString(ReadBytes(len));
		}

		public void WriteFixedString(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				Write((short) 0, true);
				return;
			}

			byte[] bytes = Encoding.UTF8.GetBytes(value);

			Write((short) bytes.Length, true);
			Write(bytes);
		}

		public string ReadFixedString()
		{
			if (_reader.Position == _reader.Length) return string.Empty;
			short len = ReadShort(true);
			if (len <= 0) return string.Empty;
			return Encoding.UTF8.GetString(_reader.Read(len).Span);
		}

		public void Write(Vector2 vec)
		{
			Write(vec.X);
			Write(vec.Y);
		}

		public Vector2 ReadVector2()
		{
			return new Vector2(ReadFloat(), ReadFloat());
		}

		public void Write(Vector3 vec)
		{
			Write(vec.X);
			Write(vec.Y);
			Write(vec.Z);
		}

		public Vector3 ReadVector3()
		{
			return new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
		}

		public long ReadEntityId()
		{
			return ReadSignedVarLong();
		}

		public void Write<T>(T dataObject) where T : IPacketDataObject
		{
			if (dataObject == null && typeof(T).IsAssignableTo(typeof(System.Collections.IEnumerable)))
			{
				WriteLength(0);
				return;
			}

			dataObject.Write(this);
		}

		public void Write<T>(IEnumerable<T> dataObjects) where T : IPacketDataObject
		{
			if (dataObjects == null)
			{
				WriteLength(0);
				return;
			}

			WriteLength(dataObjects.Count());
			foreach(var dataObject in dataObjects)
			{
				Write(dataObject);
			}
		}


		public void Write(BlockCoordinates coord)
		{
			WriteSignedVarInt(coord.X);
			WriteUnsignedVarInt((uint) coord.Y);
			WriteSignedVarInt(coord.Z);
		}

		public BlockCoordinates ReadBlockCoordinates()
		{
			return new BlockCoordinates(ReadSignedVarInt(), (int) ReadUnsignedVarInt(), ReadSignedVarInt());
		}

		public PlayerRecords ReadPlayerRecords()
		{
			return PlayerRecords.Read(this);
		}

		public void Write(PlayerLocation location)
		{
			Write(location.X);
			Write(location.Y);
			Write(location.Z);
			var d = 256f / 360f;
			Write((byte) Math.Round(location.Pitch * d)); // 256/360
			Write((byte) Math.Round(location.HeadYaw * d)); // 256/360
			Write((byte) Math.Round(location.Yaw * d)); // 256/360
		}

		public PlayerLocation ReadPlayerLocation()
		{
			PlayerLocation location = new PlayerLocation();
			location.X = ReadFloat();
			location.Y = ReadFloat();
			location.Z = ReadFloat();
			location.Pitch = ReadByte() * 1f / 0.71f;
			location.HeadYaw = ReadByte() * 1f / 0.71f;
			location.Yaw = ReadByte() * 1f / 0.71f;

			return location;
		}

		public void Write(IPEndPoint endpoint)
		{
			if (endpoint.AddressFamily == AddressFamily.InterNetwork)
			{
				Write((byte) 4);
				var parts = endpoint.Address.ToString().Split('.');
				foreach (var part in parts)
				{
					Write((byte) ~byte.Parse(part));
				}
				Write((short) endpoint.Port, true);
			}
		}


//typedef struct sockaddr_in6
//{
//	ADDRESS_FAMILY sin6_family; // AF_INET6.
//	USHORT sin6_port;           // Transport level port number.
//	ULONG sin6_flowinfo;       // IPv6 flow information.
//	IN6_ADDR sin6_addr;         // IPv6 address.
//	union {
//ULONG sin6_scope_id;     // Set of interfaces for a scope.
//	SCOPE_ID sin6_scope_struct;
//};
//}
//SOCKADDR_IN6_LH, * PSOCKADDR_IN6_LH, FAR * LPSOCKADDR_IN6_LH;

		public IPEndPoint ReadIPEndPoint()
		{
			byte ipVersion = ReadByte();

			IPAddress address = IPAddress.Any;
			int port = 0;

			if (ipVersion == 4)
			{
				string ipAddress = $"{(byte) ~ReadByte()}.{(byte) ~ReadByte()}.{(byte) ~ReadByte()}.{(byte) ~ReadByte()}";
				address = IPAddress.Parse(ipAddress);
				port = (ushort) ReadShort(true);
			}
			else if (ipVersion == 6)
			{
				ReadShort(); // Address family
				port = (ushort) ReadShort(true); // Port
				ReadLong(); // Flow info
				var addressBytes = ReadBytes(16);
				address = new IPAddress(addressBytes);
			}
			else
			{
				Log.Error($"Wrong IP version. Expected IPv4 or IPv6 but was IPv{ipVersion}");
			}

			return new IPEndPoint(address, port);
		}

		public void Write(IPEndPoint[] endpoints)
		{
			foreach (var endpoint in endpoints)
			{
				Write(endpoint);
			}
		}

		public IPEndPoint[] ReadIPEndPoints(int count)
		{
			if (count == 20 && _reader.Length < 120) count = 10;
			var endPoints = new IPEndPoint[count];
			for (int i = 0; i < endPoints.Length; i++)
			{
				endPoints[i] = ReadIPEndPoint();
			}

			return endPoints;
		}

		public void Write(UUID uuid)
		{
			if (uuid == null) throw new Exception("Expected UUID, required");
			Write(uuid.GetBytes());
		}

		public UUID ReadUUID()
		{
			UUID uuid = new UUID(ReadBytes(16));
			return uuid;
		}

		public void Write(Nbt nbt)
		{
			Write(nbt, _writer.BaseStream, nbt.NbtFile.UseVarInt || this is McpeBlockEntityData || this is McpeUpdateEquipment);
		}

		public static void Write(Nbt nbt, Stream stream, bool useVarInt)
		{
			NbtFile file = nbt.NbtFile;
			file.BigEndian = false;
			file.UseVarInt = useVarInt;

			byte[] saveToBuffer = file.SaveToBuffer(NbtCompression.None);
			stream.Write(saveToBuffer, 0, saveToBuffer.Length);
		}


		public Nbt ReadNbt()
		{
			return ReadNbt(_reader);
		}

		public static Nbt ReadNbt(Stream stream, bool allowAlternativeRootTag = true, bool useVarInt = true)
		{
			Nbt nbt = new Nbt();
			NbtFile nbtFile = new NbtFile();
			nbtFile.BigEndian = false;
			nbtFile.UseVarInt = useVarInt;
			nbtFile.AllowAlternativeRootTag = allowAlternativeRootTag;

			nbt.NbtFile = nbtFile;
			nbtFile.LoadFromStream(stream, NbtCompression.None);

			return nbt;
		}

		public static NbtCompound ReadNbtCompound(Stream stream, bool useVarInt = false)
		{
			NbtFile file = new NbtFile();
			file.BigEndian = false;
			file.UseVarInt = useVarInt;
			file.AllowAlternativeRootTag = false;

			file.LoadFromStream(stream, NbtCompression.None);

			return (NbtCompound) file.RootTag;
		}

		public void Write(MetadataInts metadata)
		{
			if (metadata == null)
			{
				WriteUnsignedVarInt(0);
				return;
			}

			WriteUnsignedVarInt((uint) metadata.Count);

			for (byte i = 0; i < metadata.Count; i++)
			{
				MetadataInt slot = metadata[i] as MetadataInt;
				if (slot != null)
				{
					WriteUnsignedVarInt((uint) slot.Value);
				}
			}
		}

		public MetadataInts ReadMetadataInts()
		{
			MetadataInts metadata = new MetadataInts();
			uint count = ReadUnsignedVarInt();

			for (byte i = 0; i < count; i++)
			{
				metadata[i] = new MetadataInt((int) ReadUnsignedVarInt());
			}

			return metadata;
		}

		public CreativeItemStacks ReadCreativeItemStacks()
		{
			return CreativeItemStacks.Read(this);
		}

		public ItemStacks ReadItemStacks()
		{
			return ItemStacks.Read(this);
		}

		public Transaction ReadTransaction()
		{
			return Transaction.Read(this);
		}

		public ItemStackRequests ReadItemStackRequests()
		{
			return ItemStackRequests.Read(this);
		}

		public ItemStackResponses ReadItemStackResponses()
		{
			return ItemStackResponses.Read(this);
		}
		
		public ItemComponentList ReadItemComponentList()
		{
			return ItemComponentList.Read(this);
		}

		public EnchantOptions ReadEnchantOptions()
		{
			return EnchantOptions.Read(this);
		}

		public AnimationKey[] ReadAnimationKeys()
		{
			var count = ReadLength();
			var keys = new AnimationKey[count];
			for (int i = 0; i < count; i++)
			{
				keys[i] = AnimationKey.Read(this);
			}

			return keys;
		}

		private const int ShieldId = 355;
		public void Write(Item stack, bool writeUniqueId = true)
		{
			if (stack == null || stack is ItemAir)
			{
				WriteSignedVarInt(0);
				return;
			}

			WriteSignedVarInt(stack.RuntimeId);

			Write((short) stack.Count);
			WriteUnsignedVarInt((uint) stack.Metadata);

			if (writeUniqueId)
			{
				Write(stack.UniqueId != 0);

				if (stack.UniqueId != 0)
				{
					WriteVarInt(stack.UniqueId);
				}
			}

			WriteSignedVarInt(stack.BlockRuntimeId);

			byte[] extraData = null;
			//Write extra data
			using (var ms = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(ms, Encoding.UTF8, true))
				{
					if (stack.ExtraData != null)
					{
						binaryWriter.Write((ushort) 0xffff);
						binaryWriter.Write((byte)1);
						var nbtData = GetNbtData(stack.ExtraData, false);
						binaryWriter.Write(nbtData);
					}
					else
					{
						binaryWriter.Write((short) 0);
					}

					binaryWriter.Write(0); //Write Int
					binaryWriter.Write(0); //Write Int

					if (stack is ItemShield)
					{
						binaryWriter.Write((long) 0);
					}
				}

				extraData = ms.ToArray();
			}
			
			WriteLength(extraData.Length);
			Write(extraData);
		}

		public Item ReadItem(bool readUniqueId = true)
		{
			int runtimeId = ReadSignedVarInt();
			if (runtimeId == 0)
			{
				return new ItemAir();
			}

			var count = ReadShort();
			var metadata = ReadUnsignedVarInt();

			Item stack = ItemFactory.GetItem(runtimeId, (short) metadata, count);

			if (readUniqueId)
			{
				if (ReadBool()) stack.UniqueId = ReadVarInt();
			}

			var blockRuntimeId = ReadSignedVarInt();

			int length = ReadLength();
			var data = ReadBytes(length);

			using (MemoryStream ms = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(ms))
				{
					ushort nbtLen = binaryReader.ReadUInt16();
					if (nbtLen == 0xffff)
					{
						byte version = binaryReader.ReadByte();

						if (version != 1)
						{
							throw new Exception($"Fringe nbt version when reading item extra NBT: {version}");
						}

						var beforeRead = ms.Position;
						stack.ExtraData = ReadNbtCompound(ms, false);
						var afterRead = ms.Position;
						var nbtCompoundLength = afterRead - beforeRead;
					}
					else if (nbtLen > 0)
					{
						throw new Exception($"Fringe nbt length when reading item extra NBT: {nbtLen}");
					}
					
					int canPlace = binaryReader.ReadInt32();
					for (int i = 0; i < canPlace; i++)
					{
						var l = binaryReader.ReadInt16();
						binaryReader.ReadBytes(l);
					}
					int canBreak = binaryReader.ReadInt32();
					for (int i = 0; i < canBreak; i++)
					{
						var l = binaryReader.ReadInt16();
						binaryReader.ReadBytes(l);
					}

					if (stack.BlockRuntimeId == ShieldId) // shield
					{
						binaryReader.ReadInt64(); // something about tick, crap code
					}
				}
			}
			return stack;
		}


		public static byte[] GetNbtData(NbtCompound nbtCompound, bool useVarInt = true)
		{
			nbtCompound.Name = string.Empty;
			var file = new NbtFile(nbtCompound);
			file.BigEndian = false;
			file.UseVarInt = useVarInt;

			return file.SaveToBuffer(NbtCompression.None);
		}

		public void Write(MetadataDictionary metadata)
		{
			if (metadata != null)
			{
				metadata.WriteTo(_writer);
			}
		}

		public MetadataDictionary ReadMetadataDictionary()
		{
			//_buffer.Position = _reader.Position;
			var reader = new BinaryReader(_reader);
			var dictionary = MetadataDictionary.FromStream(reader);
			//_reader.Position = (int) _buffer.Position;
			return dictionary;
		}

		public AttributeModifiers ReadAttributeModifiers()
		{
			return AttributeModifiers.Read(this);
		}

		public PlayerAttributes ReadPlayerAttributes()
		{
			return PlayerAttributes.Read(this);
		}

		public void WriteGameRules(GameRules gameRules)
		{
			if (gameRules == null)
			{
				WriteVarInt(0);
				return;
			}

			gameRules.Write(this);
		}

		public GameRules ReadGameRules()
		{
			return GameRules.Read(this);
		}

		public EntityAttributes ReadEntityAttributes()
		{
			return EntityAttributes.Read(this);
		}

		public ItemStates ReadItemStates()
		{
			return ItemStates.Read(this);
		}

		public BlockPalette ReadBlockPalette()
		{
			var  result = new BlockPalette();
			var count  = ReadUnsignedVarInt();

			for (int runtimeId = 0; runtimeId < count; runtimeId++)
			{
				var record = new PaletteBlockStateContainer();
				record.RuntimeId = runtimeId;
				record.Id = ReadString();

				var nbt = ReadNbt(_reader);
				var rootTag = nbt.NbtFile.RootTag;

				foreach (var state in GetBlockStates(rootTag))
				{
					record.AddState(state);
				}
			}

			return result;
		}
		
		private IEnumerable<IBlockState> GetBlockStates(NbtTag tag)
		{
			switch (tag.TagType)
			{
				case NbtTagType.List:
				{
					foreach (var state in GetBlockStatesFromList((NbtList) tag))
						yield return state;
				} break;

				case NbtTagType.Compound:
				{
					foreach (var state in GetBlockStatesFromCompound((NbtCompound) tag))
						yield return state;
				} break;

				default:
				{
					if (TryGetStateFromTag(tag, out var state))
						yield return state;
				} break;
			}
		}

		private IEnumerable<IBlockState> GetBlockStatesFromCompound(NbtCompound list)
		{
			if (list.TryGet("states", out NbtTag states))
			{
				foreach (var state in GetBlockStates(states))
				{
					yield return state;
				}
			}
		}
		
		
		private IEnumerable<IBlockState> GetBlockStatesFromList(NbtList list)
		{
			foreach (NbtTag tag in list)
			{
				if (TryGetStateFromTag(tag, out var state))
				{
					yield return state;
				}
				else
				{
					foreach (var s in GetBlockStates(tag))
					{
						yield return s;
					}
				}
			}
		}

		private bool TryGetStateFromTag(NbtTag tag, out IBlockState state)
		{
			switch (tag.TagType)
			{
				case NbtTagType.Byte:
					state = new BlockStateByte()
					{
						Name = tag.Name, Value = tag.ByteValue
					};
					return true;

				case NbtTagType.Int:
					state = new BlockStateInt()
					{
						Name = tag.Name, Value = tag.IntValue
					};
					return true;

				case NbtTagType.String:
					state = new BlockStateString()
					{
						Name = tag.Name, Value = tag.StringValue
					};
					return true;
			}

			state = null;

			return false;
		}

		public void Write(BlockPalette palette)
		{
			if(palette == null)
			{
				WriteUnsignedVarInt(0);
				return;
			}
			WriteUnsignedVarInt((uint)palette.Count);
			foreach (var record in palette)
			{
				Write(record.Id);
				Write(record.StatesCacheNbt);
			}
		}
		
		public AbilityLayers ReadAbilityLayers()
		{
			return AbilityLayers.Read(this);
		}

		public EntityLinks ReadEntityLinks()
		{
			return EntityLinks.Read(this);
		}

		public void Write(TexturePackInfos packInfos)
		{
			if (packInfos == null || packInfos.Count == 0)
			{
				Write((short) 0);
				return;
			}

			packInfos.Write(this);
		}

		public TexturePackInfos ReadTexturePackInfos()
		{
			return TexturePackInfos.Read(this);
		}

		public CdnUrls ReadCdnUrls()
		{
			return CdnUrls.Read(this);
		}

		public void Write(ResourcePackInfos packInfos)
		{
			if (packInfos == null || packInfos.Count == 0)
			{
				Write((short) 0); // LE
				//WriteVarInt(0);
				return;
			}

			packInfos.Write(this);
		}

		public ResourcePackInfos ReadResourcePackInfos()
		{
			return ResourcePackInfos.Read(this);
		}

		public ResourcePackIdVersions ReadResourcePackIdVersions()
		{
			return ResourcePackIdVersions.Read(this);
		}

		public void Write(ResourcePackIds ids)
		{
			if (ids == null)
			{
				Write((short) 0);
				return;
			}

			ids.Write(this);
		}

		public ResourcePackIds ReadResourcePackIds()
		{
			return ResourcePackIds.Read(this);
		}

		public void Write(Skin skin)
		{
			Write(skin.SkinId);
			Write(skin.PlayFabId);
			Write(skin.ResourcePatch);
			Write(skin.Width);
			Write(skin.Height);
			WriteByteArray(skin.Data);

			if (skin.Animations?.Count > 0)
			{
				Write(skin.Animations.Count);
				foreach (Animation animation in skin.Animations)
				{
					Write(animation.ImageWidth);
					Write(animation.ImageHeight);
					WriteByteArray(animation.Image);
					Write(animation.Type);
					Write(animation.FrameCount);
					Write(animation.Expression);
				}
			}
			else
			{
				Write(0);
			}

			Write(skin.Cape.ImageWidth);
			Write(skin.Cape.ImageHeight);
			WriteByteArray(skin.Cape.Data);
			Write(skin.GeometryData);
			Write(skin.GeometryDataVersion);
			Write(skin.AnimationData);

			Write(skin.Cape.Id);
			Write(skin.SkinId + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()); // some unique skin id
			Write(skin.ArmSize);
			Write(skin.SkinColor);
			Write(skin.PersonaPieces.Count);
			foreach (PersonaPiece piece in skin.PersonaPieces)
			{
				Write(piece.PieceId);
				Write(piece.PieceType);
				Write(piece.PackId);
				Write(piece.IsDefaultPiece);
				Write(piece.ProductId);
			}
			Write(skin.SkinPieces.Count);
			foreach (SkinPiece skinPiece in skin.SkinPieces)
			{
				Write(skinPiece.PieceType);
				Write(skinPiece.Colors.Count);
				foreach (string color in skinPiece.Colors)
				{
					Write(color);
				}
			}
			
			Write(skin.IsPremiumSkin);
			Write(skin.IsPersonaSkin);
			Write(skin.Cape.OnClassicSkin);
			Write(skin.IsPrimaryUser);
			Write(skin.isOverride);
		}

		public Skin ReadSkin()
		{
			Skin skin = new Skin();

			skin.SkinId = ReadString();
			skin.PlayFabId = ReadString();
			skin.ResourcePatch = ReadString();
			skin.Width = ReadInt();
			skin.Height = ReadInt();
			skin.Data = ReadByteArray(false);

			int animationCount = ReadInt();
			for (int i = 0; i < animationCount; i++)
			{
				skin.Animations.Add(
					new Animation()
					{
						ImageWidth = ReadInt(),
						ImageHeight = ReadInt(),
						Image = ReadByteArray(false),
						Type = ReadInt(),
						FrameCount = ReadFloat(),
						Expression = ReadInt() 
					}
				);
			}

			skin.Cape.ImageWidth = ReadInt();
			skin.Cape.ImageHeight = ReadInt();
			skin.Cape.Data = ReadByteArray(false);
			skin.GeometryData = ReadString();
			skin.GeometryDataVersion = ReadString();
			skin.AnimationData = ReadString();

			skin.Cape.Id = ReadString();
			ReadString(); // fullSkinId
			skin.ArmSize = ReadString();
			skin.SkinColor = ReadString();
			int personaPieceCount = ReadInt();
			for (int i = 0; i < personaPieceCount; i++)
			{
				var p = new PersonaPiece();
				p.PieceId = ReadString();
				p.PieceType = ReadString();
				p.PackId = ReadString();
				p.IsDefaultPiece = ReadBool();
				p.ProductId = ReadString();
				skin.PersonaPieces.Add(p);
			}

			int skinPieceCount = ReadInt();
			for (int i = 0; i < skinPieceCount; i++)
			{
				var piece = new SkinPiece();
				piece.PieceType = ReadString();
				int colorAmount = ReadInt();
				for (int i2 = 0; i2 < colorAmount; i2++)
				{
					piece.Colors.Add(ReadString());
				}
				skin.SkinPieces.Add(piece);
			}
			
			skin.IsPremiumSkin = ReadBool();
			skin.IsPersonaSkin = ReadBool();
			skin.Cape.OnClassicSkin = ReadBool();
			skin.IsPrimaryUser = ReadBool();
			skin.isOverride = ReadBool();
			//Log.Debug($"SkinId={skin.SkinId}");
			//Log.Debug($"SkinData lenght={skin.Data.Length}");
			//Log.Debug($"CapeData lenght={skin.Cape.Data.Length}");
			//Log.Debug("\n" + HexDump(skin.Cape.Data));
			//Log.Debug($"SkinGeometryName={skin.GeometryName}");
			//Log.Debug($"SkinGeometry lenght={skin.GeometryData.Length}");

			return skin;
		}

		public Recipes ReadRecipes()
		{
			return Recipes.Read(this);
		}

		public PotionContainerChangeRecipe[] ReadPotionContainerChangeRecipes()
		{
			var count = (int) ReadUnsignedVarInt();
			var recipes = new PotionContainerChangeRecipe[count];
			for (int i = 0; i < recipes.Length; i++)
			{
				recipes[i] = PotionContainerChangeRecipe.Read(this);
			}

			return recipes;
		}

		public MaterialReducerRecipe[] ReadMaterialReducerRecipes()
		{
			var count = (int) ReadUnsignedVarInt();
			var recipes = new MaterialReducerRecipe[count];
			for (int i = 0; i < recipes.Length; i++)
			{
				recipes[i] = MaterialReducerRecipe.Read(this);
			}

			return recipes;
		}

		public PotionTypeRecipe[] ReadPotionTypeRecipes()
		{
			var count = (int) ReadUnsignedVarInt();
			var recipes = new PotionTypeRecipe[count];
			for (int i = 0; i < recipes.Length; i++)
			{
				recipes[i] = PotionTypeRecipe.Read(this);
			}

			return recipes;
		}

		public MapInfo ReadMapInfo()
		{
			return MapInfo.Read(this);
		}

		public ScoreEntries ReadScoreEntries()
		{
			return ScoreEntries.Read(this);
		}

		public ScoreboardIdentityEntries ReadScoreboardIdentityEntries()
		{
			return ScoreboardIdentityEntries.Read(this);
		}

		public Experiments ReadExperiments()
		{
			return Experiments.Read(this);
		}

		public void Write(Experiments experiments)
		{
			if (experiments == null)
			{
				Write(0);
				return;
			}

			experiments.Write(this);
		}

		public void Write(EducationUriResource resource)
		{
			Write(resource.ButtonName);
			Write(resource.LinkUri);
		}
		
		public EducationUriResource ReadEducationUriResource()
		{
			var name = ReadString();
			var uri = ReadString();

			return new EducationUriResource(name, uri);
		}

		public UpdateSubChunkBlocksPacketEntry ReadUpdateSubChunkBlocksPacketEntry()
		{
			return UpdateSubChunkBlocksPacketEntry.Read(this);
		}

		public UpdateSubChunkBlocksPacketEntry[] ReadUpdateSubChunkBlocksPacketEntrys()
		{
			var count = ReadLength();
			var entries = new UpdateSubChunkBlocksPacketEntry[(int) count];

			for (int i = 0; i < entries.Length; i++)
			{
				entries[i] = UpdateSubChunkBlocksPacketEntry.Read(this);
			}

			return entries;
		}

		public void Write(HeightMapData data)
		{
			if (data == null)
			{
				Write((byte) SubChunkPacketHeightMapType.NoData);

				return;
			}

			if (data.IsAllTooHigh)
			{
				Write((byte) SubChunkPacketHeightMapType.AllTooHigh);

				return;
			}

			if (data.IsAllTooLow)
			{
				Write((byte) SubChunkPacketHeightMapType.AllTooLow);

				return;
			}
			
			Write((byte) SubChunkPacketHeightMapType.Data);

			for (int i = 0; i < data.Heights.Length; i++)
			{
				Write((byte) data.Heights[i]);
			}
		}

		public HeightMapData ReadHeightMapData()
		{
			SubChunkPacketHeightMapType type = (SubChunkPacketHeightMapType) ReadByte();

			if (type != SubChunkPacketHeightMapType.Data)
				return null;

			short[] heights = new short[256];

			for (int i = 0; i < heights.Length; i++)
			{
				heights[i] = (short) ReadByte();
			}

			return new HeightMapData(heights);
		}

		public void Write(SubChunkPositionOffset offset)
		{
			Write(offset.XOffset);
			Write(offset.YOffset);
			Write(offset.ZOffset);
		}

		public SubChunkPositionOffset ReadSubChunkPositionOffset()
		{
			SubChunkPositionOffset result = new SubChunkPositionOffset();
			result.XOffset = ReadSByte();
			result.YOffset = ReadSByte();
			result.ZOffset = ReadSByte();
			return result;
		}

		public void Write(SubChunkPositionOffset[] offsets)
		{
			Write(offsets.Length);

			foreach (var offset in offsets)
			{
				Write(offset);
			}
		}
		
		public SubChunkPositionOffset[] ReadSubChunkPositionOffsets()
		{
			var count = ReadInt();
			SubChunkPositionOffset[] offsets = new SubChunkPositionOffset[count];

			for (int i = 0; i < offsets.Length; i++)
			{
				offsets[i] = ReadSubChunkPositionOffset();
			}

			return offsets;
		}

		public DimensionData ReadDimensionData()
		{
			return DimensionData.Read(this);
		}
		
		public DimensionDefinitions ReadDimensionDefinitions()
		{
			return DimensionDefinitions.Read(this);
		}

		public void Write(PropertySyncData syncData)
		{
			if (syncData == null)
			{
				WriteUnsignedVarInt(0);
				WriteUnsignedVarInt(0);
				return;
			}

			syncData.Write(this);
		}

		public PropertySyncData ReadPropertySyncData()
		{
			return PropertySyncData.Read(this);
		}

		public bool CanRead()
		{
			return _reader.Position < _reader.Length;
		}

		public void SetEncodedMessage(byte[] encodedMessage)
		{
			_encodedMessage = encodedMessage;
		}

		public virtual void Reset()
		{
			ResetPacket();

			ReliabilityHeader = new ReliabilityHeader();

			NoBatch = false;
			ForceClear = false;

			_encodedMessage = null;
			Bytes = null;
			Timer.Restart();

			_writer?.Close();
			_reader?.Close();
			_buffer?.Close();
			_writer = null;
			_reader = null;
			_buffer = null;
		}

		protected virtual void ResetPacket()
		{
		}

		private object _encodeSync = new object();

		private static RecyclableMemoryStreamManager _streamManager = new RecyclableMemoryStreamManager();
		private static ConcurrentDictionary<int, bool> _isLob = new ConcurrentDictionary<int, bool>();

		public virtual byte[] Encode()
		{
			byte[] cache = _encodedMessage;
			if (cache != null) return cache;

			lock (_encodeSync)
			{
				// This construct to avoid unnecessary contention and double encoding.
				if (_encodedMessage != null) return _encodedMessage;

				// Dynamic pooling. If this packet has been registered as a large object in previous
				// runs, we use the pooled stream for it instead to avoid LOB allocations
				bool isLob = _isLob.ContainsKey(Id);
				_buffer = isLob ? _streamManager.GetStream() : new MemoryStream();
				using (_writer = new BinaryWriter(_buffer, Encoding.UTF8, true))
				{
					EncodePacket();

					_writer.Flush();
					// This WILL allocate LOB. Need to convert this to work with array segment and pool it.
					// then we will use GetBuffer instead.
					// Also remember to move dispose entirely to Reset (dispose) when that happens.
					var buffer = (MemoryStream) _buffer;
					_encodedMessage = buffer.ToArray();
					if (!isLob && _encodedMessage.Length >= 85_000)
					{
						_isLob.TryAdd(Id, true);
						//Log.Warn($"LOB {GetType().Name} {_encodedMessage.Length}, IsLOB={_isLob}");
					}
				}
				_buffer.Dispose();

				_writer = null;
				_buffer = null;

				return _encodedMessage;
			}
		}

		protected virtual void EncodePacket()
		{
			_buffer.Position = 0;
			if (IsMcpe) WriteVarInt(Id);
			else Write((byte)Id);
		}

		[Obsolete("Use decode with ReadOnlyMemory<byte> instead.")]
		public virtual Packet Decode(byte[] buffer)
		{
			return Decode(new ReadOnlyMemory<byte>(buffer));
		}

		public virtual Packet Decode(ReadOnlyMemory<byte> buffer)
		{
			Bytes = buffer;
			_reader = new MemoryStreamReader(buffer);

			DecodePacket();

			if (Log.IsDebugEnabled && _reader.Position != (buffer.Length))
			{
				Log.Warn($"{GetType().Name}: Still have {buffer.Length - _reader.Position} bytes to read!!\n{HexDump(buffer.ToArray())}");
			}

			_reader.Dispose();
			_reader = null;

			return this;
		}

		protected virtual void DecodePacket()
		{
			Id = IsMcpe ? ReadVarInt() : ReadByte();
		}

		public abstract void PutPool();

		public static string HexDump(ReadOnlyMemory<byte> bytes, int bytesPerLine = 16, bool printLineCount = false)
		{
			return HexDump(bytes.Span, bytesPerLine, printLineCount);
		}

		private static string HexDump(ReadOnlySpan<byte> bytes, in int bytesPerLine, in bool printLineCount)
		{
			var sb = new StringBuilder();
			for (int line = 0; line < bytes.Length; line += bytesPerLine)
			{
				byte[] lineBytes = bytes.Slice(line).ToArray().Take(bytesPerLine).ToArray();
				if (printLineCount) sb.AppendFormat("{0:x8} ", line);
				sb.Append(string.Join(" ", lineBytes.Select(b => b.ToString("x2"))
						.ToArray())
					.PadRight(bytesPerLine * 3));
				sb.Append(" ");
				sb.Append(new string(lineBytes.Select(b => b < 32 ? '.' : (char) b)
					.ToArray()));
				sb.AppendLine();
			}
			return sb.ToString();
		}

		public static string ToJson(Packet message)
		{
			var jsonSerializerSettings = new JsonSerializerSettings
			{
				PreserveReferencesHandling = PreserveReferencesHandling.Arrays,
				Formatting = Formatting.Indented,
			};
			jsonSerializerSettings.Converters.Add(new NbtIntConverter());
			jsonSerializerSettings.Converters.Add(new NbtStringConverter());
			jsonSerializerSettings.Converters.Add(new IPAddressConverter());
			jsonSerializerSettings.Converters.Add(new IPEndPointConverter());

			return JsonConvert.SerializeObject(message, jsonSerializerSettings);
		}
	}

	/// Base package class
	public abstract partial class Packet<T> : Packet, IDisposable where T : Packet<T>, new()
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(Packet<T>));

		private static readonly ObjectPool<T> Pool = new ObjectPool<T>(() => new T());

		private bool _isPermanent;
		private bool _isPooled;
		private long _referenceCounter;

		[JsonIgnore]
		public bool IsPooled
		{
			get { return _isPooled; }
		}

		[JsonIgnore]
		public long ReferenceCounter
		{
			get { return _referenceCounter; }
			set { _referenceCounter = value; }
		}


		public T MarkPermanent(bool permanent = true)
		{
			if (!_isPooled) throw new Exception("Tried to make non pooled item permanent");
			_isPermanent = permanent;

			return (T) this;
		}

		public T AddReferences(long numberOfReferences)
		{
			if (_isPermanent) return (T) this;

			if (!_isPooled) throw new Exception("Tried to reference count a non pooled item");
			Interlocked.Add(ref _referenceCounter, numberOfReferences);

			return (T) this;
		}

		public T AddReference(Packet<T> item)
		{
			if (_isPermanent) return (T) this;

			if (!item.IsPooled) throw new Exception("Item template needs to come from a pool");

			Interlocked.Increment(ref item._referenceCounter);
			return (T) item;
		}

		public T MakePoolable(long numberOfReferences = 1)
		{
			_isPooled = true;
			_referenceCounter = numberOfReferences;
			return (T) this;
		}


		public static T CreateObject(long numberOfReferences = 1)
		{
			T item = Pool.GetObject();
			item._isPooled = true;
			item._referenceCounter = numberOfReferences;
			item.Timer.Restart();
			return item;
		}

		// DO NOT UNCOMMENT THIS!!!
		// It will have > 100% performance impact overall.
		//~Packet()
		//{
		//	if (_isPooled)
		//	{
		//		//Log.Error($"Unexpected dispose 0x{Id:x2} {GetType().Name}, IsPooled={_isPooled}, IsPermanent={_isPermanent}, Refs={_referenceCounter}");
		//	}
		//}

		public override void PutPool()
		{
			if (_isPermanent) return;
			if (!IsPooled) return;

			long counter = Interlocked.Decrement(ref _referenceCounter);
			if (counter > 0) return;

			if (counter < 0)
			{
				Log.Error($"Pooling error. Added pooled object too many times. 0x{Id:x2} {GetType().Name}, IsPooled={IsPooled}, IsPooled={_isPermanent}, Refs={_referenceCounter}");
				return;
			}

			Reset();

			_isPooled = false;

			//Pool.PutObject((T) this);
		}

		public void Dispose()
		{
			PutPool();
		}
	}
}