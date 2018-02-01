using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army : MonoBehaviour {

    [SerializeField]
    List<GameObject> Swordsman = new List<GameObject>();

    [SerializeField]
    List<GameObject> Archer = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reclute(NPC Barroboy) {

        if (Barroboy.GetBoyType() == "Swordsman" && Barroboy.GetState() != "Follow") {
                Swordsman.Add(Barroboy.gameObject);
                Barroboy.GetComponent<NPC>().SetState("Follow");
        }
    }

}
