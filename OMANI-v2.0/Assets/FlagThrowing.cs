using System.Collections.Generic;
using UnityEngine;

public class FlagThrowing : MonoBehaviour
{

    LookDirectionsAndOrder lookDAO;
    Army army;
    PlayerInputInterface player;

    [SerializeField]
    List<Flag> flags = new List<Flag>();
    Robot robotToThrow = null;

    // Start is called before the first frame update
    void Start()
    {
        lookDAO = FindObjectOfType<LookDirectionsAndOrder>();
        army = FindObjectOfType<Army>();
        player = FindObjectOfType<PlayerInputInterface>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.inputs.GetButtonDown("Throw") && army.currentFighter != null)
        {

            robotToThrow = army.currentFighter;
            robotToThrow.Fired();
            robotToThrow.Dematerialize();
        }
        if (player.inputs.GetButtonUp("Throw") && robotToThrow != null)
        {
            Flag thrownFlag = flags[flags.Count - 1];
            thrownFlag.transform.SetParent(null);
            thrownFlag.transform.position = lookDAO.miradaposition;
            thrownFlag.Thrown(robotToThrow);
            flags.Remove(thrownFlag);
            robotToThrow = null;
        }
    }
}
