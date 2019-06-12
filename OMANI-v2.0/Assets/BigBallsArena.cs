using UnityEngine;
using UnityEngine.UI;
public class BigBallsArena : MonoBehaviour
{
    float timer;

    [SerializeField]
    float maxTimer;
    string niceTime;

    [SerializeField]
    Text timerText;

    int points;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = niceTime + "   " + points;
    }

    public void PointDone()
    {
        points++;
    }
}
