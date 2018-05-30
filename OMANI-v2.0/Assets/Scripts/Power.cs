using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour {

    //Amount of energy for every power;
    public float maxpowerPool = 0, powerPool = 0;

    public virtual void CastPower() {
        // do the power
    }

}
