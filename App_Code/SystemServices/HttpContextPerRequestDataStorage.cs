using System;
using System.Collections;
using System.Web;
using Iface;
namespace SystemServices
{
	public class HttpContextPerRequestDataStorage : IPerRequestDataStorage
	{
		public IDictionary Items
		{
			get
			{
				return HttpContext.Current.Items;
			}
		}
	}
}
