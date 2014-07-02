using System;
using System.Runtime.Serialization;

namespace EventSourcing.Library
{
  [Serializable]
  [DataContract]
  public class Event : CommandEventBase
  {
    public virtual void Handle(ServiceLocator locator)
    {
      // do nothing
    }
  }
}