using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour {
    List<Material> OriginalMats;
    List<Renderer> Renderers;
    private void Start()
    {
        var findMaterials = GetComponentsInChildren<Renderer>();

        foreach (var item in findMaterials)
        {
            Renderers.Add(item);
            OriginalMats.Add(new Material(item.material));
        }

    }
    public void ReplaceMats (Material _mat)
        {
            foreach (var item in Renderers)
            {
                item.material = _mat;
            }
        }
    public void Restore()
    {
        for (int i = 0; i < Renderers.Count; i++)
        {
            Renderers[i].material = OriginalMats[i];
        }
    }

}
