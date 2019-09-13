using UnityEngine;

public class QueenLegs : MonoBehaviour
{
    public static QueenLegs queenLegs;


    //Each HAND distance to the ground
    //Sounds and collisions

    // Start is called before the first frame update
    void Awake()
    {
        if (queenLegs == null)
        {
            queenLegs = this;
        }

    }

    public void Collision(Collider collision, Transform _transform, bool frontLeg)
    {
        if (frontLeg)
        {
            StepPool.stepPool.StepSpawn(_transform);
        }
        else
        {
            StepPool.stepPool.StepSpawnRear(_transform);
        }

    }
}

