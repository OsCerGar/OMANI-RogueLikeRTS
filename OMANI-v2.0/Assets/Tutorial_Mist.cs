using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Mist : MonoBehaviour {

    Transform Player;

	// Use this for initialization
	void Start () {
        Player = FindObjectOfType<Player>().transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
