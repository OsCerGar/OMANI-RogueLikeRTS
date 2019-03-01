using UnityEngine;

public class UI_PointerDirection : MonoBehaviour
{

    SpriteRenderer dots, mouse;
    [SerializeField]
    ParticleSystem dotsAnimation;
    bool timer;
    float timerAnimation;
    LayerMask mask;

    Color originalEmission;
    // Use this for initialization
    void Start()
    {
        dots = transform.Find("Dots").GetComponent<SpriteRenderer>();
        mouse = transform.GetComponent<SpriteRenderer>();
        dots.material.renderQueue = 4000;
        originalEmission = mouse.material.GetColor("_EmissionColor");
        mouse.material.renderQueue = 4000;
        mask = LayerMask.GetMask("Terrain");
    }

    public void Click()
    {
        if (dots != null)
        {
            dots.enabled = false;

            Instantiate(dotsAnimation, dots.transform.position, dots.transform.rotation);

            timer = true;
        }
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
            dots.enabled = true;
            timer = false;
        }
    }

    private void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(dots.transform.position, 3f, mask);
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

    }
}
