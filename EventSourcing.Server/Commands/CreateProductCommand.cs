using System.Collections.Generic;
using System.Linq;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using EventSourcing.Server.Events;

namespace EventSourcing.Server.Commands
{
  public class CreateProductCommand : Command
  {
    public CreateProductCommand(string name)
    {
      this.name = name;
    }

    public override Event Process(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      var existing = products.FirstOrDefault(it => it.Name == name);
      if (existing != null)
        return null; // can be replaced by an error event

      return new ProductCreatedEvent(name);
    }

    //

    private readonly string name;
  }
}