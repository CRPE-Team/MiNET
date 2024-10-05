using BenchmarkDotNet.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;
using MiNET.Blocks.States;

namespace MiNET.Test.Performance
{
	[TestClass]
	[Ignore("only for manual run")]
	public class BlockStatePerformanceTests : PerformanceTestBase
	{
		private readonly Block _existingBlock = new OakLog() { PillarAxis = PillarAxis.Z };

		[Benchmark]
		public void SimpleCreation()
		{
			_ = new Air().RuntimeId;
		}

		[Benchmark]
		public void RuntimeIdCache()
		{
			_ = _existingBlock.RuntimeId;
		}

		[Benchmark]
		public void SimpleCreationBlockWithStates()
		{
			_ = new OakLog().RuntimeId;
		}

		[Benchmark]
		public void CreationWithCustomStates()
		{
			_ = new OakLog() { PillarAxis = PillarAxis.Z }.RuntimeId;
		}

		// 10/04/2024
		//
		// Windows 11 Home Single Language - 23H2
		// Intel(R) Core(TM) Ultra 7 155H   3.80 GHz
		// 16.0 GB
		//
		// | Method                        | Mean       | Error     | StdDev    |
		// |------------------------------ |-----------:|----------:|----------:|
		// | SimpleCreation                |  17.378 ns | 0.3884 ns | 0.4912 ns |
		// | RuntimeIdCache                |   1.258 ns | 0.0371 ns | 0.0347 ns |
		// | SimpleCreationBlockWithStates |  40.687 ns | 0.8283 ns | 1.2896 ns |
		// | CreationWithCustomStates      | 132.211 ns | 2.6179 ns | 4.8524 ns |
	}
}
