using System;
using System.Runtime.Serialization;

namespace EventSourcing.Library
{
  [Serializable]
  [DataContract]
  public class Event : CommandEventBase
  {
    public static readonly Event NULL = new Event();

    public virtual void Handle(ServiceLocator locator)
    {
      // do nothing
    }
  }
}