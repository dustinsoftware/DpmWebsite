using System;
using System.Collections.Generic;

namespace DpmWebsite
{
	public class DpmService
	{
		public DpmService()
		{
			_visitors = new HashSet<string>();
		}

		public int GetVisitors() => _visitors.Count;

		public void RecordVisit(string ipAddress, string userAgent)
		{
			lock (_lock)
			{
				var key = $"{ipAddress}-{userAgent}";
				if (_visitors.Contains(key))
				{
					return;
				}

				_visitors.Add(key);
			}
		}

		private readonly HashSet<string> _visitors;
		private readonly object _lock = new object();
	}
}
