using System;
using System.Runtime.Serialization;

namespace EventSourcing.Library
{
  [Serializable]
  [DataContract]
  public class Command : CommandEventBase
  {
    public virtual Event Process(ServiceLocator locator)
    {
      return null;
    }
  }
}