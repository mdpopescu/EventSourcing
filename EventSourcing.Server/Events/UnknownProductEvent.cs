using System;
using EventSourcing.Library;

namespace EventSourcing.Server.Events
{
  public class UnknownProductEvent : Event
  {
    public UnknownProductEvent(string name)
    {
      this.name = name;
    }

    public override void Handle(ServiceLocator locator)
    {
      Console.WriteLine("[{0}] Unknown product {1}.", CreatedOn.ToString("yyyy-MM-dd HH:mm:ss"), name);
    }

    //

    private readonly string name;
  }
}