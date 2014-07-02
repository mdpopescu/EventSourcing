using System;
using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract]
  public class Event
  {
    public static readonly Event NULL = new Event { Id = Guid.Empty };

    [ProtoMember(1)]
    public Guid Id { get; private set; }

    public Event()
    {
      Id = Guid.NewGuid();
    }

    public virtual void Handle(ServiceLocator locator)
    {
      // do nothing
    }
  }
}