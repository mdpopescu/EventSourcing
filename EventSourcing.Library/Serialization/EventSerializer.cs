using System.Collections.Generic;
using System.IO;

namespace EventSourcing.Library.Serialization
{
  public interface EventSerializer
  {
    IEnumerable<Event> LoadEvents(Stream file);
    void Serialize(Stream file, Event ev);
  }
}