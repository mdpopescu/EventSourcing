﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EventSourcing.Library;
using EventSourcing.Server.Data;

namespace EventSourcing.Server.Events
{
  [Serializable]
  [DataContract]
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

      Console.WriteLine("[{0}] Product {1} created.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), name);
    }

    //

    [DataMember(Order = 3)] private readonly string name;
  }
}