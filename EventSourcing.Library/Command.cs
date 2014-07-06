using System;
using System.Runtime.Serialization;

namespace EventSourcing.Library
{
  [Serializable]
  [DataContract]
  public class Command : CommandEventBase
  {
    public static readonly Command NULL = new Command();

    public virtual Event Process(ServiceLocator locator)
    {
      return Event.NULL;
    }
  }
}