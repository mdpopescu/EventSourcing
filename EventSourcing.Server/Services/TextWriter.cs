namespace EventSourcing.Server.Services
{
  public interface TextWriter
  {
    void Write(string s);
    void WriteLine(string s);
  }
}