using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiNET.Blocks;

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
			_ = new OakLog() { PillarAxis = "z" }.RuntimeId;
		}
	}
}
