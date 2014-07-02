using System;
using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract]
  public class Command
  {
    public static readonly Command NULL = new Command { Id = Guid.Empty };

    [ProtoMember(1)]
    public Guid Id { get; private set; }

    public Command()
    {
      Id = Guid.NewGuid();
    }

    public virtual Event Process(ServiceLocator locator)
    {
      return Event.NULL;
    }
  }
}