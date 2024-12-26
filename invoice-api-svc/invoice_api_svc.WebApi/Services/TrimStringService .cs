using System.Linq;
using System.Reflection;

namespace invoice_api_svc.WebApi.Services
{
    public class TrimStringService
    {
        public T TrimStringProperties<T>(T obj)
        {
            if (obj == null) return default;

            // Get all properties of the object
            var properties = obj.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.PropertyType == typeof(string) && p.CanWrite && p.CanRead);

            foreach (var property in properties)
            {
                // Get the value of the property
                var value = property.GetValue(obj) as string;

                if (value != null)
                {
                    // Trim the value and set it back
                    property.SetValue(obj, value.Trim());
                }
            }

            return obj;
        }
    }
}
