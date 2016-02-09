using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebCaching
{
	public partial class OutputCache : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Thread.Sleep(20); // 20 ms
		}
	}
}