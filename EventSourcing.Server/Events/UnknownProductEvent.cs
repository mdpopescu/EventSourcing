using System;
using EventSourcing.Library;
using ProtoBuf;

namespace EventSourcing.Server.Events
{
  [ProtoContract]
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

    [ProtoMember(3)] private readonly string name;
  }
}