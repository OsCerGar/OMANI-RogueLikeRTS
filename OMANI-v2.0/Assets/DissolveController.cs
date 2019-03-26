using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [SerializeField] Renderer dissolvingMaterial;
    bool dissolve = false;
    float dissolveAmount = 0.05f;

    private void Start()
    {
        dissolvingMaterial = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dissolve)
        {
            dissolveAmount += Time.deltaTime / 2;
            MK.Toon.MKToonMaterialHelper.SetDissolveAmount(dissolvingMaterial.material, dissolveAmount);
        }
    }

    private void OnEnable()
    {
        dissolvingMaterial = GetComponent<Renderer>();

        MK.Toon.MKToonMaterialHelper.SetDissolveAmount(dissolvingMaterial.material, 0);
    }

    private void OnDisable()
    {
        dissolvingMaterial = GetComponent<Renderer>();

        MK.Toon.MKToonMaterialHelper.SetDissolveAmount(dissolvingMaterial.material, 0);
    }

    public void StartDissolving() { dissolve = true; }
}
