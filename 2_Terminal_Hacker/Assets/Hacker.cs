using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Definitions
    enum Screen
    { 
        MainMenu,
        WaitForResponse,
        WaitForPassKey,
        Delay,
        Password,
        Easy,
        Medium,
        Hard,
        Win
    }

    //Class Variables
    string userName;
    int levelChoice;
    int timerCount;
    Screen gameState;
    string passKey;
    
    string[] levelNames = { "Intro to magical world", "Help with my OWLs", "Magical career planning" };
    string[] easyPasswords = { "potter","weaseley","granger","lovegood","black" };
    string[] mediumPasswords = { "hogwarts","gringotts","honeydukes","azkaban","theburrow" };
    string[] hardPasswords = { "avadakedavra", "confringo", "expectopatronum", "expelliarmus", "morsmordre" };

    // Start is called before the first frame update
    void Start()
    {
        gameState = Screen.MainMenu;
        userName = "Mithra";
        timerCount = 0;
        passKey = null;
        gameState = Screen.MainMenu;
    }

    void Update()
    {
        //Main Screen
        if (gameState == Screen.MainMenu)
        {
            showMainMenu();
            gameState = Screen.WaitForResponse;
        }

        //delay check
        if (gameState == Screen.Delay)
        {
            timerCount++;
            if (timerCount > 300)
            {
                timerCount = 0;
                gameState = Screen.MainMenu;
            }
        }

        //Easy
        if (gameState == Screen.Easy)
        {
            Terminal.WriteLine("You chose (1) " + levelNames[0]);
            gameState = Screen.Password;

            //Set passward
            passKey = easyPasswords[UnityEngine.Random.Range(0, easyPasswords.Length)];
        }

        //Medium
        if (gameState == Screen.Medium)
        {
            Terminal.WriteLine("You chose (2) " + levelNames[1]);
            gameState = Screen.Password;

            //Set passward
            passKey = mediumPasswords[UnityEngine.Random.Range(0, mediumPasswords.Length)];
        }

        //Hard
        if (gameState == Screen.Hard)
        {
            Terminal.WriteLine("You chose (3) " + levelNames[2]);
            gameState = Screen.Password;

            //Set passward
            passKey = hardPasswords[UnityEngine.Random.Range(0, hardPasswords.Length)];
        }

        //Password
        if(gameState == Screen.Password)
        {
            Terminal.WriteLine("Here is the key: " + scrambleString(passKey));
            Terminal.WriteLine("What does it mean?: ");
            gameState = Screen.WaitForPassKey;
        }

        if(gameState==Screen.Win)
        {
            Terminal.ClearScreen();
            Terminal.WriteLine("ACCESS GRANTED");
            gameState = Screen.WaitForResponse;
        }
    }

    //Method that shows what to display for a new menu
    void showMainMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Hello " + userName + ". Welcome to PotterWorld!");
        Terminal.WriteLine("What level of secrets do you need?");

        //Terminal.WriteLine("1. Intro to magical world");
        //Terminal.WriteLine("2. Help with my OWLs");
        //Terminal.WriteLine("3. Magical career planning");
        for (int i=0;i<levelNames.Length;i++)
        {
            int tempLevel = i + 1;
            Terminal.WriteLine(tempLevel.ToString() + ". " + levelNames[i]);
        }
        Terminal.WriteLine("OR type 'menu' to see this menu again.");
        Terminal.WriteLine("Enter your choice: ");
    }

    //Get User Input
    void OnUserInput(string input)
    {
        if(gameState == Screen.WaitForResponse)
        {
            bool validInt = false;

            try
            {
                levelChoice = Int32.Parse(input);
                validInt = true;
            }
            catch (FormatException)
            {
                input = input.ToLower();
                if (input == "menu")
                {
                    gameState = Screen.MainMenu;
                }
                else
                {
                    Terminal.WriteLine("You must be Muggle! (" + input + ")");
                    gameState = Screen.Delay;
                }
            }

            if (validInt)
            {
                switch (levelChoice)
                {
                    case 1:
                        gameState = Screen.Easy;
                        break;
                    case 2:
                        gameState = Screen.Medium;
                        break;
                    case 3:
                        gameState = Screen.Hard;
                        break;
                    default:
                        Terminal.WriteLine("You chose... an invalid option Muggle!");
                        gameState = Screen.Delay;
                        break;
                }
            }
        }

        if(gameState == Screen.WaitForPassKey)
        {
            if (input == passKey)
            {
                gameState = Screen.Win;
            }
            else
            {
                Terminal.WriteLine("No dice, MUGGLE!");
                gameState = Screen.Delay;
            }
        }
    }

    string scrambleString(string input)
    {
        StringBuilder sbInput = new StringBuilder(input);
        StringBuilder scrambledStr = new StringBuilder(input);
        int index = 0;

        bool isEmpty = false;

        while(!isEmpty)
        {
            //Find random character in input string and add to temp string
            int randomIndex = UnityEngine.Random.Range(0, input.Length);
            if(sbInput[randomIndex] != '\0')
            {
                scrambledStr[index] = sbInput[randomIndex];
                sbInput[randomIndex] = '\0';
                index++;
            }

            //check if whole string is empty
            isEmpty = true;
            for(int i=0;i<input.Length;i++)
            {
                if (sbInput[i] != '\0')
                {
                    isEmpty = false;
                    break;
                }
            }
        }
        return scrambledStr.ToString();
    }
}
