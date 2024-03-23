namespace ContainerManagerApp
{
    public class LiquidCargo(double weight, bool isHazardous) : Cargo(weight)
    {
        private bool IsHazardous { get; } = isHazardous;


        public bool GetIsHazardous() => IsHazardous;
    }

}