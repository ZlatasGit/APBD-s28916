namespace ContainerManagerApp{
    public class ContainerShip{
        public ContainerShip(int maxSpeed, int maxContainerAmount, double maxWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerAmount = maxContainerAmount;
            MaxWeight = maxWeight;
            Containers = new Dictionary<string,Container>();
        }

        public Dictionary<string,Container> Containers { get; set; }

        //knots
        public int MaxSpeed { get; }
        public int MaxContainerAmount { get; }
        //tons
        public double MaxWeight { get; }
        public double CargoWeight { get; set; }

        public void AddContainer(Container container)
        {
            if(Containers.Count<MaxContainerAmount&&(container.GetCargoWeight()+CargoWeight)<MaxWeight)
            {
                Containers.Add(container.GetSerialNumber(), container);
            }
        }

        public bool LoadContainer(string serialNumber, int cargo){
            try 
            {
                Containers[serialNumber].LoadCargo(cargo);
            } catch(OverfillException){
                Console.WriteLine("This container doesn't have enough capacity for the cargo. Choose a different container");
                return false;
            }
            return true;
        }
        public void AddContainers(List<Container> containers)
        {
            foreach (Container container in containers)
            {
                Containers.Add(container.GetSerialNumber(), container);
            }
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
        public Container ReplaceContainer(string oldContainer,Container newContainer)
        {
            Container container = Containers[oldContainer];
            Containers.Remove(oldContainer);
            Containers.Add(newContainer.GetSerialNumber(), newContainer);
            return container;
        }
    }
}