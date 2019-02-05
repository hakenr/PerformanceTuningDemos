using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace StringConcatenation
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 1)]
	[MemoryDiagnoser]
	public class StringConcatenationBenchmarks
	{
		[Params(1, 2, 3, 5, 10, 100, 1000, 10_000, 100_000)]
		public int Concatenations { get; set; }

		[Benchmark]
		public string StringConcat()
		{
			var str = string.Empty;

			for (int i = 0; i < Concatenations; i++)
			{
				str = str + "A";
			}

			return str;
		}

		[Benchmark]
		public string StringBuilder()
		{
			var sb = new StringBuilder();

			for (int i = 0; i < Concatenations; i++)
			{
				sb.Append("A");
			}

			return sb.ToString();
		}
	}
}

//        Method | Concatenations |               Mean | Error |  Gen 0/1k Op |  Gen 1/1k Op |  Gen 2/1k Op | Allocated Memory/Op |
//-------------- |--------------- |-------------------:|------:|-------------:|-------------:|-------------:|--------------------:|
//  StringConcat |              1 |           2.115 ns |    NA |            - |            - |            - |                   - |
// StringBuilder |              1 |          32.707 ns |    NA |       0.0209 |            - |            - |                88 B |
//  StringConcat |              2 |          21.823 ns |    NA |       0.0047 |            - |            - |                20 B |
// StringBuilder |              2 |          34.559 ns |    NA |       0.0219 |            - |            - |                92 B |
//  StringConcat |              3 |          41.469 ns |    NA |       0.0095 |            - |            - |                40 B |
// StringBuilder |              3 |          37.839 ns |    NA |       0.0219 |            - |            - |                92 B |
//  StringConcat |              5 |          84.065 ns |    NA |       0.0209 |            - |            - |                88 B |
// StringBuilder |              5 |          46.354 ns |    NA |       0.0228 |            - |            - |                96 B |
//  StringConcat |             10 |         209.550 ns |    NA |       0.0579 |            - |            - |               244 B |
// StringBuilder |             10 |          73.386 ns |    NA |       0.0256 |            - |            - |               108 B |
//  StringConcat |            100 |       4,583.561 ns |    NA |       2.7542 |            - |            - |             11584 B |
// StringBuilder |            100 |         611.789 ns |    NA |       0.1497 |            - |            - |               632 B |
//  StringConcat |           1000 |     231,475.884 ns |    NA |     241.9434 |            - |            - |           1016142 B |
// StringBuilder |           1000 |       4,196.067 ns |    NA |       1.0300 |            - |            - |              4344 B |
//  StringConcat |          10000 |   7,898,224.961 ns |    NA |   23851.5625 |            - |            - |         100242290 B |
// StringBuilder |          10000 |      43,555.416 ns |    NA |      12.5732 |            - |            - |             52872 B |
//  StringConcat |         100000 | 916,247,700.000 ns |    NA | 2972000.0000 | 2545000.0000 | 2545000.0000 |       10002184772 B |
// StringBuilder |         100000 |     381,182.900 ns |    NA |      62.0117 |      62.0117 |      62.0117 |            409736 B |
