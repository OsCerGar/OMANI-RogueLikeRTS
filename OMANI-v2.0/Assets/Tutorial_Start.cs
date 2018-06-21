using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Start : MonoBehaviour
{

    NPC masterWorker, player;
    [SerializeField]
    Light[] lights = new Light[8];
    Light directionalLight;
    GameObject masterWorkerObjective, spotLight, backgroundLights, backgroundLights2;
    Cinemachine.CinemachineVirtualCamera startCamera, standardCamera;

    float timer, oldIntensity;
    [SerializeField]
    float totalTimer = 2;

    // Use this for initialization
    void Start()
    {
        spotLight = this.transform.Find("Lights/SpotLight").gameObject;
        backgroundLights = this.transform.Find("Lights/backgroundLight").gameObject;
        backgroundLights2 = this.transform.Find("Lights/backgroundLight2").gameObject;
        directionalLight = this.transform.Find("Lights/Aura Directional Light").GetComponent<Light>();
        oldIntensity = directionalLight.intensity;
        masterWorker = this.transform.Find("MasterWorker").GetComponent<NPC>();
        masterWorkerObjective = this.transform.Find("Gameplay/MasterWorkerObjective").gameObject;
        player = FindObjectOfType<Player>();

        LightsOff();

        /*
        startCamera = this.transform.Find("Cameras/StartCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        standardCamera = this.transform.parent.Find("CamBrain/Standard").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        masterWorker.Order(masterWorkerObjective);
        */
    }

    private void Update()
    {

        if (Vector3.Distance(masterWorker.transform.position, masterWorkerObjective.transform.position) <= 1)
        {
            timer += Time.deltaTime;
            if (timer > totalTimer)
            {
                LightsOn();
            }
        }
    }

    public void LightsOn()
    {
        spotLight.SetActive(true);
        backgroundLights.SetActive(true);
        backgroundLights2.SetActive(true);
        directionalLight.intensity = oldIntensity;
        foreach (Light light in lights)
        {
            light.color = Color.green;
        }
    }
    public void LightsOff()
    {
        directionalLight.intensity = 0.01f;
        spotLight.SetActive(false);
        backgroundLights.SetActive(false);
        backgroundLights2.SetActive(false);

        foreach (Light light in lights)
        {
            light.color = Color.red;
        }
    }
}
