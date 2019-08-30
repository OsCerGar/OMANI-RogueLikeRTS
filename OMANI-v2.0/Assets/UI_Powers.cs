using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Powers : MonoBehaviour
{

    public List<Image> dyingEffect = new List<Image>();
    bool dying;
    [SerializeField]
    Powers powers;
    float startingPoint;
    [SerializeField]
    AudioMixer musicMixer;

    //DASH
    public Animator DashAnim;
    public Image dashImage;

    //POINTS
    public Text pointsText;

    // Use this for initialization
    void Start()
    {
        startingPoint = powers.maxArmor / 4f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GamemasterController.GameMaster.Money > 0)
        {
            pointsText.enabled = true;
            pointsText.text = GamemasterController.GameMaster.Money.ToString();
        }

        if (CharacterMovement.movement.dashCooldown > 2.9f)
        {
            DashAnim.SetBool("Dash", false);
        }
        else
        {
            DashAnim.SetBool("Dash", true);
        }

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

        if (!dying)
        {
            dyingEffect[0].transform.Rotate(0, 0, 0.25f, Space.Self);
            dyingEffect[1].transform.Rotate(0, 0, -0.15f, Space.Self);
            dyingEffect[2].transform.Rotate(0, 0, 0.15f, Space.Self);

        }
    }
}
