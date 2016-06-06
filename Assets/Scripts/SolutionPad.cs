using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SolutionPad : MonoBehaviour {

    public int solutionNumber;


    
    // Use this for initialization
    void Start ()
    {
        gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = solutionNumber.ToString();
    }

    

}
