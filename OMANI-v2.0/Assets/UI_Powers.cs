using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Powers : MonoBehaviour
{

    public List<Image> dyingEffect = new List<Image>();
    bool dying;
    Powers powers;
    float startingPoint;

    // Use this for initialization
    void Start()
    {
        powers = transform.root.GetComponentInChildren<Powers>();
        startingPoint = powers.maxpowerPool / 2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (powers.powerPool < startingPoint)
        {
            dying = false;
            foreach (Image img in dyingEffect)
            {
                var tempColor = img.color;
                tempColor.a = 1 - (powers.powerPool / startingPoint);
                img.color = tempColor;
            }
        }
        else
        {
            if (!dying)
            {
                dying = true;
                foreach (Image img in dyingEffect)
                {
                    var tempColor = img.color;
                    tempColor.a = 0f;
                    img.color = tempColor;
                }
            }
        }
    }
}
