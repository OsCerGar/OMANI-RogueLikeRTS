using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHorizontal : MonoBehaviour {
    public GameObject PlaceHolderObjective;
    public Vector3 Objective;
    Vector3 initialAttackPos, finalAttackPos;
    bool attack = false;
    float counter = 0;
    [SerializeField] ParticleSystem WarningDust;

    // Update is called once per frame
    void Update () {
        if (attack)
        {
            counter += Time.deltaTime;
            if (counter < 2.5f)
            {
            }
            else if (counter < 5) //from 2.5 to 5 seconds the attack happens
            {
                transform.position = Vector3.Lerp(transform.position, finalAttackPos, Time.deltaTime * 2);

            }
            else if (counter < 9) //From 5 to 9 rests
            {
            }
            else if (counter < 11)//From 5 to 9 return to ground
            {
                transform.position = Vector3.Lerp(transform.position, initialAttackPos, Time.deltaTime * 2);
            }
            else
            {
                attack = false;
                counter = 0;
            }

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TentacleAttack(PlaceHolderObjective);
        }
    }
    public void  TentacleAttack(GameObject objective)
    {
        if (attack == false)
        {
            transform.position = new Vector3(transform.position.x, -2, transform.position.z);
            initialAttackPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
            finalAttackPos = new Vector3(transform.position.x, 1.5f, transform.position.z);
            transform.LookAt(new Vector3(objective.transform.position.x, -2, objective.transform.position.z));
            attack = true;
            ActivateWarning(objective.transform.position);
        }
        
    }

    private void ActivateWarning(Vector3 _pos)
    {
        WarningDust.transform.position= new Vector3(transform.position.x, 0f, transform.position.z);
        WarningDust.transform.LookAt(new Vector3(_pos.x, 0, _pos.z));
        WarningDust.Play();
        //Put Warning 
    }
}
