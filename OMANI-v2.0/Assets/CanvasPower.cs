using UnityEngine;
using UnityEngine.UI;
public class CanvasPower : MonoBehaviour
{

    [SerializeField]
    Powers power;

    [SerializeField]
    Text currentLife, quarter;

    // Update is called once per frame
    void Update()
    {
        currentLife.text = ((int)power.powerPool).ToString();
    }
}
