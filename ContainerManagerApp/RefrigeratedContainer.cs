namespace ContainerManagerApp
{
    public class RefrigeratedContainer : Container
    {
        public RefrigeratedContainer(int serialNumber, double cargoMass, double height, double weight, double depth, double maxPayload, string productType) : base(serialNumber, cargoMass, height, weight, depth, maxPayload)
        {
            SetSerialNumber("R", serialNumber);
            ProductType = productType;
            Temperature = ProductTypeRepo.FindStorageTemperature(productType);

        }

        private string ProductType;
        private double Temperature;

        public override void EmptyCargo()
        {
            throw new NotImplementedException();
        }
    }
}