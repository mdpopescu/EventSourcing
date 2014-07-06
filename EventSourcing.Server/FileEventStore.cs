using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using EventSourcing.Library;
using EventSourcing.Library.Serialization;

namespace EventSourcing.Server
{
  public class FileEventStore : EventStore
  {
    public IObserver<Event> Events
    {
      get { return events; }
    }

    public FileEventStore(EventSerializer serializer, string path)
    {
      this.serializer = serializer;
      this.path = path;

      var file = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Read);
      events = new Subject<Event>();
      events.Subscribe(ev => serializer.Serialize(file, ev), ex => file.Dispose(), () => file.Dispose());
    }

    public IEnumerable<Event> LoadEvents()
    {
      using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
      {
        return serializer
          .LoadEvents(file)
          .ToList();
      }
    }

    //

    private readonly EventSerializer serializer;
    private readonly string path;

    private readonly Subject<Event> events;
  }
}