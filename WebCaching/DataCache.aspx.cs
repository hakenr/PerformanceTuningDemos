using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCaching
{
	public partial class DataCache : System.Web.UI.Page
	{
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			MyDataLb.Text = GetData();
			Thread.Sleep(20);
		}

		private static string FetchDataFromDb()
		{
			DateTime now = DateTime.Now;
			Thread.Sleep(100);
			return DateTime.Now.ToString();
		}

		private static string GetData()
		{
			//return FetchDataFromDb();  // no caching

			const string cacheKey = "MyData";

			string result = (string)HttpRuntime.Cache[cacheKey];
			if (result == null)
			{
				lock (dataLock)
				{
					result = (string)HttpRuntime.Cache[cacheKey];
					if (result == null)
					{
						result = FetchDataFromDb();

						//HttpRuntime.Cache[cacheKey] = result;	  // alternativa
						HttpRuntime.Cache.Add(
							key: cacheKey,
							value: result,
							dependencies: null,
							absoluteExpiration: DateTime.Now.AddSeconds(5),
							slidingExpiration: System.Web.Caching.Cache.NoSlidingExpiration,  // nebo TimeSpan
							priority: System.Web.Caching.CacheItemPriority.Normal,
							onRemoveCallback: null);
					}
				}
			}
			return result;
		}
		private static object dataLock = new object();
	}
}