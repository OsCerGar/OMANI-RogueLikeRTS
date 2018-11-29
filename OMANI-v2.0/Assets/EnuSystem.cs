using System.Collections.Generic;
using UnityEngine;

public class EnuSystem : MonoBehaviour
{

    public List<WackaEnu> wackaEnus = new List<WackaEnu>();
    Animator systemAnimator;
    // Use this for initialization
    void Start()
    {
        wackaEnus.AddRange(GetComponentsInChildren<WackaEnu>());
        systemAnimator = GetComponent<Animator>();

    }

    public void startSystem()
    {
        systemAnimator.SetInteger("State", Random.Range(1, 6));
    }

    public void stopSystem()
    {
        systemAnimator.SetFloat("Speed", 0.5f);
        systemAnimator.SetInteger("State", 0);
        foreach (WackaEnu wackaEnu in wackaEnus) { wackaEnu.resetEnu(); }
    }

    public void resetSystem()
    {
        foreach (WackaEnu wackaEnu in wackaEnus) { wackaEnu.resetEnu(); }
        systemAnimator.SetFloat("Speed", systemAnimator.GetFloat("Speed") + 0.1f);

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
