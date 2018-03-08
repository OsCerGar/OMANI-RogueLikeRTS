using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public int numberOfCamps;
    public GameObject[] ResourcePrefab;
    public GameObject[] POISavageCamps;
    public Transform[] ResPositions;
    public List<GameObject> Res = new List<GameObject>();
    List<int> usedNumbers = new List<int>();
    // Use this for initialization
    void Start () {
        usedNumbers.Add(100000);
        FillResources();
        ActivateSavageCamp();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void FillResources()
    {
        for (int i = 0; i < ResPositions.Length/2; i++)
        {
            var posNumber = Random.Range(0,ResPositions.Length );
            if (!usedNumbers.Contains(posNumber) || usedNumbers == null)
            {
                usedNumbers.Add(posNumber);
                
                var ress = Instantiate(ResourcePrefab[Random.Range(0, ResourcePrefab.Length)],ResPositions[posNumber].position, ResPositions[posNumber].rotation);
                Res.Add(ress);
            }
            else
            {
                i--;
            }
        }
    }
    void ActivateSavageCamp()
    {
        bool completed = false;
        while (!completed)
        {
            var CampSelection = Random.Range(0, POISavageCamps.Length );
            Debug.Log(CampSelection);
            if (!POISavageCamps[CampSelection].activeSelf)
            {
                POISavageCamps[CampSelection].SetActive(true);
                POISavageCamps[CampSelection].GetComponent<SavageCamp>().createSavageShack();
                completed = true;
            }
        }
    }
}
