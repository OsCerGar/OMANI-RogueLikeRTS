using UnityEngine;

public class RimEffect : MonoBehaviour
{
    Renderer renderer;
    float amount;
    bool pimpam;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (amount < 0) { pimpam = true; }
        if (amount > 1) { pimpam = false; }
        if (pimpam)
        {
            amount += Time.deltaTime;
            MK.Toon.MKToonMaterialHelper.SetRimIntensity(renderer.material, amount);

        }
        else
        {
            amount -= Time.deltaTime;
            MK.Toon.MKToonMaterialHelper.SetRimIntensity(renderer.material, amount);
        }
    }
    public void setRimToZero()
    {
        amount = 0;
        MK.Toon.MKToonMaterialHelper.SetRimIntensity(renderer.material, amount);
    }
}
