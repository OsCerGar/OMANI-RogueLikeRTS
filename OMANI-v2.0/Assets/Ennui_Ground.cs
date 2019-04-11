using UnityEngine;

public class Ennui_Ground : Interactible
{

    [SerializeField]
    private int energy = 5;
    private float timer, totalTimer = 10;
    int build, mask, peopl;
    public Animator ennuiAnimator;
    Collider colliderEnnui;
    Powers power;
    bool completed;

    public void Awake()
    {
        base.Awake();
        ennuiAnimator = GetComponent<Animator>();
        colliderEnnui = GetComponent<Collider>();

    }
    public override void Start()
    {
        base.Start();
        power = FindObjectOfType<Powers>();
        build = 1 << LayerMask.NameToLayer("Building");
        peopl = 1 << LayerMask.NameToLayer("People");
        mask = build | peopl;
        //Laser Price
        linkPrice = 75;
        price = 50;
        finalLinkPrice = 100;
        currentLinkPrice = 0;

        t = 0.2f;
    }

    public override void Update()
    {
        if (Time.time - startTime > 2f && powerReduced > 0)
        {
            ReducePower(15);
        }

        if (timer < totalTimer)
        {
            timer += Time.deltaTime;
        }

        if (timer > totalTimer)
        {
            timer = 0;
            laserTarget.gameObject.SetActive(false);

            ennuiAnimator.SetBool("Die", true);
        }

        if (!ennuiAnimator.GetBool("Die") && !ennuiAnimator.GetBool("Explosion"))
        {
            //animation
            ennuiAnimator.Play("Iddle", 0, powerReduced / price);
        }
    }
    public void Action(Powers _powerPool)
    {
        if (!ennuiAnimator.GetBool("Die") && !ennuiAnimator.GetBool("Explosion"))
        {
            float returned = power.addPowerReturn(50);
            numberPool.NumberSpawn(numbersTransform, returned, Color.green, numbersTransform.GetChild(0).gameObject, true);
            laserTarget.gameObject.SetActive(false);

            ennuiAnimator.SetBool("Die", true);
            colliderEnnui.enabled = false;
        }
    }
    public override void ActionCompleted()
    {
        if (!completed)
        {
            base.ActionCompleted();
            completed = true;
            float returned = power.addPowerReturn(50);
            numberPool.NumberSpawn(numbersTransform, returned, Color.green, numbersTransform.GetChild(0).gameObject, true);

            laserTarget.gameObject.SetActive(false);
            ennuiAnimator.SetBool("Explosion", true);
        }
    }

    private void OnDisable()
    {
        if (ennuiAnimator != null)
        {
            ennuiAnimator.SetBool("Die", false);
            ennuiAnimator.SetBool("Explosion", false);
        }
        colliderEnnui.enabled = true;

        laserTarget.gameObject.SetActive(true);
        powerReduced = 0;
        currentLinkPrice = 0;
        t = 0.2f;
        completed = false;
    }

    public override void Action()
    {
        if (!ennuiAnimator.GetBool("Die") && !ennuiAnimator.GetBool("Explosion"))
        {
            base.Action();
        }
    }
}
