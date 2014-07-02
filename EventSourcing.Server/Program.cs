using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using EventSourcing.Library;

namespace EventSourcing.Server
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var commands = new Subject<Command>();
      var events = LoadEvents().Concat(commands.Select(ProcessCommand));
      events.Subscribe(ev => ev.Handle(locator));
    }

    public static IObservable<Event> LoadEvents()
    {
      // deserialize the list of events
      return new Subject<Event>();
    }

    public static Event ProcessCommand(Command command)
    {
      var ev = command.Process(locator);
      // serialize the event
      return ev;
    }

    //

    private static readonly ServiceLocator locator = new DictionaryBasedLocator();
  }
}