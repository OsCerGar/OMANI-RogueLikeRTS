using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Tutorial_Start : MonoBehaviour
{

    NPC masterWorker, player;
    [SerializeField]
    Light[] lights = new Light[8];
    Color[] oldColors = new Color[8];

    Light directionalLight;
    GameObject masterWorkerObjective, spotLight, backgroundLights, backgroundLights2;
    Cinemachine.CinemachineVirtualCamera startCamera, standardCamera;
    Cinemachine.PostFX.CinemachinePostFX cameraFX;
    [SerializeField]
    PostProcessingProfile postFX;
    LocomotionBrain locomotion;
    CharacterMovement control;

    float timer, oldIntensity, oldFogStart, oldFogEnd;
    [SerializeField]
    float totalTimer = 2;
    [SerializeField]
    bool lightsIn = false, lightsOut = false, gameplay = false;
    // Use this for initialization
    void Start()
    {
        cameraFX = FindObjectOfType<Cinemachine.PostFX.CinemachinePostFX>();

        spotLight = this.transform.Find("Lights/SpotLight").gameObject;
        backgroundLights = this.transform.Find("Lights/backgroundLight").gameObject;
        backgroundLights2 = this.transform.Find("Lights/backgroundLight2").gameObject;
        directionalLight = this.transform.Find("Lights/Aura Directional Light").GetComponent<Light>();
        locomotion = FindObjectOfType<LocomotionBrain>();
        control = FindObjectOfType<CharacterMovement>();
        int i = 0;

        foreach (Light light in lights)
        {
            oldColors[i] = light.color;
            i++;
        }

        //save normal values
        oldIntensity = directionalLight.intensity;
        oldFogStart = RenderSettings.fogStartDistance;
        oldFogEnd = RenderSettings.fogEndDistance;
        Debug.Log(oldFogEnd);
        Debug.Log(oldFogStart);
        masterWorker = this.transform.Find("MasterWorker").GetComponent<NPC>();
        masterWorkerObjective = this.transform.Find("Gameplay/MasterWorkerObjective").gameObject;
        player = FindObjectOfType<Player>();

        LightsOff();
        startCamera = this.transform.Find("Timeline/Cameras/StartCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();

    }

    private void Update()
    {
        if (lightsIn) { LightsOn(); }
        if (lightsOut) { LightsOff(); }
        if (gameplay) { Gameplay(); }
    }
    public void Gameplay()
    {
        startCamera.gameObject.SetActive(false);
        control.enabled = true;
        locomotion.enabled = true;
    }

    public void LightsOn()
    {
        cameraFX.m_Profile = postFX;
        RenderSettings.fogEndDistance = oldFogEnd;
        Debug.Log("Changed" + RenderSettings.fogEndDistance);
        RenderSettings.fogStartDistance = oldFogStart;
        spotLight.SetActive(true);
        backgroundLights.SetActive(true);
        backgroundLights2.SetActive(true);
        directionalLight.intensity = oldIntensity;
        int i = 0;
        foreach (Light light in lights)
        {
            light.color = oldColors[i];
            i++;
        }
    }
    public void LightsOff()
    {
        RenderSettings.fogStartDistance = 105;
        RenderSettings.fogEndDistance = 200;
        directionalLight.intensity = 0.01f;
        spotLight.SetActive(false);

        foreach (Light light in lights)
        {
            light.color = Color.red;
        }
    }
}
