using UnityEngine;

public class Tutorial_Start2 : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetTrigger("Activated");
        //anim.Play("Activated", 0, 0.5f);
    }
}
