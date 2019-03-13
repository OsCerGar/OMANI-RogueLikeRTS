using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonActivated : MonoBehaviour
{
    BU_Building_ActionTutorial action2;
    bool done;
    TIMELINE_INTERFACE timeline;

    private void Start()
    {
        action2 = GetComponent<BU_Building_ActionTutorial>();
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
