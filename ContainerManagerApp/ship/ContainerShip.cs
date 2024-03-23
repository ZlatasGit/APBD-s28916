namespace ContainerManagerApp{
    public class ContainerShip{
        public ContainerShip(int serialNumber, int maxSpeed, int maxContainerAmount, double maxWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerAmount = maxContainerAmount;
            MaxWeight = maxWeight;
            Containers = new Dictionary<string,Container>();
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

        public void AddContainer(Container container)
        {
            if(Containers.Count==MaxContainerAmount||(container.cargoWeight()+container.containerWeight()+CargoWeight)>MaxWeight)
            {
                throw new OverfillException("Not enough capacity to load container onto the ship.");
            }
            Containers.Add(container.SerialNumber(), container);
        }
        public void AddContainers(List<Container> containers)
        {
            double totalWeight = 0;
            foreach (Container container in containers)
            {
                totalWeight+=container.cargoWeight();
                totalWeight+=container.containerWeight();
            }
            if ((totalWeight+CargoWeight)>MaxWeight){
                throw new OverfillException("The weight of containers exceedes the weight limit of the ship.");
            } else if ((containers.Count+Containers.Count)>MaxContainerAmount){
                throw new OverfillException("Cannot load "+containers.Count+" containers. Only "
                +(MaxContainerAmount-Containers.Count)+" spots available");
            }
            foreach (Container container in containers)
            {
                Containers.Add(container.SerialNumber(), container);
            }
            CargoWeight+=totalWeight;
        }

        public bool LoadContainer(string serialNumber, Cargo cargo){
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
            Containers.Add(newContainer.SerialNumber(), newContainer);
            return container;
        }
    }
}