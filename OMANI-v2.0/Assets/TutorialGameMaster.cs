using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialGameMaster : MonoBehaviour
{

    [SerializeField] int pointReached;


    public int PointReached { get => pointReached; set => pointReached = value; }
    public static TutorialGameMaster tutorialGameMaster;

    // Start is called before the first frame update
    void Awake()
    {
        if (tutorialGameMaster != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            tutorialGameMaster = this;
        }
        DontDestroyOnLoad(gameObject);
        
    }
}
