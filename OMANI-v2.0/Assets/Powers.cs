using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{
    [SerializeField]
    public List<Power> power = new List<Power>();
    int selectedPower = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            power[selectedPower].CastPower();
        }
    }


}
