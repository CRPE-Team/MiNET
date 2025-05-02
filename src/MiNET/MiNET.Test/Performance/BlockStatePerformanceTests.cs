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
		private readonly OakLog _existingBlock2 = new OakLog() { PillarAxis = PillarAxis.Z };
		private readonly OakLog _existingBlock3 = new OakLog() { PillarAxis = PillarAxis.Z };

		[Benchmark]
		public void CreationOnly()
		{
			new Air();
		}

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
		public void RuntimeIdStateChange()
		{
			_existingBlock3.PillarAxis = PillarAxis.Z;
			_ = _existingBlock3.RuntimeId;
		}

		[Benchmark]
		public void StateChange()
		{
			_existingBlock2.PillarAxis = PillarAxis.X;
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


		// 05/03/2025
		//
		// Windows 11 Home Single Language - 23H2
		// Intel(R) Core(TM) Ultra 7 155H   3.80 GHz    (5-30% performance)
		// 16.0 GB
		//
		// before changes
		//
		//| Method                        | Mean       | Error     | StdDev    |
		//|------------------------------ |-----------:|----------:|----------:|
		//| CreationOnly                  |  29.522 ns | 0.5342 ns | 0.4997 ns |
		//| SimpleCreation                |  33.203 ns | 0.4832 ns | 0.4284 ns |
		//| RuntimeIdCache                |   2.345 ns | 0.0185 ns | 0.0173 ns |
		//| RuntimeIdStateChange          | 135.988 ns | 0.6031 ns | 0.5036 ns |
		//| StateChange                   |   2.861 ns | 0.0319 ns | 0.0283 ns |
		//| SimpleCreationBlockWithStates |  75.509 ns | 0.7782 ns | 0.7279 ns |
		//| CreationWithCustomStates      | 228.909 ns | 3.2578 ns | 3.0473 ns |
		//
		//
		// after changes
		//
		//| Method                        | Mean       | Error     | StdDev    |
		//|------------------------------ |-----------:|----------:|----------:|
		//| CreationOnly                  |   9.316 ns | 0.0780 ns | 0.0691 ns |
		//| SimpleCreation                |  56.389 ns | 0.9715 ns | 0.8613 ns |
		//| RuntimeIdCache                |   2.337 ns | 0.0093 ns | 0.0072 ns |
		//| RuntimeIdStateChange          | 161.645 ns | 1.5120 ns | 1.3403 ns |
		//| StateChange                   |  21.519 ns | 0.1356 ns | 0.1269 ns |
		//| SimpleCreationBlockWithStates |  88.485 ns | 0.9224 ns | 0.8628 ns |
		//| CreationWithCustomStates      | 215.462 ns | 1.8950 ns | 1.7726 ns |
	}
}
