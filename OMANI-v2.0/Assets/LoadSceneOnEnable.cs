using UnityEngine;

using UnityEngine.SceneManagement;
public class LoadSceneOnEnable : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    private void OnEnable()
    {
        if (sceneName == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
