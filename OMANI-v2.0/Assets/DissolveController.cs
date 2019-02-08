using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [SerializeField] Material dissolvingMaterial;
    bool dissolve = false;
    float dissolveAmount = 0.05f;


    // Update is called once per frame
    void Update()
    {
        if (dissolve)
        {
            dissolveAmount += Time.deltaTime / 2;
            MK.Toon.MKToonMaterialHelper.SetDissolveAmount(dissolvingMaterial, dissolveAmount);
        }
    }

    private void OnEnable()
    {
        MK.Toon.MKToonMaterialHelper.SetDissolveAmount(dissolvingMaterial, 0);
    }

    private void OnDisable()
    {
        MK.Toon.MKToonMaterialHelper.SetDissolveAmount(dissolvingMaterial, 0);
    }

    public void StartDissolving() { dissolve = true; }
}
