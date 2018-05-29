using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCollection
{
	public static class SearchCollectionProgram
	{
		public static void Main(string[] args)
		{
			Search(collectionSize: 100, searchItemsCount: 50);
			Search(collectionSize: 1000, searchItemsCount: 5000);
			Search(collectionSize: 10000, searchItemsCount: 100000);
			Search(collectionSize: 100000, searchItemsCount: 15000);
		}

		private static void Search(int collectionSize, int searchItemsCount)
		{
			Console.WriteLine($"V množině o velikost {collectionSize:n0} hledáno {searchItemsCount:n0}x:");

			#region Sample data
			var sw = new System.Diagnostics.Stopwatch();
			var list = new List<Guid>();
			var dictionary = new Dictionary<Guid, object>();
			foreach (var guid in Enumerable.Range(0, collectionSize).Select(g => Guid.NewGuid()))
			{
				list.Add(guid);
				dictionary.Add(guid, null);
			}
			var lookup = list.ToLookup(i => i);
			var sortedArray = list.ToArray();
			Array.Sort(sortedArray);

			// vytvoření sample-dat, které budeme hledat
			var rand = new Random();
			var hledane = Enumerable.Range(0, searchItemsCount).Select(g => (rand.NextDouble() > 0.5) ? Guid.NewGuid() : list[rand.Next(list.Count)]).ToList();

			#endregion

			// Contains = sekvenční vyhledávání = O(n), též hledání LINQ-to-XY: .Where(), First(), Count(), ... !!!
			sw.Start();
			int found = hledane.Count(t => list.Contains(t));
			sw.Stop();
			Console.WriteLine($"\tList<>.Contains():          Nalezeno {found:n0}x, čas {sw.ElapsedTicks,10:n0} ticks");

			// binární půlení = O(log(n))
			sw.Restart();
			found = hledane.Count(t => Array.BinarySearch<Guid>(sortedArray, t) >= 0);
			sw.Stop();
			Console.WriteLine($"\tArray.BinarySearch<>():     Nalezeno {found:n0}x, čas {sw.ElapsedTicks,10:n0} ticks");

			// Dictionary = Hashtable, O(1), též HashSet
			sw.Restart();
			found = hledane.Count(t => dictionary.ContainsKey(t));
			sw.Stop();
			Console.WriteLine($"\tDictionary<>.ContainsKey(): Nalezeno {found:n0}x, čas {sw.ElapsedTicks,10:n0} ticks");

			// ToLookup = Hashtable, O(1)
			sw.Restart();
			found = hledane.Count(i => lookup.Contains(i));
			sw.Stop();
			Console.WriteLine($"\tLINQ ILookup:               Nalezeno {found:n0}x, čas {sw.ElapsedTicks,10:n0} ticks");

			Console.WriteLine();
		}
	}
}
