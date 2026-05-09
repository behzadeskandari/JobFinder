using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> left,
            IEnumerable<TRight> right,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TKey, TResult> resultSelector)
        {
            var leftLookup = left.ToLookup(leftKeySelector);
            var rightLookup = right.ToLookup(rightKeySelector);

            var keys = leftLookup.Select(p => p.Key).Union(rightLookup.Select(p => p.Key)).Distinct();

            return keys.SelectMany(key =>
            {
                var leftGroup = leftLookup[key];
                var rightGroup = rightLookup[key];

                if (!leftGroup.Any())
                {
                    return rightGroup.Select(rightItem => resultSelector(default(TLeft), rightItem, key));
                }

                if (!rightGroup.Any())
                {
                    return leftGroup.Select(leftItem => resultSelector(leftItem, default(TRight), key));
                }

                return leftGroup.SelectMany(leftItem => rightGroup.Select(rightItem => resultSelector(leftItem, rightItem, key)));
            });
        }
    }
}
