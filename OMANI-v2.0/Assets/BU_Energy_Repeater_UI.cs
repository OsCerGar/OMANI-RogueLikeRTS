using System.Collections.Generic;
using UnityEngine;

public class BU_Energy_Repeater_UI : MonoBehaviour
{

    List<MeshRenderer> materialList = new List<MeshRenderer>();
    // Use this for initialization
    void Start()
    {
        materialList.AddRange(transform.GetComponentsInChildren<MeshRenderer>());

        foreach (MeshRenderer uiMaterial in materialList)
        {
            uiMaterial.material.mainTextureOffset = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach (MeshRenderer uiMaterial in materialList)
        {
            Vector2 textureOffset = uiMaterial.material.mainTextureOffset;
            uiMaterial.material.mainTextureOffset = new Vector2(textureOffset.x, textureOffset.y - 0.5f * Time.deltaTime);
        }

    }
}
