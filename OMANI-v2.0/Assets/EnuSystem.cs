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
    }

    public void resetSystem()
    {
        foreach (WackaEnu wackaEnu in wackaEnus) { wackaEnu.resetEnu(); }
    }

}
