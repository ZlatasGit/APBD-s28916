namespace ContainerManagerApp{
    public class GasContainer : Container, IHazardNotifier
    {
        public GasContainer(int serialNumber, double cargoMass, double height, double weight, double depth, double maxPayload, double pressure) : base(serialNumber, cargoMass, height, weight, depth, maxPayload)
        {
            SetSerialNumber("G", serialNumber);
            IsHazardous = false;
            Pressure = pressure;
        }

        private bool IsHazardous;
        //atm
        private double Pressure;


        public override void EmptyCargo()
        {
            CargoWeight*=0.05;
        }

        public void NotifyHazard(string message)
        {
            IsHazardous = true;
            throw new HazardException("Container "+SerialNumber+message);
        }
    }
}