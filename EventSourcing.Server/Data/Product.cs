using System;

namespace EventSourcing.Server.Data
{
  public class Product
  {
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Quantity { get; set; }

    public Product(string name)
    {
      Id = Guid.NewGuid();
      Name = name;
      Quantity = 0;
    }
  }
}