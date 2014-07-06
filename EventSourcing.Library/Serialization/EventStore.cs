using System.Collections.Generic;

namespace EventSourcing.Library.Serialization
{
  public interface EventStore
  {
    IEnumerable<Event> LoadEvents();
    void Save(Event ev);
  }
}