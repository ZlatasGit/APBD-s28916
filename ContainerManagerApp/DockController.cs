namespace ContainerManagerApp
{
    public class DockController
    {
        public DockController()
        {
            Containers = new Dictionary<string, Container>();
        }
        

        private int ContainerCount = 0;
        public Dictionary<string,Container> Containers { get; set; }

        public void TransferContainer(string serialNumber, ){


        }
        public string CreateContainer(char type, double cargoMass, double height, double weight, 
        double depth, double maxPayload, double pressure, string productType)
        {
            ContainerCount = 10000+Containers.Count+1;
            Container container;
            switch (type)
            {
                case 'g': 
                    container = new GasContainer(ContainerCount, cargoMass, height, weight, depth, maxPayload, pressure); 
                    Containers.Add(container.GetSerialNumber(), container);
                    return container.GetSerialNumber();
                case 'r': 
                    container =  new RefrigeratedContainer(ContainerCount, cargoMass, height, weight, depth, maxPayload, productType);
                    Containers.Add(container.GetSerialNumber(), container);
                    return container.GetSerialNumber();
                case 'l': 
                    container =  new LiquidContainer(ContainerCount, cargoMass, height, weight, depth, maxPayload);
                    Containers.Add(container.GetSerialNumber(), container);
                    return container.GetSerialNumber();
            }
            throw new IOException("Invalid type input while trying to create a container");
        }
        
    }
}