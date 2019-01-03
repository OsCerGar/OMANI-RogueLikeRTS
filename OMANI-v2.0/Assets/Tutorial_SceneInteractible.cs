using MK.Toon;
using UnityEngine;
public class Tutorial_SceneInteractible : MonoBehaviour
{
    public Interactible_RepeaterTutorial repeater_Tutorial;
    public Material MKToonMaterialDoor;
    float dissolveAmount = 0;
    // Update is called once per frame
    void Update()
    {
        if (repeater_Tutorial.energy > 0)
        {
            dissolveAmount = Mathf.Lerp(dissolveAmount, 1, Time.unscaledTime * 0.0005f);

            MKToonMaterialHelper.SetDissolveAmount(MKToonMaterialDoor, dissolveAmount);
        }
    }

    private void OnDisable()
    {
        MKToonMaterialHelper.SetDissolveAmount(MKToonMaterialDoor, 0);

    }
}
