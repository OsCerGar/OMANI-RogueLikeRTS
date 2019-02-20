using UnityEngine;
public class SceneLoad : MonoBehaviour
{

    [SerializeField] string sceneToLoad;

    [SerializeField] Animator TeleporterAnimation;
    bool inside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inside = true;
            TeleporterAnimation.SetTrigger("Activated");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inside = false;
        }
    }
    public void LoadNextScene()
    {
        if (inside)
        {
            Initiate.Fade(sceneToLoad, Color.white, 0.5f);
        }
    }
}
