using System;
using EventSourcing.Library;

namespace EventSourcing.Server.Events
{
  public class InvalidQuantityEvent : Event
  {
    public InvalidQuantityEvent(string qty)
    {
      this.qty = qty;
    }

    public override void Handle(ServiceLocator locator)
    {
      Console.WriteLine("[{0}] Invalid quantity {1}.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), qty);
    }

    //

    private readonly string qty;
  }
}