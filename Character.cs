using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExperienceCalculator
{
    class Character
    {
        string firstName { get; set; }
        string lastName { get; set; }
        int currentLevel { get; set; }
        int battleCount { get; set; }
        int victoryCount { get; set; }
        double currentBattleEXP { get; set; }

        //default constructor
        public Character()
        {

        }

        //parameterized constructor
        public Character(string thisFirstName, string thisLastName, int thisLevel, int thisBattle, int thisVictory, double thisEXP)
        {
            firstName = thisFirstName;
            lastName = thisLastName;
            currentLevel = thisLevel;
            battleCount = thisBattle;
            victoryCount = thisVictory;
            currentBattleEXP = thisEXP;
        }

        public void Display()
        {
            Console.WriteLine("    {0,-8}{1,11}    {2,9}    {3,6}    {4,6}    {5,10}", 
                firstName, lastName, currentLevel.ToString(), battleCount.ToString(), victoryCount.ToString(), currentBattleEXP.ToString());
        }

        public void addExperience()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"\n\nAdding experience points to the shown character:\n\n\t {firstName} {lastName}\n");
                    Console.WriteLine("What action did they do for EXP gain?\n---------------------------\n" +
                        "    0.) Go Back\n    1.) Attacked Enemy\n    2.) Other Action");
                    int menuResponse = Convert.ToInt32(Console.ReadLine());

                    switch(menuResponse)
                    {
                        case 0:
                            return;
                        case 1:
                            try
                            {
                                bool hitEnemy;
                                int enemyLevel;
                                int levelDifference;
                                double experienceGained;

                                Console.WriteLine("Did the attack hit the enemy? (Enter number) \n    1.) Yes\n    2.) No");
                                switch(Convert.ToInt32(Console.ReadLine()))
                                {
                                    case 1:
                                        hitEnemy = true;
                                        battleCount++;
                                        break;
                                    case 2:
                                        hitEnemy = false;
                                        battleCount++;
                                        Console.WriteLine($"    {firstName} gained no experience.\n    Still at {currentBattleEXP} EXP." +
                                        $"\n    Battle count is now {battleCount}.\n    Victory count is at {victoryCount}." +
                                        $"\n\nPress any key to move on.");
                                        Console.ReadKey();
                                        return;
                                    default:
                                        Console.WriteLine("Invalid entry.\nPress any key to continue.");
                                        Console.ReadKey();
                                        return;
                                }

                                if(hitEnemy == true)
                                {
                                    Console.WriteLine("Did the attack kill (or capture) the enemy? (Enter number) \n    1.) Yes\n    2.) No");
                                    switch (Convert.ToInt32(Console.ReadLine()))
                                    {
                                        case 1:
                                            //Killed enemy is true
                                            //get enemy level, give kill exp, victory++, then return
                                            try
                                            {
                                                Console.WriteLine("Enemy Level? (Enter number)");
                                                enemyLevel = Convert.ToInt32(Console.ReadLine());
                                            }
                                            catch(Exception)
                                            {
                                                Console.WriteLine("Invalid entry.\nPress any key to continue.");
                                                Console.ReadKey();
                                                return;
                                            }

                                            levelDifference = enemyLevel - currentLevel;

                                            if(levelDifference >= 0)
                                            {
                                                experienceGained = (30 + (Math.Pow(levelDifference, 1.575)));
                                                victoryCount++;
                                            }
                                            else
                                            {
                                                experienceGained = (30 - (Math.Pow(Math.Abs(levelDifference), 1.6)));
                                            }

                                            experienceGained = Math.Round(experienceGained, 2);

                                            if (experienceGained < 0)
                                            {
                                                experienceGained = 1;
                                            }

                                            if(experienceGained > 100)
                                            {
                                                experienceGained = 100;
                                            }

                                            currentBattleEXP += experienceGained;

                                            Console.WriteLine($"    {firstName} gained {experienceGained} experience point(s)!" +
                                                $"\n    Now at {currentBattleEXP} EXP." +
                                                $"\n    Battle count is now {battleCount}." +
                                                $"\n    Victory count is at {victoryCount}." +
                                                $"\n\nPress any key to move on.");
                                            Console.ReadKey();

                                            return;
                                        case 2:
                                            //killed enemy is false
                                            //get enemy level, give ATTACK exp, then return
                                            try
                                            {
                                                Console.WriteLine("Enemy Level? (Enter number)");
                                                enemyLevel = Convert.ToInt32(Console.ReadLine());
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Invalid entry.\nPress any key to continue.");
                                                Console.ReadKey();
                                                return;
                                            }

                                            levelDifference = enemyLevel - currentLevel;

                                            if (levelDifference >= 0)
                                            {
                                                experienceGained = (5 + (Math.Pow(levelDifference, 1.125)));
                                            }
                                            else
                                            {
                                                experienceGained = (5 - (Math.Pow(Math.Abs(levelDifference), 0.975)));
                                            }

                                            experienceGained = Math.Round(experienceGained, 2);

                                            if (experienceGained < 0)
                                            {
                                                experienceGained = 0;
                                            }

                                            if (experienceGained > 100)
                                            {
                                                experienceGained = 100;
                                            }

                                            currentBattleEXP += experienceGained;

                                            Console.WriteLine($"    {firstName} gained {experienceGained} experience point(s)!" +
                                                $"\n    Now at {currentBattleEXP} EXP." +
                                                $"\n    Battle count is now {battleCount}." +
                                                $"\n    Victory count is at {victoryCount}." +
                                                $"\n\nPress any key to move on.");
                                            Console.ReadKey();
                                            return;
                                        default:
                                            Console.WriteLine("Invalid entry.\nPress any key to continue.");
                                            Console.ReadKey();
                                            return;
                                    }
                                }
                                
                            }
                            catch(Exception)
                            {
                                Console.WriteLine("Invalid entry. Please try again.\nPress any key to continue.");
                                Console.ReadKey();
                            }
                            break;
                        case 2:
                            Console.WriteLine($"How much EXP did {firstName} gain from this action?\nEnter a number:");
                            try
                            {
                                double experienceGained = Convert.ToDouble(Console.ReadLine());
                                currentBattleEXP += experienceGained;

                                Console.WriteLine($"{firstName} gained {experienceGained} experience points!" +
                                    $"\nNow at {currentBattleEXP} EXP." +
                                    $"\nBattle count is at {battleCount}." +
                                    $"\nVictory count is at {victoryCount}" +
                                    $"\nPress any key to move on...");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid entry. Please enter a number." +
                                    "\nPress any key to move on...");
                                Console.ReadLine();
                                break;
                            }
                            Console.ReadKey();
                            return;
                        default:
                            Console.WriteLine("Invalid entry, please enter a valid menu number.\nPress any key to move on.");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        public void changeValues()
        {
            int changeWhatResponse = -1;
            double newEXP;
            int newLevel;
            int newBattleCount;
            int newVictoryCount;
            string newFirstName;
            string newLastName;

            Console.WriteLine($"Looking to change values for the character:\n\t{firstName} {lastName}\n");
            Console.WriteLine("    0.) Go Back\n    1.) Experience\n    2.) Level\n    3.) Battle Count\n    4.) Victory Count\n    5.) Name\n" +
                "Change what? (enter #)");

            if (Int32.TryParse(Console.ReadLine(), out changeWhatResponse))
            {
                switch(changeWhatResponse)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine($"What is the new experience amount for {firstName}? \n(Type a number, or any negative number to cancel)" +
                            $"\n\tPlease note that decimal numbers are supported.");
                        if((Double.TryParse(Console.ReadLine(), out newEXP)))
                        {
                            if(newEXP <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Changing the EXP for {firstName} from {currentBattleEXP} EXP to {newEXP} EXP." +
                                    $"\nType 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if(confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    currentBattleEXP = newEXP;
                                    Console.WriteLine($"{firstName} now has an EXP amount of {currentBattleEXP}.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid number.");
                        }
                        break;
                    case 2:
                        Console.WriteLine($"What is the new level for {firstName}? \n(Type a whole number, or any negative number to cancel)");
                        if(Int32.TryParse(Console.ReadLine(), out newLevel))
                        {
                            if(newLevel <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Changing the level for {firstName} from {currentLevel} to {newLevel}." +
                                    $"\nType 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    currentLevel = newLevel;
                                    Console.WriteLine($"{firstName} is now level {currentLevel}.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid whole number.");
                        }
                        break;
                    case 3:
                        Console.WriteLine($"What is the new battle count for {firstName}? \n(Type a whole number, or any negative number to cancel)");
                        if (Int32.TryParse(Console.ReadLine(), out newBattleCount))
                        {
                            if (newBattleCount <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Changing the battle count for {firstName} from {battleCount} to {newBattleCount}." +
                                    $"\nType 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    battleCount = newBattleCount;
                                    Console.WriteLine($"{firstName} now has a battle count of {battleCount}.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid whole number.");
                        }
                        break;
                    case 4:
                        Console.WriteLine($"What is the new battle count for {firstName}? \n(Type a whole number, or any negative number to cancel)");
                        if (Int32.TryParse(Console.ReadLine(), out newVictoryCount))
                        {
                            if (newVictoryCount <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Changing the battle count for {firstName} from {victoryCount} to {newVictoryCount}." +
                                    $"\nType 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    victoryCount = newVictoryCount;
                                    Console.WriteLine($"{firstName} now has a victory count of {victoryCount}.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid whole number.");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Change what for the name?\n    0.) Cancel\n    1.) First Name\n    2.) Last Name\n    " +
                            "3.) Both First and Last\nEnter Corresponding Number:");
                        int changeWhichName;
                        
                        if(Int32.TryParse(Console.ReadLine(), out changeWhichName))
                        {
                            switch(changeWhichName)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Console.WriteLine($"What is the new first name for {firstName}?\n(Enter a word, or type 0 to cancel.");
                                    newFirstName = Console.ReadLine();
                                    if(newFirstName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Changing {firstName} to {newFirstName}. \nEnter 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            firstName = newFirstName;
                                            Console.WriteLine($"This character's first name is now {firstName}.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine($"What is the new last name for {firstName}?\n(Enter a word, or type 0 to cancel.");
                                    newLastName = Console.ReadLine();
                                    if (newLastName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Changing {lastName} to {newLastName}. \nEnter 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            lastName = newLastName;
                                            Console.WriteLine($"This character's last name is now {lastName}.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("New first name? Alternatively, type 0 to cancel");
                                    newFirstName = Console.ReadLine();
                                    
                                    if(newFirstName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("New last name?");
                                        newLastName = Console.ReadLine();

                                        Console.WriteLine($"Changing {firstName} {lastName} to {newFirstName} {newLastName}.\nType 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            firstName = newFirstName;
                                            lastName = newLastName;
                                            Console.WriteLine($"This character's name is now {firstName} {lastName}.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"{changeWhichName}.\nPress any key to move on.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid whole number.\nPress any key to move on.");
                            Console.ReadKey();
                        }
                        break;
                    default:
                        Console.WriteLine($"There is no option for the number {changeWhatResponse}. Please enter a valid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a number corresponding to a menu choice.");
            }
        }



        public string getFullName()
        {
            string fullName = firstName + " " + lastName;
            return fullName;
        }

        public int getCurrentLevel()
        {
            return currentLevel;
        }

        public int getBattleCount()
        {
            return battleCount;
        }

        public int getVictoryCount()
        {
            return victoryCount;
        }

        public double getCurrentBattleEXP()
        {
            return currentBattleEXP;
        }
    }
}
