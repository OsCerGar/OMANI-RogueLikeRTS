using UnityEngine;

public class WorkerSM : SoundsManager
{
    public AudioSource FlipClip;
    public AudioSource robot_selection, damageRecieved, MaterializeSound;
    public void Flip()
    {
        FlipClip.Play();
    }

    public void selectionRobot()
    {
        if (robot_selection != null)
        {
            robot_selection.Play();
        }
    }
    public void DamageRecieved()
    {
        if (damageRecieved != null)
        {
            damageRecieved.Play();
        }
    }
   
    public void Materialize()
    {
        MaterializeSound.Play();
    }
    public void Dematerialize()
    {
        MaterializeSound.Play();
    }

}
