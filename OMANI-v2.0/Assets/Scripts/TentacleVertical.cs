using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleVertical : MonoBehaviour {

    public GameObject PlaceHolderObjective;
    public Vector3 Objective;
    Vector3 initialAttackPos, finalAttackPos;
    bool attack = false;
    float counter = 0;
    [SerializeField]ParticleSystem WarningDust;
   
    // Update is called once per frame
    void Update()
    {
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
            else if (counter < 7) //From 5 to 7 rests
            {
            }
            else if(counter < 9)//From 7 to 9 return to ground
            {
                transform.position = Vector3.Lerp(transform.position, initialAttackPos, Time.deltaTime * 2);
            } else
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
    public void TentacleAttack(GameObject objective)
    {
        if (attack == false)
        {
            Transform objTransform = objective.transform;
            transform.position = new Vector3(objTransform.position.x, -5, objTransform.position.z);
            initialAttackPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            finalAttackPos = new Vector3(transform.position.x, 0f, transform.position.z);
            attack = true;
            ActivateWarning(finalAttackPos);
        }

    }

    private void ActivateWarning(Vector3 _pos)
    {
        WarningDust.transform.position = _pos;
        WarningDust.Play();
    }
}
