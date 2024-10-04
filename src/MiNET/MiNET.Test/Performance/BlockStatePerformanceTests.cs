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
		[Benchmark]
		public void SimpleCreation()
		{
			_ = new Air().RuntimeId;
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
	}
}
