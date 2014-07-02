using System;
using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract]
  public class Command
  {
    [ProtoMember(1)]
    public Guid Id { get; private set; }

    public Command()
    {
      Id = Guid.NewGuid();
    }
  }
}