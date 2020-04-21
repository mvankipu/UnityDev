using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager
{
    public enum TestMode
    {
        FailToEnd,
        FixedLength,
    }

    TestMode mode;

    enum PatternState
    {
        ActiveDisplay, //Means that this function returns a item from the pattern
        PatternInterval, //No pattern item returned during tshe interval period
    }

    PatternState currentPatternState; //Patern State Manager

    int blockCount;
    int testIterationCounter;
    int userInputPatternCounter;
    int maxPatternLength;

    Pattern testPattern;

    float patternDisplayOnTime = 2.0f;
    float patternDisplayOffTime = 1.0f;
    float userInputDisplayTime = 1.0f;

    public float UserInputDisplayTime { get { return userInputDisplayTime; } }

    CountDownTimer displayOn;
    CountDownTimer displayOff;
    CountDownTimer userInputDisplayOn;

    public TestManager(int blockCountInput = 4, TestMode testModeInput = TestMode.FailToEnd)
    {
        blockCount = blockCountInput;
        mode = testModeInput;
        maxPatternLength = 21;

        displayOn = new CountDownTimer(patternDisplayOnTime);
        displayOff = new CountDownTimer(patternDisplayOffTime);
        userInputDisplayOn = new CountDownTimer(UserInputDisplayTime);

        InitializeTest();
    }

    public int UpdateTestPattern(float deltaTime)
    /**
    Return -1 if pattern is complete
    Return 1...N for active block
    0 if no block is highlighted
     */
    {
        if (currentPatternState == PatternState.ActiveDisplay)
        {
            if (displayOn.IsComplete(deltaTime))
            {
                displayOff.Reset();
                currentPatternState = PatternState.PatternInterval;
                return 0;
            }
            else
            {
                return testPattern.GetActivePatternListItem();
            }
        }
        else
        {
            if (displayOff.IsComplete(deltaTime))
            {
                testPattern.StepPattern();

                if (testPattern.IsComplete(testIterationCounter))
                {
                    return -1;
                }
                else
                {
                    displayOn.Reset();
                    currentPatternState = PatternState.ActiveDisplay;
                    return 0;
                }
            }
            else
                return 0;
        }
    }

    
    public void InitializeTest()
    {
        //Reset the testIteration counter variable
        //TODO Depends on mode - EndToFail
        testIterationCounter = 1;

        //To set up a test we need a pattern
        testPattern = new Pattern(maxPatternLength, 1, blockCount);
        testPattern.Print(testIterationCounter);

        //Start CountDown Timer
        displayOn.Reset();

        //Set intial pattern display state
        currentPatternState = PatternState.ActiveDisplay;

        //Initialize variable for user input
        userInputPatternCounter = 0;
    }

    public bool UserClicked(int clickedObject)
    {
        if (testPattern.GetItemAtIndex(userInputPatternCounter) == clickedObject)
        {
            userInputPatternCounter++;
            return true;
        }
        else
            return false;
    }

    public bool UserComplete()
    /** This function checks to see if a test is complete
     Returns true if the user input pattern counter is done
    Returns false otherwise.
     */
    {
        if (userInputPatternCounter >= testIterationCounter)
            return true;
        else
            return false;
    }

    public void NextTest()
    {
        testIterationCounter++;

        //Start CountDown Timer
        displayOn.Reset();

        //Set intial pattern display state
        currentPatternState = PatternState.ActiveDisplay;

        //Initialize variable for user input
        userInputPatternCounter = 0;

        //Reset ActivePatternItem Tracker variable
        testPattern.ResetPattern();
    }

    public float GetUserInputDisplayTime()
    {
        return userInputDisplayTime;
    }
}