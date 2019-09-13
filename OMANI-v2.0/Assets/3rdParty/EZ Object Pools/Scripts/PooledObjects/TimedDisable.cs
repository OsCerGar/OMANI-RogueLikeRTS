using EZObjectPools;
using UnityEngine;

[AddComponentMenu("EZ Object Pools/Pooled Objects/Timed Disable")]
public class TimedDisable : PooledObject
{
    float timer = 0;
    public float DisableTime;
    [SerializeField] SpriteRenderer steps;
    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        Color temp = steps.color;
        temp.a -= Time.deltaTime /10f;
        steps.color = temp;

        if (timer > DisableTime)
        {
            temp.a = 0.3f;
            steps.color = temp;
            transform.parent = ParentPool.transform;
            gameObject.SetActive(false);
        }
    }
}
