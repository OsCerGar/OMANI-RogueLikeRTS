using UnityEngine;

public class Power_Laser : MonoBehaviour
{

    public Transform laserShotPosition;
    public float speed = 1f;
    public ParticleSystem startWavePS;
    public ParticleSystem startParticles;
    public int startParticlesCount = 100;
    public GameObject laserShotPrefab;
    public LaserColision laserColision;
    private Vector3 mouseWorldPosition;
    private Animator anim;
    private float contador = 0;
    ConLaser CLaser;
    MetaAudioController AudioControl;
    LookDirectionsAndOrder lookdir;
    float widthToSend = 1, scaleToSend = 0.15f;
    public Transform Sphere;

    void Start()
    {
        anim = GetComponent<Animator>();
        CLaser = GetComponentInChildren<ConLaser>();
        AudioControl = GetComponentInChildren<MetaAudioController>();
        lookdir = FindObjectOfType<LookDirectionsAndOrder>();
        laserColision = FindObjectOfType<LaserColision>();
    }

    private void Update()
    {

        if (contador > 0)
        {
            contador -= Time.deltaTime;
        }
        else
        {
            anim.SetBool("Fire", false);
            laserColision.laserEnabled = false;
        }

    }
    private void LateUpdate()
    { 
        transform.LookAt(lookdir.miradaposition);
    }

    public void EmitOffensiveLaser()
    {
        AudioControl.ResetLaserProgress();
        StartEffects();
        contador = 0.1f;
        anim.SetBool("Fire", true);
        Instantiate(laserShotPrefab, laserShotPosition.position, transform.rotation);
    }

    public void StartEffects()
    {
        AudioControl.StartSound();
        startWavePS.Emit(1);
        startParticles.Emit(startParticlesCount);
    }

    public void EmitLaser()
    {
        AudioControl.ResetLaserProgress();
        CLaser.SetGlobalProgress();
        contador = 0.2f;
        anim.SetBool("Fire", true);
        laserColision.laserEnabled = true;
    }

    public void setWidth(float _width)
    {
        Debug.Log("hello");
        if (_width > widthToSend)
        {
            widthToSend += Time.unscaledDeltaTime * 3;
        }
        else if (_width < widthToSend)
        {
            widthToSend -= Time.unscaledDeltaTime * 10;
        }
        widthToSend = Mathf.Clamp(widthToSend, 0.5f, 5f);
        CLaser.WidthMultiplayer = widthToSend;
    }

    public void setSphereWidth(float _Scale)
    {
        if (_Scale > scaleToSend)
        {
            scaleToSend += Time.unscaledDeltaTime / 2;
        }
        else if (_Scale < scaleToSend)
        {
            scaleToSend -= Time.unscaledDeltaTime / 2;
        }
        scaleToSend = Mathf.Clamp(scaleToSend, 0.01f, 0.33f);
        Sphere.localScale = new Vector3(scaleToSend, scaleToSend, scaleToSend);

    }


}
