using UnityEngine;

public class QueenLegs : MonoBehaviour
{
    AudioSource pisada_de_arena, movimiento_de_pierna;
    StepPool stepPool;

    bool ReadyLeftArmPos, ReadyRightArmPos, ReadyLeftLegPos, ReadyRightLegPos;
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
    }

    public void Collision(Collider collision, AudioSource _step, Transform _transform)
    {
        //if terrain tag == sand
        _step.clip = pisada_de_arena.clip;

        float volume = Random.Range(0.05f, 0.15f);
        if (volume > 0.13f && !movimiento_de_pierna.isPlaying)
        {
            movimiento_de_pierna.volume = volume;
            movimiento_de_pierna.Play();
        }
        _step.volume = volume;
        _step.Play();

        stepPool.StepSpawn(_transform);

    }
}
