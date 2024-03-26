namespace ContainerManagerApp
{
    public class GasCargo : Cargo
    {
        public GasCargo(double weight, bool isHazardous, string gasType) : base(weight)
        {
            IsHazardous = isHazardous;
            switch(gasType.ToLower()){
                case "chlor": N = weight*1000/70.82; break;
                case "neon": N = weight*1000/40.36; break;
                case "hydrogen": N = weight*1000/2; break;
                case "helium": N = weight*1000/8; break;
                default: throw new IOException("Unsupported type of gas. supported types: chlor, neon, hydrogen, helium");
            }
        }

        private bool IsHazardous {get;}
        //chemical amount
        private double N {get;}
        public bool GetIsHazardous() => IsHazardous;
        public double GetN() => N;
        public override string GetCargoType() => "g";
    }

}