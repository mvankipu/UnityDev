using System;
using System.Linq;
using UnityEngine;

public class Pattern
{
	int[] patternList;

	enum PatternState
    {
		ActiveDisplay, //Means that this function returns a item from the pattern
		PatternInterval, //No pattern item returned during tshe interval period
    }

	PatternState pState; //Patern State Manager

	//Global variable tracking the current active pattern item
	int activePatternItem;

	float activeDuration; //How long is a pattern item active?
	float intervalDuration; //How long is the pattern interval?

	//Global variables - global to this function to manage count down timers
	//TODO: Create count down timer class?
	float patternOnTime; //Tracking time that pattern item is "ON"	
	float patternOffTime; //Tracking time that the pattern is "OFF"

	
	//Global variable to track pattern item related to checking user input
	int inputCheckVarItem;

	//Given some input variables, this constructor creates a pattern list
	public Pattern(int patternLength = 3, int rangeMin = 1, int rangeMax = 4)
	{
		patternList = new int[patternLength];
		
		for(int i=0;i<patternLength;i++)
        {
			patternList[i] = UnityEngine.Random.Range(rangeMin, rangeMax + 1);
		}

		activePatternItem = 1;
	}
	
	public void startPattern(float onTime = 2.0f, float offTime = 1.0f)
	/**
	The function needs to be called once before pattern display. It allows users to set the display and interval time for the pattern.
	*/
	{
		//Set once and save for while this instance of pattern class exists
		//TO DO: Likely onTime and offTime should be set in the constructor
		activeDuration = onTime;
		intervalDuration = offTime;

		//Count Down Timer variables are set
		patternOnTime = activeDuration;

		//patternOffTime = intervalDuration;
		pState = PatternState.ActiveDisplay;

		//set active pattern item to the start of the list (1...N)
		activePatternItem = 1;
		//UnityEngine.Debug.Log(patternList[activePatternItem - 1].ToString());
	}

	public int GetActivePatternItem()
    {
		return activePatternItem;
    }

/*	
	public int getActivePatternItem(float deltaTime)
    /**
	Return 0 if state is interval
	Returns -1 when pattern is complete

	If state is active and we are currently parsing the pattern,
	this finction returns the step in the pattern list that is active.
	NOTE: This is not the actual item, just the step in the pattern list.
	*/
/*	{
		if(pState == PatternState.ActiveDisplay)
        {
			patternOnTime -= deltaTime;
			if (patternOnTime <= 0.0)
			{
				activePatternItem++;

				//UnityEngine.Debug.Log(patternList[activePatternItem - 1].ToString());
				patternOffTime = intervalDuration;
				pState = PatternState.PatternInterval;
				return 0;
			}
            else
            {
				return activePatternItem;
            }
		}
		else
        {
			patternOffTime -= deltaTime;
			if (patternOffTime <= 0.0)
			{
				if (activePatternItem > patternList.Length)
					return -1;
				else
                {
					patternOnTime = activeDuration;
					pState = PatternState.ActiveDisplay;
					return activePatternItem;
				}
			}
			else
				return 0;
		}
		//return activePatternItem;
    }
*/

	public int getLength()
    {
		return patternList.Length;
    }

	public int GetActivePatternListItem()
	/**
	Returns value of item that is currently active in the pattern
	Should only be called when the state is active
	*/
    {
		//TODO Error Checking needed
		//UnityEngine.Debug.Log(index.ToString());
		//return patternList[index-1];

		//if (pState == PatternState.ActiveDisplay)
		return patternList[activePatternItem - 1];
		//else
		//	throw new ArgumentException("Pattern state is not active.");
    }

	public void Print()
	/**
	Prints the pattern when asked like a boss
	**/
    {
		string temp = "Pattern = ";
		for (int i = 0; i < patternList.Length; i++)
		{
			temp += patternList[i].ToString() + " ";
		}
		//temp += ": Parameters passed = ( " + patternList.Length.ToString() + " , " + patternList.Min().ToString() + " , " + patternList.Max().ToString() + ")"; 
		UnityEngine.Debug.Log(temp);
	}

	public void Print(int testLength)
	/**
	Prints the pattern when asked like a boss
	**/
	{
		string temp = "Pattern = ";

		if(testLength<patternList.Length)
        {
			for (int i = 0; i < testLength; i++)
			{
				temp += patternList[i].ToString() + " ";
			}
		}
		else
        {
			for (int i = 0; i < patternList.Length; i++)
			{
				temp += patternList[i].ToString() + " ";
			}
		}		
		//temp += ": Parameters passed = ( " + patternList.Length.ToString() + " , " + patternList.Min().ToString() + " , " + patternList.Max().ToString() + ")"; 
		UnityEngine.Debug.Log(temp);
	}

	public bool UserClicked(int clickedCube)
    /**
	When the user clicks a block, this function checks if it fits the test pattern sequence.
	Return true if it does and false otherwise.
	 */
	{
		if(patternList[inputCheckVarItem] == clickedCube)
        {
			inputCheckVarItem++;
			return true;
        }
		else
			return false;
    }

	public int GetItemAtIndex(int index)
    {
		if (index >= 0 && index < patternList.Length)
			return patternList[index];
		else
			return -1;
    }

	public void setInputCheckVar()
    /**
	The main game maanger might need to reset
	inputCheckVarItem. This function exists to facillitate that.
	 */
	{
		inputCheckVarItem = 0;
    }

	public bool Complete()
	/**
	This function is called when the game is ready to get user input.
	Return true if the user has completed the pattern and false otherwise.
	 */
    {
		if(inputCheckVarItem>=patternList.Length)
        {
			return true;
        }
		else
        {
			return false;
        }
    }

	public void StepPattern()
    {
		activePatternItem++;
    }

	public bool IsComplete(int testlength)
    {
		if (activePatternItem > testlength)
			return true;
		else if (activePatternItem > patternList.Length)
			return true;
		else
			return false;
	}

	public void ResetPattern()
    {
		//patternOffTime = intervalDuration;
		pState = PatternState.ActiveDisplay;

		//set active pattern item to the start of the list (1...N)
		activePatternItem = 1;
	}
}
