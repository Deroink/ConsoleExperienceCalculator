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
        /// <summary>
        /// Main Function - If there isn't a directory for the documents used, it creates it. Within said directory, assuming it
        /// does exist, if the files used for input do not exist, then it creates one. Then, it reads in data from the files used,
        /// creating a list collection out of it. Afterwards, the user is able to modify or display data found on the text file,
        /// via choosing their actions from a menu.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Setting a default window size for easier reading.
            Console.SetWindowSize(100, 30);

            // List collection for the text files that will be read in.
            List<Character> listOfCharacters = new List<Character>();

            // Instance of program class used for calling other necessary functions.
            Program programInstance = new Program();

            // Used for reading in a file line-by-line.
            string line;
            
            // If the directory doesn't exist, creates it. Otherwise, it does nothing.
            System.IO.Directory.CreateDirectory(@"C:/Users/Public/Documents/CharacterData");

            // If the file doesn't exist in the path created, create the file, and add a sample line.
            if(!File.Exists("C:/Users/Public/Documents/CharacterData/CharacterDataInput.txt"))
            {
                Console.WriteLine("CharacterDataInput.txt not found in the proper directory. \nCreating a sample file so application can continue." +
                    "\nRefer to menu options to modify data as necessary.");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/CharacterDataInput.txt"))
                {
                    file.WriteLine("Sample Line 0 0 0 0");
                }
            }

            // Read in and create instances of character data read in for each line.
            using (StreamReader sr = new StreamReader("C:/Users/Public/Documents/CharacterData/CharacterDataInput.txt"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(' ');
                    listOfCharacters.Add(new Character(words[0], words[1], Convert.ToInt32(words[2]),
                        Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToDouble(words[5])));
                }// End While-Loop.
            }

            // Initial data display.
            programInstance.DisplayList(listOfCharacters);


            // Main menu of the application, where user selects what they wish to do.
            while (true)
            {
                Console.WriteLine("\n\n-----------------------\n    0.) Exit Application\n    1.) Display All Stats\n    2.) Add EXP" +
                    "\n    3.) Change Character Values\n    4.) Add Character\n    5.) Remove Character\n    " +
                    "6.) Reset EXP, Battles, and Victories Gained This Battle" +
                    "\n-----------------------\n\nDo what? (Enter Number)");
                
                // Checks menu response to make sure it is a valid entry.
                if(Int32.TryParse(Console.ReadLine(), out int startingMenuResponse))
                {
                    // Calls appropriate function based on what the user entered.
                    switch (startingMenuResponse)
                    {
                        case 0:
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
                            break;
                        case 1:
                            programInstance.DisplayList(listOfCharacters);
                            break;
                        case 2:
                            programInstance.AddExperience(listOfCharacters);
                            break;
                        case 3:
                            programInstance.ChangeValues(listOfCharacters);
                            break;
                        case 4:
                            programInstance.AddCharacter(listOfCharacters);
                            break;
                        case 5:
                            programInstance.RemoveCharacter(listOfCharacters);
                            break;
                        case 6:
                            programInstance.ResetValues(listOfCharacters);
                            break;
                        default:
                            Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                            Console.ReadKey();
                            break;
                    }// End Switch Block.
                }
                else
                {
                    Console.WriteLine("Invalid response. \nPlease enter the number corresponding to the action you wish to do." +
                        "\nPress any key to continue.");
                    Console.ReadKey();
                }// End If-Else block.
            }// End while loop.
        }//End Main function.

        /// <summary>
        /// DisplayList creates a header that describes what each character variable pertains to. Then, for every character that exists
        /// in the current list, it calls a function in the Character class for that instance, in which the variables
        /// associated with the character instance are displayed in formatted output, so it is easier for the user to 
        /// understand.
        /// </summary>
        /// <param name="currentList"></param>
        void DisplayList(List<Character> currentList)
        {
            // Creates an initial header for labelling variables.
            Console.WriteLine("\n------------------------\n\t  Name\t\t\t Level    Battles    Victories    EXP Gained\n" +
            "------------------------");
            foreach (Character thisCharacter in currentList)
            {
                thisCharacter.Display();
            }
            Console.WriteLine("------------------------\n\nEnd of data display. Press any key to go to menu.");
            Console.ReadKey();
        }// End DisplayList function.

        /// <summary>
        /// AddExperience function runs through the current list of characters, then requests the user to specify which character
        /// they wish to add experience to, or to cancel their action altogether. Upon selecting a character, it calls a function from
        /// the character class that allows the user to update the experience amount for a character as necessary. After any changes 
        /// are made to data, it will call the SaveData function, in order to commit any changes  
        /// </summary>
        /// <param name="currentList"></param>
        void AddExperience(List<Character> currentList)
        {
            int menuNumberChoice = -1;

            // Using a while loop here, because this is most likely the function where the user will spend the most amount of time
            // during a run of their tabletop game, since they need to be able to manipulate experience amounts quickly.
            while (menuNumberChoice != 0)
            {
                Console.WriteLine("\n\n----------------------");
                int count = 1;

                // Lists out the possible options that the user can perform.
                Console.WriteLine("    0.) Go Back");
                foreach (Character person in currentList)
                {
                    Console.WriteLine("    " + count + ".) " + person.GetFullName());
                    count++;
                }
                Console.WriteLine("----------------------\n\nAdd EXP to who? (Enter number of option above): ");

                if (Int32.TryParse(Console.ReadLine(), out menuNumberChoice))
                {
                    // Try-catch block for catching an exception if the user enters a number that is not associated with a character currently.
                    try
                    {
                        if(menuNumberChoice == 0)
                        {
                            return;
                        }
                        else
                        {
                            // Calling the function with the menu number choice, minus 1, since collections start at position 0, not 1.
                            // Note that, from looking at the menu, option 1 will always be the "lowest" collection entry, which corresponds
                            // to the number 0 entry.
                            currentList[menuNumberChoice - 1].AddExperience();
                            SaveData(currentList);
                        }
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Error: Please enter a valid number of the menu options.\nPress any key to move on.");
                        Console.ReadKey();
                    }// End try-catch block.
                }
                else
                {
                    Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                    Console.ReadKey();
                }//End if-else block.
            }//End While-loop.
        }//End addExperience method.

        /// <summary>
        /// ChangeValues function lists out characters in the current collection of characters, and allows users to select a character,
        /// and directly manipulate their data as a kind of group of mutator methods. This is so, if there is some sort of mistake in data entry,
        /// the user can change as necessary, as opposed to being forced to remove and add the character again. Any changes to data made will be
        /// automatically saved to the text files for a new input and output copy.
        /// </summary>
        /// <param name="currentList"></param>
        void ChangeValues(List<Character> currentList)
        {
            int menuNumberChoice = -1;

            while (menuNumberChoice != 0)
            {
                Console.WriteLine("\n\n----------------------");
                int count = 1;

                Console.WriteLine("    0.) Go Back");
                foreach (Character person in currentList)
                {
                    Console.WriteLine("    " + count + ".) " + person.GetFullName());
                    count++;
                }
                Console.WriteLine("----------------------\n\nChange values of who? (Enter number of option above): ");

                if(Int32.TryParse(Console.ReadLine(), out menuNumberChoice))
                {
                    if(menuNumberChoice == 0)
                    {
                        return;
                    }
                    else
                    {
                        try
                        {
                            currentList[menuNumberChoice - 1].ChangeValues();
                            SaveData(currentList);
                        }
                        catch(ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Error: Invalid menu entry. Please enter a valid number corresponding to your choice." +
                                "\nPress any key to move on.");
                            Console.ReadKey();
                        }
                    }
                    
                }
                else
                {
                    Console.WriteLine("Error: Invalid menu entry. Please enter a valid number corresponding to your choice.\nPress any key to move on.");
                    Console.ReadKey();
                }//End if-else block.
            }// End while loop.
        }// End ChangeValues function.

        /// <summary>
        /// AddCharacter function allows user to create a new character, and add it to the current list. It requests the user to input all
        /// values associated with a character, including first and last name, level, and any battles, victories, or EXP they may be starting with.
        /// After the user finishes the necessary data entry, it adds the character directly to the list collection, then calls the SaveData function
        /// to create a new input and output file.
        /// </summary>
        /// <param name="currentList"></param>
        void AddCharacter(List<Character> currentList)
        {
            string firstName = "";
            string lastName = "";
            int characterLevel = 1;
            int battleCount = 0;
            int victoryCount = 0;
            double experienceAmount = 0.0;

            // Get first name - type '-' to cancel.
            while (true)
            {
                Console.WriteLine("\n\nAdding a new character." +
                    "\n\nWhat is the first name of this new character?\n(Alternatively, type '-' to cancel.)");
                firstName = Console.ReadLine();

                if (firstName.Equals("-"))
                {
                    Console.WriteLine("Cancelling action. Press any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    break;
                }
            }// End while loop.

            // Get last name - type '-' for no last name.
            while (true)
            {
                Console.WriteLine($"\n\nWhat is the last name of {firstName}?\n(Alternatively, type '-' if there is no last name.");

                lastName = Console.ReadLine();

                if (lastName.Equals("-"))
                {
                    lastName = "NoLastName";
                }
                break;
            }// End while loop.

            // Get level - can't be less than 1.
            while (true)
            {
                Console.WriteLine($"\n\nWhat is the level of {firstName} {lastName}?\nPlease enter a whole number, 1 or greater.");
                if (Int32.TryParse(Console.ReadLine(), out characterLevel))
                {
                    if (characterLevel < 1)
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
            }// End while loop.

            // Check if there's starting battles - can't be less than 0.
            while (true)
            {
                Console.WriteLine($"Is there a starting number of battles for {firstName}?\nType 'y' if there is.");
                string checkForBattles = Console.ReadLine();

                if (checkForBattles.Equals("y") || checkForBattles.Equals("Y"))
                {
                    Console.WriteLine("How many battles?\nPlease enter a whole number, that is at least 0.");
                    if (Int32.TryParse(Console.ReadLine(), out battleCount))
                    {
                        if (battleCount < 0)
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
            }// End while loop.

            // Check if there's starting victories - can't be less than 0; can't be more than the number of battles.
            while (true)
            {
                if (battleCount < 1)
                {
                    Console.WriteLine("\n\nBattle count is at 0, assuming no victories.\n");
                    break;
                }
                else
                {
                    Console.WriteLine($"\n\nAre there any starting victories for {firstName}?\nType 'y' if there are.");
                    string checkForVictories = Console.ReadLine();

                    if (checkForVictories.Equals("y") || checkForVictories.Equals("Y"))
                    {
                        Console.WriteLine($"\nHow many victories?\nEnter a number that is in the range of 0 up to {battleCount}." +
                            $"\n(Note: {battleCount} is the number of battles you previously entered.)");

                        if (Int32.TryParse(Console.ReadLine(), out victoryCount))
                        {
                            if (victoryCount < 0)
                            {
                                Console.WriteLine("Error: victory count of character cannot be less than 0.\nPlease enter a number that is 0 or greater." +
                                    "\nPress any key to continue.");
                                Console.ReadKey();
                            }
                            else if (victoryCount > battleCount)
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
            }// End while loop.

            // Check if there's starting EXP.
            while (true)
            {
                Console.WriteLine($"Is there a starting number of EXP for {firstName}?\nType 'y' if there is.");
                string checkForExp = Console.ReadLine();

                if (checkForExp.Equals("y") || checkForExp.Equals("Y"))
                {
                    Console.WriteLine("How much EXP? Please enter a number.\n(Note: Decimal numbers are supported.");
                    if (Double.TryParse(Console.ReadLine(), out experienceAmount))
                    {
                        if (experienceAmount < 0)
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
            }// End while loop.

            // Adding new character to list.
            currentList.Add(new Character(firstName, lastName, characterLevel, battleCount, victoryCount, experienceAmount));

            // Saving new input/output files with new data.
            SaveData(currentList);

            // Exiting function
            Console.WriteLine("Character has been added to the data.\nPress any key to display new data, and go back to menu.");
            Console.ReadKey();
            DisplayList(currentList);
        }// End AddCharacter function.

        /// <summary>
        /// RemoveCharacter function displays a list of the characters that exist in the current list, then prompts the user to select a character
        /// to remove from the collection, or to cancel. Upon selecting a character, the application prompts the user to confirm deleting the character,
        /// and, upon confirmation, removes the character from the list, and then calls the SaveData function, creating a new input and output file.
        /// </summary>
        /// <param name="currentList"></param>
        void RemoveCharacter(List<Character> currentList)
        {
            while (true)
            {
                Console.WriteLine("\n\n----------------------");
                int count = 1;

                Console.WriteLine("    0.) Go Back");
                foreach (Character person in currentList)
                {
                    Console.WriteLine("    " + count + ".) " + person.GetFullName());
                    count++;
                }

                Console.WriteLine("----------------------\n\nRemove who? (Enter number)");

                if (Int32.TryParse(Console.ReadLine(), out int removeWhoResponse))
                {
                    if (removeWhoResponse == 0)
                    {
                        return;
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine($"We're going to remove \n     " + currentList[removeWhoResponse - 1].GetFullName() + "\nAre you sure?" +
                            "\nEnter, in all caps, 'YES' (with all caps, too) if you are sure.");

                            string confirmRemoval = Console.ReadLine();

                            if (confirmRemoval.Equals("YES"))
                            {
                                currentList.RemoveAt(removeWhoResponse - 1);
                                SaveData(currentList);
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
                }// End if-else block.
            }//End while loop.
        }// End RemoveCharacter function.

        /// <summary>
        /// ResetValues function informs the user that the purpose is to set the EXP, battle, and victory amount back to 0, in order to prepare for the
        /// next use of the program. The user is prompted to confirm that they are okay with the change of data, and, upon confirmation, sets the aforementioned
        /// data values back to 0 for each character in the entire list collection.
        /// </summary>
        /// <param name="currentList"></param>
        void ResetValues(List<Character> currentList)
        {
            Console.WriteLine("\n\nThis will set the battle count, victory count, and EXP earned back to 0,\n" +
                "for the purposes of clearing for the next battle.\nThis applies to EVERY character that exists on the current character list.\n" +
                "Are you absolutely sure that you are ready to reset these values?\n\nType 'YES' (in all caps) if you are sure.");

            string confirmReset = Console.ReadLine();

            if (confirmReset.Equals("YES"))
            {
                foreach (Character person in currentList)
                {
                    person.ResetValues();
                }

                SaveData(currentList);

                Console.WriteLine("\n\nAll characters' battle stats have been reset.\nPress any key to go back to the menu.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Cancelling action. Press any key to go back to the menu.");
                Console.ReadKey();
            }// End If-Else block.

        }// End ResetValues function.
        
        /// <summary>
        /// SaveData function utilizes any data changes that has been made to the current list of characters, and creates a new input file, so booting the
        /// application again will utilize the new data instead of resetting. It also creates an output file, one that is far more simple for a user to read,
        /// as opposed to attempting to read the input data. This function exists so that it is called when there are any changes to the list character data.
        /// </summary>
        /// <param name="currentList"></param>
        void SaveData(List<Character> currentList)
        {
            // Saving an input copy, for use upon booting the program again.
            Console.WriteLine("Saving data to a new input file output...");
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/CharacterDataInput.txt"))
                {
                    foreach (Character person in currentList)
                    {
                        file.WriteLine(person.GetFullName() + " " + person.GetCurrentLevel() + " " + person.GetBattleCount() + " " +
                            person.GetVictoryCount() + " " + person.GetCurrentBattleEXP());
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: File has not been saved. The given data path may not exist\nin the current run of the application." +
                    "\nPress any key to move on.");
                Console.ReadKey();
            }// End try-catch
            Console.WriteLine("File saved.");
            
            //Saving an output copy, for making it simple for a user to read it.
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
                            person.GetFullName(), person.GetCurrentLevel(), person.GetBattleCount(),
                            person.GetVictoryCount(), person.GetCurrentBattleEXP());
                        file.WriteLine($"{temp}");
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Output file has not been saved. The given path in the source code may not exist in\n" +
                    "the current run of the application.\nPress any key to move on.");
                Console.ReadKey();
            }// End try-catch
            Console.WriteLine("File saved.");
        }// End SaveData function.
    }// End Program class.
}// End Namespace ConsoleExperienceCalculator
