using System;

namespace EventSourcing.Server.Services
{
  public class ConsoleWriter : TextWriter
  {
    public static TextWriter Default = new ConsoleWriter();

    public void Write(string s)
    {
      Console.Write(s);
    }

    public void WriteLine(string s)
    {
      Console.WriteLine(s);
    }
  }
}