using System;

namespace EventSourcing.Library
{
  public static class SystemSettings
  {
    public static Func<DateTime> Clock = () => DateTime.Now;
  }
}