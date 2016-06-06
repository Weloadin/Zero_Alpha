using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalculationManager : MonoBehaviour {

    public enum Calculation
    {
        Nothing,
        Add,
        Subtract,
        Multiply
    };

    public  Calculation currentCalculation;

    static private List<CalculationManager> calculationManagers;


    static public CalculationManager Calculate(GameObject orangePad, GameObject solutionPad, Calculation calculationState)
    {
        foreach (CalculationManager calculationManager in calculationManagers)
        {

            int givenNum1 = orangePad.GetComponent<OrangePad>().givenNumber;
            int givenNum2 = solutionPad.GetComponent<SolutionPad>().solutionNumber;

            // if disabled, then it's available
           calculationManager.currentCalculation = calculationState;
            switch (calculationManager.currentCalculation)
            {
                case Calculation.Nothing:
                    {

                    }
                    break;
                case Calculation.Add:
                    {
                       int answer = givenNum1 + givenNum2;
                       givenNum1 = answer;
                       orangePad.GetComponent<OrangePad>().givenNumber = givenNum1;
                        orangePad.transform.GetChild(0).GetComponent<TextMesh>().text = givenNum1.ToString();
                    }
                    break;
                case Calculation.Subtract:
                    {

                    }
                    break;
                case Calculation.Multiply:
                    {

                    }
                    break;
            }
            return calculationManager;

        }
        
        return null;
    }



    static public CalculationManager  SetState(Calculation newCalculationState)
    {
        foreach (CalculationManager calculationManager in calculationManagers)
        {
            calculationManager.currentCalculation = newCalculationState;
            return calculationManager;
        }

        return null;
    }


    protected void Awake()
    {
        // does the pool exist yet?
        if (calculationManagers == null)
        {
            // lazy initialize it
            calculationManagers = new List<CalculationManager>();
        }
        // add myself to the pool
        calculationManagers.Add(this);
    }

    

    protected void OnDestroy()
    {
        // remove myself from the pool
        calculationManagers.Remove(this);
        // was I the last one?
        if (calculationManagers.Count == 0)
        {
            // remove the pool itself
            calculationManagers = null;
        }
    }

}
