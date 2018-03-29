using System;

namespace Adventure_Game
{
    //Main Creature class with subclasses below it
    class Creature
    {
        public string creatureName;
        public int creatureHitPoints;
        //aggression on scale of 1-10
        public int creatureAgro;
        //
        public int creatureArmor;
        public Weapon creatureWeapon;
       
        public Creature()
        {
            creatureName = "name";
            creatureHitPoints = 1;
        }
        public Creature (string name, int hitPoints, int agro, int armor)
        {
            creatureName = name;
            creatureHitPoints = hitPoints;
            creatureAgro = agro;
            creatureArmor = armor;
            
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
            Random rnd = new Random();
            while(this.IsAlive() && c.IsAlive())
            {
                for(int i=0; i < creatureWeapon.numSwings; i++)
                {
                    //roll to see if fighter can even attempt to do damage 1-20 and compare to the opponent's armor value
                    int attackAttempt = rnd.Next(1, 21);
                    //check to see if the roll is greater than the creature being attacked armor value
                    if (attackAttempt > c.creatureArmor)
                    {
                        int dmg = rnd.Next(1,creatureWeapon.physicalAttack);
                        c.creatureHitPoints -= dmg;
                        Console.WriteLine("{0} hit {1} for {2} damage!", this.creatureName, c.creatureName, dmg);
                    }
                    else
                    {
                        Console.WriteLine("{0} couldn't hit through {1}'s armour!", this.creatureName, c.creatureName);
                    }
                    
                }
                //Random sleep to put in if the user wants to see the progression
                //System.Threading.Thread.Sleep(4000);
                if (c.IsAlive())
                    c.fight(this);
                else
                {
                    //Hero wins the fight
                    if (this is Hero)
                    {
                        Console.WriteLine("The fight is over! {0} won! and has {1} HP remaining...", this.creatureName, this.creatureHitPoints);
                    }
                    //Hero dies to the creature
                    else
                    {
                        Console.WriteLine("GAME OVER");
                    }
                    
                }
            }
            


        }


    }
    class Hero : Creature
    {
        public string[] items;
        public Weapon wep = new Sword(); 

        /* hero starts with 10 armor and when additional items are picked up then he gains more armor
         * base hp: 50, agro: 0 since user, name given in the beginning of program
         * starting weapon is a Sword
        */
        public Hero () : base ("", 50, 0, 10)
        {
            creatureWeapon = wep;
        }

       
    }

    class Goblin : Creature
    {
        public Weapon wep = new Dagger();
        /*
         * goblin defaults to 15 hp, 2 aggression, 6 armor
        */
        public Goblin() : base("Goblin", 15, 2, 6)
        {
            creatureWeapon = wep;
        }
       
    }

    class LesserDemon : Creature
    {
        public Weapon wep = new DemonClaws();
        public LesserDemon () : base("Lesser Demon", 30, 4, 12)
        {
            creatureWeapon = wep;
        }
    }

    class Skeleton : Creature
    {
        public Weapon wep = new Sword();
        /*
         * skeleton defaults to 20 hp, 5 aggression, 8 armor
         * 
         */

        public Skeleton() : base("Skeleton", 20, 5, 8)
        {
            creatureWeapon = wep;
        }
    }

    


    //Main Weapons class with subclasses below it
    class Weapon
    {
        public int numSwings;
        public int magicAttack;
        public int physicalAttack;

        public Weapon(int n, int p, int m)
        {
            numSwings = n;
            physicalAttack = p;
            magicAttack = m;
        }
    }

    class Dagger : Weapon
    {
        /*
         * 3 swings, up to 4 damage
         */
        public Dagger() : base(3, 4, 0)
        {

        }
    }

    class Sword : Weapon
    {
        public Sword() : base(2, 5, 0)
        {

        }
    }

    class DemonClaws : Weapon
    {
        public DemonClaws() : base (1, 8, 0)
        {

        }
    }

    class TwoHander : Weapon
    {
        public TwoHander() : base(1, 14, 0)
        {

        }
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


            //to do: initialize rooms, can just hardcode if we want

            Console.WriteLine("Welcome to the Start of the Adventure Game");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("What is your character's name? ");
            characterName = Console.ReadLine();
            you.creatureName = characterName;
            Console.WriteLine("Name is: {0} Hp is: {1}", you.creatureName, you.getHP());
            Console.WriteLine("He has a {0}", you.creatureWeapon.ToString());
            
            //did this shit just to see if classes worked
            Goblin bob = new Goblin();
            Console.WriteLine("Name is: {0} Hp is: {1}", bob.creatureName, bob.creatureHitPoints);


            you.fight(bob);

            LesserDemon titsMcGee = new LesserDemon();
            Console.WriteLine("Name is: {0} Hp is: {1} ", titsMcGee.creatureName, titsMcGee.creatureHitPoints);

            you.fight(titsMcGee);


            //make menu



            //prevents console from closing
            Console.ReadLine();
        }
    }
}
