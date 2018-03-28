using System;

namespace Adventure_Game
{
    //Main Creature class with subclasses below it
    class Creature
    {
        public string creatureName;
        public int creatureHitPoints;
        public bool isAlive;
        public Weapons weapon;
       
        public Creature()
        {
            creatureName = "name";
            creatureHitPoints = 0;
            isAlive = true;
        }
        public Creature (string name, int hitPoints)
        {
            creatureName = name;
            creatureHitPoints = hitPoints;

        }

        public int getHP()
        {
            return this.creatureHitPoints;
        }
        
        
        //method for isAlive
        public bool IsAlive()
        {
            if (this.creatureHitPoints > 0)
                return true;
            else
                return false;
        }


        //method for fighting
        public void fight(Creature c)
        {
            while(IsAlive() && c.IsAlive())
            {
                for(int i=0; i < weapon.numSwings; i++)
                {

                }
            }
        }


    }
    class Hero : Creature
    {
        public string[] items;
        public string heroName;

        public Hero () : base ()
        {
            heroName = "";
            creatureHitPoints = 50;
        }
        public Hero (string name, int hitPoints ) : base (name, hitPoints)
        {
            heroName = name;
            hitPoints = 50;
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
        public int numSwings;
        public int magicAttack;
        public int physicalAttack;


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
            string characterName;
            Console.WriteLine("Welcome to the Start of the Adventure Game");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("What is your character's name? ");
            characterName = Console.ReadLine();
            you.heroName = characterName;
            Console.WriteLine("Name is: " + you.heroName + " Hp is: " + you.getHP());
            Console.ReadLine();
        }
    }
}
