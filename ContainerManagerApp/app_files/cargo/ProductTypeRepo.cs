namespace ContainerManagerApp
{
    public class ProductTypeRepo
    {
        public ProductTypeRepo(){}
        private static Dictionary<string,double> ProductDictionary = new Dictionary<string, double>{
                { "bananas", 13.3 },
                { "chocolate", 18 },
                { "fish", 2 },
                { "meat", -15 },
                { "ice cream", -18 },
                { "frozen pizza", -30 },
                { "cheese", 7.2 },
                { "sausages", 5 },
                { "butter", 20.5 },
                { "eggs", 19 }
            };
        

        public static double FindStorageTemperature(string product)
        {
            double temp = ProductDictionary[product];
            return temp;
        }

        public static void AddTemperature(string product, double temperature){
            ProductDictionary.Add(product,temperature);
        }

        
    }
    
    

}