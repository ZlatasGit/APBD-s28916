namespace ContainerManagerApp
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(int serialNumber, double cargoMass, double height, double weight, double depth, double maxPayload) : base(serialNumber, cargoMass, height, weight, depth, maxPayload)
        {
            SetSerialNumber("L",serialNumber);
        }

        private bool isHazardous;

        public override void EmptyCargo()
        {
            CargoWeight = 0;
            isHazardous = false;
        }

        public override bool LoadCargo(double cargoWeight)
        {
            if (isHazardous&&(this.CargoWeight+cargoWeight>MaxPayload*0.5))
            {
                throw new OverfillException("Container contains hazardous cargo and can only be filled up to half of its maximum capacity");
            } else if (this.CargoWeight+cargoWeight>MaxPayload*0.9)
            {
                throw new OverfillException("Cargo weight exceeds the maximum capacity");
            }
            this.CargoWeight+=cargoWeight;
            return true;
        }

        public void NotifyHazard(string message)
        {
            isHazardous = true;
            throw new HazardException("Container "+SerialNumber+message);
        }
    }
}