using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_WeaponsMaker_Animation : MonoBehaviour
{

    public Animator anim;
    public GameObject[] Stuff;
    public Material[] materials;
    private Color color1, color2, finalColor1, finalColor2;
    private float finalColor1Value, finalColor2Value;

    private void Start()
    {
     //   color1 = materials[0].GetColor("_EmissionColor");
       // color2 = materials[1].GetColor("_EmissionColor");
    }

    public void buildingAnimations(int energy)
    {/*
        switch (energy)
        {
            case 0:
                Stuff[0].SetActive(false);
                Stuff[1].SetActive(false);
                Stuff[2].SetActive(false);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 0, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 0, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);

                break;
            case 1:

                Stuff[0].SetActive(true);
                Stuff[1].SetActive(true);
                Stuff[2].SetActive(false);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 0, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 0, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);

                break;
            case 2:

                Stuff[0].SetActive(true);
                Stuff[1].SetActive(true);
                Stuff[2].SetActive(true);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 3, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 3, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);

                break;
            case 3:

                Stuff[0].SetActive(true);
                Stuff[1].SetActive(true);
                Stuff[2].SetActive(true);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 10, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 10, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);
                break;
        }

        */
    }
    public void OnDisable()
    {
        /*
        Stuff[0].SetActive(false);
        Stuff[1].SetActive(false);
        Stuff[2].SetActive(false);

        materials[0].SetColor("_EmissionColor", color1);
        materials[1].SetColor("_EmissionColor", color2);
        */
    }

}
