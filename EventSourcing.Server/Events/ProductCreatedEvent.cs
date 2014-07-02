using System;
using System.Collections.Generic;
using EventSourcing.Library;
using EventSourcing.Server.Data;
using ProtoBuf;

namespace EventSourcing.Server.Events
{
  [ProtoContract]
  public class ProductCreatedEvent : Event
  {
    public ProductCreatedEvent(string name)
    {
      this.name = name;
    }

    public override void Handle(ServiceLocator locator)
    {
      var products = locator.Get<List<Product>>();
      products.Add(new Product(name));

      Console.WriteLine("Product " + name + " created.");
    }

    //

    [ProtoMember(2)] private readonly string name;
  }
}