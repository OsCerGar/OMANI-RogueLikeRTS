using UnityEngine;
using UnityEngine.UI;
public class BigBallsArena : MonoBehaviour
{
    #region conditions & ui
    float timer;

    [SerializeField]
    float maxTimer;
    string niceTime;

    [SerializeField]
    Text timerText;
    int points;
    #endregion

    #region Ending&UI
    [SerializeField]
    Canvas ArenaEndedCanvas;

    [SerializeField]
    Text end_time, end_points, end_enemies, end_earnings, end_results;
    #endregion

    bool matchover;
    // Start is called before the first frame update
    void Start()
    {
        timer = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!matchover)
        {
            timer -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer - minutes * 60);
            niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = niceTime + "   " + points;

            if (timer < 0)
            {
                MatchOver();
            }
        }

    }

    public void PointDone()
    {
        points++;

        if (points > 2) { MatchOver(); }
    }

    public void MatchOver()
    {
        matchover = true;

        ArenaEndedCanvas.enabled = true;
        end_time.text = end_time.text + timerText.text;
        end_points.text = end_points.text + points;
        end_earnings.text = end_earnings.text + (points * timer);

        if (points > 0)
        {
            end_results.text = "VICTORY";
        }
        else
        {
            end_results.text = "DEFEAT";
        }
    }
}
