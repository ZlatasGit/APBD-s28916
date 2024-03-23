namespace ContainerManagerApp
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(int serialNumber, double cargoMass, double height, double weight, double depth, double maxPayload) 
        : base(serialNumber, cargoMass, height, weight, depth, maxPayload)
        {
            SetSerialNumber("L",serialNumber);
        }

        private bool IsHazardous;

        public override void EmptyCargo()
        {
            CargoWeight = 0;
            IsHazardous = false;
        }

        public override bool LoadCargo(Cargo cargo)
        {
            if(!cargo.GetType().Equals("LiquidCargo")) {
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
            throw new NotImplementedException();
        }
    }
}