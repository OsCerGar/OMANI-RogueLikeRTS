using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffectController : MonoBehaviour {
    bool Dissolve , Revert, readyToDissolve = false;
    // Use this for initialization
    float Progress;
    [SerializeField]GameObject StandardMeshes;
    Renderer[] DissolveMats;
	void Start () {
        DissolveMats = GetComponentsInChildren<Renderer>();
       
        foreach (var item in DissolveMats)
        {
            item.material.SetFloat("_Progress", 0);
        }

    }
    private void Update()
    {
        if (Dissolve)
        {
            if (!readyToDissolve)
            {
                if (Progress < 1)
                {
                    //2 seconds to prepare to disolve
                    Progress += Time.deltaTime / 5;
                    foreach (var item in DissolveMats)
                    {
                        item.material.SetFloat("_Progress", Progress);
                    }

                }
                else
                {
                    StandardMeshes.SetActive(false);
                    readyToDissolve = true;
                }
            }
            else
            {
                if (Progress > 0)
                {
                    //2 seconds to completely dissolve
                    Progress -= Time.deltaTime / 10;
                    foreach (var item in DissolveMats)
                    {
                        item.material.SetFloat("_Progress", Progress);
                    }

                }
                else
                {
                    //Bye bye Robot
                    transform.parent.gameObject.SetActive(false);
                }
            }

        }
        else if (Revert)
        {
            if (!readyToDissolve)
            {

                StandardMeshes.SetActive(true);
                if (Progress > 0)
                {
                    //2 seconds to completely revert the effect
                    Progress -= Time.deltaTime / 10;
                    foreach (var item in DissolveMats)
                    {
                        item.material.SetFloat("_Progress", Progress);
                    }

                }
                else
                {
                    //Resurrected, Hooray

                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                if (Progress < 1)
                {
                    //2 to  revert dissolving
                    Progress += Time.deltaTime / 5;
                    foreach (var item in DissolveMats)
                    {
                        item.material.SetFloat("_Progress", Progress);
                    }

                }
                else
                {
                    readyToDissolve = false;
                    StandardMeshes.SetActive(false);
                }
            }
        }
    }
    public void StartDissolve()
    {
        readyToDissolve = false;
        Progress = 0;
        Dissolve = true;
        foreach (var item in DissolveMats)
        {
            item.material.SetFloat("_Progress", 0);
        }
    }
    public void StartRevert()
    {
        Dissolve = false;
        Revert = true;
    }

}
