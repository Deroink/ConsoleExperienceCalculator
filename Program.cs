using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleExperienceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            List<Character> listOfCharacters = new List<Character>();
            string line;

            //If the directory doesn't exist, creates it. Otherwise, it does nothing.
            System.IO.Directory.CreateDirectory(@"C:/Users/Public/Documents/CharacterData");

            //If the file doesn't exist in the path created, create the file, and add a sample line.
            if(!File.Exists("C:/Users/Public/Documents/CharacterData/CharacterData.txt"))
            {
                Console.WriteLine("CharacterDataInput.txt not found in the proper directory. \nCreating a sample file so application can continue." +
                    "\nRefer to menu options to modify data as necessary.");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/CharacterData.txt"))
                {
                    file.WriteLine("Sample Line 0 0 0 0");
                }
            }

            //Read in and create instances of character data read in for each line.
            using (StreamReader sr = new StreamReader("C:/Users/Public/Documents/CharacterData/CharacterData.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    listOfCharacters.Add(new Character(words[0], words[1], Convert.ToInt32(words[2]),
                        Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToDouble(words[5])));
                }//End While-Loop.
            }

            //Initial data display.
            displayList(listOfCharacters);

            while (true)
            {
                Console.WriteLine("\n\n-----------------------\n    0.) Exit Application\n    1.) Display All Stats\n    2.) Add EXP" +
                    "\n    3.) Change Character Values\n    4.) Add Character\n    5.) Remove Character\n    " +
                    "6.) Reset EXP, Battles, and Victories Gained This Battle" +
                    "\n-----------------------\n\nDo what? (Enter Number)");
                
                if(Int32.TryParse(Console.ReadLine(), out int startingMenuResponse))
                {
                    switch (startingMenuResponse)
                    {
                        case 0:
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
                            break;
                        case 1:
                            displayList(listOfCharacters);
                            break;
                        case 2:
                            addExperience(listOfCharacters);
                            break;
                        case 3:
                            changeValues(listOfCharacters);
                            break;
                        case 4:
                            addCharacter(listOfCharacters);
                            break;
                        case 5:
                            removeCharacter(listOfCharacters);
                            break;
                        case 6:
                            resetValues(listOfCharacters);
                            break;
                        default:
                            Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                            Console.ReadKey();
                            break;
                    }//End Switch Block.
                }
                else
                {
                    Console.WriteLine("Invalid response. \nPlease enter the number corresponding to the action you wish to do." +
                        "\nPress any key to continue.");
                    Console.ReadKey();
                }
            }
        
            void displayList(List<Character> currentList)
            {
                Console.WriteLine("\n------------------------\n\t  Name\t\t\t Level    Battles    Victories    EXP Gained\n" +
                "------------------------");
                foreach (Character thisCharacter in listOfCharacters)
                {
                    thisCharacter.Display();
                }
                Console.WriteLine("------------------------\n\nEnd of data display. Press any key to go to menu.");
                Console.ReadKey();
            }

            void addExperience(List<Character> currentList)
            {
                int MenuNumberChoice = -1;

                while(MenuNumberChoice != 0)
                {
                    Console.WriteLine("\n\n----------------------");
                    int count = 1;

                    Console.WriteLine("    0.) Go Back");
                    foreach (Character person in currentList)
                    {
                        Console.WriteLine("    " + count + ".) " + person.getFullName());
                        count++;
                    }
                    Console.WriteLine("----------------------\n\nAdd EXP to who? (Enter number of option above): ");
                    try
                    {
                        MenuNumberChoice = Convert.ToInt32(Console.ReadLine());
                        if(MenuNumberChoice == 0)
                        {
                            return;
                        }
                        else
                        {
                            currentList[MenuNumberChoice - 1].addExperience();
                            saveData(currentList);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                        Console.ReadKey();
                    }//End menu-entry try-catch block.
                }//End While-loop.
            }//End addExperience method.
            
            void changeValues(List<Character> currentList)
            {
                int MenuNumberChoice = -1;

                while(MenuNumberChoice != 0)
                {
                    Console.WriteLine("\n\n----------------------");
                    int count = 1;

                    Console.WriteLine("    0.) Go Back");
                    foreach (Character person in currentList)
                    {
                        Console.WriteLine("    " + count + ".) " + person.getFullName());
                        count++;
                    }
                    Console.WriteLine("----------------------\n\nChange values of who? (Enter number of option above): ");
                    try
                    {
                        MenuNumberChoice = Convert.ToInt32(Console.ReadLine());
                        if (MenuNumberChoice == 0)
                        {
                            return;
                        }
                        else
                        {
                            currentList[MenuNumberChoice - 1].changeValues();                
                            saveData(currentList);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                        Console.ReadKey();
                    }//End menu-entry try-catch block.
                }
                
            }

            void addCharacter(List<Character> currentList)
            {
                string firstName = "";
                string lastName = "";
                int characterLevel = 1;
                int battleCount = 0;
                int victoryCount = 0;
                double experienceAmount = 0.0;
                
                //get first name - type '-' to cancel
                while(true)
                {
                    Console.WriteLine("\n\nAdding a new character." +
                        "\n\nWhat is the first name of this new character?\n(Alternatively, type '-' to cancel.)");
                    firstName = Console.ReadLine();

                    if(firstName.Equals("-"))
                    {
                        Console.WriteLine("Cancelling action. Press any key to go back to the menu.");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
                
                //get last name - type '-' for no last name
                while(true)
                {
                    Console.WriteLine($"\n\nWhat is the last name of {firstName}?\n(Alternatively, type '-' if there is no last name.");

                    lastName = Console.ReadLine();

                    if (lastName.Equals("-"))
                    {
                        lastName = "NoLastName";
                    }
                    break;
                }

                //get level - can't be less than 1
                while (true)
                {
                    Console.WriteLine($"\n\nWhat is the level of {firstName} {lastName}?\nPlease enter a whole number, 1 or greater.");
                    if(Int32.TryParse(Console.ReadLine(), out characterLevel))
                    {
                        if(characterLevel < 1)
                        {
                            Console.WriteLine("Error: level of character cannot be less than 1.\nPlease enter a number that is 1 or greater." +
                                "\nPress any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Please enter a valid, whole number.\nPress any key to move on.");
                        Console.ReadKey();
                    }
                }

                //check if there's starting battles - can't be less than 0
                while (true)
                {
                    Console.WriteLine($"Is there a starting number of battles for {firstName}?\nType 'y' if there is.");
                    string checkForBattles = Console.ReadLine();

                    if(checkForBattles.Equals("y") || checkForBattles.Equals("Y"))
                    {
                        Console.WriteLine("How many battles?\nPlease enter a whole number, that is at least 0.");
                        if(Int32.TryParse(Console.ReadLine(), out battleCount))
                        {
                            if(battleCount < 0)
                            {
                                Console.WriteLine("Error: battle count of character cannot be less than 0.\nPlease enter a number that is 0 or greater." +
                                "\nPress any key to continue.");
                                Console.ReadKey();
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Please enter a valid, whole number.\nPress any key to move on.");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                //check if there's starting victories - can't be less than 0; can't be more than the number of battles
                while (true)
                {
                    if(battleCount < 1)
                    {
                        Console.WriteLine("\n\nBattle count is at 0, assuming no victories.\n");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\n\nAre there any starting victories for {firstName}?\nType 'y' if there are.");
                        string checkForVictories = Console.ReadLine();

                        if(checkForVictories.Equals("y") || checkForVictories.Equals("Y"))
                        {
                            Console.WriteLine($"\nHow many victories?\nEnter a number that is in the range of 0 up to {battleCount}." +
                                $"\n(Note: {battleCount} is the number of battles you previously entered.)");

                            if(Int32.TryParse(Console.ReadLine(), out victoryCount))
                            {
                                if(victoryCount < 0)
                                {
                                    Console.WriteLine("Error: victory count of character cannot be less than 0.\nPlease enter a number that is 0 or greater." +
                                        "\nPress any key to continue.");
                                    Console.ReadKey();
                                }
                                else if(victoryCount > battleCount)
                                {
                                    Console.WriteLine("Error: Victory count of character cannot be greater than their number of battles." +
                                        $"\nPlease enter a number in the range of 0 up to {battleCount}." +
                                        $"\nPress any key to continue.");

                                    Console.ReadKey();
                                }
                                else
                                {
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error: Please enter a valid, whole number.\nPress any key to move on.");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                //check if there's starting EXP
                while (true)
                {
                    Console.WriteLine($"Is there a starting number of EXP for {firstName}?\nType 'y' if there is.");
                    string checkForExp = Console.ReadLine();

                    if(checkForExp.Equals("y") || checkForExp.Equals("Y"))
                    {
                        Console.WriteLine("How much EXP? Please enter a number.\n(Note: Decimal numbers are supported.");
                        if(Double.TryParse(Console.ReadLine(), out experienceAmount))
                        {
                            if(experienceAmount < 0)
                            {
                                Console.WriteLine("Error: EXP amount cannot be less than 0. Enter an amount greater than 0." +
                                    "\nPress any key to continue.");
                                Console.ReadKey();
                            }
                            else
                            {
                                experienceAmount = Math.Round(experienceAmount, 2);
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid entry. Please enter a number.\nPress any key to continue.");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                //Adding new character to list.
                currentList.Add(new Character(firstName, lastName, characterLevel, battleCount, victoryCount, experienceAmount));

                //Saving new input/output files with new data.
                saveData(currentList);

                //Exiting function
                Console.WriteLine("Character has been added to the data.\nPress any key to display new data, and go back to menu.");
                displayList(currentList);
                Console.ReadKey();
            }

            void removeCharacter(List<Character> currentList)
            {
                while(true)
                {
                    Console.WriteLine("\n\n----------------------");
                    int count = 1;

                    Console.WriteLine("    0.) Go Back");
                    foreach (Character person in currentList)
                    {
                        Console.WriteLine("    " + count + ".) " + person.getFullName());
                        count++;
                    }

                    Console.WriteLine("----------------------\n\nRemove who? (Enter number)");

                    if(Int32.TryParse(Console.ReadLine(), out int removeWhoResponse))
                    {
                        if(removeWhoResponse == 0)
                        {
                            return;
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine($"We're going to remove \n     " + currentList[removeWhoResponse - 1].getFullName() + "\nAre you sure?" +
                                "\nEnter, in all caps, 'YES' (with all caps, too) if you are sure.");

                                string confirmRemoval = Console.ReadLine();

                                if (confirmRemoval.Equals("YES"))
                                {
                                    currentList.RemoveAt(removeWhoResponse - 1);
                                    saveData(currentList);
                                    Console.WriteLine("Removed the character. Press any key to go back to the menu.");
                                    Console.ReadKey();
                                    return;
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling action. Press any key to move on.");
                                    Console.ReadKey();
                                }
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("\nSee above error. Please enter a number that corresponds to the options you have." +
                                    "\nPress any key to move on.");
                                Console.ReadKey();
                            }
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid entry. Please enter a number corresponding to the menu selection." +
                            "\nPress any key to move on.");
                        Console.ReadKey();
                    }
                }
            }
            
            void resetValues(List<Character> currentList)
            {
                Console.WriteLine("\n\nThis will set the battle count, victory count, and EXP earned back to 0,\n" +
                    "for the purposes of clearing for the next battle.\nThis applies to EVERY character that exists on the current character list.\n" +
                    "Are you absolutely sure that you are ready to reset these values?\n\nType 'YES' (in all caps) if you are sure.");

                string confirmReset = Console.ReadLine();

                if(confirmReset.Equals("YES"))
                {
                    foreach(Character person in currentList)
                    {
                        person.resetValues();
                    }

                    saveData(currentList);

                    Console.WriteLine("\n\nAll characters' battle stats have been reset.\nPress any key to go back to the menu.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Cancelling action. Press any key to go back to the menu.");
                    Console.ReadKey();
                }
            }
            
            void saveData(List<Character> currentList)
            {
                Console.WriteLine("Saving data to a new input file output...");
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/CharacterDataInput.txt"))
                    {
                        foreach (Character person in currentList)
                        {
                            file.WriteLine(person.getFullName() + " " + person.getCurrentLevel() + " " + person.getBattleCount() + " " +
                                person.getVictoryCount() + " " + person.getCurrentBattleEXP());
                        }
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("File not saved. Author should check for errors in saving code.");
                    Console.ReadKey();
                }
                Console.WriteLine("File saved.");

                Console.WriteLine("Saving data to a text file output...");
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/CharacterDataOutput.txt"))
                    {
                        file.WriteLine("\t\t\t\tOUTPUT COPY");
                        file.WriteLine("*****************************************************************************");
                        file.WriteLine("\t  Name\t\t\tLevel    Battles    Victories    EXP Gained");
                        file.WriteLine("*****************************************************************************");


                        foreach (Character person in currentList)
                        {
                            var temp = String.Format("    {0,-20}   {1,9}    {2,5}    {3,8}    {4,12}",
                                person.getFullName(), person.getCurrentLevel(), person.getBattleCount(),
                                person.getVictoryCount(), person.getCurrentBattleEXP());
                            file.WriteLine($"{temp}");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("File not saved. Author should check for errors in saving code.");
                    Console.ReadKey();
                }
                Console.WriteLine("File saved.");
            }
            
        }//End Main function.
    }//End Program class.
}
