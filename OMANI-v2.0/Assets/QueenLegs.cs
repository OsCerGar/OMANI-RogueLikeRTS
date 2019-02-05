using UnityEngine;

public class QueenLegs : MonoBehaviour
{
    AudioSource pisada_de_arena, pisada_de_cueva, pisada_de_metal, movimiento_de_pierna, sand;

    StepPool stepPool;

    bool stepPlaying;
    float timePlaying, timeToPlay, stepVolume, stepVolumeGradual, stepVolumeGradualReduction = 0.25f;
    int layerMask = 1 << 8;
    RaycastHit hit;

    CharacterMovement characterMovement;

    //Each HAND distance to the ground
    //Sounds and collisions

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioSource audio in GetComponents<AudioSource>())
        {
            if (audio.clip.name == "Robot_pisada_arena")
            {
                pisada_de_arena = audio;
            }
            if (audio.clip.name == "Robot_pisada_cueva")
            {
                pisada_de_cueva = audio;
            }
            if (audio.clip.name == "Robot_pisada_metal")
            {
                pisada_de_metal = audio;
            }
            if (audio.clip.name == "Movimiento_de_pierna")
            {
                movimiento_de_pierna = audio;
            }
            if (audio.clip.name == "Consequencia_pisada_de_arena")
            {
                sand = audio;
            }
        }

        characterMovement = FindObjectOfType<CharacterMovement>();
        stepPool = FindObjectOfType<StepPool>();
        timeToPlay = 0.25f;
    }

    private void Update()
    {
        if (stepPlaying)
        {
            timePlaying += Time.deltaTime;
        }
        if (timePlaying > timeToPlay)
        {
            stepPlaying = false;
            timePlaying = 0;
        }

        if (Input.GetKeyDown("left"))
        {
            timeToPlay -= 0.01f;
            Debug.Log("Tiempo entre pisadas = " + timeToPlay);
        }
        if (Input.GetKeyDown("right"))
        {
            timeToPlay += 0.01f;
            Debug.Log("Tiempo entre pisadas = " + timeToPlay);
        }

        if (Input.GetKeyDown("up"))
        {
            stepVolumeGradualReduction += 0.01f;
            Debug.Log("Valor volumen gradual = " + stepVolumeGradualReduction);
        }

        if (Input.GetKeyDown("down"))
        {
            stepVolumeGradualReduction -= 0.01f;
            Debug.Log("Valor volumen gradual = " + stepVolumeGradualReduction);
        }
    }

    public void Collision(Collider collision, AudioSource _step, Transform _transform)
    {
        //if terrain tag == sand
        StepParameters(collision, _step);

        float pitch = Random.Range(0.85f, 1.15f);

        //STEP STUFF
        stepVolume = Random.Range(0.01f, 0.02f);
        _step.volume = stepVolume;

        if (characterMovement.onMovementTime > 1.5f)
        {
            stepVolumeGradual += stepVolumeGradualReduction * Time.unscaledDeltaTime;
            stepVolume -= stepVolumeGradual;
            stepVolume = Mathf.Clamp(stepVolume, 0.001f, 0.02f);

            _step.volume = stepVolume;
        }

        if (!stepPlaying)
        {
            stepPlaying = true;

            _step.pitch = pitch;
            _step.Play();
        }


        //visual
        stepPool.StepSpawn(_transform);
    }

    public void CollisionOut(Collider collision, AudioSource _leg)
    {
        //_leg.clip = movimiento_de_pierna.clip;
        //_leg.volume = movimiento_de_pierna.volume;
        //_leg.Play();

    }

    void StoppedSound()
    {
        //Resets step volume
        stepVolumeGradual = 0;
        if (!sand.isPlaying && characterMovement.onMovementTime > 2f)
        {
            sand.Play();
        }
    }

    private void StepParameters(Collider _collision, AudioSource _step)
    {
        if (_collision.CompareTag("Sand"))
        {
            _step.clip = pisada_de_arena.clip;
        }
        else if (_collision.CompareTag("Cave"))
        {
            _step.clip = pisada_de_cueva.clip;
        }
        else if (_collision.CompareTag("Metal"))
        {
            _step.clip = pisada_de_metal.clip;
        }
    }

    void OnEnable()
    {
        CharacterMovement.OnStopping += StoppedSound;
    }
    void OnDisable()
    {
        CharacterMovement.OnStopping -= StoppedSound;
    }
}
  
