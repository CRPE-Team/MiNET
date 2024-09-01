using BenchmarkDotNet.Running;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiNET.Test.Performance
{
	[TestClass]
	public abstract class PerformanceTestBase
	{
		[TestMethod]
		public void StartBenchmark()
		{
			Assert.IsFalse(BenchmarkRunner.Run(GetType()).HasCriticalValidationErrors);
		}
	}
}
