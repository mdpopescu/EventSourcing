using System;
using System.Runtime.Serialization;
using EventSourcing.Library;

namespace EventSourcing.Server.Events
{
  [Serializable]
  [DataContract]
  public class UnknownProductEvent : Event
  {
    public UnknownProductEvent(string name)
    {
      this.name = name;
    }

    public override void Handle(ServiceLocator locator)
    {
      Console.WriteLine("[{0}] Unknown product {1}.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), name);
    }

    //

    [DataMember(Order = 3)] private readonly string name;
  }
}