using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdditionPad : MonoBehaviour {

    static private List<AdditionPad> lAddition;

    protected void Awake()
    {
        // does the pool exist yet?
        if (lAddition == null)
        {
            // lazy initialize it
            lAddition = new List<AdditionPad>();
        }
        // add myself to the pool
        lAddition.Add(this);
    }

    protected void OnDestroy()
    {
        // remove myself from the pool
        lAddition.Remove(this);
        // was I the last one?
        if (lAddition.Count == 0)
        {
            // remove the pool itself
            lAddition = null;
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("OrangePad"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            other.GetComponent<OrangePad>().isAddition = true;
        }
    }


}
