using System;

namespace EventSourcing.Library
{
  public interface ServiceLocator
  {
    T Get<T>(string name = null);
    void Register<T>(T instance, string name = null);
    void Register<T>(Func<ServiceLocator, T> builder, string name = null);
  }
}