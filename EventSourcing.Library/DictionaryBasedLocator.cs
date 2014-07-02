using System;
using System.Collections.Generic;

namespace EventSourcing.Library
{
  public class DictionaryBasedLocator : ServiceLocator
  {
    public T Get<T>(string name = null)
    {
      var key = GetKey<T>(name);
      return dict.ContainsKey(key) ? (T) dict[key].Invoke(this) : default(T);
    }

    public void Register<T>(T instance, string name = null)
    {
      var key = GetKey<T>(name);
      dict[key] = _ => instance;
    }

    public void Register<T>(Func<ServiceLocator, T> builder, string name = null)
    {
      var key = GetKey<T>(name);
      dict[key] = c => builder(c);
    }

    //

    private readonly Dictionary<string, Func<ServiceLocator, object>> dict = new Dictionary<string, Func<ServiceLocator, object>>();

    private static string GetKey<T>(string name)
    {
      return typeof (T).FullName + "." + name;
    }
  }
}