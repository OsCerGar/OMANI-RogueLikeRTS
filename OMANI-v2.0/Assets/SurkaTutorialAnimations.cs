using UnityEngine;

public class SurkaTutorialAnimations : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    public void Z()
    {
        anim.SetFloat("Z", 1f);
    }

    public void Yell()
    {
        anim.SetTrigger("Yell");
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }

}
