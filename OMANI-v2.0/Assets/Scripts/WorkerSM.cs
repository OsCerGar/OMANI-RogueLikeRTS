using UnityEngine;

public class WorkerSM : SoundsManager
{
    public AudioClip FlipClip;
    public AudioSource robot_selection, damageRecieved, damageDealt;
    public void Flip()
    {
        AS.clip = FlipClip;
        AS.Play();
    }

    public void selectionRobot()
    {
        robot_selection.Play();
    }
    public void DamageRecieved()
    {
        damageRecieved.Play();
    }
    public void DamageDealt()
    {
        damageDealt.Play();
    }

}
