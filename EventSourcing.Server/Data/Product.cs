using System.Globalization;
using EventSourcing.Server.Services;

namespace EventSourcing.Server.Data
{
  public class Product
  {
    public Product(string name)
    {
      this.name = name;
      quantity = 0;
    }

    public void WriteTo(TextWriter writer)
    {
      writer.WriteLine(string.Format("{0,-50} {1,12:N}", name, quantity));
    }

    public bool HasName(string s)
    {
      return string.Compare(name, s, true, CultureInfo.InvariantCulture) == 0;
    }

    public bool CanSell(decimal qty)
    {
      return qty <= quantity;
    }

    public void Sell(decimal qty)
    {
      quantity -= qty;
    }

    public void Buy(decimal qty)
    {
      quantity += qty;
    }

    //

    private readonly string name;
    private decimal quantity;
  }
}