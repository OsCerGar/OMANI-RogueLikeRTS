using UnityEngine;
public class SceneLoadBasic : MonoBehaviour
{

    [SerializeField] string sceneToLoad;
    [SerializeField] string hexColor;
    Color myColor;
    private void Update()
    {
        myColor = new Color();
        ColorUtility.TryParseHtmlString(hexColor, out myColor);

        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadNextScene();
        }
    }
    public void LoadNextScene()
    {
        Initiate.Fade(sceneToLoad, myColor, 0.5f);

    }

    public void LoadNextScene(string _sceneToLoad)
    {
        Initiate.Fade(_sceneToLoad, myColor, 0.5f);

    }
}
