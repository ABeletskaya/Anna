using System;

namespace ConsoleApplication1004
{

    class Program {
       public enum Ganre
        {
            Rock,
            RocknRoll,
            Metall,
            Jazz
        }

        static void Main(string[] args) 
        {
            Band rolingStones = new Band();
            Musician = new Musician[]
            {
               // ganre=Ganre.Rock
                    new Musician() { Name = "Mick Jagger"},
                new Musician() { Name = "Mick Jagger2" }
                
            },
            TechTeam = new TechSpecialist[]
            {
                new TechSpecialist() {Name = "John Smitt" },
                new TechSpecialist() {Name = "John Smitt" },
                new TechSpecialist() {Name = "John Smitt" }
            },
            
        }
    

    public class Band
    {
        public Ganre ganre { get; set; }
            public Musician[] musicians { get; set; }
            public Human[] techTeam { get; set; }
            public Producer[] producer { get; set; }
            public TechSpecialist[] TechTeam { get; set; }
            public string name { get; set; }
        }

   public abstract class Human
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }

        public abstract void DoWork();

    }

   public  class Musician: Human
    {
        public string [] instrument { get; set; }
    }

  public  class Producer:Human
    {
       public bool IsCheating { get; set; }
            public override void DoWork() {
                if (IsCheating)
                    Console.WriteLine("IsCheating");
                else
                    Console.WriteLine("Popular group");

            }
        }

    public class instrument
    {
        public string Name { get; set; }
        public void Play() 
        {
            Console.WriteLine($"Instrument is:{Name}");
        }
    }

        public class TechSpecialist : Human
        {
            public override void DoWork() {

            }
        }
}
}
