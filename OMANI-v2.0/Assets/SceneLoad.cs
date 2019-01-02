using UnityEngine;

public class SceneLoad : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player")) { Initiate.Fade(sceneToLoad, Color.white, 0.5f); }
    }
}
