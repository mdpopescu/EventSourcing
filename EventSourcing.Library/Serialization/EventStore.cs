using System;
using System.Collections.Generic;

namespace EventSourcing.Library.Serialization
{
  public interface EventStore
  {
    IObserver<Event> Events { get; }

    IEnumerable<Event> LoadEvents();
  }
}