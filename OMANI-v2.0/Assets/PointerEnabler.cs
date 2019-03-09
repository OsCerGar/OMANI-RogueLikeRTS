using UnityEngine;

public class PointerEnabler : MonoBehaviour
{
    GameObject pointer;

    [SerializeField]
    OMANINPUT controls;
    bool state;
    private void Awake()
    {
        pointer = transform.Find("PointerDirection").gameObject;
        controls.PLAYER.OrderLaser.performed += context => PointerState();
    }

    private void PointerState()
    {

        if (!state) { pointer.SetActive(false); state = true; }
        else { pointer.SetActive(true); state = false; }
    }
}
