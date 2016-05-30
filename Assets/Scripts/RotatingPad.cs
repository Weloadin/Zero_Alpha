using UnityEngine;
using System.Collections;

public class RotatingPad : MonoBehaviour {

    public enum RotatingPadState
    {
        RotateUp,
        RotateRight,
        RotateDown,
        RotateLeft
    };

    public RotatingPadState ChangeStateTo;
	
	void OnMouseDown()
    {
        switch(ChangeStateTo)
        {
            case RotatingPadState.RotateUp:
                {
                    Quaternion newRotation = Quaternion.Euler(0, 0, 180);
                    this.transform.rotation = newRotation;
                    ChangeStateTo = RotatingPadState.RotateRight;
                }
                break;
            case RotatingPadState.RotateRight:
                {
                    Quaternion newRotation = Quaternion.Euler(0, 0, 90);
                    this.transform.rotation = newRotation;
                    ChangeStateTo = RotatingPadState.RotateDown;
                }
                break;
            case RotatingPadState.RotateDown:
                {
                    Quaternion newRotation = Quaternion.Euler(0, 0, 0);
                    this.transform.rotation = newRotation;
                    ChangeStateTo = RotatingPadState.RotateLeft;
                }
                break;
            case RotatingPadState.RotateLeft:
                {
                    Quaternion newRotation = Quaternion.Euler(0, 0, 270);
                    this.transform.rotation = newRotation;
                    ChangeStateTo = RotatingPadState.RotateUp;
                }
                break;
        }
    }
}                                                                   
