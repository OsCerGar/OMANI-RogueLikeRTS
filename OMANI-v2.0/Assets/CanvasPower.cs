using UnityEngine;
using UnityEngine.UI;
public class CanvasPower : MonoBehaviour
{
    [SerializeField]
    Text currentLife, quarter;

    // Update is called once per frame
    void Update()
    {
        if (Powers.powers != null)
        {
            currentLife.text = ((int)Powers.powers.armor).ToString();
        }
    }
}
