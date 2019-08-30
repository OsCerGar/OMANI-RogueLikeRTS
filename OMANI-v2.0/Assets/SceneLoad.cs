using UnityEngine;
public class SceneLoad : MonoBehaviour
{

    [SerializeField] string sceneToLoad;

    [SerializeField] Animator TeleporterAnimation;
    [SerializeField] ParticleSystem TPEffect;
    [SerializeField] AudioSource TeleportSFX;

    [SerializeField] GameObject DisablePlayer;

    [SerializeField] GameObject destroy;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadNextScene();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            DisablePlayer.SetActive(true);
            TeleporterAnimation.SetTrigger("Activated");
            TPEffect.transform.gameObject.SetActive(true);
            TeleportSFX.Play();
        }
    }

    public void LoadNextScene()
    {
        Initiate.Fade(sceneToLoad, Color.white, 0.5f);
        Destroy(destroy);
    }

    public void LoadNextScene(string _sceneToLoad)
    {
        Initiate.Fade(_sceneToLoad, Color.white, 0.5f);
        Destroy(destroy);
    }
}
