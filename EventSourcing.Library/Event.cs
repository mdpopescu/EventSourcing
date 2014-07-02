using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract]
  public class Event : CommandEventBase
  {
    public virtual void Handle(ServiceLocator locator)
    {
      // do nothing
    }
  }
}