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
            //The following try-catch block pertains to the CharacterData.txt file, in order to throw in error if it is not found in the specified path.
            try
            {
                //Begin declaring and finding the text file here. Change the path if necessary.
                using (StreamReader sr = new StreamReader("C:/Users/Public/Documents/CharacterData/CharacterData.txt"))
                {
                    //Declaring string and list of character class for purposes of reading in the file.
                    string line;
                    List<Character> listOfCharacters = new List<Character>();
                                        
                    //The following block reads in each line of the text file, and assigns the line as values of a character object.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        listOfCharacters.Add(new Character(words[0], words[1], Convert.ToInt32(words[2]), 
                            Convert.ToInt32(words[3]), Convert.ToInt32(words[4]), Convert.ToDouble(words[5])));
                    }//End While-Loop.

                    //Initial display of data from text file.
                    Console.WriteLine("\n----------------------\n\t  Name\t\t\tLevel    Battles    Victories    EXP Gained\n----------------------");
                    foreach (Character thisCharacter in listOfCharacters)
                    {
                        thisCharacter.Display();
                    }//End foreach-loop.
                    Console.WriteLine("\n----------------------\nRefer to initial display above. Press any key to bring up the menu.");
                    Console.ReadKey();
                    
                    while(true)
                    {
                        Console.WriteLine("\nDo what?\n------------------\n    0.) Exit\n    1.) Display All Stats\n    2.) Add EXP" +
                            "\n    3.) Change Character Values\n    4.) Add or Remove Character\n    5.) Reset EXP, Battles, and Victories Gained This Battle" +
                            "\nEnter Number: ");
                        try
                        {
                            int MenuNumberChoice = Convert.ToInt32(Console.ReadLine());
                            switch (MenuNumberChoice)
                            {
                                case 0:
                                    Console.WriteLine("Exiting program...");
                                    Environment.Exit(0);
                                    break;
                                case 1:
                                    Console.WriteLine("\n----------------------\n\t  Name\t\t\tLevel    Battles    Victories    EXP Gained\n" +
                                        "----------------------");
                                    foreach (Character thisCharacter in listOfCharacters)
                                    {
                                        thisCharacter.Display();
                                    }
                                    Console.WriteLine("\n----------------------\nEnd of data display. Press any key to move on...");
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    addExperience(listOfCharacters);
                                    break;
                                case 3:
                                    changeValues(listOfCharacters);
                                    break;
                                case 4:
                                    addOrRemoveCharacter(listOfCharacters);
                                    break;
                                case 5:
                                    resetValues(listOfCharacters);
                                    break;
                                default:
                                    Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                                    Console.ReadKey();
                                    break;
                            }//End Switch Block.
                        }//End Try.
                        catch(Exception)
                        {
                            Console.WriteLine("Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                            Console.ReadKey();
                        }//End menu try-catch block.
                    }//End While-Loop.
                }//End text file using block.
            }//End Try.
            catch(Exception)
            {
                Console.WriteLine("CharacterData.txt file not found. Is the file in the character data folder in documents?" +
                    "\nCannot currently continue without file. Exiting program.");
                Environment.Exit(1);
            }//End text file try-catch block.

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

            void addOrRemoveCharacter(List<Character> currentList)
            {
                int addOrRemoveResponse = -1;

                while (addOrRemoveResponse != 0)
                {
                    Console.WriteLine("\n    0.) Go Back\n    1.) Add New Character\n    2.) Remove a Character\nDo What? (Enter number)");
                    if (Int32.TryParse(Console.ReadLine(), out addOrRemoveResponse))
                    {
                        switch (addOrRemoveResponse)
                        {
                            case 0:
                                break;
                            case 1:
                                Console.WriteLine("Creating a new character.\nWhat is this character's first name?(Type - to cancel)");
                                string newFirstName = Console.ReadLine();
                                if (newFirstName.Equals("-"))
                                {
                                    break;
                                }

                                Console.WriteLine($"What is the last name of {newFirstName}?\n(Type - if no last name");
                                string newLastName = Console.ReadLine();
                                if (newLastName.Equals("-"))
                                {
                                    newLastName = "NoLastName";
                                }


                                int newLevel = -1;
                                while (newLevel <= 0)
                                {
                                    Console.WriteLine($"What is the level of {newFirstName} {newLastName}?\n   Enter a positive whole number:");
                                    if (Int32.TryParse(Console.ReadLine(), out newLevel))
                                    {
                                        if (newLevel <= 0)
                                        {
                                            Console.WriteLine("Please enter a positive, whole number.");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        newLevel = -1;
                                        Console.WriteLine("Please enter a positive, whole number.");
                                    }
                                }

                                
                                int anyBattlesResponse = -1;
                                int newBattleCount = 0;
                                while (anyBattlesResponse != 1 || anyBattlesResponse != 2)
                                {
                                    Console.WriteLine($"Does {newFirstName} have any battles to start with?\n    1.) Yes\n    2.) No");
                                    if(Int32.TryParse(Console.ReadLine(), out anyBattlesResponse))
                                    {

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid response. Please enter the choice of the menu number.");
                                    }

                                    if (anyBattlesResponse == 1)
                                    {
                                        Console.WriteLine("How many battles? (enter a whole number greater than 0)");

                                        if(Int32.TryParse(Console.ReadLine(), out newBattleCount))
                                        {
                                            if(newBattleCount < 0)
                                            {
                                                Console.WriteLine("Please only use positive numbers. Try again.");
                                                anyBattlesResponse = 0;
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid response. Make sure it's a positive, whole number.");
                                            anyBattlesResponse = 0;
                                        }
                                    }
                                    else if(anyBattlesResponse == 2)
                                    {
                                        newBattleCount = 0;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid response. Please enter 1 for yes, or 2 for no.");
                                    }
                                }
                                

                                
                                int anyVictoriesResponse = -1;
                                int newVictoryCount = 0;
                                while (anyVictoriesResponse != 1 || anyVictoriesResponse != 2)
                                {
                                    Console.WriteLine($"Does {newFirstName} have any victories to start with?\n    1.) Yes\n    2.) No");
                                    if (Int32.TryParse(Console.ReadLine(), out anyVictoriesResponse))
                                    {

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid response. Please enter the choice of the menu number.");
                                    }

                                    if (anyVictoriesResponse == 1)
                                    {
                                        Console.WriteLine("How many victories? (enter a whole, positive number)");
                                        if (Int32.TryParse(Console.ReadLine(), out newVictoryCount))
                                        {
                                            if (newVictoryCount < 0)
                                            {
                                                Console.WriteLine("Please only use positive numbers. Try again.");
                                                anyVictoriesResponse = 0;
                                            }
                                            else
                                            {
                                                if (newVictoryCount > newBattleCount)
                                                {
                                                    Console.WriteLine("You can't have more victories than battles.\nThe battle count you entered is {newBattleCount}." +
                                                        "\nEnter a number less than that.");
                                                    anyVictoriesResponse = 0;
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid response. Make sure it's a positive, whole number.");
                                            anyVictoriesResponse = 0;
                                        }
                                    }
                                    else if (anyVictoriesResponse == 2)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid response. Please enter 1 for yes, or 2 for no.");
                                    }
                                }


                                
                                int anyEXPResponse = -1;
                                double newEXPAmount = 0;
                                while (anyEXPResponse != 1 || anyEXPResponse != 2)
                                {
                                    Console.WriteLine($"Does {newFirstName} have any EXP to start with?\n    1.) Yes\n    2.) No");
                                    if (Int32.TryParse(Console.ReadLine(), out anyEXPResponse))
                                    {

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid response. Please enter the choice of the menu number.");
                                    }
                                    if (anyEXPResponse == 1)
                                    {
                                        Console.WriteLine("How much EXP? (Enter a number, decimal supported):");
                                        if (Double.TryParse(Console.ReadLine(), out newEXPAmount))
                                        {
                                            if (newEXPAmount < 0)
                                            {
                                                Console.WriteLine("Please only use positive numbers. Try again.");
                                                anyEXPResponse = 0;
                                            }
                                            else
                                            {
                                                
                                                break;
                                            }

                                            
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid response. Make sure it's a positive, whole number.");
                                            anyEXPResponse = 0;
                                        }

                                    }
                                    else if (anyEXPResponse == 2)
                                    {
                                        newEXPAmount = 0;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid response. Please enter 1 for yes, or 2 for no.");
                                    }
                                }


                                currentList.Add(new Character(newFirstName, newLastName, newLevel, newBattleCount, newVictoryCount, newEXPAmount));
                                saveData(currentList);
                                Console.WriteLine("Character has been added.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine($"{addOrRemoveResponse} is not a valid response. \nPlease enter a number corresponding to your desired action." +
                            $"\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{addOrRemoveResponse} is not a valid response. \nPlease enter a number corresponding to your desired action." +
                            $"\nPress any key to move on.");
                        Console.ReadKey();
                    }
                }
            }
            
            void resetValues(List<Character> currentList)
            {

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
