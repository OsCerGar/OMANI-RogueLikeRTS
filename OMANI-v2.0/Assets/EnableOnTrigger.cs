using UnityEngine;

public class EnableOnTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject thingToEnable;
    bool enabledd;

    private void OnTriggerEnter(Collider other)
    {
        if (!enabledd)
        {
            if (other.CompareTag("Player"))
            {
                thingToEnable.SetActive(true);
                enabledd = true;
            }
        }
    }

}
