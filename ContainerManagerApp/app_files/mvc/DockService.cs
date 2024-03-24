using System.Data;
using System.Runtime.InteropServices; 

namespace ContainerManagerApp
{
    public class DockService
    {
        public DockService()
        {
            Containers = [];
            Ships = [];
            ContainersOnShips = [];
        }
        
        public Dictionary<string,Container> Containers { get; set; }
        public Dictionary<string, ContainerShip> Ships { get; set; }
        public Dictionary<string, ContainerShip> ContainersOnShips { get; set; }


        public void TransferContainer(string serialNumber, ContainerShip ship){
    
            if(ContainersOnShips.ContainsKey(serialNumber))
            {
                Container OldContainer = ContainersOnShips[serialNumber].RemoveContainer(serialNumber);
                ContainersOnShips.Remove(serialNumber);
            }
            if(ship!=null) 
            {
                ship.AddContainer(Containers[serialNumber]);
                ContainersOnShips.Add(serialNumber, ship);
            }
        }
        public string CreateContainer(char type, double height, double depth, double maxPayload, [Optional] double[] temperatureRange)
        {
            Container container;
            switch (type)
            {
                case 'g': 
                    container = new GasContainer(Containers.Count()+10000, height, depth, maxPayload); 
                    Containers.Add(container.GetSerialNumber(), container);

                    return container.GetSerialNumber();
                case 'r': 
                    if(temperatureRange==null)
                    {
                        throw new NoNullAllowedException("Ro create a Refrigerated Container the temperatureRange parameter of CreateContainer() method cannot be null.");
                    }
                    container =  new RefrigeratedContainer(Containers.Count()+10000, height, depth, maxPayload, temperatureRange);
                    Containers.Add(container.GetSerialNumber(), container);

                    return container.GetSerialNumber();
                case 'l': 
                    container =  new LiquidContainer(Containers.Count()+10000, height, depth, maxPayload);
                    Containers.Add(container.GetSerialNumber(), container);

                    return container.GetSerialNumber();
            }
            throw new IOException("Invalid type input while trying to create a container");
        }
        public bool LoadToContainer(string serialNumber, Cargo cargo){
            bool IsLoaded = false;
            try 
            {
                IsLoaded = Containers[serialNumber].LoadCargo(cargo);
            } catch(OverfillException o){
                Console.WriteLine(serialNumber+": "+o.Message);
            } catch(HazardException h){
                Console.WriteLine(serialNumber+": "+h.Message);
            } catch(UnsupportedProductTypeException u){
                Console.WriteLine(serialNumber+": "+u.Message);
            } catch(InvalidCastException i){
                Console.WriteLine(serialNumber+": "+i.Message);
            }
            return IsLoaded;
        }
        public string AddShip( int maxSpeed, int maxContainerAmount, double maxWeight){
            ContainerShip Ship = new(Ships.Count+1000,maxSpeed,maxContainerAmount,maxWeight);
            Ships.Add(Ship.GetSerialNumber(), Ship);
            return Ship.GetSerialNumber();
        }
        
    }

}