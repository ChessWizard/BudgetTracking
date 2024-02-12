using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracking.Data.Extensions.Reflection
{
    public static class ClassReflectionExtensions
    {
        /// <summary>
        /// Class property'leri üzerindeki DisplayName değerlerini almaya yarar
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetDisplayName(string propertyName, Type type)
        {
            // T tipindeki propertyName isimli property'i al
            PropertyInfo property = type.GetProperty(propertyName);

            // Property üzerindeki DisplayName attribute'unu al
            var displayNameAttribute = property?.GetCustomAttribute<DisplayNameAttribute>();

            // Eğer attribute bulunursa, onun değerini döndür; aksi takdirde "" döndür
            return displayNameAttribute?.DisplayName ?? "";
        }
    }
}
