using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BU_Electricity_Animations : MonoBehaviour
{

    public Animator anim;
    public GameObject[] ElectricRays;
    public Material[] materials;
    private Color color1, color2, finalColor1, finalColor2;
    private float finalColor1Value, finalColor2Value;

    private void Start()
    {
        /*
        color1 = materials[0].GetColor("_EmissionColor");
        color2 = materials[1].GetColor("_EmissionColor");
        */
    }
    public void buildingAnimations(int energy)
    {/*
        switch (energy)
        {
            case 0:
                anim.SetInteger("Energy", 0);

                ElectricRays[0].SetActive(false);
                ElectricRays[1].SetActive(false);
                ElectricRays[2].SetActive(false);
                ElectricRays[3].SetActive(false);
                ElectricRays[4].SetActive(false);
                ElectricRays[5].SetActive(false);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 0, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 0, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);

                break;
            case 1:
                anim.SetInteger("Energy", 1);

                ElectricRays[0].SetActive(true);
                ElectricRays[1].SetActive(true);
                ElectricRays[2].SetActive(false);
                ElectricRays[3].SetActive(false);
                ElectricRays[4].SetActive(false);
                ElectricRays[5].SetActive(false);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 2, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 3, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);


                break;
            case 2:
                anim.SetInteger("Energy", 2);

                ElectricRays[0].SetActive(true);
                ElectricRays[1].SetActive(true);
                ElectricRays[2].SetActive(true);
                ElectricRays[3].SetActive(false);
                ElectricRays[4].SetActive(false);
                ElectricRays[5].SetActive(false);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 5, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 7, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);

                break;
            case 3:
                anim.SetInteger("Energy", 3);

                ElectricRays[0].SetActive(true);
                ElectricRays[1].SetActive(true);
                ElectricRays[2].SetActive(true);
                ElectricRays[3].SetActive(true);
                ElectricRays[4].SetActive(true);
                ElectricRays[5].SetActive(true);

                finalColor1Value = Mathf.Lerp(finalColor1Value, 10, 2 * Time.unscaledDeltaTime);
                finalColor2Value = Mathf.Lerp(finalColor2Value, 15, 2 * Time.unscaledDeltaTime);

                finalColor1 = color1 * Mathf.LinearToGammaSpace(finalColor1Value);
                finalColor2 = color2 * Mathf.LinearToGammaSpace(finalColor2Value);

                materials[0].SetColor("_EmissionColor", finalColor1);
                materials[1].SetColor("_EmissionColor", finalColor2);
                break;
        }
        */
    }
    public void OnDisable()
    {/*
        anim.SetInteger("Energy", 0);

        ElectricRays[0].SetActive(false);
        ElectricRays[1].SetActive(false);
        ElectricRays[2].SetActive(false);
        ElectricRays[3].SetActive(false);
        ElectricRays[4].SetActive(false);
        ElectricRays[5].SetActive(false);

        materials[0].SetColor("_EmissionColor", color1);
        materials[1].SetColor("_EmissionColor", color2);
        */
    }
}
