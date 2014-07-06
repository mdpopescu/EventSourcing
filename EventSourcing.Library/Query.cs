using System;
using System.Runtime.Serialization;

namespace EventSourcing.Library
{
  [Serializable]
  [DataContract]
  public class Query : Command
  {
  }
}