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
            if(item == "Small Potion")
            {
                this.creatureHitPoints += 10;
            }
            if(item == "Medium Potion")
            {
                this.creatureHitPoints += 20;
                this.creatureArmor -= 1;
            }
            if(item == "Large Potion")
            {
                this.creatureHitPoints += 30;
                this.creatureArmor -= 2;
            }

            //check to see if the creature's hitpoints didn't go over initial values, if so then reset to default
            if (this.creatureHitPoints > this.creatureStartingHitPoints )
            {
                this.creatureHitPoints = this.creatureStartingHitPoints;
            }

            //check to see if the hero's armor value didn't go all the way down to 0 or less
            if (this.creatureArmor <= 0)
            {
                //yay you get 1 armor
                this.creatureArmor = 1;
            }

            //Now get rid of the item from the item list


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
                System.Threading.Thread.Sleep(2000);
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
            if ((chance + this.creatureAgro) > 8 && this.IsAlive())
            {
                return true;
            }
            else return false;
        }
    }
    class Hero : Creature
    {
        //Hero can have 6 items
        public int numItems = 0;
        //coins for purchasing
        public int coinBag;
        public string[] items = new string[6];
        public Weapon wep = new Sword(); 

        /* hero starts with 10 armor and when additional items are picked up then he gains more armor
         * base hp: 50, agro: 0 since user, name given in the beginning of program
         * starting weapon is a Sword
        */
        public Hero () : base ("", 50, 0, 10)
        {
            coinBag = 0;
            creatureWeapon = wep;
        }
        public void AddItem(string itemToAdd)
        {
            //int whereToAdd = items.Length;
            items[numItems] = itemToAdd;
            numItems++;
        }
        public void ShowInventory()
        {
            //Console.WriteLine("[{0}]", string.Join(", ", yourArray));
            Console.WriteLine("{0}", string.Join(",", items));
        }

        public void Purchase(string itemToBuy)
        {
            if (this.numItems == 6)
            {
                Console.WriteLine("Cannot buy any items... Get rid of some from the bag");
            }
            if (itemToBuy == "Small Potion")
            {
                this.creatureHitPoints += 10;
            }
            if (itemToBuy == "Medium Potion")
            {
                this.creatureHitPoints += 20;
                this.creatureArmor -= 1;
            }
            if (itemToBuy == "Large Potion")
            {
                this.creatureHitPoints += 30;
                this.creatureArmor -= 2;
            }
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
            string item;
            string itemToConsume;
            int roomToMoveTo = 0;
            //0 - creature can roll to agro 1 - user can go through menu
            int turn = 1;

            //create rooms -- just decided to hardcode it all
            Room[] rooms = new Room[9];
            Room current;

            Room room0 = new Room(null, "Small Potion", 0, new int[] { 1 });
            rooms[0] = room0;

            Goblin G1 = new Goblin();
            Room room1 = new Room(G1, "Small Potion", 10, new int[] { 0, 2 });
            rooms[1] = room1;

            Goblin G2 = new Goblin();
            Room room2 = new Room(G2, "nothing", 5, new int[] { 1, 3, 5 });
            rooms[2] = room2;

            LesserDemon L1 = new LesserDemon();
            Room room3 = new Room(L1, "Medium Potion", 20, new int[]{2, 4 });
            rooms[3] = room3;

            //room 4 will have a nice weap since the golem is hard to kill
            Golem GL1 = new Golem();
            Room room4 = new Room(GL1, "Large Potion", 40, new int[] { 3 });
            rooms[4] = room4;

            //room 5 just a normal room with a weak enemy
            Goblin G3 = new Goblin();
            Room room5 = new Room(G3, "nothing", 4, new int[] { 6, 7 });
            rooms[5] = room5;

            //room 6 medium room
            LesserDemon L2 = new LesserDemon();
            Room room6 = new Room(L2, "Medium Potion", 10, new int[] { 5 });
            rooms[6] = room6;

            //make a new creature for room 7




            //room 8 final boss
            FinalBoss FB = new FinalBoss();
            Room room8 = new Room(FB, "asdfasdf", 1000000, new int[] { 9 });
            rooms[8] = room8;

            //to do: initialize rooms, can just hardcode if we want
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("| Welcome to the Start of the Adventure Game |");
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("\nWhat is your character's name? ");
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
                if (current.roomCreature == null || current.roomCreature.creatureHitPoints < 1)
                {
                    //room creature is dead
                    current.roomCreature = null;
                    if (current.gp != 0)
                    {
                        Console.WriteLine("Creature defeated! You found {0} gp!", current.gp);
                        you.coinBag += current.gp;
                    }
                    current.gp = 0;
                }
                else 
                {
                    if (current.roomCreature.rollAgro() && turn == 0)
                    {
                        you.fight(current.roomCreature);
                        if (current.gp != 0)
                        {
                            Console.WriteLine("Creature defeated! You found {0} gp!", current.gp);
                            you.coinBag += current.gp;
                        }
                        current.gp = 0;
                    }
                    else
                        Console.WriteLine("The creature in the room doesn't attack but stays watching");
                    
                }
                
                Console.WriteLine("menu: (1) fight, (2) use item, (3) display what's in room, (4) pick up items, (5) backpack \n\t(6)Hero Info (7) move rooms, (8) exit game ");
                command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        //fight
                        if (current.roomCreature == null)
                        {
                            Console.WriteLine("The creature is dead! It cannot fight anymore!");
                        }
                        else
                        {
                            turn = 0;
                            you.fight(current.roomCreature);
                        }
                        break;

                    case "2":
                        //use item
                        Console.WriteLine("\nWhich item do you want to consume?");
                        itemToConsume = Console.ReadLine();
                        you.consumeItem(itemToConsume);
                        break;

                    case "3":
                        //Display information about the room to the user
                        current.DisplayInformation();
                        break;

                    case "4":
                        //add item to bag
                        item = current.itemInRoom;
                        if (item == "nothing")
                        {
                            Console.WriteLine("There are no items left in the room to take.");
                            break;
                        }
                        else
                        {
                            //add item and then change the item in the room to nothing
                            you.AddItem(item);
                            current.itemInRoom = "nothing";
                            break;
                        }

                    case "5":
                        //backpack
                        foreach(var backpackItem in you.items)
                        {
                            Console.WriteLine(string.Join("|", you.items));
                        }
                        break;

                    case "6":
                        //Hero info, 
                        Console.WriteLine("\nHero Info: HP:{0}, ARMOR:{1}, WEAPON:{2}", you.creatureHitPoints, you.creatureArmor, you.creatureWeapon.weaponName);
                        break;
                    case "7":
                        //move rooms
                        turn = 0;
                        Console.WriteLine("Here are the exits for this room");
                        foreach(var ex in current.exit)
                        {
                            Console.WriteLine(string.Join(",", current.exit));
                        }
                        try
                        {                  
                        Console.WriteLine("Which room do you want to exit to?");
                        roomToMoveTo = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Insert a proper number!");
                        }
                        current = rooms[roomToMoveTo];

                        break;
                    case "8":
                        //leave the game
                        gameEnd = true;
                        break;
                   
                }

            }

            /*todo: Refine the purchase method

             * create new creature

             */
            Console.WriteLine("Press enter to leave the game.");
            //prevents console from closing
            Console.ReadLine();
        }
    }
}
