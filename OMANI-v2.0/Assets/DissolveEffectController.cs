using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffectController : MonoBehaviour {
    bool Dissolve;
    // Use this for initialization
    float Progress;
    Renderer[] Renderers;
	void Start () {
        Renderers = GetComponentsInChildren<Renderer>();
       

    }
    private void Update()
    {
        if (Dissolve)
        {
            if (Progress > 0)
            {
                Progress -= Time.deltaTime / 10;
                foreach (var item in Renderers)
                {
                    var color = item.material.color;
                    color.a = Progress;
                    item.material.color = color;
                }
            }else
            {
                foreach (var item in Renderers)
                {
                    var color = item.material.color;
                    color.a = 1;
                    item.material.color = color;
                }
                transform.parent.gameObject.SetActive(false);
            }
           
        }
        else if (Progress < 1)
        {

            Progress += Time.deltaTime;
            foreach (var item in Renderers)
            {
                var color = item.material.color;
                color.a = Progress;
                item.material.color = color;
            }
        }
    }
    public void StartDissolve()
    {
            Dissolve = true;
    }
    public void StartRevert()
    {
        Dissolve = false;
    }

}
