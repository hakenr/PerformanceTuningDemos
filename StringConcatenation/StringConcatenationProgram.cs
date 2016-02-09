using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringConcatenation
{
	public static class StringConcatenationProgram
	{
		public static void Main(string[] args)
		{
			const int iterations = 100000;

			string str;

			// string concatenation
			str = string.Empty;
			Stopwatch sw1 = new Stopwatch();
			sw1.Start();
			for (int i = 0; i < iterations; i++)
			{
				str = str + (char)(i % 26 + 65);
			}
			sw1.Stop();
			Console.WriteLine($"string + string: {sw1.ElapsedTicks, 15:n0} ticks");

			// StringBuilder
			str = string.Empty;
			var sw2 = new Stopwatch();
			sw2.Start();
			var sb = new StringBuilder();
			for (int i = 0; i < iterations; i++)
			{
				sb.Append((char)(i % 26 + 65));
			}
			str = sb.ToString();
			sw2.Stop();
			Console.WriteLine($"StringBuilder:   {sw2.ElapsedTicks, 15:n0} ticks");
		}
	}
}
