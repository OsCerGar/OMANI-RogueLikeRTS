using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanCorruption : MonoBehaviour
{
    Renderer[] AllChildrenRenderers;
    [SerializeField] ParticleSystem BoltsPE;
    [SerializeField] ParticleSystem CorruptionPE;
    Projector Proj;
    float dissolveCounter;
    // Start is called before the first frame update
    void Start()
    {
        
        dissolveCounter = 0;
        AllChildrenRenderers = GetComponentsInChildren<Renderer>();
        Proj = GetComponentInChildren<Projector>();
        DissolveAndClear();
    }


    


    public void DissolveAndClear()
    {
        BoltsPE.Play();
        CorruptionPE.Play();
        StartCoroutine(Dematerialize(Time.deltaTime/4));
    }
    private IEnumerator Dematerialize(float waitTime)
    {
        while (dissolveCounter < 1)
        {
            dissolveCounter += waitTime;
            foreach (var renderer in AllChildrenRenderers)
            {
                MK.Toon.MKToonMaterialHelper.SetDissolveAmount(renderer.material, dissolveCounter);
                Proj.orthographicSize += 0.05f;
                //transform.position -= transform.up * Time.deltaTime / 100;
            }

            yield return new WaitForSeconds(waitTime);
        }
        dissolveCounter = 0;
        while (dissolveCounter < 1)
        {
            dissolveCounter += waitTime;
            foreach (var renderer in AllChildrenRenderers)
            {
                Proj.orthographicSize += 0.05f;
                transform.position -= transform.up * Time.deltaTime / 40;
            }

            yield return new WaitForSeconds(waitTime);
        }


    }
}
