using System.Collections.Generic;
using System.Linq;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using EventSourcing.Server.Events;

namespace EventSourcing.Server.Commands
{
  public class SellCommand : Command
  {
    public SellCommand(string name, string qty)
    {
      this.name = name;
      this.qty = qty;
    }

    public override Event Process(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      var existing = products.FirstOrDefault(it => it.HasName(name));
      if (existing == null)
        return new UnknownProductEvent(name);

      decimal q;
      if (!decimal.TryParse(qty, out q))
        return new InvalidQuantityEvent(qty);

      if (!existing.CanSell(q))
        return new InsufficientStockEvent(name, q);

      return new SoldEvent(name, q);
    }

    //

    private readonly string name;
    private readonly string qty;
  }
}