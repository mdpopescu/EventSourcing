using System;
using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract]
  public class CommandEventBase
  {
    [ProtoMember(1)]
    public Guid Id { get; private set; }

    [ProtoMember(2)]
    public DateTime CreatedOn { get; private set; }

    public CommandEventBase()
    {
      Id = Guid.NewGuid();
      CreatedOn = SystemSettings.Clock();
    }
  }
}