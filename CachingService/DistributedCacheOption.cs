using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingService
{
	public class DistributedCacheOption
	{
		public const string Name = "DistributedCache";
		public string AppName { get; set; }
		public string ConnectionString { get; set; }
		public CacheType Type { get; set; } = CacheType.Redis;
		public string TableName { get; set; } = "DistributedCache";
		public string SchemaName { get; set; } = "cache";
	}

	public enum CacheType
	{
		Memory, Redis, Sql
	}
}
