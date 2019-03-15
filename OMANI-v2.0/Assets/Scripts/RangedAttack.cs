using UnityEngine;
using UnityEngine.AI;

public class RangedAttack : MonoBehaviour
{

    [SerializeField] bool Knockback;
    [SerializeField] int Damage = 1, damageOffset = 1, criticalChance = 10;
    [SerializeField] NPC thisNpcScript;
    [SerializeField] LayerMask LayerMasktoAttack;
    AudioSource AudiosSource;

    private void Start()
    {
        AudiosSource = GetComponent<AudioSource>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (AudiosSource != null)
        {
            AudiosSource.Play();
        }
        if (IsInLayerMask(other.gameObject, LayerMasktoAttack))
        {

            var EnemyNPC = other.GetComponent<NPC>();
            var EnemyNavMesh = other.GetComponent<NavMeshAgent>();
            if (Damage == 0)
            {
                Damage = thisNpcScript.Damage;
            }

            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(Damage, true, 5, transform.parent.transform);

            }
            else
            {
                int damageFinal;
                int offset = Random.Range(-damageOffset, damageOffset + 1);
                int criticalChanceTemporal = Random.Range(0, 100);

                if (criticalChanceTemporal < criticalChance)
                {
                    damageFinal = (Damage + offset) * 2;
                    EnemyNPC.TakeDamage(damageFinal, Color.yellow);
                }
                else
                {
                    damageFinal = Damage + offset;
                    EnemyNPC.TakeDamage(damageFinal, Color.white);
                }
            }


        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // Convert the object's layer to a bitfield for comparison
        int objLayerMask = (1 << obj.layer);
        if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
