using System.Collections.Generic;
using UnityEngine;

public class EnuSystem : MonoBehaviour
{

    public List<WackaEnu> wackaEnus = new List<WackaEnu>();
    Animator systemAnimator;
    bool systemUP;
    // Use this for initialization
    void Start()
    {
        wackaEnus.AddRange(GetComponentsInChildren<WackaEnu>());
        systemAnimator = GetComponent<Animator>();

    }

    public void startSystem()
    {
        systemUP = true;
        systemAnimator.SetInteger("State", Random.Range(1, 6));
    }

    public void stopSystem()
    {
        systemUP = false;
        systemAnimator.SetFloat("Speed", 0.5f);
        systemAnimator.SetInteger("State", 0);
    }

    public void resetSystem()
    {
        if (systemUP)
        {
            foreach (WackaEnu wackaEnu in wackaEnus) { wackaEnu.resetEnu(); }
            systemAnimator.SetFloat("Speed", systemAnimator.GetFloat("Speed") + 0.25f);

            bool done = false;
            int randomRange = Random.Range(1, 6);

            while (!done)
            {
                if (randomRange == systemAnimator.GetFloat("State"))
                {
                    randomRange = Random.Range(1, 6);
                }
                else
                {
                    systemAnimator.SetInteger("State", Random.Range(1, 6));
                    done = true;
                }
            }
        }
    }

}
