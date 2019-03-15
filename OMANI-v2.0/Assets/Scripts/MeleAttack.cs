using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleAttack : MonoBehaviour {
    [SerializeField] bool Knockback;
    [SerializeField] float ActiveHitboxTime = 0.1f;
    [SerializeField] int Damage = 0, damageOffset = 1, criticalChance = 5;
    [SerializeField] LayerMask LayerMasktoAttack;
    [HideInInspector]public  bool PowerUp;
    [SerializeField] ParticleSystem Effect;
    [SerializeField] NPC thisNpcScript;


    [SerializeField] SoundsManager SoundManager;

    string tagToAttack, secondTagToAttack;
    bool missed;
    ParticleSystem PowerUpEffect, PowerUpHitEffect;
    
    
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        
        if (IsInLayerMask(other.gameObject, LayerMasktoAttack))
        {
            var EnemyNPC = other.GetComponent<NPC>();
            if (Damage == 0)
            {
                Damage = thisNpcScript.Damage;
            }

            //Make his take damage;
            if (Knockback)
            {
                EnemyNPC.TakeDamage(Damage, true, 5,transform.parent.transform);
                
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
            //If he's dead, then forget about him
            missed = false;

            if (SoundManager != null)
            {
                SoundManager.AttackHit();
            }
           
        }
    }
    private void OnEnable()
    {
        missed = true; 
        if (Effect != null)
        {
            Effect.Play();
        }
        
        
        StartCoroutine(WaitandDisable());
    }
    
    IEnumerator WaitandDisable()
    {
        
        yield return new WaitForSeconds(ActiveHitboxTime);
        if (missed) {
            if (SoundManager != null)
            {
                SoundManager.AttackMiss();
            }
        }
        transform.gameObject.SetActive(false);
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        // Convert the object's layer to a bitfield for comparison
        int objLayerMask = (1 << obj.layer);
        if ((layerMask.value & objLayerMask) > 0)  // Extra round brackets required!
            return true;
        else
            return false;
    }
}
