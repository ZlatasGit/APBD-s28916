using System;
using System.Runtime.InteropServices; 

namespace ContainerManagerApp
{
    public class DockView{
        public void ShowMenu(List<ContainerShip> ships, List<Container> containers)
        {
            ShowShipList(ships);
            ShowContainerList(containers);
            Console.WriteLine("Choose an action:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Add a container ship");
            if (ships.Count==0)
            {
                return;
            }
            Console.WriteLine("2. Remove a container ship");
            Console.WriteLine("3. Add a container");
            if (containers.Count == 0)
            {
                return;
            }
            Console.WriteLine("4. Remove a container");
            Console.WriteLine("5. Remove a container from the ship");
            Console.WriteLine("6. Load a container onto a ship");
            Console.WriteLine("7. Unload a container");
            Console.WriteLine("8. Show containers on a ship");
            Console.WriteLine("9. Load cargo on container");

        }

        public string ReadInput([Optional]string promptMessage)
        {
            if(!string.IsNullOrWhiteSpace(promptMessage))
            {
                Console.WriteLine(promptMessage);
            }
            Console.WriteLine("->");
            string UserInput = Console.ReadLine().ToLower().Trim();
            return UserInput ?? throw new IOException("no input found");
        }
        private double ReadPositiveDouble(string message)
        {
            double? num = null;
            while(num == null)
            {
                try
                {
                    num = Double.Parse(ReadInput(message));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be in floating point form. (e.g. 1,0)");
                }
            }
            return (double)num;
        }
        private bool ReadBool(string message)
        {
            int num = -1;
            while(num != 0 && num!= 1)
            {
                try
                {
                    num = Int32.Parse(ReadInput(message));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input must be 0 for false, 1 for true.");
                }
            }
            if (num == 1)
            {
                return true;
            }
            return false;
        }
        

        public (char, double, double ,double, double[]) ReadContainerInfo()
        {
            double? height = null;
            double? depth = null;
            double? maxPayload = null;
            double?[] temperature = [null, null];

            string containerType = ReadInput("Enter container type (g: Gas, r: Refrigerated, l: Liquid):").ToLower().Trim();
            Console.WriteLine();
            if (!containerType.Equals("g")&&!containerType.Equals("l")&&!containerType.Equals("r"))
            {
                throw new ArgumentException("Invalid container type provided.");
            }

            height = ReadPositiveDouble("Enter height in m: ");
            depth = ReadPositiveDouble("Enter depth in m: ");
            maxPayload = ReadPositiveDouble("Enter maximum payload in kg: ");

            if (containerType.Equals("r"))
            {
                temperature[0] = ReadPositiveDouble("Enter lower temperature range in celcius");
                temperature[1] = ReadPositiveDouble("Enter upper temperature range in celcius");
                return (containerType.ToCharArray()[0], (double)height, (double)depth, (double)maxPayload, [(double)temperature[0],(double)temperature[1]]);
            }
            
            return (containerType.ToCharArray()[0], (double)height, (double)depth, (double)maxPayload, null);
        }

        public (double, bool?, string, string) ReadCargoInfo(char cargoType)
        {
            // char cargo type, double weight, bool isHazardous, string gasType, string product type
            double weigth;
            bool? isHazardous = null;
            string? gasType = null;
            string? productType = null;

            weigth = ReadPositiveDouble("Enter weight in kg: ");
            switch (cargoType)
            {
                case 'r':
                    productType = ReadInput("Provide product type: ").ToLower();
                    return (weigth, null, null, productType);
                case 'l':
                    isHazardous = ReadBool("If cargo is hazardous enter 1, otherwise enter 0");
                    return (weigth, isHazardous, null, null);
                case 'g':
                    isHazardous = ReadBool("If cargo is hazardous enter 1, otherwise enter 0");
                    gasType = ReadInput("Provide the gas type: (supported gases: chlor, neon, hydrogen, helium)");
                    return (weigth, isHazardous, gasType, null);
            }
            return (weigth, isHazardous, gasType, productType);
        }

        public (int, int, double) ReadShipInfo()
        {
            // int maxSpeed, int maxContainerAmount, double maxWeight
            int maxSpeed;
            int maxContainerCount;
            double maxPayload;

            maxPayload = ReadPositiveDouble("Enter max payload in kg:");
            maxSpeed = (int)ReadPositiveDouble("Enter max speed in knots: ");
            maxContainerCount = (int)ReadPositiveDouble("Enter max amount of containers on this ship: ");
            
            return(maxSpeed, maxContainerCount, maxPayload);
        }

        public void ShowContainerList(List<Container> containers)
        {
            Console.WriteLine("List of containers: ");
            if (containers.Count==0)
            {
                Console.WriteLine("none");
            }
            foreach (Container container in containers)
            {
                Console.WriteLine(container.Info());
            }
            Console.WriteLine();
            
        }
        private void ShowShipList(List<ContainerShip> ships)
        {
            Console.WriteLine("List of ships: ");
            if (ships.Count==0)
            {
                Console.WriteLine("none");
            }
            foreach (ContainerShip ship in ships)
            {
                Console.WriteLine(ship.Info());
            }
            Console.WriteLine();
        }
        public void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message+"\n");
            Console.ResetColor();
        }
        public void ShowException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message+"\n");
            Console.ResetColor();
        }
    }
}