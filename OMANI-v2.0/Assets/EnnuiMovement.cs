using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnuiMovement : MonoBehaviour {
    [SerializeField]float Force;
    [SerializeField] Rigidbody[] AllRb;
    float counter;

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 1)
        {

        foreach (var item in AllRb)
            {
            var randomizer = new Vector3(Random.Range(-Force, Force), Random.Range(-Force, Force), Random.Range(-Force, Force));
            item.AddForce(randomizer,ForceMode.Force);
            }
            counter = 0;
        }
    }
}
