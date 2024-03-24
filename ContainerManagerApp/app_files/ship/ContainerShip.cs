using Microsoft.AspNetCore.Identity.Data;

namespace ContainerManagerApp{
    public class ContainerShip{
        public ContainerShip(int serialNumber, int maxSpeed, int maxContainerAmount, double maxWeight)
        {
            //knots
            MaxSpeed = maxSpeed;
            MaxContainerAmount = maxContainerAmount;
            //kg
            MaxWeight = maxWeight;
            Containers = [];
            SerialNumber = "SHP-"+serialNumber;
        }
        private string SerialNumber {get;}

        public Dictionary<string,Container> Containers { get; set; }

        //knots
        public int MaxSpeed { get; }
        public int MaxContainerAmount { get; }
        //tons
        public double MaxWeight { get; }
        public double CargoWeight { get; set; }
        public string GetSerialNumber() => SerialNumber;

        public void AddContainer(Container container)
        {
            if(Containers.Count==MaxContainerAmount||(container.GetCargoWeight()+container.GetContainerWeight()+CargoWeight)>MaxWeight)
            {
                throw new OverfillException("Not enough capacity to load container onto the ship.");
            }
            Containers.Add(container.GetSerialNumber(), container);
        }
        public void AddContainers(List<Container> containers)
        {
            double totalWeight = 0;
            foreach (Container container in containers)
            {
                totalWeight+=container.GetCargoWeight();
                totalWeight+=container.GetContainerWeight();
            }
            if ((totalWeight+CargoWeight)>MaxWeight){
                throw new OverfillException("The weight of containers exceedes the weight limit of the ship.");
            } else if ((containers.Count+Containers.Count)>MaxContainerAmount){
                throw new OverfillException("Cannot load "+containers.Count+" containers. Only "
                +(MaxContainerAmount-Containers.Count)+" spots available");
            }
            foreach (Container container in containers)
            {
                Containers.Add(container.GetSerialNumber(), container);
            }
            CargoWeight+=totalWeight;
        }
        
        public Container RemoveContainer(string serialNumber)
        {
            Container container = Containers[serialNumber];
            Containers.Remove(serialNumber);
            return container;
        }
        public void UnloadContainer(string serialNumber)
        {
            Containers[serialNumber].EmptyCargo();
        }
        public Container ReplaceContainer(string oldContainer, Container newContainer)
        {
            Container container = Containers[oldContainer];
            Containers.Remove(oldContainer);
            Containers.Add(newContainer.GetSerialNumber(), newContainer);
            return container;
        }
        public string Info()
        {
            string Info = "Max speed = "+MaxSpeed+" knots, Available container capacity = "+(MaxContainerAmount-Containers.Count)+", Available weight capacity = "+MaxWeight+" kg";
            return SerialNumber+"( "+Info+" )";
        }
    }
}