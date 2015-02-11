using System;
using System.Collections;
namespace Iface
{
	public interface IPerRequestDataStorage
	{
		IDictionary Items
		{
			get;
		}
	}
}
