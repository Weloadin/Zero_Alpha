using UnityEngine;
using System.Collections;

public class GoalPad : MonoBehaviour
{

    public int goalNumber;



    // Use this for initialization
    void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = goalNumber.ToString();
    }

}
