using UnityEngine;

public class QueenHandCollisionManager : MonoBehaviour
{
    QueenLegs _queenLegs;
    AudioSource step;

    private void Start()
    {
        _queenLegs = FindObjectOfType<QueenLegs>();
        step = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            _queenLegs.Collision(other, step, this.transform);

        }
    }
}
