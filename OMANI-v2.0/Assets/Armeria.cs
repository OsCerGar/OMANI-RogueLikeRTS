using System.Collections.Generic;
using UnityEngine;

public class Armeria : MonoBehaviour
{
    [SerializeField]
    List<GameObject> mainmenuStuff = new List<GameObject>();
    [SerializeField]
    List<GameObject> armeryUI = new List<GameObject>();


    public void AbrirArmeria()
    {
        foreach (GameObject thing in mainmenuStuff)
        {
            thing.SetActive(false);
        }
        foreach (GameObject thing in armeryUI)
        {
            thing.SetActive(true);
        }
    }
    public void CerrarArmeria()
    {
        foreach (GameObject thing in mainmenuStuff)
        {
            thing.SetActive(true);
        }
        foreach (GameObject thing in armeryUI)
        {
            thing.SetActive(false);
        }
    }
}
