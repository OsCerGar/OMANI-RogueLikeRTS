using UnityEngine;

public class BallTriggerForce : MonoBehaviour
{
    [SerializeField] Animator anim;

    float progress;

    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovableObject"))
        {
            if (rigid == null)
            {
                rigid = other.GetComponent<Rigidbody>();
            }
            rigid.AddForce(transform.forward, ForceMode.Acceleration);
            progress += Time.deltaTime / 128;
            progress = Mathf.Clamp(progress, 0, 0.98f);
            anim.Play("ProgressAnimation", 0, progress);
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
