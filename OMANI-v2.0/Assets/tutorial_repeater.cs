﻿using UnityEngine;

public class tutorial_repeater : MonoBehaviour
{
    [SerializeField] CleanCorruption cleanCorruption;
    [SerializeField] Interactible_Repeater repeater;
    TIMELINE_INTERFACE timeline_interface;

    bool done;
    // Start is called before the first frame update
    void Start()
    {
        repeater = GetComponent<Interactible_Repeater>();
        timeline_interface = GetComponent<TIMELINE_INTERFACE>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (repeater.energy > 0) { done = true; cleanCorruption.DissolveAndClear(); }
        }
    }
}
