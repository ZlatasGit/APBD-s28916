namespace ContainerManagerApp{
    public class GasContainer : Container, IHazardNotifier
    {
        public GasContainer(int serialNumber, double height, double depth) 
        {
            SerialNumber = GenerateSerialNumber("G", serialNumber);
            Height = height;
            Depth = depth;
            IsHazardous = false;
            //liters
            Volume = depth*height*height/100;
            //atm
            Pressure = 1;
            CargoWeight = Pressure*101325*Volume*28.97/(8.3145*273.15);
            //mass of air at 5 atm
            MaxPayload = 5*101325*Volume*28.97/(8.3145*273.15);
        }

        private bool IsHazardous;
        //atm
        private double Pressure;
        private readonly double Volume;


        public override bool LoadCargo(Cargo cargo)
        {
            if (!cargo.GetType().Equals("GasCargo"))
            {
                throw new InvalidCastException("Cannot load " + cargo.GetType() + " into Liquid container.");
            }
            GasCargo gasCargo = (GasCargo)cargo;

            if (CargoWeight + gasCargo.GetWeight() > MaxPayload)
            {
                throw new OverfillException("Cargo weight exceeds the maximum payload");
            }
            if (!IsHazardous && gasCargo.GetIsHazardous())
            {
                NotifyHazard("Hazardous cargo has been loaded.");
            }
            else if (IsHazardous && !gasCargo.GetIsHazardous())
            {
                NotifyHazard("Non-hazardous cargo cannot be loaded into a container with hazardous leftovers.");
                return false;
            }
            CargoWeight += gasCargo.GetWeight();
            //we assume that gas container is always at 0 degrees celcius
            Pressure = CalculatePressure(gasCargo.GetN(), 0);
            return true;
        }

        public override void EmptyCargo()
        {
            CargoWeight*=0.05;
        }
        public override string Info()
        {
            string Info = "Available capacity="+(MaxPayload-CargoWeight)+" kg, ";
            if (IsHazardous){
                Info+="Contains hazardous cargo";
            } else {
                Info+="Doesn't contain hazardous cargo";
            }
            return SerialNumber+"( "+Info+" )";
        }
        public void NotifyHazard(string message)
        {
            IsHazardous = true;
            throw new HazardException(message);
        }
        private double CalculatePressure(double n, double celcius) => n * 8.3145 * (celcius + 273.15) / (Volume * 101325);
    }
}