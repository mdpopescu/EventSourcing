using System.Collections.Generic;
using System.Linq;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using EventSourcing.Server.Events;
using ProtoBuf;

namespace EventSourcing.Server.Commands
{
  [ProtoContract]
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

      products.Add(new Product(name));
      return new ProductCreatedEvent(name);
    }

    //

    [ProtoMember(2)] private readonly string name;
  }
}