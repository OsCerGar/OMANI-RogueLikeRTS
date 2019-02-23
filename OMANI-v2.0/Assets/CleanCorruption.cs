using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;
public class CleanCorruption : MonoBehaviour
{
    MeshRenderer[] AllChildrenRenderers;
    SkinnedMeshRenderer[] AllChildrenSkinnedRenderers;
    Animator[] anims;
    [SerializeField] ParticleSystem BoltsPE;
    [SerializeField] ParticleSystem CorruptionPE;
    EnemyPooler EnemyPooler;
    [SerializeField] GameObject PositionToSpawn;
    float dissolveDistance;
    Light Pointlight;

    [SerializeField] GameObject cmFreeCam;
    [SerializeField] bool spawn;
    AudioSource ScreamSound;

    
    // Start is called before the first frame update
    void Start()
    {
        ScreamSound = GetComponent<AudioSource>();
        Pointlight = GetComponentInChildren<Light>();
        Pointlight.transform.parent = null;
        dissolveDistance = 0;
        AllChildrenRenderers = GetComponentsInChildren<MeshRenderer>();
        AllChildrenSkinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        EnemyPooler = FindObjectOfType<EnemyPooler>();

        foreach (var renderer in AllChildrenRenderers)
        {
            renderer.shadowCastingMode = ShadowCastingMode.Off;
        }

        foreach (var renderer in AllChildrenSkinnedRenderers)
        {
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            
        }
    }


    


    public void DissolveAndClear()
    {
        BoltsPE.Play();
        CorruptionPE.Play();
        StartCoroutine(Dematerialize(0.1f));
    }
    private IEnumerator Dematerialize(float DistanceGrower)
    {
        bool shaked = false;
        dissolveDistance = 8;
        while (dissolveDistance < 35)
        {
            dissolveDistance += DistanceGrower;

            //Loop for mesh renderers 
            // ---- We check a distance, that we keep growing, and if within distance, we start adding to the dissolveamount
            //  PointLight also grows in intensity 
            foreach (var renderer in AllChildrenRenderers)
            {
                if (Vector3.Distance (Pointlight.transform.position,renderer.transform.position) < dissolveDistance)
                {
                    
                        MK.Toon.MKToonMaterialHelper.SetDissolveAmount(renderer.material, MK.Toon.MKToonMaterialHelper.GetDissolveAmount(renderer.material) + Time.deltaTime);
                }
                
            }

            foreach (var renderer in AllChildrenSkinnedRenderers)
            {
                if (Vector3.Distance(Pointlight.transform.position, renderer.transform.position) < dissolveDistance)
                {
                    renderer.transform.parent.GetComponent<Corruption>().Die();
                    MK.Toon.MKToonMaterialHelper.SetDissolveAmount(renderer.material, MK.Toon.MKToonMaterialHelper.GetDissolveAmount(renderer.material) + Time.deltaTime);

                    
                    
                }

            }

            if (Pointlight.intensity < 1.5f)
            {
                Pointlight.intensity += 0.05f;
            }
            yield return new WaitForSeconds(Time.deltaTime);

            if (spawn)
            {
                if (dissolveDistance > 20f && !shaked)
                {
                    shaked = true;
                    StartCoroutine(enableShake());
                }
            }
            
        }
        if (spawn)
        {
            EnemyPooler.SpawnEnemy("SurkaMele", PositionToSpawn.transform);
            //soundandshake
            
        }
        transform.gameObject.SetActive(false);


    }
    private IEnumerator enableShake()
    {
        cmFreeCam.SetActive(true);
        ScreamSound.Play();
        yield return new WaitForSeconds(1.5f);
        cmFreeCam.SetActive(false);

    }
}
