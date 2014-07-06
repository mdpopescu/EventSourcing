using System;
using System.Collections.Generic;
using EventSourcing.Library;
using EventSourcing.Server.Data;

namespace EventSourcing.Server.Queries
{
  public class ListInventoryQuery : Query
  {
    public override Event Process(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      foreach (var product in products)
      {
        Console.WriteLine("{0,-50} {1}", product.Name, product.Quantity);
      }

      // queries don't normally generate events
      return Event.NULL;
    }
  }
}