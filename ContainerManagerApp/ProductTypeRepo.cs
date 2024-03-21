using Microsoft.AspNetCore.Mvc;

namespace ContainerManagerApp
{
    public class ProductTypeRepo
    {
        public ProductTypeRepo()
        {
            ProductDictionary = new Dictionary<string, double>();
            ProductDictionary.Add("bananas", 13.3);
            ProductDictionary.Add("chockolate", 18);
            ProductDictionary.Add("fish", 2);
            ProductDictionary.Add("meat", -15);
            ProductDictionary.Add("ice cream", -18);
            ProductDictionary.Add("frozen pizza", -30);
            ProductDictionary.Add("cheese", 7.2);
            ProductDictionary.Add("sausages", 5);
            ProductDictionary.Add("butter", 20.5);
            ProductDictionary.Add("eggs", 19);
        }
        private static Dictionary<string,double> ProductDictionary = new Dictionary<string, double>();
        

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