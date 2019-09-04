using UnityEngine;

using UnityEngine.SceneManagement;
public class LoadSceneOnEnable : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    private void OnEnable()
    {
        if (sceneName == "this")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
