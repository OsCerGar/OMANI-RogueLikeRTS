using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Start : MonoBehaviour
{

    NPC masterWorker, player;
    GameObject masterWorkerObjective, spotLight, backgroundLights, backgroundLights2;
    Cinemachine.CinemachineVirtualCamera startCamera, standardCamera;

    float timer;
    [SerializeField]
    float totalTimer = 2;

    // Use this for initialization
    void Start()
    {
        masterWorker = this.transform.Find("MasterWorker").GetComponent<NPC>();
        masterWorkerObjective = this.transform.Find("Gameplay/MasterWorkerObjective").gameObject;
        player = FindObjectOfType<Player>();
        spotLight = this.transform.Find("Lights/SpotLight").gameObject;
        backgroundLights = this.transform.Find("Lights/backgroundLight").gameObject;
        backgroundLights2 = this.transform.Find("Lights/backgroundLight2").gameObject;

        spotLight.SetActive(false);
        backgroundLights.SetActive(false);
        backgroundLights2.SetActive(false);

        startCamera = this.transform.Find("Cameras/StartCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        standardCamera = this.transform.parent.Find("CamBrain/Standard").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        masterWorker.Order(masterWorkerObjective);
    }

    private void Update()
    {
        if (Vector3.Distance(masterWorker.transform.position, masterWorkerObjective.transform.position) <= 1)
        {
            startCamera.Follow = player.transform;
            startCamera.LookAt = player.transform;

            timer += Time.deltaTime;
            if (timer > totalTimer)
            {
                startCamera.gameObject.SetActive(false);
                standardCamera.gameObject.SetActive(true);


                spotLight.SetActive(enabled);
                backgroundLights.SetActive(enabled);
                backgroundLights2.SetActive(enabled);
            }
        }
    }
}
