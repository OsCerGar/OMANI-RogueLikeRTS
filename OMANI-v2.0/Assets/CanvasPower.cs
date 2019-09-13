using UnityEngine;
using UnityEngine.UI;
public class CanvasPower : MonoBehaviour
{
    [SerializeField]
    Text currentLife, quarter;
    [SerializeField] Image image, image2;
    // Update is called once per frame
    void Update()
    {
        if (Powers.powers != null)
        {
            currentLife.text = ((int)Powers.powers.armor).ToString();
            image.fillAmount = Powers.powers.armor / Powers.powers.maxArmor;
            image2.fillAmount = Powers.powers.armor / Powers.powers.maxArmor + 0.05f;

        }
    }
}
