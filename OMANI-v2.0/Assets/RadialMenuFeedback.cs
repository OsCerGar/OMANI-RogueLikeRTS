using UnityEngine;

public class RadialMenuFeedback : MonoBehaviour
{
    [SerializeField]
    GameObject feedbackOn, feedbackOff;

    public void FeedbackOn() { feedbackOn.SetActive(true); feedbackOff.SetActive(false); }
    public void FeedbackOff() { feedbackOn.SetActive(false); feedbackOff.SetActive(true); }
}
