using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using EventSourcing.Library;
using EventSourcing.Server.Commands;
using EventSourcing.Server.Data;

namespace EventSourcing.Server
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var commands = new Subject<Command>();
      var events = LoadEvents().Concat(commands.Select(ProcessCommand).Where(ev => ev != null));
      events.Subscribe(ev => ev.Handle(locator));

      var products = new List<Product>();
      locator.Register(products);

      while (true)
      {
        var command = GetCommand();
        if (command != null)
          commands.OnNext(command);
      }
    }

    public static IObservable<Event> LoadEvents()
    {
      // deserialize the list of events
      return Observable.Empty<Event>();
    }

    public static Event ProcessCommand(Command command)
    {
      var ev = command.Process(locator);
      // serialize the event
      return ev;
    }

    //

    private static readonly ServiceLocator locator = new DictionaryBasedLocator();

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
      Console.WriteLine("4. Add product to cart");
      Console.WriteLine("5. Checkout");
      Console.WriteLine();
      Console.Write("Enter command: ");
    }

    private static Command IdentifyCommand(string cmd)
    {
      if (cmd == "")
        Environment.Exit(0);

      string name;
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
          var qty = Console.ReadLine();
          return new AddInventoryCommand(name, qty);

        case "3":
          // we *could* use a command / event pair here, in case we want to know when this command was invoked
          // in this case, though, that's not necessary
          ListInventory();
          return new Command();

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