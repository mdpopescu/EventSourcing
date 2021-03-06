﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EventSourcing.Library;
using EventSourcing.Library.Serialization;

namespace EventSourcing.Server.Services.Serialization
{
  public class BinarySerializer : EventSerializer
  {
    public IEnumerable<Event> LoadEvents(Stream file)
    {
      var bf = new BinaryFormatter();
      while (file.Position != file.Length)
      {
        yield return bf.Deserialize(file) as Event;
      }
    }

    public void Serialize(Stream file, Event ev)
    {
      if (ev == null || ev == Event.NULL)
        return;

      var bf = new BinaryFormatter();
      bf.Serialize(file, ev);
      file.Flush();
    }
  }
}