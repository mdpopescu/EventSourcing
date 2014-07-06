namespace EventSourcing.Library
{
  public interface ServiceLocator
  {
    T Get<T>(string name = null);
  }
}