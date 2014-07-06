using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Subjects;
using EventSourcing.Library;
using EventSourcing.Server.Commands;
using EventSourcing.Server.Queries;

namespace EventSourcing.Server
{
  public class Client
  {
    public IObservable<Command> Commands
    {
      get { return commands; }
    }

    public Client()
    {
      commands = new Subject<Command>();
      commandsQueue = new Queue<Command>();
    }

    public void AcceptCommands()
    {
      while (true)
      {
        try
        {
          var command = GetCommand();
          commands.OnNext(command);
        }
        catch
        {
          break;
        }
      }
    }

    //

    private readonly Subject<Command> commands;
    private readonly Queue<Command> commandsQueue;

    private Command GetCommand()
    {
      if (commandsQueue.Any())
        return commandsQueue.Dequeue();

      ShowMenu();
      var cmd = Console.ReadLine();
      return IdentifyCommand(cmd);
    }

    private static void ShowMenu()
    {
      Console.WriteLine("1. Create product (admin)");
      Console.WriteLine("2. Add inventory (admin)");
      Console.WriteLine("3. Sell");
      Console.WriteLine("4. List inventory (admin)");
      Console.WriteLine();
      Console.WriteLine("9. Create random commands");
      Console.WriteLine();
      Console.Write("Enter command: ");
    }

    private Command IdentifyCommand(string cmd)
    {
      if (cmd == "")
        throw new Exception();

      string name, qty;
      switch (cmd)
      {
        case "1":
          name = Read("Product name: ");
          return new CreateProductCommand(name);

        case "2":
          name = Read("Product name: ");
          qty = Read("Quantity: ");
          return new AddInventoryCommand(name, qty);

        case "3":
          name = Read("Product name: ");
          qty = Read("Quantity: ");
          return new SellCommand(name, qty);

        case "4":
          // this is a query
          return new ListInventoryQuery();

        case "9":
          var count = Read("Generate how many commands: ");
          GenerateRandomCommands(count);
          return Command.NULL;

        default:
          return Command.NULL;
      }
    }

    private static string Read(string message)
    {
      Console.Write(message);
      return Console.ReadLine();
    }

    private void GenerateRandomCommands(string count)
    {
      int n;
      if (!int.TryParse(count, out n))
        return;

      var rnd = new Random();
      for (var i = 0; i < n; i++)
      {
        var name = "test" + rnd.Next(9);
        var qty = rnd.Next(100).ToString(CultureInfo.InvariantCulture);

        var cmd = GenerateRandomCommand(rnd, name, qty);
        commandsQueue.Enqueue(cmd);
      }
    }

    private static Command GenerateRandomCommand(Random rnd, string name, string qty)
    {
      var index = rnd.Next(1, 4);

      switch (index)
      {
        case 1:
          return new CreateProductCommand(name);

        case 2:
          return new AddInventoryCommand(name, qty);

        case 3:
          return new SellCommand(name, qty);
      }

      return Command.NULL;
    }
  }
}