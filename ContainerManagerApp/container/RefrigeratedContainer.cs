namespace ContainerManagerApp
{
    public class RefrigeratedContainer : Container
    {
        public RefrigeratedContainer(int serialNumber, double cargoMass, double height, double weight, double depth, double maxPayload, double[] temperature) 
        : base(serialNumber, cargoMass, height, weight, depth, maxPayload)
        {
            SetSerialNumber("R", serialNumber);
            MaintainedTemperature = temperature;
        }

        private string ProductType = "";
        private double[] MaintainedTemperature {get;}

        public override bool LoadCargo(Cargo cargo)
        {
            if (CargoWeight+cargo.weight()>MaxPayload)
            {
                throw new OverfillException("Cargo weight exceeds the maximum payload");
            } else if (!ProductType.Equals(cargo.productType()))
            {
                throw new UnsupportedProductTypeException(
                    "Cargo of type "+cargo.productType()+" cannot be loaded into container with "+ProductType+" cargo."
                );
            } else if (ProductType.Equals("")
                &&(cargo.storageTemperature()<MaintainedTemperature[0]
                ||cargo.storageTemperature()>MaintainedTemperature[1]))
            {
                throw new UnsupportedProductTypeException(
                    "Cargo of type "+cargo.productType()+" cannot be stored in temperature range "
                    +MaintainedTemperature[0]+" ~ "+MaintainedTemperature[1]
                );
            }
            ProductType = cargo.productType();
            CargoWeight+=cargo.weight();
            return true;
        }

        public override void EmptyCargo()
        {
            CargoWeight = 0;
            ProductType = "";
        }
    }
}