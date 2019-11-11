using System.Collections.Generic;

namespace Extension.Native
{
    public static class ListExtension
    {
        /// <summary>
        /// Returns list without itemToExclude.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="itemToExclude"></param>
        /// <returns></returns>
        public static List<T> Except<T>(this List<T> list, T itemToExclude)
        {
            list.Remove(itemToExclude);
            return list;
        }
    }
}
