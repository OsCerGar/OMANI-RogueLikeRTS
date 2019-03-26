using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonActivated : MonoBehaviour
{
    [SerializeField]
    Collider action2;
    bool done;
    TIMELINE_INTERFACE timeline;
    [SerializeField]
    List<Tutorial_ArmLock> armlocks = new List<Tutorial_ArmLock>();
    private void Start()
    {
        action2 = GetComponent<Collider>();
        timeline = GetComponent<TIMELINE_INTERFACE>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!done)
        {
            if (!action2.enabled)
            {
                done = true; inputGuidesOn();

                foreach (Tutorial_ArmLock armlock in armlocks)
                {
                    RimEffect rim = armlock.GetComponent<RimEffect>();
                    rim.enabled = true;

                }

            }
        }
    }

    public void inputGuidesOn()
    {
        timeline.TPlay();
    }
}
