namespace ContainerManagerApp;
public abstract class Container
{
 protected Container(int serialNumber, double cargoMass, double height, double weight, double depth, double maxPayload)
    {
        CargoWeight = cargoMass;
        Height = height;
        Weight = weight;
        Depth = depth;
        MaxPayload = maxPayload;
    }
    
    protected string SerialNumber = "";
    //kg
    protected double CargoWeight = 0;
    //cm
    protected double Height;
    //kg
    protected double Weight; 
    //cm
    protected double Depth;
    //kg
    protected double MaxPayload;

    protected void SetSerialNumber(string type, int serialNumber)
    {
        SerialNumber=("KON-"+type+"-"+(serialNumber+1));
    }

    public abstract void EmptyCargo();
    public virtual bool LoadCargo(double cargoWeight)
    {
        if (this.CargoWeight+cargoWeight>MaxPayload)
        {
            throw new OverfillException("Cargo weight exceeds the maximum payload");
        }
        this.CargoWeight+=cargoWeight;
        return true;
    }
    public string GetSerialNumber(){
        return SerialNumber;
    }
    public double GetCargoWeight(){
        return CargoWeight;
    }
}