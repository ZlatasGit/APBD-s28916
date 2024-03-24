namespace ContainerManagerApp;
public abstract class Container(int serialNumber, double height, double depth, double maxPayload)
{
    protected string SerialNumber = serialNumber.ToString();
    //kg
    protected double CargoWeight = 0;
    //cm
    protected double Height = height;
    //kg
    protected double Weight = (0.41*height*depth*4+0.41*height*height*2)*7.85/1000; 
    //cm
    protected double Depth = depth;
    //kg
    protected double MaxPayload = maxPayload;

    protected string GenerateSerialNumber(string type, int serialNumber)
    {
        return "KON-"+type+"-"+(serialNumber+1);
    }
    public abstract string Info();
    public abstract void EmptyCargo();
    public virtual bool LoadCargo(Cargo cargo)
    {
        if (CargoWeight+cargo.GetWeight()>MaxPayload)
        {
            throw new OverfillException("Cargo weight exceeds the maximum payload");
        }
        CargoWeight+=cargo.GetWeight();
        return true;
    }
    public string GetSerialNumber() => SerialNumber;
    public double GetCargoWeight() => CargoWeight;
    public double GetContainerWeight() => Weight;
    public double GetTotalWeight() => CargoWeight+Weight;

}