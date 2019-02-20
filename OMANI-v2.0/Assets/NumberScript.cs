using UnityEngine;
using UnityEngine.UI;

public class NumberScript : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Text number;
    Animator anim;
    float damageDealt;

    private GameObject numberOwner;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
        number = GetComponentInChildren<Text>();
        anim = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        transform.LookAt(mainCamera.transform);
    }

    private void OnDisable()
    {
        number.text = "0";
        number.fontSize = 150;
        damageDealt = 0;
    }

    public void numberUpdate(float damage_value, Color _color)
    {
        damageDealt += damage_value;
        number.text = damageDealt.ToString();
        number.color = _color;
        anim.SetBool("Updated", true);

        if (damageDealt > 50) { number.fontSize = 200; }
        if (damageDealt > 100) { number.fontSize = 250; }
        if (damageDealt > 150) { number.fontSize = 300; }
    }

    public void SetNumberOwner(GameObject _owner) { numberOwner = _owner; }
    public GameObject GetNumberOwner() { return numberOwner; }
}
