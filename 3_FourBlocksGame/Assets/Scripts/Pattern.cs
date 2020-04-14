using System;
using System.Linq;
using UnityEngine;

public class Pattern
{
	int[] patternList;

	enum PatternState
    {
		active,
		inactive,
    }

	PatternState pState;
	float startTime;
	float bufferTime;
	float inactiveTime;
	float onDeltaTime;
	float offDeltaTime;
	int activePatternItem;

	public Pattern(int patternLength = 3, int rangeMin = 1, int rangeMax = 4)
	{
		activePatternItem = -1;

		patternList = new int[patternLength];
		
		for(int i=0;i<patternLength;i++)
        {
			patternList[i] = UnityEngine.Random.Range(rangeMin, rangeMax + 1);
		}
	}
	
	public void startPattern(float onTime = 2.0f, float offTime = 1.0f)
    {
		activePatternItem = 1;
		onDeltaTime = onTime;
		offDeltaTime = offTime;
		startTime = onDeltaTime;
		bufferTime = offDeltaTime;
		pState = PatternState.active;
		//UnityEngine.Debug.Log(patternList[activePatternItem - 1].ToString());
	}

	
	public int getActivePatternItem(float deltaTime)
    {
		if(pState == PatternState.active)
        {
			startTime -= deltaTime;
			if (startTime <= 0.0)
			{
				activePatternItem++;

				//UnityEngine.Debug.Log(patternList[activePatternItem - 1].ToString());
				bufferTime = offDeltaTime;
				pState = PatternState.inactive;
				return 0;
			}
            else
            {
				return activePatternItem;
            }
		}
		else
        {
			bufferTime -= deltaTime;
			if (bufferTime <= 0.0)
			{
				if (activePatternItem > patternList.Length) //TODO WHY DOES THIS WORK?????
					return -1;
				else
                {
					startTime = onDeltaTime;
					pState = PatternState.active;
					return activePatternItem;
				}
			}
			else
				return 0;
		}
		//return activePatternItem;
    }

	public int getLength()
    {
		return patternList.Length;
    }

	public int getPatternListItem(int index)
    {
		//TODO Error Checking needed
		//UnityEngine.Debug.Log(index.ToString());
		return patternList[index-1];
    }

	public void print()
    {
		string temp = "Pattern = ";
		for (int i = 0; i < patternList.Length; i++)
		{
			temp += patternList[i].ToString() + " ";
		}
		//temp += ": Parameters passed = ( " + patternList.Length.ToString() + " , " + patternList.Min().ToString() + " , " + patternList.Max().ToString() + ")"; 
		UnityEngine.Debug.Log(temp);
	}
}
