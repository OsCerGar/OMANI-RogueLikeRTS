using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHealthDisplay : MonoBehaviour {
    //substituir WALL por BUILDING!
    [SerializeField] Wall npcScript;
    [SerializeField] Image hpDisplay;
    Camera cam;
    // Use this for initialization
    void Start()
    {
        hpDisplay = transform.Find("HealthBack/Health").GetComponent<Image>();
        npcScript = transform.GetComponentInParent<Wall>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        hpDisplay.fillAmount = (float)npcScript.ScrapCounter / npcScript.scrapNeeded;
        transform.LookAt(cam.transform);
    }
}
