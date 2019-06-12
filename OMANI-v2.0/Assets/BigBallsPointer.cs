using UnityEngine;

public class BigBallsPointer : MonoBehaviour
{
    Rigidbody rigid;
    public bool point;

    [SerializeField]
    BigBallsArena arena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovableObject"))
        {
            arena.PointDone();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovableObject"))
        {
            if (rigid == null)
            {
                rigid = other.GetComponent<Rigidbody>();
            }
            rigid.AddForce(transform.up * 15, ForceMode.Acceleration);
            point = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovableObject"))
        {
            rigid = null;
        }
    }
}
