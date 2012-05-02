using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Common
{
    public interface IReadOnlyList<T> : IEnumerable<T>
    {
        T this[int index] { get; }
        int Count { get; }
        int IndexOf(T value);
        bool Contains(T value);
        void CopyTo(T[] array, int index);
    }

    public class ReadOnlyList<T> : ReadOnlyCollection<T>, IReadOnlyList<T>
    {
        public ReadOnlyList(IEnumerable<T> list) : base(list.NullCheck().ToArray())
        {
        }
    }

    public static class ReadOnlyListExtensions
    {
        public static IReadOnlyList<T> AsReadOnly<T>(this IEnumerable<T> source)
        {
            return new ReadOnlyList<T>(source);
        }
    }
}