using UnityEngine;

public class WorkerAnimation : MonoBehaviour
{
    public bool animationRollAttack, getUpAnimation;
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (animationRollAttack)
        {
            animationRollAttack = false;
            animationAttack();
        }
        if (getUpAnimation)
        {
            anim.ResetTrigger("Attack");
            getUpAnimation = false;
            animationGetUp();
        }
    }

    private void animationAttack()
    {
        anim.SetTrigger("Attack");
    }
    private void animationGetUp()
    {
        anim.SetTrigger("GetUp");
    }
}
