using System;

namespace Adventure_Game
{
    //Main Creature class with subclasses below it
    class Creature
    {
        public string creatureName;
        public int creatureHitPoints;

        public Creature()
        {

        }
        public Creature (string name, int hitPoints)
        {
            creatureName = name;
            creatureHitPoints = hitPoints;
        }


    }
    class Hero : Creature
    {
        string[] items;

        public Hero () : base ()
        {
            
        }
        public Hero (string name, int hitPoints ) : base (name, hitPoints)
        {
            
        }
    }

    class Goblin : Creature
    {
        public Goblin (string name, int hitPoints, int agro) : base(name, hitPoints)
        {
            name = "Goblin";
            hitPoints = 10;
            agro = 2;
        }
    }

    class lesserDemon : Creature
    {
        public lesserDemon (string name, int hitPoints, int agro) : base(name, hitPoints)
        {
            name = "Lesser Demon";
            hitPoints = 20;
            agro = 4;
        }
    }
    

    //Main Weapons class with subclasses below it
    class Weapons
    {

    }

    class Sword : Weapons
    {
        
    }

    class TwoHander : Weapons
    {
        //make sure when Two Hander is equiped have no other item in hand
    }

    //Armour class with subclasses below it
    class Armor
    {

    }

    
    class Shield : Armor
    {

    }

    class ChestPlate : Armor
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Hero you = new Hero();
            //variables
            string Cha
            Console.WriteLine("Welcome to the Start of the Adventure Game");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("What is your character's name? ");
            CharacterName = Console.ReadLine();

        }
    }
}
