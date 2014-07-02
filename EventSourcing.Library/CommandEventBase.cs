using System;
using System.Runtime.Serialization;

namespace EventSourcing.Library
{
  [Serializable]
  [DataContract]
  public class CommandEventBase
  {
    [DataMember(Order = 1)]
    public Guid Id { get; private set; }

    [DataMember(Order = 2)]
    public DateTime CreatedOn { get; private set; }

    public CommandEventBase()
    {
      Id = Guid.NewGuid();
      CreatedOn = SystemSettings.Clock();
    }
  }
}