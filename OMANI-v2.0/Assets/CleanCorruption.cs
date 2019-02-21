using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CleanCorruption : MonoBehaviour
{
    MeshRenderer[] AllChildrenRenderers;
    SkinnedMeshRenderer[] AllChildrenSkinnedRenderers;
    Animator[] anims;
    [SerializeField] ParticleSystem BoltsPE;
    [SerializeField] ParticleSystem CorruptionPE;
    float dissolveDistance;
    Light Pointlight;

    
    // Start is called before the first frame update
    void Start()
    {
        Pointlight = GetComponentInChildren<Light>();
        Pointlight.transform.parent = null;
        dissolveDistance = 0;
        AllChildrenRenderers = GetComponentsInChildren<MeshRenderer>();
        AllChildrenSkinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

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
        }
        

        transform.gameObject.SetActive(false);


    }
}
