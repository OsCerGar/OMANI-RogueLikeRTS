using UnityEngine;

public class QueenHandCollisionManager : MonoBehaviour
{
    QueenLegs _queenLegs;
    AudioSource step, legMovement;

    private void Start()
    {
        _queenLegs = FindObjectOfType<QueenLegs>();

        step = GetComponents<AudioSource>()[0];
        legMovement = GetComponents<AudioSource>()[1];
    }


    private void OnTriggerEnter(Collider other)
    {
        _queenLegs.Collision(other, step, transform);

    }
    private void OnTriggerExit(Collider other)
    {
        _queenLegs.CollisionOut(other, legMovement);

    }
}
