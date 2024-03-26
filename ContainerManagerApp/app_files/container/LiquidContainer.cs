namespace ContainerManagerApp
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(int serialNumber, double height, double depth, double maxPayload) : base(serialNumber, height, depth, maxPayload)
        {
            SerialNumber = GenerateSerialNumber("l", serialNumber);
        }

        private bool IsHazardous;

        public override void EmptyCargo()
        {
            CargoWeight = 0;
            IsHazardous = false;
        }

        public override bool LoadCargo(Cargo cargo)
        {
            //check if cargo is of correct type
            if(!cargo.GetCargoType().Equals("l")) {
                throw new InvalidCastException("Cannot load "+cargo.GetType()+" into Liquid container.");
            }
            LiquidCargo liquidCargo = (LiquidCargo)cargo;

            if (IsHazardous&&(CargoWeight+liquidCargo.GetWeight()>MaxPayload*0.5))
            {
                throw new OverfillException("Container contains hazardous cargo and can only be filled up to half of its maximum capacity");
            } else if (CargoWeight+liquidCargo.GetWeight()>MaxPayload*0.9)
            {
                throw new OverfillException("Cargo weight exceeds the maximum capacity");
            }

            CargoWeight+=liquidCargo.GetWeight();

            if (liquidCargo.GetIsHazardous())
            {
                IsHazardous = true;
                NotifyHazard("Hazardous cargo has been loaded.");
            }

            return true;
        }

        public void NotifyHazard(string message)
        {
            IsHazardous = true;
            throw new HazardException(message);
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
    }
}