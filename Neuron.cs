using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//=====================================================================================
// This class is the basis for all Neurons
//=====================================================================================
public class Neuron : MonoBehaviour
{
    public float magnitudeX0;
    public float weightingX0;

    public float magnitudeX1;

    public float numOfVal;


    public float result;
     public void setValues1(float magnitudeX0, float weightingX0)
    {
        this.weightingX0 = weightingX0;
        this.magnitudeX0 = magnitudeX0;

        numOfVal = 1;
    }

    public void setValues2(float magnitudeX0, float magnitudeX1, float weightingX0)
    {
        this.weightingX0 = weightingX0;
        this.magnitudeX0 = magnitudeX0;
        this.magnitudeX1 = magnitudeX1;

        numOfVal = 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

   

    public float getResult()
    {
        switch (numOfVal)
        {
            case 1:
                result = weightingX0 * magnitudeX0;
                return result;

            case 2:
                result = (weightingX0 * magnitudeX0) + (weightingX0 * magnitudeX1);
                return result;

            default:
                throw new ArgumentException("Neuron can not compute this number of values: Unfortunately the student who coded this must not be very smart if you're getting this error.");
        }
        
    }

   
}
