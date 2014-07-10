using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using EventSourcing.Library;
using EventSourcing.Library.Serialization;
using EventSourcing.Server.Data;
using EventSourcing.Server.Services.Serialization;

namespace EventSourcing.Server
{
  internal class Program
  {
    private static void Main()
    {
      serializer = new BinarySerializer();
      eventStore = new FileEventStore(serializer, "events.buf");

      // the database
      var products = new List<Product>();
      locator.Register(products);

      var client = new Client();

      // all events must be handled but only the new events should be serialized
      var newEvents = client.Commands
        .Select(command => command.Process(locator))
        .Where(ev => ev != Event.NULL)
        .Do(ev => eventStore.Save(ev));

      var oldEvents = eventStore.LoadEvents().ToList();
      var events = oldEvents
        .ToObservable()
        .Concat(newEvents);
      using (events.Subscribe(ev => ev.Handle(locator)))
      {
        Console.WriteLine("Replayed " + oldEvents.Count + " events.");
        
        client.AcceptCommands();
      }
    }

    //

    private static readonly DictionaryBasedLocator locator = new DictionaryBasedLocator();
    private static EventSerializer serializer;
    private static EventStore eventStore;
  }
}