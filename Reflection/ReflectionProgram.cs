using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
	public static class ReflectionProgram
	{
		private const int ITERATIONS = 1000000;

		public static void Main(string[] args)
		{
			var instance = new MyClass();
			var sw = new Stopwatch();

			// standardní nastavení
			sw.Start();
			for (int i = 0; i < ITERATIONS; i++)
			{
				instance.MyProperty = i;
			}
			sw.Stop();
			Console.WriteLine($"Standardní: {sw.ElapsedTicks:n0} ticks");
			Console.WriteLine();

			// reflection s cacheovaným PropertyInfo
			sw.Restart();
			var propInfo = typeof(MyClass).GetProperty("MyProperty");
			for (int i = 0; i < ITERATIONS; i++)
			{
				propInfo.SetValue(instance, i, null);
			}
			sw.Stop();
			Console.WriteLine($"Cacheované PropInfo: {sw.ElapsedTicks:n0} ticks");
			Console.WriteLine();

			// reflection
			sw.Restart();
			for (int i = 0; i < ITERATIONS; i++)
			{
				propInfo = typeof(MyClass).GetProperty("MyProperty");
				propInfo.SetValue(instance, i, null);
			}
			sw.Stop();
			Console.WriteLine($"Vždy nové PropInfo: {sw.ElapsedTicks:n0} ticks");
			Console.WriteLine();

			// dynamic
			sw.Restart();
			dynamic dyn = instance;
			for (int i = 0; i < ITERATIONS; i++)
			{
				dyn.MyProperty = i;
			}
			sw.Stop();
			Console.WriteLine($"Dynamic: {sw.ElapsedTicks:n0} ticks");
			Console.WriteLine();

			Console.ReadKey();
		}

		public class MyClass
		{
			public int MyProperty { get; set; }
		}
	}
}
