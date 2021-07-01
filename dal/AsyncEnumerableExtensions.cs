using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunAxiom.Transformations.DAL
{
    public static class AsyncEnumerableExtensions
    {
        public static async IAsyncEnumerable<TResult> Map<TSource, TResult>(this IAsyncEnumerable<TSource> sources, Func<TSource, TResult> mapper)
        {
            await foreach(var item in sources)
            {
                yield return mapper(item);
            }
        }
    }
}
