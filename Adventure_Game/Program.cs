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
        public Armor creatureArmorName;
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
        public int coinBag;
        public string[] items = new string[6];
        public Weapon wep = new Sword();
        public Armor arm = new Armor();
        public int damageDealt = 0;

        /* hero starts with 10 armor and when additional items are picked up then he gains more armor
         * base hp: 50, agro: 0 since user, name given in the beginning of program
         * starting weapon is a Sword
        */
        public Hero () : base ("", 50, 0, 10)
        {
            coinBag = 0;
            creatureWeapon = wep;
            items = new string[6];
        }
        public void AddItem(string itemToAdd)
        {
            
            items[numItems] = itemToAdd;
            numItems++;
        }
        public void ShowInventory()
        {
            //Console.WriteLine("[{0}]", string.Join(", ", yourArray));
            Console.WriteLine("{0}", string.Join(",", items));
        }


        //method call to consume item
        public void consumeItem(string item, int index)
        {
            
            if (item == "Small Potion")
            {
                this.creatureHitPoints += 10;
                numItems--;
                
            }
            if (item == "Medium Potion")
            {
                this.creatureHitPoints += 20;
                this.creatureArmor -= 1;
                numItems--;
            }
            if (item == "Large Potion")
            {
                this.creatureHitPoints += 30;
                this.creatureArmor -= 2;
                numItems--;
            }

            if (item == "Chestplate")
            {
                this.creatureArmor += 5;
            }

            //check to see if the creature's hitpoints didn't go over initial values, if so then reset to default
            if (this.creatureHitPoints > this.creatureStartingHitPoints)
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
            
            this.items[index] = "";

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
        public Weapon wep = new Boulder();
        /*
         * Golem defaults to 40 hp, 6 aggression, 15 armor
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
       
        public Weapon wep = new TwoHander();

        //pretty much will always agro after item usage
        public FinalBoss() : base("The Emperor of Palamecia, Lord Master of Hell", 100, 10, 9)
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
        public TwoHander() : base(1, 20, 0)
        {
            weaponName = "Two Hander";
        }
        //make sure when Two Hander is equiped have no other item in hand
    }

    class Boulder : Weapon
    {
        public Boulder() : base(1, 15, 0)
        {
            weaponName = "Boulder";
        }
        //make sure when Two Hander is equiped have no other item in hand
    }
    //Armour class with subclasses below it
    class Armor
    {
        public string armorName;
        public int armorValue;
        bool power;
        public Armor()
        {

        }

        class Shield : Armor
        {
            public Shield() : base()
            {
                armorName = "Shield";
                armorValue = 3;
                power = false;
            }
        }

        class Chestplate : Armor
        {
            public Chestplate() : base()
            {
                armorName = "Chestplate";
                armorValue = 5;
                power = true;
            }
        }

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
            Room room6 = new Room(L2, "Large Potion", 10, new int[] { 5 });
            rooms[6] = room6;

            //room 7 give chestplate for final boss
            LesserDemon L3 = new LesserDemon();
            Room room7 = new Room(L3, "Large Potion", 40, new int[] { 5, 8 });
            rooms[7] = room7;

            //room 8 final boss
            FinalBoss FB = new FinalBoss();
            Room room8 = new Room(FB, "The Emperor of Palamecia, Lord Master of Hell's Head", 1000000, new int[] {7, 9 });
            rooms[8] = room8;

            //to do: initialize rooms, can just hardcode if we want
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("| Welcome to the Start of the Adventure Game |");
            Console.WriteLine("|--------------------------------------------|");
            Console.WriteLine("\nWhat is your character's name? ");
            characterName = Console.ReadLine();
            you.creatureName = characterName;
            Console.WriteLine("Name is: {0} | Hp is: {1} | Armor is: {2}", you.creatureName, you.getHP(), you.creatureArmor);
            Console.WriteLine("He has a {0}\n", you.creatureWeapon.weaponName);

           
            current = room1;
            current.DisplayInformation();

            //running the game
            /*
             * The game will run as long as the Hero is alive and the gameEnd boolean isn't true
             * gameEnd is set to true if the user wants to leave the game or if the end of the Room is reached
             * 
             */
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
                        if(current.Equals(room7))
                        {
                            you.AddItem("Chestplate");
                        }
                        if(current.Equals(room4))
                        {
                            you.AddItem("Two Hander");
                        }
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
                        int inBag = -1;
                        string[] search;
                        Console.WriteLine("\nWhich item do you want to consume?");
                        itemToConsume = Console.ReadLine();
                        search = you.items;
                        inBag = Array.IndexOf(search, itemToConsume);
                        if (inBag > -1)
                        {
                            you.consumeItem(itemToConsume, inBag);
                        }
                        else
                        {
                            Console.WriteLine("That isn't in the bag!");
                        }
                        
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
                            Console.WriteLine(string.Join("|", backpackItem));
                        }
                        break;

                    case "6":
                        //Hero info, 
                        Console.WriteLine("\nHero Info: HP:{0}, ARMOR:{1}, WEAPON:{2}", you.creatureHitPoints, you.creatureArmor, you.creatureWeapon.weaponName);
                        break;
                    case "7":
                        bool isThere = false;
                        //move rooms
                        Console.WriteLine("Here are the exits for this room");
                        foreach(var ex in current.exit)
                        {
                            Console.WriteLine(string.Join(",", ex));
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
                        foreach(var ex in current.exit)
                        {
                            if(roomToMoveTo == ex)
                            {
                                isThere = true;
                            }
                        }
                        if(isThere != true)
                        {
                            Console.WriteLine("That's not one of the exit numbers for this room!");
                            break;
                        }
                        current = rooms[roomToMoveTo];
                        if (!current.roomCreature.IsAlive() && roomToMoveTo == 9)
                        {
                            Console.WriteLine("You beat The Emperor of Palamecia, Lord Master of Hell!!!");
                            Console.WriteLine("Fin.");
                            gameEnd = true;
                            break;
                        }
                        turn = 0;
                        break;
                    case "8":
                        //leave the game
                        gameEnd = true;
                        break;
                   
                }

            }
            //Message when beat the game.
            Console.WriteLine("Final Stats: {0} gp found, {1} hp left, {2} damage dealt", you.coinBag, you.creatureHitPoints, you.damageDealt);

                Console.WriteLine("Press enter to leave the game.");
            //prevents console from closing
            Console.ReadLine();
        }
    }
}
