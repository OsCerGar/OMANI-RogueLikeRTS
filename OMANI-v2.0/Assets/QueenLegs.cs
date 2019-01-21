using UnityEngine;

public class QueenLegs : MonoBehaviour
{
    AudioSource pisada_de_arena, movimiento_de_pierna;
    StepPool stepPool;

    bool stepPlaying;
    float timePlaying, timeToPlay;
    int layerMask = 1 << 8;
    RaycastHit hit;

    //Each HAND distance to the ground
    //Sounds and collisions

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioSource audio in GetComponents<AudioSource>())
        {
            if (audio.clip.name == "Pisada_de_arena")
            {
                pisada_de_arena = audio;
            }
            if (audio.clip.name == "Movimiento_de_pierna")
            {
                movimiento_de_pierna = audio;
            }
        }

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
    }

    public void Collision(Collider collision, AudioSource _step, Transform _transform)
    {
        //if terrain tag == sand
        _step.clip = pisada_de_arena.clip;

        float volume = Random.Range(0.01f, 0.05f);
        float pitch = Random.Range(0.85f, 1.15f);

        //STEP STUFF
        if (!stepPlaying)
        {
            stepPlaying = true;

            _step.pitch = pitch;
            _step.volume = volume;
            _step.Play();
        }

        //visual
        stepPool.StepSpawn(_transform);
    }

    public void CollisionOut(Collider collision, AudioSource _leg)
    {
        _leg.clip = movimiento_de_pierna.clip;

        //_leg.pitch = pitch;
        _leg.volume = movimiento_de_pierna.volume;
        //_leg.Play();

    }
}
