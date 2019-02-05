using System;
using System.Diagnostics;
using BenchmarkDotNet.Attributes;

namespace Reflection
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 3)]
	public class ReflectionBenchmarks
	{
		[Params(1000000)]
		public int Iterations { get; set; }


		[Benchmark]
		public MyClass DirectAssignment()
		{
			for (int i = 0; i < Iterations; i++)
			{
				instance.MyProperty = i;
			}

			return instance;
		}


		[Benchmark]
		public MyClass Reflection()
		{
			for (int i = 0; i < Iterations; i++)
			{
				var propInfo = typeof(MyClass).GetProperty("MyProperty");
				propInfo.SetValue(instance, i, null);
			}
			return instance;
		}


		[Benchmark]
		public MyClass ReflectionWithCachedPropertyInfo()
		{
			var propInfo = typeof(MyClass).GetProperty("MyProperty");

			for (int i = 0; i < Iterations; i++)
			{
				propInfo.SetValue(instance, i, null);
			}
			return instance;
		}

		[Benchmark]
		public MyClass Dynamic()
		{
			dynamic dyn = instance;
			for (int i = 0; i < Iterations; i++)
			{
				dyn.MyProperty = i;
			}
			return instance;
		}


		private MyClass instance = new MyClass();

		public class MyClass
		{
			public int MyProperty { get; set; }
		}
	}
}

//                           Method | Iterations |         Mean |        Error |     StdDev |
//--------------------------------- |----------- |-------------:|-------------:|-----------:|
//                 DirectAssignment |    1000000 |     273.8 us |     24.14 us |   1.323 us |
//                       Reflection |    1000000 | 282,122.6 us |  3,730.58 us | 204.486 us |
// ReflectionWithCachedPropertyInfo |    1000000 | 223,217.6 us | 10,899.92 us | 597.461 us |
//                          Dynamic |    1000000 |  12,087.8 us |  1,009.71 us |  55.346 us |
