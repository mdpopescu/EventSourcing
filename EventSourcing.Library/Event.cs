using System;
using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract(SkipConstructor = true)]
  public class Event
  {
    public Guid Id
    {
      get { return id; }
    }

    public Event(Guid id)
    {
      this.id = id;
    }

    //

    [ProtoMember(1)] private readonly Guid id;
  }
}