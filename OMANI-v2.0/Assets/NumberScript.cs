using UnityEngine;

public class NumberScript : MonoBehaviour
{
    [SerializeField]
    Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        transform.LookAt(mainCamera.transform);
    }
}
