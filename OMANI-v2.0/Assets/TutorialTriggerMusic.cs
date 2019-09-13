using UnityEngine;

public class TutorialTriggerMusic : MonoBehaviour
{
    [SerializeField] bool inside;
    [SerializeField] string theme;
    [SerializeField] bool deactivate;
    [SerializeField] GameObject activate;
    [SerializeField] float time;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (inside)
            {
                inside = false;

                if (theme == "mainTheme")
                {
                    MusicManager.musicManager.MusicUp("mainTheme");
                }
                else if (theme == "CombatMusic")
                {
                    MusicManager.musicManager.MusicUp("combatMusic");
                }
                else if (theme == "CombatStop")
                {
                    MusicManager.musicManager.MusicUp("combatMusic");
                }
                else if (theme == "ambiente")
                {
                    MusicManager.musicManager.MusicUp("ambiente");
                }
                if (deactivate)
                {
                    gameObject.SetActive(false);
                }
                if (activate != null) { activate.SetActive(true); }
            }
            else
            {
                inside = true;
                if (theme == "mainTheme")
                {
                    MusicManager.musicManager.LowerMusic("mainTheme", time);
                }
                else if (theme == "CombatMusic")
                {
                    MusicManager.musicManager.LowerMusic("combatMusic", 1f);
                    gameObject.SetActive(false);
                }
                else if (theme == "CombatStop")
                {
                    MusicManager.musicManager.LowerMusic("combatStop", 1f);
                }
                else if (theme == "ambiente")
                {
                    MusicManager.musicManager.LowerMusic("ambiente", 1f);
                }
                if (deactivate)
                {
                    gameObject.SetActive(false);
                }
                if (activate != null) { activate.SetActive(true); }

            }
        }
    }
}
