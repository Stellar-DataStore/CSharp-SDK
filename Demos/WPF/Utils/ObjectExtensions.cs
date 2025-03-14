using System.Reflection;

namespace StellarDS.Demos.WPF;

public static class ObjectExtensions
{
    public static T ToObject<T>(this IDictionary<string, object> source)
        where T : class, new()
    {
        var someObject = new T();
        var someObjectType = someObject.GetType();

        foreach (var item in source)
        {
            
            
            someObjectType
                .GetProperty(item.Key, BindingFlags.IgnoreCase |  BindingFlags.Public | BindingFlags.Instance)
                ?.SetValue(someObject, item.Value, null);
        }

        return someObject;
    }

    public static Dictionary<string, object?> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
    {
        return source.GetType().GetProperties(bindingAttr).ToDictionary
        (
            propInfo => propInfo.Name,
            propInfo => propInfo.GetValue(source, null)
        );

    }
}