using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    [SerializeField] float numberOfBigFeatures;
    public GameObject[] Hills;


    private  List<WorldFeature> BigFeatures;
    [SerializeField]
    GameObject BasePrefab;

    [SerializeField]
    GameObject CreepPrefab;



    public int numberOfCamps;
    public GameObject[] ResourcePrefab;

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

    //TerrainData
    Terrain terrain;
    private float tWidth,t;
    private float tLength;

    // Use this for initialization
    void Start () {
        //Getting terrian Data 
        /*
        BigFeatures = new List<WorldFeature>();
        terrain = FindObjectOfType<Terrain>();
        tWidth = terrain.terrainData.size.x;
        tLength = terrain.terrainData.size.z;
        
        SpawnBase();

        Debug.Log(BigFeatures.Count);

        SpawnHills();
        */
        

        usedNumbers.Add(100000);
        FillResources();
        ActivateSavageCamp();
        //I clear used numbers to recicle in SpawnCreeps()
        usedNumbers.Clear();
        SpawnCreeps();
        SpawnWorkers();
        SpawnArtifacts();
        
    }

    private void SpawnHills()
    {
        for (int i = 0; i < numberOfBigFeatures; i++)
        {
            var hillToSpawn = Hills[UnityEngine.Random.Range(0, Hills.Length)];
            bool SpotFound = false;
            while (!SpotFound)
            {
                //get a randomSpot in the terrain and 
                Vector3 PosToSpawn = GetPosToSpawn();
                if (checkDistances(PosToSpawn, hillToSpawn.GetComponent<WorldFeature>()))
                {
                    var newHill = Instantiate(hillToSpawn, PosToSpawn, Quaternion.Euler(0,UnityEngine.Random.Range(0, 180), 0));
                    BigFeatures.Add(newHill.GetComponent<WorldFeature>());
                    SpotFound = true;
                }
            }

        }
    }

    private bool checkDistances(Vector3 _PosToSpawn, WorldFeature ThingToSpawn)
    {
        //Checks that the distance beetwen the thing u want to spawn and the rest of existing features is not too low 
        for (int i = 0; i < BigFeatures.Count; i++)
        {
            if (Vector3.Distance(_PosToSpawn, BigFeatures[i].transform.position) < ThingToSpawn.Rad + BigFeatures[i].Rad)
            {
                return false;
            }
        }
       
        return true;
    }

    private Vector3 GetPosToSpawn()
    {
        return new Vector3(UnityEngine.Random.Range(-tWidth / 2, tWidth / 2), 0, UnityEngine.Random.Range(-tLength / 2, tLength / 2));
    }

    private void SpawnBase()
    {
        var mainBase  = (GameObject) Instantiate(BasePrefab, new Vector3(0, 0 , 0), BasePrefab.transform.rotation);
        BigFeatures.Add( (WorldFeature )mainBase.GetComponent<WorldFeature>());
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
                for (int t = 0; t < 1; t++)
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
