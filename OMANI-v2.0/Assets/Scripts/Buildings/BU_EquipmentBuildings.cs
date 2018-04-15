using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_EquipmentBuildings : MonoBehaviour
{

    [SerializeField]
    public Component buildingTypeAndBehaviour;

    BU_WeaponsBay_GUI weaponsBayGUI;
    public int totalEnergy, requiredEnergy;
    float time = 0, timeToExplode = 20;

    [SerializeField]
    BU_Plug[] plugs;

    List<MeshRenderer> plugMaterial = new List<MeshRenderer>();
    [SerializeField]
    AudioSource[] audios;
    [SerializeField]
    AudioSource alarm, explosion, working;

    ParticleSystem particleExplosion;

    // Temporal 
    float creationTime;

    // Use this for initialization
    void Start()
    {

        weaponsBayGUI = this.transform.GetComponentInChildren<BU_WeaponsBay_GUI>();

        plugs = this.transform.GetChild(0).GetComponentsInChildren<BU_Plug>();

        foreach (BU_Plug plug in plugs)
        {
            plugMaterial.Add(plug.gameObject.GetComponent<MeshRenderer>());
        }

        particleExplosion = this.GetComponentInChildren<ParticleSystem>();

        foreach (AudioSource audios in transform.GetComponents<AudioSource>())
        {
            switch (audios.clip.name)
            {
                case "SOUND_BU_ALARM":
                    alarm = audios;
                    break;
                case "SOUND_BU_EXPLOSION":
                    explosion = audios;
                    break;
                case "SOUND_BU_WORKING":
                    working = audios;
                    break;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

        totalEnergy = 0;

        foreach (BU_Plug plug in plugs)
        {
            totalEnergy += plug.energy;
        }

        if (buildingTypeAndBehaviour != null)
        {
            working.volume = 0.3f;
            if (requiredEnergy > totalEnergy)
            {
                weaponsBayGUI.ChangeEnergyColor(Color.red);
                weaponsBayGUI.ChangeEnergyClock(ExplodeTime());

                alarm.volume = 0.7f;

                time += Time.deltaTime;

                if (time > timeToExplode)
                {
                    DestroyBuilding();
                    time = 0;
                    alarm.volume = 0;
                }
            }
            else
            {
                weaponsBayGUI.ChangeEnergyColor(Color.yellow);
                weaponsBayGUI.ChangeEnergyClock(creationTime);

                alarm.volume = 0;
                time = 0;
                TurnToWhite();

            }
        }

        else
        {
            working.volume = 0;

            TurnToWhite();

        }
    }

    //Temporal

    public void ReturnCreationTime(float _creationTime)
    {
        creationTime = _creationTime;
    }

    public float ExplodeTime()
    {
        return time / timeToExplode;
    }


    public void TurnToRed()
    {
        bool changedToRed = false;

        foreach (MeshRenderer plugMaterials in plugMaterial)
        {
            if (changedToRed != true && requiredEnergy > totalEnergy)
            {
                if (plugMaterials.material.color == Color.white)
                {
                    plugMaterials.material.color = Color.red;
                    changedToRed = true;
                }
            }
        }
    }

    public void TurnToWhite()
    {
        foreach (MeshRenderer plugMaterials in plugMaterial)
        {
            if (plugMaterials.material.color != Color.yellow)
            {
                plugMaterials.material.color = Color.white;
            }
        }
    }

    private void DestroyBuilding()
    {
        particleExplosion.Play();
        explosion.Play();
        Destroy(buildingTypeAndBehaviour);
        buildingTypeAndBehaviour = null;
        requiredEnergy = 0;
    }


}
