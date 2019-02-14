using UnityEngine;

public class SurkaTutorialAnimations : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    public void Yell()
    {
        anim.SetTrigger("Yell");
    }
}
