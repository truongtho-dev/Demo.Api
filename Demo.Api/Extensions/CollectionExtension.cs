using System.Collections.Generic;
using System;

namespace Demo.Api.Extensions
{
	public static class CollectionExtension
	{
		public static void RemoveRange<T>(this ICollection<T> @this, IEnumerable<T> collection)
		{

			if (@this == null || collection == null)
			{
				return;
			}

			if (@this.IsReadOnly)
			{
				throw new NotSupportedException("The read-only collection can't remove items.");
			}

			foreach (T item in collection)
			{
				@this.Remove(item);
			}
		}
	}
}
