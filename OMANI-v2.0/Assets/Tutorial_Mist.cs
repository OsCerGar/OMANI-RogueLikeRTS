using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Mist : MonoBehaviour
{
    [SerializeField]
    GameObject toSpawn;
    Transform player;
    [SerializeField]
    AudioSource Horn;
    bool attacked;

    public Transform other;
    public float closeDistance = 5.0f;
    private float timer, oldIntensity;

    float minimumDistance = 500.0f;
    float maximumDistance = 3000f;

    float minimumDistanceScaleFog;
    float minimumDistanceScaleFogStart;
    float maximumDistanceScaleFog = 35f;
    float maximumDistanceScaleFogStart = 15f;
    float sqrLen;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        minimumDistanceScaleFog = RenderSettings.fogEndDistance;
        minimumDistanceScaleFogStart = RenderSettings.fogStartDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = player.position - transform.position;
        sqrLen = offset.sqrMagnitude;
        if (sqrLen < closeDistance * closeDistance && !attacked)
        {
            Attack();
        }

        else if (sqrLen < 3000f)
        {
            Travelin();
        }

    }

    void Travelin()
    {
        float norm = (sqrLen - minimumDistance) / (maximumDistance - minimumDistance);
        norm = Mathf.Clamp01(norm);

        RenderSettings.fogStartDistance = Mathf.Lerp(maximumDistanceScaleFogStart, minimumDistanceScaleFogStart, norm);
        RenderSettings.fogEndDistance = Mathf.Lerp(maximumDistanceScaleFog, minimumDistanceScaleFog, norm);

    }

    private void Attack()
    {
        attacked = true;
        Horn.Play();
        toSpawn.SetActive(true);
    }
}
