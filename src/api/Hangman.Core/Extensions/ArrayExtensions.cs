namespace System
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Copy an array with one extra slot and fill the last slot
        /// </summary>
        /// <typeparam name="T">Type of the item</typeparam>
        /// <param name="array">Array to copy</param>
        /// <param name="item">Item to append</param>
        /// <returns>A new array with the extra element</returns>
        public static T[] CopyAndAppend<T>(this T[] array, T item)
        {
            var length = array.Length + 1;

            var newArray = new T[length];
            array.CopyTo(newArray, 0);
            newArray[length - 1] = item;

            return newArray;
        }
    }
}
