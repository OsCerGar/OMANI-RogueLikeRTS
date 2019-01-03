using UnityEngine;

public class intro : MonoBehaviour
{

    public GameObject camera, sand;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) { camera.SetActive(false); sand.SetActive(true); }
    }
}
