using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PVPNetConnect.RiotObjects
{
    /// <summary>
    /// RiotGamesObject is the base class for all Riot objects.
    /// </summary>
   public abstract class RiotGamesObject
   {
       /// <summary>
       /// The base virtual DoCallback method.
       /// </summary>
       /// <param name="result">The result.</param>
      public virtual void DoCallback(TypedObject result)
      {
         return;
      }

      /// <summary>
      /// Sets the fields of the object and decode/parse into correct fields.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="obj">The obj.</param>
      /// <param name="result">The result.</param>
      /// <exception cref="System.NotSupportedException"></exception>
      public void SetFields<T>(T obj, TypedObject result)
      {
         if (result == null)
            return;

         foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly))
         {
            var intern = prop.GetCustomAttributes(typeof(InternalNameAttribute), false).FirstOrDefault() as InternalNameAttribute;
            if (intern == null)
               continue;

            object value;
            var type = prop.PropertyType;

            if (!result.TryGetValue(intern.Name, out value))
            {
               continue;
            }


            try
            {
               if (type.Equals(typeof(string)))
               {
                  value = Convert.ToString(result[intern.Name]);
               }
               else if (type.Equals(typeof(Int32)))
               {
                  value = Convert.ToInt32(result[intern.Name]);
               }
               else if (type.Equals(typeof(Int64)))
               {
                  value = Convert.ToInt64(result[intern.Name]);
               }
               else if (type.Equals(typeof(double)))
               {
                  value = Convert.ToInt64(result[intern.Name]);
               }
               else if (type.Equals(typeof(bool)))
               {
                  value = Convert.ToBoolean(result[intern.Name]);
               }
               else if (type.Equals(typeof(DateTime)))
               {
                   value = result[intern.Name];
               }
               else if (type.Equals(typeof(TypedObject)))
               {
                  value = (TypedObject)result[intern.Name];
               }

               else if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(List<>)))
               {
                   object[] temp = result.GetArray(intern.Name);

                   // Create List<T> with correct T type by reflection
                   Type elementType = type.GetGenericArguments()[0];
                   var genericListType = typeof(List<>).MakeGenericType(new[] { elementType });
                   IList objectList = (IList)Activator.CreateInstance(genericListType);           

                   foreach (object data in temp)
                   {
                       if (elementType.Equals(typeof(string)))
                       {
                           objectList.Add((string)data);
                       }
                       else
                       {
                           objectList.Add(Activator.CreateInstance(elementType, data));
                       }
                   }

                   value = objectList;
               }
               else if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Dictionary<,>)))
               {
                   TypedObject to = result.GetTO(intern.Name);

                   Type[] elementTypes = type.GetGenericArguments();
                   var genericDictionaryType = typeof(Dictionary<,>).MakeGenericType(elementTypes);
                   IDictionary objectDictionary = (IDictionary)Activator.CreateInstance(genericDictionaryType);

                   foreach (string key in to.Keys)
                   {
                       objectDictionary.Add(key, Activator.CreateInstance(elementTypes[1], to[key]));
                   }

                   value = objectDictionary;
               }
               else if (type.Equals(typeof(object[])))
               {
                   value = new ArrayCollection(result.GetArray(intern.Name));
               }
               else if (type.Equals(typeof(object)))
               {
                   value = result[intern.Name];
               }
               else
               {
                   try
                   {
                       value = Activator.CreateInstance(type, result[intern.Name]);
                   }
                   catch (Exception e)
                   {
                       throw new NotSupportedException(string.Format("Type {0} not supported by flash serializer", type.FullName), e);

                   }
               }
               prop.SetValue(obj, value, null);
            }
            catch
            {
            }

         }
      }
   }

   /// <summary>
   /// The InternalName Atribute class to specify the name that Riot's server is expecting.
   /// </summary>
   public class InternalNameAttribute : Attribute
   {
       /// <summary>
       /// Gets or sets the name of the InternalName
       /// </summary>
       /// <value>
       /// The name.
       /// </value>
      public string Name { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="InternalNameAttribute"/> class.
      /// </summary>
      /// <param name="name">The name.</param>
      public InternalNameAttribute(string name)
      {
         Name = name;
      }
   }

}
