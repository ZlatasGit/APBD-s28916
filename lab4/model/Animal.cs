namespace lab4
{
    class Animal
    {
        public Guid Id {get; set;} = Guid.NewGuid();
        public string Name {get;}
        public string Category {get;}
        public double Weight {get; set;}
        public string FurColor {get;}
        
    }
}