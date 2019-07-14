using UnityEngine;
public class SceneLoadBasic : MonoBehaviour
{

    [SerializeField] string sceneToLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        Initiate.Fade(sceneToLoad, Color.white, 0.5f);

    }

    public void LoadNextScene(string _sceneToLoad)
    {
        Initiate.Fade(_sceneToLoad, Color.white, 0.5f);

    }
}
