namespace ContainerManagerApp
{
    public class RefrigeratedContainer : Container
    {
        public RefrigeratedContainer(int serialNumber, double height, double depth, double maxPayload, double[] temperature) 
        : base(serialNumber, height, depth, maxPayload)
        {
            SerialNumber = GenerateSerialNumber("r", serialNumber);
            MaintainedTemperature = temperature;
        }

        private string ProductType = "";
        private double[] MaintainedTemperature {get;}

        public override bool LoadCargo(Cargo cargo)
        {
            //check if cargo is of correct type
            if(!cargo.GetType().Equals("ProduceCargo")) {
                throw new InvalidCastException("Cannot load "+cargo.GetType()+" into Refrigerated container.");
            }
            ProduceCargo produceCargo = (ProduceCargo)cargo;
            if (CargoWeight+produceCargo.GetWeight()>MaxPayload)
            {
                throw new OverfillException("Cargo weight exceeds the maximum payload");
            } else if (!ProductType.Equals(produceCargo.GetProductType()))
            {
                throw new UnsupportedProductTypeException(
                    "Cargo of type "+produceCargo.GetProductType()+" cannot be loaded into container with "+ProductType+" cargo."
                );
            } else if (ProductType.Equals("")
                &&(produceCargo.GetStorageTemperature()<MaintainedTemperature[0]
                ||produceCargo.GetStorageTemperature()>MaintainedTemperature[1]))
            {
                throw new UnsupportedProductTypeException(
                    "Cargo of type "+produceCargo.GetProductType()+" cannot be stored in temperature range "
                    +MaintainedTemperature[0]+" ~ "+MaintainedTemperature[1]
                );
            }
            ProductType = produceCargo.GetProductType();
            CargoWeight+= produceCargo.GetWeight();
            return true;
        }

        public override void EmptyCargo()
        {
            CargoWeight = 0;
            ProductType = "";
        }

        public override string Info()
        {
            string Info = "Available capacity = "+(MaxPayload-CargoWeight)+" kg, ";
            if (!ProductType.Equals("")){
                Info+="Product stored = "+ProductType+", ";
            } 
            Info+="Temperature range maintained = "+MaintainedTemperature[0]+" ~ "+MaintainedTemperature[1];
            return SerialNumber+"( "+Info+" )";
        }
    }
}