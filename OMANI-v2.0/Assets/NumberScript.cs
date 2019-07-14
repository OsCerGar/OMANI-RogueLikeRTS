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
    NumberPool pool;

    // Start is called before the first frame update
    void Awake()
    {
        number = GetComponentInChildren<Text>();
        anim = GetComponentInParent<Animator>();
        pool = FindObjectOfType<NumberPool>();
    }

    private void OnEnable()
    {
        mainCamera = Camera.main;
        transform.LookAt(mainCamera.transform);
    }

    private void OnDisable()
    {
        number.text = "0";
        number.fontSize = 125;
        damageDealt = 0;
        pool.RemoveText(this);
    }

    public void numberUpdate(float damage_value, Color _color, bool _restoring)
    {
        damageDealt += damage_value;
        var finalDamage = Mathf.Floor(damageDealt);

        if (_restoring)
        {
            number.text = "+" + finalDamage.ToString();
        }
        else
        {
            number.text = finalDamage.ToString();
        }
        number.color = _color;
        anim.SetBool("Updated", true);

        if (damageDealt > 25) { number.fontSize = 150; }
        else if (damageDealt > 50) { number.fontSize = 200; }
        else if (damageDealt > 100) { number.fontSize = 225; }
        else if (damageDealt > 150) { number.fontSize = 250; }
        else if (damageDealt > 200) { number.fontSize = 300; }
    }

    public void SetNumberOwner(GameObject _owner) { numberOwner = _owner; }
    public GameObject GetNumberOwner() { return numberOwner; }
}
