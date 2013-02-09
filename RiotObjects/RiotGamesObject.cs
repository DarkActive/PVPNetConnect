using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PVPNetConnect.RiotObjects
{
   public abstract class RiotGamesObject
   {
       public virtual void DoCallback(TypedObject result)
       {
           return;
       }

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
               if (type == typeof(string))
               {
                  value = Convert.ToString(result[intern.Name]);
               }
               else if (type == typeof(Int32))
               {
                  value = Convert.ToInt32(result[intern.Name]);
               }
               else if (type == typeof(Int64))
               {
                  value = Convert.ToInt64(result[intern.Name]);
               }
               else if (type == typeof(double))
               {
                  value = Convert.ToInt64(result[intern.Name]);
               }
               else if (type == typeof(bool))
               {
                  value = Convert.ToBoolean(result[intern.Name]);
               }
               else if (type == typeof(DateTime))
               {
                   value = result[intern.Name];
               }
               else if (type == typeof(TypedObject))
               {
                  value = (TypedObject)result[intern.Name];
               }

               else if (type.GetGenericTypeDefinition() == typeof(List<>))
               {
                   object[] temp = result.GetArray(intern.Name);

                   // Create List<T> with correct T type by reflection
                   Type elementType = type.GetGenericArguments()[0];
                   var genericListType = typeof(List<>).MakeGenericType(new[] { elementType });
                   IList objectList = (IList)Activator.CreateInstance(genericListType);           

                   foreach (object data in temp)
                   {
                       objectList.Add(Activator.CreateInstance(elementType, data));
                   }

                   value = objectList;
               }

               else if (type == typeof(object[]))
               {
                   value = new ArrayCollection(result.GetArray(intern.Name));
               }
               else if (type == typeof(object))
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

   public class InternalNameAttribute : Attribute
   {
      public string Name { get; set; }
      public InternalNameAttribute(string name)
      {
         Name = name;
      }
   }

}
