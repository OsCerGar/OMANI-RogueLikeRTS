using UnityEngine;

public class UI_PointerDirection : MonoBehaviour
{
    Vector3 originalScale;
    SpriteRenderer mouse;
    Sprite normalMouse;
    [SerializeField]
    ParticleSystem dotsAnimation;
    bool timer;
    float timerAnimation;
    LayerMask mask;

    Color originalEmission;
    LookDirectionsAndOrder lookDAO;

    [SerializeField]LineRenderer line;
    // Use this for initialization
    void Start()
    {
        mouse = transform.GetComponent<SpriteRenderer>();
        lookDAO = FindObjectOfType<LookDirectionsAndOrder>();
        normalMouse = mouse.sprite;
        originalScale = transform.localScale;
        originalEmission = mouse.material.GetColor("_EmissionColor");
        mouse.material.renderQueue = 4000;
        //line = transform.GetComponentInChildren<LineRenderer>();
        mask = LayerMask.GetMask("Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            timerAnimation += Time.deltaTime;
        }

        if (timerAnimation > 1f)
        {
            timerAnimation = 0;
            //Turns dots again.
            timer = false;
        }
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(mouse.transform.position, 3f, mask);
        int i = 0;
        bool mouseNotUsable = false;

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Inactive"))
            {
                mouseNotUsable = true;
                mouse.material.color = Color.red;
                mouse.material.SetColor("_EmissionColor", Color.red);
            }
            i++;
        }

        if (!mouseNotUsable)
        {
            mouse.material.color = Color.green;
            mouse.material.SetColor("_EmissionColor", originalEmission);
        }

        if (line.gameObject.activeSelf)
        {
            //line points get updated
            line.SetPosition(0, lookDAO.transform.position);
            line.SetPosition(1, lookDAO.pointerDirection.transform.position);
        }
    }

    public void ChangePointer(Sprite _newPointer, Vector3 _scale, Color _color)
    {
        mouse.sprite = _newPointer;
        mouse.color = _color;
        transform.localScale = _scale;
    }

    //flag throwing line on
    public void FlagThrowing()
    {
        line.gameObject.SetActive(true);
    }

    public void pointerDefault()
    {
        mouse.sprite = normalMouse;
        transform.localScale = originalScale;

        //flag throwing line off
        line.gameObject.SetActive(false);

    }
}
