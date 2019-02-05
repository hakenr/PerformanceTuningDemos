using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace Reflection
{
	public static class ReflectionProgram
	{
		public static void Main(string[] args)
		{
			var summmary = BenchmarkRunner.Run<ReflectionBenchmarks>();
		}
	}
}
