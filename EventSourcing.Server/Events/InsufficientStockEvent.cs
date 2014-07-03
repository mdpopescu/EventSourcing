using System;
using System.Runtime.Serialization;
using EventSourcing.Library;
using ProtoBuf;

namespace EventSourcing.Server.Events
{
  [Serializable]
  [DataContract]
  [ProtoContract(SkipConstructor = true)]
  public class InsufficientStockEvent : Event
  {
    public InsufficientStockEvent(string name, decimal qty)
    {
      this.name = name;
      this.qty = qty;
    }

    public override void Handle(ServiceLocator locator)
    {
      Console.WriteLine("[{0}] Insufficient quantity {1} for product {2}.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), qty, name);
    }

    //

    [DataMember(Order = 3)] private string name;
    [DataMember(Order = 4)] private readonly decimal qty;
  }
}