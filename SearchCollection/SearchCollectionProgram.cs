using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace SearchCollection
{
	public static class SearchCollectionProgram
	{
		public static void Main(string[] args)
		{
			var summmary = BenchmarkRunner.Run<SearchCollectionBenchmarks>();
		}
	}
}
