using UnityEngine;

public class DisableOnRobotDisable : MonoBehaviour
{

    [SerializeField]
    GameObject robotThatDisables;

    // Update is called once per frame
    void Update()
    {
        if (!robotThatDisables.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
        }

    }
}
