using System.Collections.Generic;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using EventSourcing.Server.Services;

namespace EventSourcing.Server.Queries
{
  public class ListInventoryQuery : Query
  {
    public override Event Process(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      foreach (var product in products)
      {
        product.WriteTo(ConsoleWriter.Default);
      }

      // queries don't normally generate events
      return Event.NULL;
    }
  }
}