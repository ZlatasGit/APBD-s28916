using System.Data;
using System.Runtime.InteropServices; 

namespace ContainerManagerApp
{
    public class DockModel
    {
        public DockModel()
        {
            Containers = [];
            Ships = [];
            ContainersOnShips = [];
        }
        
        private int ShipId = 10000;
        private int ContainerId = 10000;

        private Dictionary<string,Container> Containers { get; set; }
        private Dictionary<string, ContainerShip> Ships { get; set; }
        private Dictionary<string, ContainerShip> ContainersOnShips { get; set; }


        public List<Container> GetContainers() => [.. Containers.Values];
        public List<ContainerShip> GetShips() => [.. Ships.Values];
        public ContainerShip FindShip(string serialNumber) => Ships[serialNumber];
        public Container FindContainer(string serialNumber) => Containers[serialNumber];

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
                    container = new GasContainer(ContainerId, height, depth, maxPayload); 
                    Containers.Add(container.GetSerialNumber(), container);
                    ContainerId++;
                    return container.GetSerialNumber();
                case 'r': 
                    if(temperatureRange==null)
                    {
                        throw new NoNullAllowedException("To create a Refrigerated Container the temperatureRange parameter of CreateContainer() method cannot be null.");
                    }
                    container =  new RefrigeratedContainer(ContainerId, height, depth, maxPayload, temperatureRange);
                    Containers.Add(container.GetSerialNumber(), container);
                    ContainerId++;
                    return container.GetSerialNumber();
                case 'l': 
                    container =  new LiquidContainer(ContainerId, height, depth, maxPayload);
                    Containers.Add(container.GetSerialNumber(), container);
                    ContainerId++;
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
            ContainerShip Ship = new(ShipId,maxSpeed,maxContainerAmount,maxWeight);
            Ships.Add(Ship.GetSerialNumber(), Ship);
            ShipId++;
            return Ship.GetSerialNumber();
        }
        public void RemoveShip(string serialNumber)
        {
            if(Ships.ContainsKey(serialNumber))
            {
                foreach (string containerID in ContainersOnShips.Keys.Intersect(Ships[serialNumber].GetContainersKeys()))
                {
                    TransferContainer(containerID, null);
                }
                Ships.Remove(serialNumber);
            } else {
                throw new IOException("Invalid serial number.");
            }
        }
        public void RemoveContainer(string serialNumber){
            if(Containers.ContainsKey(serialNumber))
            {
                if(ContainersOnShips.ContainsKey(serialNumber))
                {
                    ContainersOnShips[serialNumber].RemoveContainer(serialNumber);
                    ContainersOnShips.Remove(serialNumber);
                }
                Containers.Remove(serialNumber);
            }
        }
    }
}