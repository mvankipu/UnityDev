using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Diagnostics;

public class MainManager : MonoBehaviour
/**
 What does this class do? Game Manager is the central hub that connects block class with testManager class.
It cyles through game states and it provides information from config file to set up the Test Manager.
It's the primary update look - it sends deltaTime to Test Manager to keep it updated.
 */
{

    enum GameState
    {
        Start,
        DisplayTestPattern,
        GetUserInput,
        ResultWin,
        ResultFail,
        End,
    }

    GameState currentState;
    TestManager currentTest;

    List<BlockManager> blockList;

    CountDownTimer onHighlightTimer;
    CountDownTimer testIntervalTimer;
    bool pass;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the test with block count and test mode
        currentTest = new TestManager(4, TestManager.TestMode.FailToEnd);
        currentState = GameState.DisplayTestPattern;

        blockList = new List<BlockManager>();

        blockList.Add(GameObject.Find("Cube1").GetComponent<BlockManager>());
        blockList.Add(GameObject.Find("Cube2").GetComponent<BlockManager>());
        blockList.Add(GameObject.Find("Cube3").GetComponent<BlockManager>());
        blockList.Add(GameObject.Find("Cube4").GetComponent<BlockManager>());

        //Assert statement to make sure none of the entries in blockList are null - do this after set up to check
        foreach (var tempBlock in blockList)
            UnityEngine.Debug.Assert(tempBlock != null);

        blockList[0].defaultMat = Resources.Load<Material>("Red1");
        blockList[1].defaultMat = Resources.Load<Material>("Blue1");
        blockList[2].defaultMat = Resources.Load<Material>("Yellow1");
        blockList[3].defaultMat = Resources.Load<Material>("Green1");

        blockList[0].highlightMat = Resources.Load<Material>("Red2");
        blockList[1].highlightMat = Resources.Load<Material>("Blue2");
        blockList[2].highlightMat = Resources.Load<Material>("Yellow2");
        blockList[3].highlightMat = Resources.Load<Material>("Green2");

        foreach (var tempBlock in blockList)
            tempBlock.setHighlighted(false);

        onHighlightTimer = null;
        testIntervalTimer = null;
        pass = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfUserQuit();

        if(currentState == GameState.DisplayTestPattern)
        {
            //Pattern display
            
            //Set all cube materials to default
            foreach (var tempBlock in blockList)
                tempBlock.setHighlighted(false);
 
            //Update Test Pattern State in Test Manager
            //Some time has passed
            int blockNumber = currentTest.UpdateTestPattern(Time.deltaTime); //Update userInput is the other function
            //UnityEngine.Debug.Log("Block ID: " + blockNumber.ToString());

            if (blockNumber>0)
            {
                blockList[blockNumber - 1].setHighlighted(true);
            }
            else if (blockNumber == -1)
            {
                UnityEngine.Debug.Log("Pattern ended. Ready for user input.");
                currentState = GameState.GetUserInput;
                //testPattern.setInputCheckVar();
            }
        }

        if(currentState == GameState.GetUserInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Adding collision code in Main Manager as game specific information is needed to complete this
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //UnityEngine.Debug.Log(hit.collider.gameObject.name);
                    int collidedObject;

                    onHighlightTimer = new CountDownTimer(currentTest.GetUserInputDisplayTime(),"User Timer");

                    switch (hit.collider.gameObject.name)
                    {
                        case "Cube1":
                            collidedObject = 1;
                            blockList[collidedObject - 1].setHighlighted(onHighlightTimer);
                            break;
                        case "Cube2":
                            collidedObject = 2;
                            blockList[collidedObject - 1].setHighlighted(onHighlightTimer);
                            break;
                        case "Cube3":
                            collidedObject = 3;
                            blockList[collidedObject - 1].setHighlighted(onHighlightTimer);
                            break;
                        case "Cube4":
                            collidedObject = 4;
                            blockList[collidedObject - 1].setHighlighted(onHighlightTimer);
                            break;
                        default:
                            collidedObject = 0;
                            break;
                    }

                    pass = currentTest.UserClicked(collidedObject);
                }
            }

            if (onHighlightTimer != null && onHighlightTimer.IsComplete())
            {
                onHighlightTimer = null;

                if (pass)
                {
                    if (currentTest.UserComplete())
                    {
                        UnityEngine.Debug.Log("Win. Next iteration.");
                        testIntervalTimer = new CountDownTimer(2.0f, "TEST-INTERVAL");
                    }
                }
                else
                {
                    UnityEngine.Debug.Log("Lose. Restart?");
                    currentState = GameState.ResultFail;
                }
            }

            if(testIntervalTimer != null && testIntervalTimer.IsComplete(Time.deltaTime))
            {
                currentTest.NextTest();
                currentState = GameState.DisplayTestPattern;
                testIntervalTimer = null;
                //currentState = GameState.ResultWin;
            }
        }
    }

    void checkIfUserQuit()
    {
        //Check if applciation has quit
        if (Input.GetKey("escape"))
        {
            UnityEngine.Debug.Log("Application will quit.");
            Application.Quit();
        }
    }
}
