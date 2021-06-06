using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetFramework.OptionPattern.Utils
{
    /// <summary>
    /// Interface for Option Pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IOptions<T>
    {
        T Value { get; }
    }
}