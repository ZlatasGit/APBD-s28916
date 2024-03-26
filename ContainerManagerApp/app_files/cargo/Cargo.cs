namespace ContainerManagerApp
{
    public abstract class Cargo(double weight)
    {
        //kg
        private double Weight { get; } = weight;

        public double GetWeight() => Weight;
        public abstract string GetCargoType();
    }

}