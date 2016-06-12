using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {


    public float startX, endX;
	// Use this for initialization
	void Start ()
    {
        gameObject.GetComponent<LineRenderer>().SetPosition(0,new Vector3(startX,0,0));
        gameObject.GetComponent<LineRenderer>().SetPosition(1, new Vector3(endX, 0, 0));

    }


}
