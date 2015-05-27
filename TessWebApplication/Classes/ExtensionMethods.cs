#region Includes
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;
using SearchKit;
#endregion
namespace Greenspoon.Tess.Classes
{
    public static class ExtensionMethods
    {
        /// <summary> 
        /// Method that provides the T-SQL EXISTS call for any IQueryable (thus extending Linq). 
        /// </summary> 
        /// <remarks>Returns whether or not the predicate conditions exists at least one time.</remarks> 
        public static bool Exists<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) {
            return source.AsExpandable().Where(predicate).Any();
        }
        /// <summary> 
        /// Method that provides the T-SQL EXISTS call for any IQueryable (thus extending Linq). 
        /// </summary> 
        /// <remarks>Returns whether or not the predicate conditions exists at least one time.</remarks> 
        public static bool Exists<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int, bool>> predicate) {
            return source.AsExpandable().Where(predicate).Any();
        }
        // Iterates through a collection and calls a method.
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach(var item in source) {
                action(item);
            }
        }
        /// <summary>
        /// Reads a query string collection and returns the value to appropriate type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">QueryString collection</param>
        /// <param name="key">The name of the query string to get value for</param>
        /// <returns>The value returned as T</returns>
        public static T GetValue<T>(this NameValueCollection collection, string key) {
            try {
                if(collection == null) {
                    throw new ArgumentNullException("collection");
                }
                var value = collection[key];
                if(string.IsNullOrEmpty(value)) {
                    return default(T);
                }
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if(!converter.CanConvertTo(value.GetType())) {
                    return default(T);
                }
                if(typeof(T) == typeof(PageModeEnum)) {
                    return
                        value == "e" ? (T)Enum.Parse(typeof(T), "Edit") 
                                     : value == "v"
                                      ? (T)Enum.Parse(typeof(T), "View") 
                                      : (T)Enum.Parse(typeof(T), "New");
                }
                if(typeof(T) == typeof(FormNameEnum)) {
                    return value == "BatchEscrow" ? (T)Enum.Parse(typeof(T), "BatchEscrow")
                               : value == "Foreclosure" ? (T)Enum.Parse(typeof(T), "Foreclosure") 
                                     : value == "Cancel" ? (T)Enum.Parse(typeof(T), "Cancel")
                                           : (T)Enum.Parse(typeof(T), "Unknown");
                }
                return (T)converter.ConvertFrom(value);
            }
            catch { return default(T); }
        }
        public static T GetFormName<T>(this string s) {
            var value = s;
            return value ==  "BatchEscrow" ? (T)Enum.Parse(typeof(T), "BatchEscrow")
                                         : value == "Foreclosure" ? (T)Enum.Parse(typeof(T), "Foreclosure") 
                                         : value == "Cancel" ? (T)Enum.Parse(typeof(T), "Cancel")
                                         : (T)Enum.Parse(typeof(T), "Unknown");
        }
        /// <summary>
        /// Formats a DateTime type to a standard time format.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateOnly(this DateTime? dt)
        {
            return dt.HasValue ? string.Format("{0:M/d/yyyy}", dt) : string.Empty;
        }

        public static string ToDateOnly(this DateTime dt) {
            try {
                return string.Format("{0:M/d/yyyy}", dt);
            }
            catch {
                return string.Empty;
            }
        }
        public static string ToCurrency(this double d) {
            try {
                return string.Format("{0:C}", d);
            }
            catch { return string.Empty; }
        }
        public static string ToCurrency(this double? d)
        {
            return d.HasValue ? string.Format("{0:C}", d) : string.Empty;
        }

        public static List<string> SplitByInt(this string s) {
            var list = new List<string>();
            list.AddRange(Regex.Split(s, @"\D+"));
            // remove any empty list.
            list.RemoveAll(i => i.Length == 0);
            // return only distinct list.
            return list.Distinct().ToList();
        }
        public static List<string> SplitByWord(this string s) {
            var list = new List<string>();
            list.AddRange(Regex.Split(s, @"\W+"));
            list.RemoveAll(i => i.Length == 0);
            return list;
        }
        
     public static List<string> SplitBySeparator(this string s) {
            var list = new List<string>();
            list.AddRange(s.Split(new[] { ',', ' ', '\n', '\r', '\t' }));
            list.RemoveAll(i => i.Length == 0);
            return list;
        }
        public static List<string> SplitByContinuousString(this string s)
        {
            var list = new List<string>();
            list.AddRange(Regex.Split(s, @"?!\S", RegexOptions.IgnorePatternWhitespace));
            list.RemoveAll(i => i.Length == 0);
            return list;
        }

        public static string RemoveNonNumeric(this string s) {
            if(!string.IsNullOrEmpty(s)) {
                var result = new char[s.Length];
                var resultIndex = 0;
                foreach (var c in s.Where(char.IsNumber))
                {
                    result[resultIndex++] = c;
                }
                if(0 == resultIndex) {
                    s = string.Empty;
                }
                else if(result.Length != resultIndex) {
                    s = new string(result, 0, resultIndex);
                }
            }
            return s;
        }
        // Extension method to check form type.
        public static bool isBatchEscrow(this FormNameEnum f) {
            return f == FormNameEnum.BatchEscrow;
        }
        public static bool isForeclosure(this FormNameEnum f) {
            return f == FormNameEnum.Foreclosure;
        }
        public static bool isCancel(this FormNameEnum f) {
            return f == FormNameEnum.Cancel;
        }
        // <remark>
        // use this method to read data from DTOs 
        // and transform into the db type.
        // </remark>
        public static T NullIfEmpty<T>(this string s)
        {
            if(string.IsNullOrEmpty(s) == false) {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                try
                {
                    if(converter.CanConvertTo(s.GetType()) == false) {
                        return default(T);
                    }
                    return (T)converter.ConvertFrom(s);
                }
                catch { return default(T); }
            }
            return default(T);
        }

        // end of null if empty.
        /// <summary>
        /// SetAllModified<T/> gets an instance of the ObjectStateEntry for the given entity T. 
        /// It then retrieves a list of all of the property names for the entity and 
        /// iteratively calls SetModifiedProperty for each property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="context"></param>
        public static void SetAllModified<T>(this T entity, ObjectContext context)
                                             where T : IEntityWithKey {
            var stateEntry = context.ObjectStateManager.GetObjectStateEntry(entity.EntityKey);
            var propertyNameList = stateEntry.CurrentValues.DataRecordInfo.FieldMetadata.Select
              (pn => pn.FieldType.Name);
            foreach(var propName in propertyNameList)
                stateEntry.SetModifiedProperty(propName);
        }
        public static T GetControl<T>(this Page page, string name) where T : Control {
            FieldInfo field = page.GetType()
                                  .GetField(name, BindingFlags.IgnoreCase 
                                           | BindingFlags.Instance 
                                           | BindingFlags.NonPublic);
            if(field != null) {
                return (T)field.GetValue(page);
            }
            return null;
        }
    } // end of class
} //  end of namespace.