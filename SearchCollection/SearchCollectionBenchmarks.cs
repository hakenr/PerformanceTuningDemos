using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace SearchCollection
{
	[SimpleJob(warmupCount: 1, launchCount: 1, targetCount: 1)]
	public class SearchCollectionBenchmarks
	{
		[Params(100, 1_000, 10_000, 50_000)]
		public int CollectionSize { get; set; }

		[Benchmark(Description ="List.Contains()")]
		public int Contains()
		{
			// Contains = sekvenční vyhledávání = O(n), též hledání LINQ-to-XY: .Where(), First(), Count(), ... !!!
			return hledane.Count(t => list.Contains(t));
		}


		[Benchmark(Description = "Array.BinarySearch()")]
		public int BinarySearch()
		{
			// binární půlení = O(log(n))
			return hledane.Count(t => Array.BinarySearch<Guid>(sortedArray, t) >= 0);
		}


		[Benchmark(Description = "Dictionary")]
		public int Dictionary()
		{
			// Dictionary = Hashtable, O(1), též HashSet
			return hledane.Count(t => dictionary.ContainsKey(t));
		}


		[Benchmark(Description = "LINQ.ToLookup()")]
		public int ToLookup()
		{
			// ToLookup = Hashtable, O(1)
			return hledane.Count(i => lookup.Contains(i));
		}


		private List<Guid> list;
		private List<Guid> hledane;
		private Dictionary<Guid, object> dictionary;
		private ILookup<Guid, Guid> lookup;
		private Guid[] sortedArray;

		[IterationSetup]
		public void IterationSetup()
		{
			list = new List<Guid>();
			dictionary = new Dictionary<Guid, object>();
			foreach (var guid in Enumerable.Range(0, CollectionSize).Select(g => Guid.NewGuid()))
			{
				list.Add(guid);
				dictionary.Add(guid, null);
			}
			lookup = list.ToLookup(i => i);
			sortedArray = list.ToArray();
			Array.Sort(sortedArray);

			var rand = new Random();
			hledane = Enumerable.Range(0, CollectionSize / 2).Select(g => (rand.NextDouble() > 0.5) ? Guid.NewGuid() : list[rand.Next(list.Count)]).ToList();
		}
	}
}

//               Method | CollectionSize |             Mean | Error |
//--------------------- |--------------- |-----------------:|------:|
//      List.Contains() |            100 |        28.487 us |    NA |
// Array.BinarySearch() |            100 |        12.473 us |    NA |
//           Dictionary |            100 |         4.360 us |    NA |
//             ToLookup |            100 |         4.067 us |    NA |
//      List.Contains() |           1000 |     2,549.650 us |    NA |
// Array.BinarySearch() |           1000 |        63.440 us |    NA |
//           Dictionary |           1000 |        31.895 us |    NA |
//             ToLookup |           1000 |        32.525 us |    NA |
//      List.Contains() |          10000 |   238,715.435 us |    NA |
// Array.BinarySearch() |          10000 |       812.550 us |    NA |
//           Dictionary |          10000 |       299.270 us |    NA |
//             ToLookup |          10000 |       303.205 us |    NA |
//      List.Contains() |          50000 | 5,873,199.320 us |    NA |
// Array.BinarySearch() |          50000 |     4,532.040 us |    NA |
//           Dictionary |          50000 |     2,322.040 us |    NA |
//             ToLookup |          50000 |     1,935.995 us |    NA |
