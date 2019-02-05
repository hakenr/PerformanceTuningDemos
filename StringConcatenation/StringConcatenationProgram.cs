using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;

namespace StringConcatenation
{
	public static class StringConcatenationProgram
	{
		public static void Main(string[] args)
		{
			var summmary = BenchmarkRunner.Run<StringConcatenationBenchmarks>();
		}
	}
}
