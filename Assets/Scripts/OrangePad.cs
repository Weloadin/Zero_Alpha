using UnityEngine;
using System.Collections;

public class OrangePad : MonoBehaviour {

    public enum OrangePadState
    {
        Idle,
        Move,
        ReachGoal,
        Lose
    };

    private OrangePadState currentState;
    private int padSpeed = 2 ;


    protected void OnDestroy()
    {
        


    }

    void Awake()
    {
        currentState = OrangePadState.Idle;                
    }



    void Update()
    {
        switch(currentState)
        {
            case OrangePadState.Idle:
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        currentState = OrangePadState.Move;
                    }
                }
                break;
            case OrangePadState.Move:
                {
                    this.transform.position -= this.transform.up * padSpeed * Time.deltaTime;
                }
                break;
            case OrangePadState.ReachGoal:
                {

                }
                break;
            case OrangePadState.Lose:
                { }
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("RotateTrigger"))
        {
            this.transform.rotation = other.GetComponentInParent<Transform>().rotation;
        }
    }
}
