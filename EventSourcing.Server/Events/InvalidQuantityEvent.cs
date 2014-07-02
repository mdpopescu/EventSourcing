using System;
using EventSourcing.Library;
using ProtoBuf;

namespace EventSourcing.Server.Events
{
  [ProtoContract]
  public class InvalidQuantityEvent : Event
  {
    public InvalidQuantityEvent(string qty)
    {
      this.qty = qty;
    }

    public override void Handle(ServiceLocator locator)
    {
      Console.WriteLine("[{0}] Invalid quantity {1}.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), qty);
    }

    //

    [ProtoMember(3)] private readonly string qty;
  }
}