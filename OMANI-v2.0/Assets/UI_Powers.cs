using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Powers : MonoBehaviour
{

    public List<Image> dyingEffect = new List<Image>();
    bool dying;
    Powers powers;
    float startingPoint;
    [SerializeField]
    AudioMixer musicMixer;

    // Use this for initialization
    void Start()
    {
        powers = transform.root.GetComponentInChildren<Powers>();
        startingPoint = powers.maxArmor / 2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (powers.armor < startingPoint)
        {
            dying = false;
            foreach (Image img in dyingEffect)
            {
                var tempColor = img.color;
                tempColor.a = 1 - (powers.armor / startingPoint);
                if (powers.armor < startingPoint / 2f)
                {
                    musicMixer.SetFloat("Lowpass", 5000 * (powers.armor / startingPoint));
                }
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
