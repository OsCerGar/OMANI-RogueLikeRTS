using UnityEngine;

public class RimEffect : MonoBehaviour
{
    Renderer renderer;
    float amount;
    bool pimpam;
    UI_PointerDirection puntero;
    Transform centerPosition;
    // Start is called before the first frame update
    void Start()
    {
        puntero = FindObjectOfType<UI_PointerDirection>();
        renderer = GetComponent<Renderer>();
        centerPosition = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(puntero.transform.position, centerPosition.position)<10)
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
        else
        {
            amount = 0;
            MK.Toon.MKToonMaterialHelper.SetRimIntensity(renderer.material, amount);

        }
    }
    public void setRimToZero()
    {
        amount = 0;
        MK.Toon.MKToonMaterialHelper.SetRimIntensity(renderer.material, amount);
    }
}
