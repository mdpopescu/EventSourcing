using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventSourcing.Library;
using EventSourcing.Library.Serialization;
using ProtoBuf;
using ProtoBuf.Meta;

namespace EventSourcing.Server.Serialization
{
  public class ProtobufSerializer : EventSerializer
  {
    public ProtobufSerializer(Func<RuntimeTypeModel, IEnumerable<KeyValuePair<Type, int>>> registrationAction)
    {
      model = TypeModel.Create();

      var list = registrationAction(model);
      prefixes = list.ToDictionary(it => it.Key, it => it.Value);
    }

    public IEnumerable<Event> LoadEvents(Stream file)
    {
      return prefixes
        .Keys
        .SelectMany(type => LoadEventsForType(file, type))
        .Where(ev => ev != null)
        .OrderBy(ev => ev.CreatedOn)
        .ToList();
    }

    public void Serialize(Stream file, Event ev)
    {
      if (ev == null)
        return;

      var type = ev.GetType();
      model.SerializeWithLengthPrefix(file, ev, type, PrefixStyle.Base128, GetIndex(type));

      file.Flush();
    }

    //

    private readonly RuntimeTypeModel model;
    private readonly Dictionary<Type, int> prefixes;

    private int GetIndex(Type type)
    {
      return prefixes.ContainsKey(type) ? prefixes[type] : 0;
    }

    private IEnumerable<Event> LoadEventsForType(Stream file, Type type)
    {
      file.Position = 0;
      while (file.Position < file.Length)
      {
        yield return model.DeserializeWithLengthPrefix(file, null, type, PrefixStyle.Base128, GetIndex(type)) as Event;
      }
    }
  }
}