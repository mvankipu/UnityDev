using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //UnityEngine.Debug.Log("Mouse Down");

            //onMouseDown Event detected
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //onMouseDown Event detected on object
                //UnityEngine.Debug.Log("Mouse Down on Object " + hit.collider.gameObject.ToString());

                //Change Material of Game Object
                hit.collider.gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
            else
            {
                Camera tempCam = GetComponent<Camera>();
                tempCam.backgroundColor = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
        }
    }
}
