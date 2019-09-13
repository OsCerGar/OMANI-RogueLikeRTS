using UnityEngine;

public class QueenHandCollisionManager : MonoBehaviour
{
    [SerializeField] bool frontLeg;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sand"))
        {
            QueenLegs.queenLegs.Collision(other, transform, frontLeg);
        }
    }

}
