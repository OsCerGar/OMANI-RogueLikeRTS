using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonActivated : MonoBehaviour
{
    [SerializeField]
    Collider action2;
    bool done;
    TIMELINE_INTERFACE timeline;

    private void Start()
    {
        action2 = GetComponent<Collider>();
        timeline = GetComponent<TIMELINE_INTERFACE>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!done) { if (!action2.enabled) { done = true; inputGuidesOn(); } }
    }

    public void inputGuidesOn()
    {
        timeline.TPlay();
    }
}
