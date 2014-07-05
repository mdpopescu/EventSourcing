using System.Collections.Generic;
using System.Linq;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using EventSourcing.Server.Events;

namespace EventSourcing.Server.Commands
{
  public class AddInventoryCommand : Command
  {
    public AddInventoryCommand(string name, string qty)
    {
      this.name = name;
      this.qty = qty;
    }

    public override Event Process(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      var existing = products.FirstOrDefault(it => it.Name == name);
      if (existing == null)
        return new UnknownProductEvent(name);

      decimal q;
      q = decimal.Parse(qty);
      //if (!decimal.TryParse(qty, out q))
      //  return new InvalidQuantityEvent(qty);

      return new InventoryAddedEvent(name, q);
    }

    //

    private readonly string name;
    private readonly string qty;
  }
}