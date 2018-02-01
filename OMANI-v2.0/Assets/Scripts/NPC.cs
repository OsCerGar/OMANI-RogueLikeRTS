using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    [SerializeField]
    string BoyType = "Swordsman";

    [SerializeField]
    string state = "Free";

    [SerializeField]
    int Life, Attack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetBoyType() {
        return BoyType;
    }
    public string GetState()
    {
        return state;
    }

    public void SetType(string state)
    {
        this.BoyType = BoyType;
    }

    public void SetState(string state)
    {
        this.state = state;
    }
}
