using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace ContainerManagerApp.Controllers
{
    public class DockController(DockModel model, DockView view)
    {
        private DockModel Model { get; set; } = model;
        private DockView View { get; set; } = view;

        public void Start()
        {
            while(true){

                StartChosenAction(GetChoice());
            }
        }
        private int GetChoice()
        {
            View.ShowMenu(Model.GetShips(), Model.GetContainers());
            string userInput = View.ReadInput("Enter the number of the action you'd like to perform:");
            int intInput = -1;
            while(intInput < 0 || intInput>FindCurrentOptionsCount())
            {
                try
                {
                    intInput = Int32.Parse(userInput);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be in integer form.");
                }
            }
            return intInput;
        }
        private void StartChosenAction(int option)
        {
            switch (option)
            {
                case 0:
                    Console.WriteLine("Exiting the program...");
                    Environment.Exit(0);
                    return;
                case 1:
                    // 1. Add a container ship
                    try
                    {
                        (int, int, double) sInfo = View.ReadShipInfo();
                        Model.AddShip(sInfo.Item1, sInfo.Item2, sInfo.Item3);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 2:
                    //2. Remove a container ship
                    try
                    {
                        Model.RemoveShip(View.ReadInput("Provide ship's full serial number:"));
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 3:
                    //3. Add a container
                    try
                    {
                        (char, double, double ,double, double[]) cInfo = View.ReadContainerInfo();
                        Model.CreateContainer(cInfo.Item1, cInfo.Item2, cInfo.Item3, cInfo.Item4, cInfo.Item5);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 4:
                    //4. Remove a container
                    try
                    {
                        Model.RemoveContainer(View.ReadInput("Provide container's full serial number:"));
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 5:
                    //5. Remove a container from the ship");
                    try
                    {
                        Model.TransferContainer(View.ReadInput("Provide container's full serial number:"), null);
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 6:
                    //6. Load a container onto a ship");
                    try
                    {
                        string container = View.ReadInput("Provide container's full serial number:");
                        string ship = View.ReadInput("Provide ship's full serial number: ");
                        Model.TransferContainer(container, Model.FindShip(ship));
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 7:
                    //7. Unload a container");
                    try
                    {
                        string container = View.ReadInput("Provide container's full serial number:");
                        Model.FindContainer(container).EmptyCargo();
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Console.WriteLine("Container has been emptied.");
                    break;
                case 8:
                    //8. Show containers on a ship");
                    try
                    {
                        string ship = View.ReadInput("Provide ship's full serial number:");
                        View.ShowContainerList(Model.FindShip(ship).GetContainers());
                        
                    }
                    catch (System.Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 9:
                    //9. Load cargo in container
                    try
                    {
                        string container = View.ReadInput("Provide container's full serial number:");
                        char containerType = container.ToCharArray()[4];
                        (double, bool?, string, string) cargoInfo = View.ReadCargoInfo(containerType);
                        switch (containerType)
                        {
                            case 'r': Model.LoadToContainer(container, new ProduceCargo(cargoInfo.Item1,cargoInfo.Item4)); break;
                            case 'l': Model.LoadToContainer(container, new LiquidCargo(cargoInfo.Item1, (bool)cargoInfo.Item2)); break;
                            case 'g': Model.LoadToContainer(container, new GasCargo(cargoInfo.Item1, (bool)cargoInfo.Item2, cargoInfo.Item3)); break;
                        }
                    }
                    catch (System.Exception)
                    {
                        
                        throw;
                    }
                    break;
                default:
                    break;
            }
        }
        private int FindCurrentOptionsCount()
        {
            int optionsCount = 1;
            if (Model.GetShips().Count>0&&Model.GetContainers().Count==0)
            {
                optionsCount = 3;
            } else if (Model.GetShips().Count>0&&Model.GetContainers().Count>0)
            {
                optionsCount = 9;
            }
            return optionsCount;
        }
    }
}
