using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Mist : MonoBehaviour
{

    Transform player;
    [SerializeField]
    AudioSource Horn;

    public Transform other;
    public float closeDistance = 5.0f;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = player.position - transform.position;
        float sqrLen = offset.sqrMagnitude;

        if (sqrLen < closeDistance * closeDistance)
        {
            Attack();
        }

    }

    private void Attack()
    {
        Horn.Play();
        Debug.Log("Attack");
    }
}
