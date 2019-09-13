using UnityEngine;

public class PassTalkInput : MonoBehaviour
{
    RPGTalk rpgTalk;
    // Start is called before the first frame update
    void Start()
    {
        rpgTalk = GetComponent<RPGTalk>();
    }
    private void Update()
    {
        if (PlayerInputInterface.inputs.GetButtonDown("PassTalk"))
        {
            if (rpgTalk.isPlaying)
            {
                rpgTalk.PlayNext();
            }
        }
    }
}
