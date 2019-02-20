using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Light directional;

    float playerDistance;
    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < 30f)
        {
            directional.intensity = Mathf.Lerp(directional.intensity, 1f, 0.5f*Time.deltaTime);
        }
        else
        {
            directional.intensity = Mathf.Lerp(directional.intensity, 0.5f, 0.5f * Time.deltaTime);
        }
    }
}
