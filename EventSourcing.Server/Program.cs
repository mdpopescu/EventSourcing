using System;
using System.Diagnostics;
using System.IO;
using EventSourcing.Library;
using ProtoBuf;

namespace EventSourcing.Server
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      using (var ms = new MemoryStream())
      {
        var guid = Guid.NewGuid();
        var val = new Event(guid);
        Serializer.Serialize(ms, val);
        ms.Position = 0;
        var clone = Serializer.Deserialize<Event>(ms);
        Debug.Assert(guid == clone.Id);
      }
      
      using (var ms = new MemoryStream())
      {
        var val = new Command();
        var guid = val.Id;
        Serializer.Serialize(ms, val);
        ms.Position = 0;
        var clone = Serializer.Deserialize<Event>(ms);
        Debug.Assert(guid == clone.Id);
      }
    }
  }
}