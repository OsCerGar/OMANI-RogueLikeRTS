using UnityEngine;
public class SceneLoadBasic : MonoBehaviour
{

    [SerializeField] string sceneToLoad;

    public void LoadNextScene()
    {
        Initiate.Fade(sceneToLoad, Color.white, 0.5f);

    }

    public void LoadNextScene(string _sceneToLoad)
    {
        Initiate.Fade(_sceneToLoad, Color.white, 0.5f);

    }
}
