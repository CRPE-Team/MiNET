using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Worlds;
using MiNET.Worlds.Utils;

namespace MiNET.Test.Performance
{
	[TestClass]
	[Ignore("only for manual run")]
	public class PalettedContainerPerformanceTests : PerformanceTestBase
	{
		private readonly PalettedContainerData _data = new PalettedContainerData(1, SubChunk.Size);
		private readonly PalettedContainer _container = new PalettedContainer(new List<int>{ 1, 2, 3 }, new PalettedContainerData(2, SubChunk.Size));

		[Benchmark]
		public void Init()
		{
			// for diff on results
			var data = new PalettedContainerData(1, SubChunk.Size);
		}

		[Benchmark]
		public void Resize1To2()
		{
			var data = new PalettedContainerData(1, SubChunk.Size);
			data.TryResize(0b11);
		}

		[Benchmark]
		public void Resize8To16()
		{
			var data = new PalettedContainerData(0xFF, SubChunk.Size);
			data.TryResize(0xFFFF);
		}

		[Benchmark]
		public void Resize1To16()
		{
			var data = new PalettedContainerData(1, SubChunk.Size);
			data.TryResize(0xFFFF);
		}

		[Benchmark]
		public void GetDataBlock()
		{
			_ = _data[100];
		}

		[Benchmark]
		public void SetDataBlock()
		{
			_data[100] = 1;
		}

		[Benchmark]
		public void GetBlock()
		{
			_ = _container[100];
		}

		[Benchmark]
		public void SetBlock()
		{
			_container[100] = 1;
		}
	}
}
