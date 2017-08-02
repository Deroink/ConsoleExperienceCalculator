using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExperienceCalculator
{
    class Character
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int CurrentLevel { get; set; }
        int BattleCount { get; set; }
        int VictoryCount { get; set; }
        double CurrentBattleEXP { get; set; }
        
        /// <summary>
        /// Character parameterized constructor. Requires variables pertaining to the character's first and last name, their level, battle count,
        /// victory count, and experience amount. 
        /// </summary>
        /// <param name="thisFirstName"></param>
        /// <param name="thisLastName"></param>
        /// <param name="thisLevel"></param>
        /// <param name="thisBattle"></param>
        /// <param name="thisVictory"></param>
        /// <param name="thisEXP"></param>
        public Character(string thisFirstName, string thisLastName, int thisLevel, int thisBattle, int thisVictory, double thisEXP)
        {
            FirstName = thisFirstName;
            LastName = thisLastName;
            CurrentLevel = thisLevel;
            BattleCount = thisBattle;
            VictoryCount = thisVictory;
            CurrentBattleEXP = thisEXP;
        }// End Character parameterized constructor.


        /// <summary>
        /// Display writes out all of the variables associated with this instance of character, in a formatted manner.
        /// </summary>
        public void Display()
        {
            Console.WriteLine("    {0,-8}{1,11}    {2,9}    {3,6}    {4,6}         {5,10}", 
                FirstName, LastName, CurrentLevel.ToString(), BattleCount.ToString(), VictoryCount.ToString(), CurrentBattleEXP.ToString());
        }// End Display function.


        /// <summary>
        /// AddExperience is called from the program class, and is used to add experience points to a character, based on what actions they performed.
        /// If they attacked an enemy, it calculates experience gained based on the level difference, and whether they killed the enemy or only hit them.
        /// The function forces a minimum amount of the character has a much higher level than the enemy, and forces a maximum if vice versa.
        /// </summary>
        public void AddExperience()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine($"\n\nAdding experience points to the shown character:\n\n\t {FirstName} {LastName}\n");
                    Console.WriteLine("What action did they do for EXP gain?\n---------------------------\n" +
                        "    0.) Go Back\n    1.) Attacked Enemy\n    2.) Other Action");
                    int menuResponse = Convert.ToInt32(Console.ReadLine());

                    switch(menuResponse)
                    {
                        case 0:
                            return;
                        case 1:
                            // An attack action was initiated against an enemy.
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
                                        // Enemy was hit
                                        hitEnemy = true;
                                        BattleCount++;
                                        break;
                                    case 2:
                                        // Enemy was not hit
                                        hitEnemy = false;
                                        BattleCount++;
                                        Console.WriteLine($"    {FirstName} gained no experience.\n    Still at {CurrentBattleEXP} EXP." +
                                        $"\n    Battle count is now {BattleCount}.\n    Victory count is at {VictoryCount}." +
                                        $"\n\nPress any key to move on.");
                                        Console.ReadKey();
                                        return;
                                    default:
                                        Console.WriteLine("Invalid entry.\nPress any key to continue.");
                                        Console.ReadKey();
                                        return;
                                }
                                
                                // If the enemy was not hit, it skips over this code block.
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

                                            levelDifference = enemyLevel - CurrentLevel;

                                            if(levelDifference >= 0)
                                            {
                                                experienceGained = (30 + (Math.Pow(levelDifference, 1.575)));
                                            }
                                            else
                                            {
                                                experienceGained = (30 - (Math.Pow(Math.Abs(levelDifference), 1.6)));
                                            }

                                            experienceGained = Math.Round(experienceGained, 2);


                                            //Forcing a minimum amount of EXP gained.
                                            if (experienceGained < 0)
                                            {
                                                experienceGained = 1;
                                            }

                                            //Forcing a maximum amount of EXP gained.
                                            if(experienceGained > 100)
                                            {
                                                experienceGained = 100;
                                            }

                                            VictoryCount++;
                                            CurrentBattleEXP += experienceGained;

                                            Console.WriteLine($"    {FirstName} gained {experienceGained} experience point(s)!" +
                                                $"\n    Now at {CurrentBattleEXP} EXP." +
                                                $"\n    Battle count is now {BattleCount}." +
                                                $"\n    Victory count is at {VictoryCount}." +
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

                                            levelDifference = enemyLevel - CurrentLevel;

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

                                            CurrentBattleEXP += experienceGained;

                                            Console.WriteLine($"    {FirstName} gained {experienceGained} experience point(s)!" +
                                                $"\n    Now at {CurrentBattleEXP} EXP." +
                                                $"\n    Battle count is now {BattleCount}." +
                                                $"\n    Victory count is at {VictoryCount}." +
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
                            // EXP was gained through some other method.
                            Console.WriteLine($"How much EXP did {FirstName} gain from this action?\nEnter a number:");
                            try
                            {
                                double experienceGained = Convert.ToDouble(Console.ReadLine());
                                CurrentBattleEXP += experienceGained;

                                Console.WriteLine($"{FirstName} gained {experienceGained} experience points!" +
                                    $"\nNow at {CurrentBattleEXP} EXP." +
                                    $"\nBattle count is at {BattleCount}." +
                                    $"\nVictory count is at {VictoryCount}" +
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
                }// End try-catch
            }// End while loop.
        }// End AddExperience function.


        /// <summary>
        /// ChangeValues allows direct data manipulation of a character's variables, whether it is changing their name, or the amount of EXP they have, among
        /// other options.
        /// </summary>
        public void ChangeValues()
        {
            string newFirstName;
            string newLastName;

            Console.WriteLine($"Looking to change values for the character:\n\t{FirstName} {LastName}\n");
            Console.WriteLine("\n\n----------------------\n    0.) Go Back\n    1.) Experience\n    2.) Level\n    " +
                "3.) Battle Count\n    4.) Victory Count\n    5.) Name\n----------------------\n\n" +
                "Change what? (enter #)");

            if (Int32.TryParse(Console.ReadLine(), out int changeWhatResponse))
            {
                switch(changeWhatResponse)
                {
                    
                    case 0:
                        // Change nothing - Go back.
                        break;
                    case 1:
                        // Change EXP amount for this character.
                        Console.WriteLine($"    What is the new experience amount for {FirstName}? \n    (Type a number, or any negative number to cancel)" +
                            $"\n    (Please note that decimal numbers are supported)");
                        if((Double.TryParse(Console.ReadLine(), out double newEXP)))
                        {
                            if(newEXP <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"    Changing the EXP for {FirstName} from {CurrentBattleEXP} EXP to {newEXP} EXP." +
                                    $"\n    Type 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if(confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    CurrentBattleEXP = newEXP;
                                    Console.WriteLine($"    {FirstName} now has an EXP amount of {CurrentBattleEXP}.\n    Press any key to move on.");
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
                        // Change level for this character.
                        Console.WriteLine($"    What is the new level for {FirstName}? \n    (Type a whole number, or any negative number to cancel)");
                        if(Int32.TryParse(Console.ReadLine(), out int newLevel))
                        {
                            if(newLevel <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"    Changing the level for {FirstName} from {CurrentLevel} to {newLevel}." +
                                    $"\n    Type 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    CurrentLevel = newLevel;
                                    Console.WriteLine($"    {FirstName} is now level {CurrentLevel}.\n    Press any key to move on.");
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
                        // Change Battle count for this character.
                        Console.WriteLine($"    What is the new battle count for {FirstName}? \n    (Type a whole number, or any negative number to cancel)");
                        if (Int32.TryParse(Console.ReadLine(), out int newBattleCount))
                        {
                            if (newBattleCount <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"    Changing the battle count for {FirstName} from {BattleCount} to {newBattleCount}." +
                                    $"\n    Type 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    BattleCount = newBattleCount;
                                    Console.WriteLine($"    {FirstName} now has a battle count of {BattleCount}.\n    Press any key to move on.");
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
                        // Change victory count for this character.
                        Console.WriteLine($"    What is the new victory count for {FirstName}? \n    (Type a whole number, or any negative number to cancel)");
                        if (Int32.TryParse(Console.ReadLine(), out int newVictoryCount))
                        {
                            if (newVictoryCount <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"    Changing the victory count for {FirstName} from {VictoryCount} to {newVictoryCount}." +
                                    $"\n    Type 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    VictoryCount = newVictoryCount;
                                    Console.WriteLine($"   {FirstName} now has a victory count of {VictoryCount}.\nPress any key to move on.");
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
                        //Change first name, last name, or both, of this character.
                        Console.WriteLine("\n    0.) Cancel\n    1.) First Name\n    2.) Last Name\n    " +
                            "3.) Both First and Last\nChange what for the name?\nEnter Corresponding Number:");
                        int changeWhichName;
                        
                        if(Int32.TryParse(Console.ReadLine(), out changeWhichName))
                        {
                            switch(changeWhichName)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Console.WriteLine($"    What is the new first name for {FirstName}?\n    (Enter a word, or type 0 to cancel.)");
                                    newFirstName = Console.ReadLine();
                                    if(newFirstName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"    Changing {FirstName} to {newFirstName}. \n    Enter 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            FirstName = newFirstName;
                                            Console.WriteLine($"    This character's first name is now {FirstName}.\n    Press any key to move on.");
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
                                    Console.WriteLine($"    What is the new last name for {FirstName}?\n    (Enter a word, or type 0 to cancel.)");
                                    newLastName = Console.ReadLine();
                                    if (newLastName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"    Changing {LastName} to {newLastName}. \n    Enter 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            LastName = newLastName;
                                            Console.WriteLine($"    This character's last name is now {LastName}.\nPress any key to move on.");
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
                                    Console.WriteLine("\n\n    New first name? \n    (Alternatively, type 0 to cancel)");
                                    newFirstName = Console.ReadLine();
                                    
                                    if(newFirstName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n\n    New last name?");
                                        newLastName = Console.ReadLine();

                                        Console.WriteLine($"    Changing {FirstName} {LastName} to {newFirstName} {newLastName}.\n    Type 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            FirstName = newFirstName;
                                            LastName = newLastName;
                                            Console.WriteLine($"    This character's name is now {FirstName} {LastName}.\nPress any key to move on.");
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
                                    Console.WriteLine($"There is no option for the number {changeWhichName}.\nPress any key to move on.");
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
        }// End ChangeValues function.



        /// <summary>
        /// ResetValues sets the variables BattleCount, VictoryCount, and CurrentBattleEXP, back to 0, in order to have the user ready for the
        /// next battle they use this application for.
        /// </summary>
        public void ResetValues()
        {
            BattleCount = 0;
            VictoryCount = 0;
            CurrentBattleEXP = 0;
        }// End ResetValues function.



        // Various accessor methods follow this.


        /// <summary>
        /// GetFullName returns a string containing both the first and last name of this character.
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            string fullName = FirstName + " " + LastName;
            return fullName;
        }// End GetFullName function.


        /// <summary>
        /// GetCurrentLevel returns an int containing the current character level.
        /// </summary>
        /// <returns></returns>
        public int GetCurrentLevel()
        {
            return CurrentLevel;
        }// End GetCurrentLevel function.


        /// <summary>
        /// GetBattleCount returns an int containing the current battle count of this character.
        /// </summary>
        /// <returns></returns>
        public int GetBattleCount()
        {
            return BattleCount;
        }// End GetBattleCount function.


        /// <summary>
        /// GetVictoryCount returns an int containing the current victory count of this character.
        /// </summary>
        /// <returns></returns>
        public int GetVictoryCount()
        {
            return VictoryCount;
        }// End GetVictoryCount function.


        /// <summary>
        /// GetCurrentBattleEXP returns a double containing the current experience amount of this character.
        /// </summary>
        /// <returns></returns>
        public double GetCurrentBattleEXP()
        {
            return CurrentBattleEXP;
        }// End GetCurrentBattleEXP function.
    }
}
