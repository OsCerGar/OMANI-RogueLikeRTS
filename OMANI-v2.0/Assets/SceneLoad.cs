using UnityEngine;
public class SceneLoad : MonoBehaviour
{

    [SerializeField] string sceneToLoad;

    [SerializeField] Animator TeleporterAnimation;
    [SerializeField] ParticleSystem TPEffect;
    [SerializeField] AudioSource TeleportSFX;

    [SerializeField] GameObject DisablePlayer;
    [SerializeField] string hexColor;
    Color myColor;

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
        myColor = new Color();
        ColorUtility.TryParseHtmlString(hexColor, out myColor);

        Initiate.Fade(sceneToLoad, myColor, 0.5f);
    }

    public void LoadNextScene(string _sceneToLoad)
    {
        myColor = new Color();
        ColorUtility.TryParseHtmlString(hexColor, out myColor);

        Initiate.Fade(_sceneToLoad, myColor, 0.5f);
    }
}
