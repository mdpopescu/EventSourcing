using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using EventSourcing.Library;
using EventSourcing.Server.Commands;
using EventSourcing.Server.Data;
using EventSourcing.Server.Serialization;

namespace EventSourcing.Server
{
  internal class Program
  {
    private static void Main()
    {
      serializer = new StreamSerializer();

      IObservable<Event> previousEvents;
      using (var file = new FileStream("events.buf", FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
      {
        previousEvents = LoadEvents(file);
      }

      using (var file = new FileStream("events.buf", FileMode.Append, FileAccess.Write, FileShare.None))
      {
        // the database
        var products = new List<Product>();
        locator.Register(products);

        // the commands and events streams
        var commands = new Subject<Command>();
        var newEvents = commands
          .Where(cmd => cmd != null)
          .Select(command => ProcessCommand(command, file))
          .Where(ev => ev != null);

        var events = previousEvents.Concat(newEvents);
        events.Subscribe(ev => ev.Handle(locator));

        // the main loop
        while (true)
        {
          try
          {
            var command = GetCommand();
            if (command != null)
              commands.OnNext(command);
          }
          catch
          {
            break;
          }
        }
      }
    }

    public static IObservable<Event> LoadEvents(FileStream file)
    {
      var list = serializer.LoadEvents(file).ToList();
      return list.ToObservable();
    }

    public static Event ProcessCommand(Command command, FileStream file)
    {
      var ev = command.Process(locator);

      serializer.Serialize(file, ev);

      return ev;
    }

    //

    private static readonly ServiceLocator locator = new DictionaryBasedLocator();
    private static StreamSerializer serializer;

    private static Command GetCommand()
    {
      ShowMenu();
      var cmd = Console.ReadLine();
      return IdentifyCommand(cmd);
    }

    private static void ShowMenu()
    {
      Console.WriteLine("1. Create product (admin)");
      Console.WriteLine("2. Add inventory (admin)");
      Console.WriteLine("3. List inventory (admin)");
      Console.WriteLine("4. Sell");
      Console.WriteLine();
      Console.Write("Enter command: ");
    }

    private static Command IdentifyCommand(string cmd)
    {
      if (cmd == "")
        throw new Exception();

      string name, qty;
      switch (cmd)
      {
        case "1":
          Console.Write("Product name: ");
          name = Console.ReadLine();
          return new CreateProductCommand(name);

        case "2":
          Console.Write("Product name: ");
          name = Console.ReadLine();
          Console.Write("Quantity: ");
          qty = Console.ReadLine();
          return new AddInventoryCommand(name, qty);

        case "3":
          // we *could* use a command / event pair here, in case we want to know when this command was invoked
          // in this case, though, that's not necessary
          ListInventory();
          return new Command();

        case "4":
          Console.Write("Product name: ");
          name = Console.ReadLine();
          Console.Write("Quantity: ");
          qty = Console.ReadLine();
          return new SellCommand(name, qty);

        default:
          return null;
      }
    }

    private static void ListInventory()
    {
      var products = locator.Get<List<Product>>();
      foreach (var product in products)
      {
        Console.WriteLine("{0,-50} {1}", product.Name, product.Quantity);
      }
    }
  }
}