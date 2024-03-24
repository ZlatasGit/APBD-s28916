namespace ContainerManagerApp
{
    public class ProduceCargo : Cargo
    {
        public ProduceCargo(double weight, string productType) : base(weight)
        {
            ProductType = productType;
            StorageTemperature = ProductTypeRepo.FindStorageTemperature(ProductType);
        }

        private string ProductType {get;}
        private double StorageTemperature {get;}


        public string GetProductType() => ProductType;
        public double GetStorageTemperature() => StorageTemperature;
    }

}