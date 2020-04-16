using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class FourBlocks : MonoBehaviour
{
    //TODO Blocks likely can be pulled into a seperate class
    //TO DO See if you can access this list using Enums
    public List<Material> blockColors;

    enum BlockColors
    {
        Red_Dark,
        Red_Bright,
        Blue_Dark,
        Blue_Bright,
        Green_Dark,
        Green_Bright,
        Yellow_Dark,
        Yellow_Bright,
        Neutral_Dark,
        Pink_Bright,
    }

    //TODO Change format of enum to caps
    enum GameState
    {
        StartScreen, //Display welcome screen with instructions
        //CountdownTimer, //prepTimer, //After user clocks start button show a 3,2,1... timer 
        Ready, //Show pattern
        GetUserInput, //listen for user input in this state
        ResultPass, //Check if user got it right and show appropriate display  
        ResultFail,
        //EndScreen, //Thanks for playing
    }

    Pattern testPattern;
    Pattern userPattern;
    GameState currentState;

    int[] userInput;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.StartScreen;

        //Tests for Test Pattern constructor
        //testPattern = new Pattern();
        //testPattern = new Pattern(7,1);
        //testPattern = new Pattern(5,10,14);

        testPattern = new Pattern(7,1,4);
        userInput = new int[testPattern.getLength()];

        //TODO Move this to the event handler for the start button when implemented
        testPattern.startPattern();
        testPattern.print();
        
        currentState = GameState.Ready;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            UnityEngine.Debug.Log("Application will quit.");
            Application.Quit();
        }

        if(currentState == GameState.Ready)
        {
            //Display Pattern
            int currentPatternItem = testPattern.getActivePatternItem(Time.deltaTime);

            //TODO Set all cube materials to default
            GameObject.Find("Cube1").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Red_Dark]; //Resources.Load<Material>("Red1");
            GameObject.Find("Cube2").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Blue_Dark]; //Resources.Load<Material>("Blue1");
            GameObject.Find("Cube3").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Yellow_Dark]; //Resources.Load<Material>("Yellow1");
            GameObject.Find("Cube4").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Green_Dark]; //Resources.Load<Material>("Green1");

            if (currentPatternItem == -1)
            {
                UnityEngine.Debug.Log("[FourBlocks] Pattern ended. Ready for user input.");
                currentState = GameState.GetUserInput;
                testPattern.setInputCheckVar();
            }
            else if (currentPatternItem == 0)
            {
                //Do Nothing
            }
            else
            {
                //UnityEngine.Debug.Log(currentPatternItem.ToString());
                int blockId = testPattern.getActivePatternListItem();

                if (blockId == 1)
                {
                    GameObject.Find("Cube1").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Red_Bright];
                }
                else if (blockId == 2)
                {
                    GameObject.Find("Cube2").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Blue_Bright];
                }
                else if (blockId == 3)
                {
                    GameObject.Find("Cube3").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Yellow_Bright];
                }
                else if (blockId == 4)
                {
                    GameObject.Find("Cube4").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Green_Bright];
                }
                else
                {
                    GameObject.Find("Cube1").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Red_Dark]; //Resources.Load<Material>("Red1");
                    GameObject.Find("Cube2").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Blue_Dark]; //Resources.Load<Material>("Blue1");
                    GameObject.Find("Cube3").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Yellow_Dark]; //Resources.Load<Material>("Yellow1");
                    GameObject.Find("Cube4").GetComponent<Renderer>().material = blockColors[(int)BlockColors.Green_Dark]; //Resources.Load<Material>("Green1");
                }
            }
        }

        if(currentState == GameState.GetUserInput)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if(Physics.Raycast(ray, out hit))
                {
                    UnityEngine.Debug.Log(hit.collider.gameObject.name);
                    int collidedObject;

                    switch (hit.collider.gameObject.name)
                    {
                        case "Cube1":
                            collidedObject = 1;
                            break;
                        case "Cube2":
                            collidedObject = 2;
                            break;
                        case "Cube3":
                            collidedObject = 3;
                            break;
                        case "Cube4":
                            collidedObject = 4;
                            break;
                        default:
                            collidedObject = 0;
                            break;
                    }
                     
                    bool pass = testPattern.UserClicked(collidedObject);

                    if (pass)
                    {
                        if (testPattern.Complete())
                            currentState = GameState.ResultPass;
                    }
                    else
                        currentState = GameState.ResultFail;
                }
            }
        }

        if(currentState == GameState.ResultPass)
        {
            UnityEngine.Debug.Log("You Win!");
        }

        if(currentState == GameState.ResultFail)
        {
            UnityEngine.Debug.Log("You Lose!");
        }
    }
}
