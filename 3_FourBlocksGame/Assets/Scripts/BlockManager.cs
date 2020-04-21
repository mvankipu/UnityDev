using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public Material defaultMat;
    public Material highlightMat;

    public bool isHighlighted;

    public CountDownTimer onTime;

    // Start is called before the first frame update
    void Start()
    {
        isHighlighted = false;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;

        if (isHighlighted)
            this.SetMaterial(highlightMat);
        else
            this.SetMaterial(defaultMat);
        
        if(onTime!=null && onTime.IsComplete(deltaTime))
        {
            UnityEngine.Debug.Log("Delta Time: " + deltaTime.ToString());
            isHighlighted = false;
            onTime = null;
        }
    }

    void SetMaterial(Material tempMat)
    {
        this.GetComponent<Renderer>().material = tempMat;
    }

    public void setHighlighted(bool highlightVar)
    {
        UnityEngine.Debug.Assert(onTime == null);
        //UnityEngine.Debug.Log("Setting highlighted to a value");
        isHighlighted = highlightVar;
    }

    public void setHighlighted(CountDownTimer onTime)
    {
        UnityEngine.Debug.Log("Setting highlighted to true and setting countown timer");
        isHighlighted = true;
        this.onTime = onTime;
    }
}
