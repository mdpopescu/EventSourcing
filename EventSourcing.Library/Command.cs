using ProtoBuf;

namespace EventSourcing.Library
{
  [ProtoContract]
  public class Command : CommandEventBase
  {
    public virtual Event Process(ServiceLocator locator)
    {
      return null;
    }
  }
}