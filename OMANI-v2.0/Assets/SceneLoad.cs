using UnityEngine;
public class SceneLoad : MonoBehaviour
{

    [SerializeField] string sceneToLoad;

    [SerializeField] Animator TeleporterAnimation;
    [SerializeField] ParticleSystem TPEffect;
    [SerializeField] AudioSource TeleportSFX;
    bool inside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            inside = true;
            TeleporterAnimation.SetTrigger("Activated");
            TPEffect.transform.gameObject.SetActive(true);
            TeleportSFX.Play();
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
