using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelloWorldPlusPlus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera tempCam = GetComponent<Camera>();
        
        tempCam.backgroundColor = new Color(1,1,1,1);
     }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("Application will quit.");
            Application.Quit();
        }
    }
}
