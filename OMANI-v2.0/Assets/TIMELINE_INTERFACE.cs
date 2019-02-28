using UnityEngine;
using UnityEngine.Playables;

public class TIMELINE_INTERFACE : MonoBehaviour
{

    public PlayableDirector timeline;

    public void TPlay() { timeline.Play(); }
    public void TStop() { timeline.Stop(); }

   


}
