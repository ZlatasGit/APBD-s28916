namespace ContainerManagerApp
{
    public class ProduceCargo : Cargo
    {
        public ProduceCargo(double weight, string productType) : base(weight)
        {
            ProductType = productType;
            StorageTemperature = ProductTypeRepo.FindStorageTemperature(ProductType);
        }

        private bool IsHazardous {get;}
        private string ProductType {get;}
        private double StorageTemperature {get;}


        public bool GetIsHazardous() => IsHazardous;
        public string GetProductType() => ProductType;
        public double GetStorageTemperature() => StorageTemperature;
    }

}