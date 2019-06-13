using UnityEngine;

public class BigBallsBall : NPC
{
    [SerializeField]
    Rigidbody rb;

    public override void TakeDamage(int damage, Color damageType, Transform _perpetrator)
    {
        //numberPool.NumberSpawn(numbersTransform, damage, Color.red, numbersTransform.gameObject, false);
        rb.AddForce((transform.position - _perpetrator.transform.position) * 3, ForceMode.Impulse);
    }
}
