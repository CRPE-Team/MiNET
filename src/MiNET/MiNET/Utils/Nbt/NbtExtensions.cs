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

using System.IO;
using fNbt;
using MiNET.Utils.Cryptography;

namespace MiNET.Utils.Nbt
{
	public static class NbtExtensions
	{
		public static uint GetFnvHash(this NbtCompound compound, NbtFlavor flavor, NbtCompression compression = NbtCompression.None)
		{
			compound.Name ??= string.Empty;
			return GetFnvHash(new NbtFile() { RootTag = compound, Flavor = flavor }, compression);
		}

		public static uint GetFnvHash(this NbtFile file, NbtCompression compression = NbtCompression.None)
		{
			var buffer = file.SaveToBuffer(compression);

			return Fnv.ComputeHash(buffer);
		}

		public static byte[] ToBytes(this NbtCompound compound, NbtFlavor flavor = null, NbtCompression compression = NbtCompression.None)
		{
			compound.Name ??= string.Empty;
			return new NbtFile() { RootTag = compound, Flavor = flavor ?? NbtFlavor.Bedrock }.SaveToBuffer(compression);
		}

		public static void Write(Stream stream, NbtCompound compound, NbtFlavor flavor = null)
		{
			compound.Name ??= string.Empty;
			Write(stream, new NbtFile() { RootTag = compound, Flavor = flavor ?? NbtFlavor.Bedrock });
		}

		public static void Write(Stream stream, NbtFile file)
		{
			file.SaveToStream(stream, NbtCompression.None);
		}

		public static NbtCompound ReadNbtCompound(Stream stream, NbtFlavor flavor = null)
		{
			return ReadNbt(stream, flavor).RootTag;
		}

		public static NbtCompound ReadNbtCompound(byte[] buffer, NbtFlavor flavor = null)
		{
			return ReadNbt(buffer, flavor).RootTag;
		}

		public static NbtFile ReadNbt(byte[] buffer, NbtFlavor flavor = null)
		{
			var file = new NbtFile() { Flavor = flavor ?? NbtFlavor.Bedrock };

			file.LoadFromBuffer(buffer, 0, buffer.Length, NbtCompression.None);

			return file;
		}

		public static NbtFile ReadNbt(Stream stream, NbtFlavor flavor = null)
		{
			var file = new NbtFile() { Flavor = flavor ?? NbtFlavor.Bedrock };

			file.LoadFromStream(stream, NbtCompression.None);

			return file;
		}
	}
}