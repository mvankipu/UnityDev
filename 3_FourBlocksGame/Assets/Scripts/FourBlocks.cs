using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FourBlocks : MonoBehaviour
{
    enum GameState
    {
        start, //Display welcome screen with instructions
        //prepTimer, //After user clocks start button show a 3,2,1... timer 
        ready, //Show pattern
        getUserInput, //listen for user input in this state
        winScreen, //Check if user got it right and show appropriate display  
        end, //Thanks for playing
    }

    Pattern testPattern;
    GameState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.start;

        //Tests for Test Pattern constructor
        //testPattern = new Pattern();
        //testPattern = new Pattern(7,1);
        //testPattern = new Pattern(5,10,14);

        testPattern = new Pattern(7,1,4);

        //TODO Move this to the event handler for the start button when implemented
        testPattern.startPattern();
        testPattern.print();
        
        currentState = GameState.ready;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            UnityEngine.Debug.Log("Application will quit.");
            Application.Quit();
        }

        if(currentState == GameState.ready)
        {
            //Display Pattern
            int currentPatternItem = testPattern.getActivePatternItem(Time.deltaTime);

            //TODO Set all cube materials to default
            GameObject.Find("Cube1").GetComponent<Renderer>().material = Resources.Load<Material>("Red1");
            GameObject.Find("Cube2").GetComponent<Renderer>().material = Resources.Load<Material>("Blue1");
            GameObject.Find("Cube3").GetComponent<Renderer>().material = Resources.Load<Material>("Yellow1");
            GameObject.Find("Cube4").GetComponent<Renderer>().material = Resources.Load<Material>("Green1");

            if (currentPatternItem == -1)
            {
                UnityEngine.Debug.Log("[FourBlocks] Pattern ended. Ready for user input.");
                currentState = GameState.getUserInput;
            }
            else if (currentPatternItem == 0)
            {
                //Do Nothing
            }
            else
            {
                //UnityEngine.Debug.Log(currentPatternItem.ToString());
                int blockId = testPattern.getPatternListItem(currentPatternItem);
                
                if (blockId == 1)
                {
                    GameObject.Find("Cube1").GetComponent<Renderer>().material = Resources.Load<Material>("Red2");
                }
                else if (blockId == 2)
                {
                    GameObject.Find("Cube2").GetComponent<Renderer>().material = Resources.Load<Material>("Blue2");
                }
                else if (blockId == 3)
                {
                    GameObject.Find("Cube3").GetComponent<Renderer>().material = Resources.Load<Material>("Yellow2");
                }
                else if (blockId == 4)
                {
                    GameObject.Find("Cube4").GetComponent<Renderer>().material = Resources.Load<Material>("Green2");
                }
                else
                {
                    GameObject.Find("Cube1").GetComponent<Renderer>().material = Resources.Load<Material>("Red1");
                    GameObject.Find("Cube2").GetComponent<Renderer>().material = Resources.Load<Material>("Blue1");
                    GameObject.Find("Cube3").GetComponent<Renderer>().material = Resources.Load<Material>("Yellow1");
                    GameObject.Find("Cube4").GetComponent<Renderer>().material = Resources.Load<Material>("Green1");
                }
            }
        }

        //if state is ready

        //create a random pattern of length 3

        //show pattern
        //highlight 1, timer, 5second - increament display counter, highlight cubes[i]...
         

    }
}
