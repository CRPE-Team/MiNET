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

using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Utils.Vectors;

namespace MiNET.Worlds.Tests
{
	[TestClass]
	public class LevelDbProviderTests
	{
		[TestMethod]
		public void RoundtripTest()
		{
			var provider = new LevelDbProvider();
			var flatGenerator = new SuperflatGenerator(Dimension.Overworld);
			flatGenerator.Initialize(null);
			SubChunk chunk = flatGenerator.GenerateChunkColumn(new ChunkCoordinates()).GetSubChunk(0);

			using var stream = new MemoryStream();
			provider.Write(chunk, stream);
			byte[] output = stream.ToArray();

			var parsedChunk = new SubChunk(0, 0, 0);
			provider.ParseSection(parsedChunk, output);

			// Assert
			for (var i = 0; i < chunk.Layers.Count; i++)
			{
				CollectionAssert.AreEqual(chunk.Layers[i].Palette.ToArray(), parsedChunk.Layers[i].Palette.ToArray());
				CollectionAssert.AreEqual(chunk.Layers[i].Data.Data, parsedChunk.Layers[i].Data.Data);
			}
		}
	}
}