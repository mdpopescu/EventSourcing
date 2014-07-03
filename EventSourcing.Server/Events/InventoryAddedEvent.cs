using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using ProtoBuf;

namespace EventSourcing.Server.Events
{
  [Serializable]
  [DataContract]
  [ProtoContract(SkipConstructor = true)]
  public class InventoryAddedEvent : Event
  {
    public InventoryAddedEvent(string name, decimal qty)
    {
      this.name = name;
      this.qty = qty;
    }

    public override void Handle(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      var existing = products.First(it => it.Name == name);
      existing.Quantity += qty;

      Console.WriteLine("[{0}] Added qty {1} to product {2}.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), qty, name);
    }

    //

    [DataMember(Order = 3)] private readonly string name;
    [DataMember(Order = 4)] private readonly decimal qty;
  }
}