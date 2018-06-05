using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] NPC npcScript;
    [SerializeField] Image hpDisplay;
    Camera cam;
    // Use this for initialization
    void Start()
    {
        hpDisplay = transform.Find("HealthBack/Health").GetComponent<Image>();
        npcScript = transform.parent.GetComponent<NPC>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        hpDisplay.fillAmount = (float)npcScript.life / npcScript.startLife;
        transform.LookAt(cam.transform);
    }
}
