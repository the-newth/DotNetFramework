using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetFramework.OptionPattern.Models
{
    #region Option Class
    public class SecretOptions
    {    
        [Option("SecretName")]
        public string secretName { get; set; }
                
        [Option("SecretKey")]
        public string secretKey { get; set; }             
    }
    #endregion

    #region Option Attribute

    /// <summary>
    /// set Web.config key name for mapping
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionAttribute : Attribute
    {
        public List<string> keyName { get; set; }

        public OptionAttribute(params string[] keyName)
        {
            this.keyName = keyName.ToList();
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionObjectIgnoreAttribute : Attribute { }
    #endregion
}