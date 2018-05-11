using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSpotBrain : MonoBehaviour {
    List<GameObject> SetOfWeakSpots = new List<GameObject>();
    DreamBoss boss;
    private bool StartCountDown;
    private float countdownLimit = 4, countdown = 0;

    // Use this for initialization
    void Start()
    {
        boss = FindObjectOfType<DreamBoss>();
        
        GetChildrenAndActivate();
        DeactivateWeakSpots();

    }

    private void GetChildrenAndActivate()
    {
        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            var child = transform.GetChild(i).gameObject;
            if (!child.activeSelf)
            {

                SetOfWeakSpots.Add(child);
                child.SetActive(true);
            }
            
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (SetOfWeakSpots.Count <= 0)
        {
            boss.Stun();
            GetChildrenAndActivate();
            DeactivateWeakSpots();
        }
        if (StartCountDown)
        {
            countdown += Time.deltaTime;
            if (countdown >= countdownLimit)
            {
                StartCountDown = false;
                countdown = 0;
                GetChildrenAndActivate();
            }
        }
    }
    public void ActivateWeakSpots()
    {
        foreach (var item in SetOfWeakSpots)
        {
            item.SetActive(true);
        }
    }
    public void DeactivateWeakSpots()
    {
        foreach (var item in SetOfWeakSpots)
        {
            item.SetActive(false);
        }
    }
    public void RemoveWeakSpot(GameObject WP)
    {
        SetOfWeakSpots.Remove(WP);
        WP.SetActive(false);
        StartCountDown = true;
    }
}
