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
    private float timer, oldIntensity;

    float minimumDistance = 5.0f;
    float maximumDistance = 100.0f;

    float minimumDistanceScaleFog;
    float minimumDistanceScaleFogStart;
    float maximumDistanceScaleFog = 60.39f;
    float maximumDistanceScaleFogStart = 54.6f;
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
        if (sqrLen < closeDistance * closeDistance)
        {
            Attack();
        }
        else if (sqrLen - (closeDistance * closeDistance) < 200f)
        {
            Travelin();
        }

    }

    void Travelin()
    {
        float norm = (sqrLen - minimumDistance) / (maximumDistance - minimumDistance);
        norm = Mathf.Clamp01(norm);
        Debug.Log("norm " + norm);
        Debug.Log("fogstartdistnace " + Mathf.Lerp(maximumDistanceScaleFogStart, minimumDistanceScaleFogStart, norm));

        RenderSettings.fogStartDistance = Mathf.Lerp(maximumDistanceScaleFogStart, minimumDistanceScaleFogStart, norm);
        RenderSettings.fogEndDistance = Mathf.Lerp(maximumDistanceScaleFog, minimumDistanceScaleFog, norm);

    }

    private void Attack()
    {
        Horn.Play();
    }
}
