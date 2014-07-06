using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using EventSourcing.Library;
using EventSourcing.Library.Serialization;
using EventSourcing.Server.Data;
using EventSourcing.Server.Serialization;

namespace EventSourcing.Server
{
  internal class Program
  {
    private static void Main()
    {
      serializer = new BinarySerializer();

      var previousEvents = LoadPreviousEvents();
      using (var file = new FileStream("events.buf", FileMode.Append, FileAccess.Write, FileShare.Read))
      {
        // the database
        var products = new List<Product>();
        locator.Register(products);

        var client = new Client();

        // only the new events should be serialized
        var newEvents = client.Commands
          .Select(command => command.Process(locator))
          .Where(ev => ev != Event.NULL)
          .Do(ev => serializer.Serialize(file, ev));

        var events = previousEvents.Concat(newEvents);
        using (events.Subscribe(ev => ev.Handle(locator), HandleError))
        {
          client.AcceptCommands();
        }
      }
    }

    //

    private static readonly DictionaryBasedLocator locator = new DictionaryBasedLocator();
    private static EventSerializer serializer;

    private static IObservable<Event> LoadPreviousEvents()
    {
      using (var file = new FileStream("events.buf", FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
      {
        return serializer
          .LoadEvents(file)
          .ToList()
          .ToObservable();
      }
    }

    private static void HandleError(Exception ex)
    {
      Console.WriteLine("Exception detected: " + ex.Message);
    }
  }
}