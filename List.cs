using System.Collections.Generic;

namespace Androrim.Generic.Extension
{
    static class List
    {

        /// <summary>Compare two lists and return an int value that indicate if an is minor, equal or major than the other list</summary>
        /// 
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="value">The list that use this method</param>
        /// <param name="list">The list passed per parameter to compare</param>
        /// <param name="propNameToCompare">On case of an object with properties, the property that it will be used to compare (an ID for example)</param>
        /// 
        /// <returns>
        ///     An integer with signal that indicate if the list compareted is minor, equal or major than the other list:
        ///     <list type="bullet">
        ///         <item>If minor that 0, the list passed per parameter it will be minor</item>
        ///         <item>If equal 0, the list passed per parameter it will be same size</item>
        ///         <item>If major that 0, the list passed per parameter it will be major</item>
        ///     </list>
        /// </returns>
        public static int  CompareWith<T>(this List<T> value, List<T> list, string propNameToCompare = null)
        {
            int c1 = value.FindDistinctsOn(list, propNameToCompare).Count;
            int c2 = list.FindDistinctsOn(value, propNameToCompare).Count;

            return c1 - c2;
        }

        /// <summary>Return the items that are distincts, contained in the passed list per parameter</summary>
        /// 
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="value">The list that use this method</param>
        /// <param name="list">The list used for extract the differents items</param>
        /// <param name="propToCompare">On case of an object with properties, the property that it will be used to compare (an ID for example)</param>
        /// 
        /// <returns>An list with the differents finded items</returns>
        public static List<T> FindDistinctsOn<T>(this List<T> value, List<T> list, string propToCompare = null)
        {
            List<T> res = new List<T>();

            foreach(T item in list)
            {
                T obj = value.Find(x =>
                {
                    if (propToCompare != null)
                    {
                        object vA = item.GetType().GetProperty(propToCompare).GetValue(item);
                        object vB = x.GetType().GetProperty(propToCompare).GetValue(x);

                        return vA.ToString() == vB.ToString();
                    }

                    return item is string ? item.ToString() == x.ToString() : item.Equals(x);
                });

                if (obj == null)
                    res.Add(item);
            }

            return res;
        }
    }
}

