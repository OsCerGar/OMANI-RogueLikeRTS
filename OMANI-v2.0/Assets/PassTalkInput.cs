using UnityEngine;

public class PassTalkInput : MonoBehaviour
{
    RPGTalk rpgTalk;
    // Start is called before the first frame update
    void Start()
    {
        rpgTalk = GetComponent<RPGTalk>();
        if (PlayerInputInterface.inputs.GetButtonDown("PassTalk")) { Debug.Log("PassTAlk"); rpgTalk.PlayNext(); }
    }
}
