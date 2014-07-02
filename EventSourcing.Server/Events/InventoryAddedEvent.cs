using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcing.Library;
using EventSourcing.Server.Data;

namespace EventSourcing.Server.Events
{
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

    private readonly string name;
    private readonly decimal qty;
  }
}