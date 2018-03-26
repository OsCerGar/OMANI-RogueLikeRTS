using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public int numberOfCamps;
    public GameObject[] ResourcePrefab;

    [SerializeField]
    GameObject CreepPrefab;

    [SerializeField]
    GameObject WorkerPrefab;

    [SerializeField]
    GameObject[] ArtifactPrefabs;

    public GameObject[] POISavageCamps;
    public Transform[] ResPositions;
    public Transform[] WorkerPositions;
    public Transform[] ArtifactPositions;
    public List<GameObject> Res = new List<GameObject>();
    List<int> usedNumbers = new List<int>();
    // Use this for initialization
    void Start () {
        usedNumbers.Add(100000);
        FillResources();
        ActivateSavageCamp();
        //I clear used numbers to recicle in SpawnCreeps()
        usedNumbers.Clear();
        SpawnCreeps();
        SpawnWorkers();
        SpawnArtifacts();
    }

    private void SpawnArtifacts()
    {
        for (int i = 0; i < ArtifactPositions.Length ; i++)
        {
            
            Instantiate(ArtifactPrefabs[UnityEngine.Random.Range(0, ArtifactPrefabs.Length)], ArtifactPositions[i].position, ArtifactPositions[i].rotation);
               
        }
    }

    private void SpawnWorkers()
    {
        for (int i = 0; i < WorkerPositions.Length / 2; i++)
        {
            var posNumber = UnityEngine.Random.Range(0, WorkerPositions.Length);
            if (!usedNumbers.Contains(posNumber) || usedNumbers == null)
            {
                usedNumbers.Add(posNumber);
                for (int t = 0; t < 3; t++)
                {

                    Instantiate(WorkerPrefab, WorkerPositions[posNumber].position, WorkerPositions[posNumber].rotation);
                }

            }
            else
            {
                i--;
            }
        }
    }

    private void SpawnCreeps()
    {
        for (int i = 0; i < ResPositions.Length / 2; i++)
        {
            var posNumber = UnityEngine.Random.Range(0, ResPositions.Length);
            if (!usedNumbers.Contains(posNumber) || usedNumbers == null)
            {
                usedNumbers.Add(posNumber);
                for (int t = 0; t < 3; t++)
                {

                    Instantiate(CreepPrefab, ResPositions[posNumber].position, ResPositions[posNumber].rotation);
                }
                
            }
            else
            {
                i--;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
    void FillResources()
    {
        for (int i = 0; i < ResPositions.Length/2; i++)
        {
            var posNumber = UnityEngine.Random.Range(0,ResPositions.Length );
            if (!usedNumbers.Contains(posNumber) || usedNumbers == null)
            {
                usedNumbers.Add(posNumber);
                
                var ress = Instantiate(ResourcePrefab[UnityEngine.Random.Range(0, ResourcePrefab.Length)],ResPositions[posNumber].position, ResPositions[posNumber].rotation);
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
            var CampSelection = UnityEngine.Random.Range(0, POISavageCamps.Length );
            if (!POISavageCamps[CampSelection].activeSelf)
            {
                POISavageCamps[CampSelection].SetActive(true);
                POISavageCamps[CampSelection].GetComponent<SavageCamp>().createSavageShack();
                completed = true;
            }
        }
    }
}
