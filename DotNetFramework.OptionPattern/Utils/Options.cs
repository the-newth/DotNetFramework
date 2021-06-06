using DotNetFramework.OptionPattern.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DotNetFramework.OptionPattern.Utils
{
    public class Options<T> : IOptions<T>
    {
        private readonly T model;
        private readonly List<string> AppSettingNames;
       
        public Options()
        {      
            AppSettingNames = ConfigurationManager.AppSettings.AllKeys.ToList();           

            T instance = Activator.CreateInstance<T>();

            model = (T)SetConfig(instance.GetType());
        }

        private object SetConfig(Type type)
        {
            object instance = Activator.CreateInstance(type);

            var instanceProperties = instance.GetType().GetProperties();

            foreach (var property in instanceProperties)
            {
                //If it is a hierarchical option class, it is searched recursively.
                if (property.GetCustomAttribute<OptionObjectIgnoreAttribute>() != null)
                {
                    property.SetValue(instance, SetConfig(property.PropertyType));
                }

                //If an attribute does not have an 'OptionAttribute', the value is searched by the attribute's original name. 
                //but if there is, the value is searched by the set name at 'OptionAttribute'
                var name = AppSettingNames.FirstOrDefault(m => property.GetCustomAttribute<OptionAttribute>() == null ?
                                                                property.Name.Equals(m, StringComparison.InvariantCultureIgnoreCase) :
                                                                property.GetCustomAttribute<OptionAttribute>().keyName.Any(n => n.Equals(m, StringComparison.InvariantCultureIgnoreCase)));

                var value = ConfigurationManager.AppSettings.Get(name);

                if (!value.Equals(String.Empty))
                {
                    property.SetValue(instance, Convert.ChangeType(value, property.PropertyType));
                }
            }

            return instance;
        }

        public T Value
        {
            get
            {
                return model;
            }
        }
    }
}