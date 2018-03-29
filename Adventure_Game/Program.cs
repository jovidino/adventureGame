using System;

namespace Adventure_Game
{
    //Main Creature class with subclasses below it
    class Creature
    {
        public string creatureName;
        public int creatureStartingHitPoints;
        public int creatureHitPoints;
        //aggression on scale of 1-10
        public int creatureAgro;
        //Armor up to 20
        public int creatureArmor;
        public Weapon creatureWeapon;
        public bool specialAttack;
       
        public Creature()
        {
            creatureName = "name";
            creatureHitPoints = 1;
        }
        //base creature class
        public Creature (string name, int hitPoints, int agro, int armor)
        {
            creatureName = name;
            creatureHitPoints = hitPoints;
            creatureAgro = agro;
            creatureArmor = armor;
            creatureStartingHitPoints = hitPoints;
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

        //method call to consume item
        public void consumeItem(string item)
        {
            if(item == "small potion")
            {
                this.creatureHitPoints += 10;
            }
            if(item == "medium potion")
            {
                this.creatureHitPoints += 20;
                this.creatureArmor -= 1;
            }
            if(item == "large potion")
            {
                this.creatureHitPoints += 30;
                this.creatureArmor -= 2;
            }

            //check to see if the creature's hitpoints didn't go over initial values, if so then reset to default
            if (this.creatureHitPoints > this.creatureStartingHitPoints)
            {
                this.creatureHitPoints = this.creatureStartingHitPoints;
            }


        }

        //method for fighting
        public void fight(Creature c)
        {
            Random rnd = new Random();
            while(this.IsAlive() && c.IsAlive())
            {
                //write usage of a special attack

                Console.WriteLine();
                for(int i=0; i < creatureWeapon.numSwings; i++)
                {
                    //roll to see if fighter can even attempt to do damage 1-20 and compare to the opponent's armor value
                    int attackAttempt = rnd.Next(1, 21);
                    //check to see if the roll is greater than the creature being attacked armor value
                    if (attackAttempt > c.creatureArmor)
                    {
                        //generate damage and subtract hp
                        int dmg = rnd.Next(1,creatureWeapon.physicalAttack);
                        c.creatureHitPoints -= dmg;
                        Console.WriteLine("{0} hit {1} for {2} damage!", this.creatureName, c.creatureName, dmg);
                    }
                    else
                    {
                        Console.WriteLine("{0} couldn't hit through {1}'s armor!", this.creatureName, c.creatureName);
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

        public bool rollAgro()
        {
            Random rnd = new Random();
            int chance = rnd.Next(1, 11);
            if ((chance + this.creatureAgro) > 8)
            {
                return true;
            }
            else return false;
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
        public void AddItem(string itemToAdd)
        {
            int whereToAdd = items.Length;
            items[whereToAdd] = itemToAdd;
        }
        public void ShowInventory()
        {
            //Console.WriteLine("[{0}]", string.Join(", ", yourArray));
            Console.WriteLine("{0}", string.Join(",", items));
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
        /*
         * lesser demon defaults to 30 hp, 4 aggression, 10 armor
        */
        public LesserDemon () : base("Lesser Demon", 30, 4, 10)
        {
            creatureWeapon = wep;
        }
    }

    class Golem : Creature
    {
        public Weapon wep = new Sword();
        /*
         * skeleton defaults to 40 hp, 6 aggression, 15 armor
         * 
         */

        public Golem() : base("Golem", 40, 6, 15)
        {
            creatureWeapon = wep;
            specialAttack = true;
        }
    }

    //Final boss?
    class FinalBoss : Creature
    {
        /*
         * Idk what to make the weapon - should it be able to attack a lot for little damage each time or 1 big hit
         * pretty much will always agro after item usage
         * high hp but not much armor so that the hero is able to get damage in
         * 
         * 
         */
        //idk what to make the weapon

        //pretty much will always agro after item usage
        public FinalBoss() : base("FINAL BOSS", 100, 10, 9)
        {

        }
    }

    


    //Main Weapons class with subclasses below it
    class Weapon
    {
        public int numSwings;
        public int magicAttack;
        public int physicalAttack;
        public string weaponName;

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
            weaponName = "Dagger";
        }
    }

    class Sword : Weapon
    {
        public Sword() : base(2, 5, 0)
        {
            weaponName = "Sword";
        }
    }

    class DemonClaws : Weapon
    {
        public DemonClaws() : base (1, 8, 0)
        {
            weaponName = "Demon Claws";
        }
    }

    class TwoHander : Weapon
    {
        public TwoHander() : base(1, 14, 0)
        {
            weaponName = "Two Hander";
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

    class Room
    {
        public Creature roomCreature;
        public String itemInRoom;
        public int gp;
        public int[] exit;

        //constructor for the room
        public Room(Creature cr, String it, int g, int[] ex)
        {
            roomCreature = cr;
            itemInRoom = it;
            gp = g;
            exit = ex;
            
        }
        //method to print what's in the room
        public void DisplayInformation()
        {
            if (this.roomCreature == null)
            {
                Console.WriteLine("The room has a no creature and a {0} in it", itemInRoom);
            }
            else
            {
                Console.WriteLine("The room has a {0} and a {1} in it", roomCreature.creatureName, itemInRoom);
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Hero you = new Hero();
            //variables
            string characterName;
            bool gameEnd = false;
            string command;

            //create rooms
            Room[] rooms = new Room[8];
            Room current;
            Room room0 = new Room(null, "smallPotion", 3, new int[] { 1 });
            rooms[0] = room0;
            Goblin G1 = new Goblin();
            Room room1 = new Room(G1, "smallPotion", 10, new int[] { 0, 2 });
            rooms[1] = room1;


            //to do: initialize rooms, can just hardcode if we want

            Console.WriteLine("Welcome to the Start of the Adventure Game");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("What is your character's name? ");
            characterName = Console.ReadLine();
            you.creatureName = characterName;
            Console.WriteLine("Name is: {0} Hp is: {1}", you.creatureName, you.getHP());
            Console.WriteLine("He has a {0}", you.creatureWeapon.weaponName);

            /*
            //did this shit just to see if classes worked
            Goblin bob = new Goblin();
            Console.WriteLine("Name is: {0} Hp is: {1}", bob.creatureName, bob.creatureHitPoints);
            Console.WriteLine("He has a {0}", bob.creatureWeapon.weaponName);

            you.fight(bob);

            LesserDemon titsMcGee = new LesserDemon();
            Console.WriteLine("Name is: {0} Hp is: {1} ", titsMcGee.creatureName, titsMcGee.creatureHitPoints);
            //fought titsMcGee just to see if the dying text worked out
            you.fight(titsMcGee);
            */
            current = room1;
            current.DisplayInformation();

            //make menu
            while (you.IsAlive() && gameEnd != true)
            {
                //first check to see if there is an enemy in the room.. then check to see if it will auto agro
                if (current.roomCreature == null)
                {
                    //do nothing and continue on
                }
                else if (current.roomCreature.creatureHitPoints == 0 || current.roomCreature == null)
                {
                    //room creature is dead
                    current.roomCreature = null;
                }
                else
                {
                    if (current.roomCreature.rollAgro())
                    {
                        you.fight(current.roomCreature);
                    }
                    else
                        Console.WriteLine("The creature in the room doesn't attack but stays watching");
                    //Console.WriteLine(current.roomCreature.creatureHitPoints);
                }
                
                Console.WriteLine("menu: (1) fight, (2) exit game");
                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        if (current.roomCreature == null)
                        {
                            Console.WriteLine("The creature is dead! It cannot fight anymore!");
                        }
                        else
                            you.fight(current.roomCreature);
                        break;
                    case "2":
                        gameEnd = true;
                        break;

                }
                //if (command == "1")
                //{
                //    if (current.roomCreature == null)
                //    {
                //        Console.WriteLine("The creature is dead! It cannot fight anymore!");
                //    }
                //    else
                //        you.fight(current.roomCreature);
                //}
                //if (command == "2")
                //{
                //    gameEnd = true;
                //}
            }

            Console.WriteLine("Press enter to leave the game.");
            //prevents console from closing
            Console.ReadLine();
        }
    }
}
