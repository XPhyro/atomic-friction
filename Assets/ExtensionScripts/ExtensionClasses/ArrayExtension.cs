using System.Linq;

namespace Extension.Native
{
    public static class ArrayExtension
    {
        /// <summary>
        /// Returns array by replacing itemToExclude with default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="itemToExclude"></param>
        /// <returns></returns>
        public static T[] Except<T>(this T[] array, T itemToExclude)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i].Equals(itemToExclude))
                {
                    array[i] = default;
                }
            }

            return array;
        }

        /// <summary>
        /// Returns array by removing slots with itemToExclude.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="itemToExclude"></param>
        /// <returns></returns>
        public static T[] ExceptResized<T>(this T[] array, T itemToExclude)
        {
            for(int i = 0; i < array.Length; i++)
            {
                if(array[i].Equals(itemToExclude))
                {
                    array[i] = default;
                }
            }

            array = array.Where(c => c != default).ToArray();

            return array;
        }
    }
}
