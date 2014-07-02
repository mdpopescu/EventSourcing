using System;
using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract(SkipConstructor = true)]
  public class Event
  {
    [ProtoMember(1)]
    public Guid Id { get; private set; }

    public Event(Guid id)
    {
      Id = id;
    }
  }
}