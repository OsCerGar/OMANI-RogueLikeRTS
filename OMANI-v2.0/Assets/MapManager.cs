using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {

    [SerializeField] float numberOfBigFeatures;
    public GameObject[] Hills;
    public GameObject[] Trees;


    private  List<WorldFeature> BigFeatures;
    [SerializeField]
    GameObject BasePrefab;
    

    [SerializeField]
    GameObject SpawnPosition;
    
    
    public GameObject[] ResourcePrefab;

  
    [SerializeField]
    GameObject[] ArtifactPrefabs;
    
    private ResPos[] ResPositions;
    public List<GameObject> Res = new List<GameObject>();
    List<int> usedNumbers = new List<int>();

    //TerrainData
    Terrain terrain;
    private float tWidth,t;
    private float tLength;

    //Navigation
    private NavMeshSurface[] surfaces;

    // Use this for initialization
    void Start () {

        //Getting terrian Data 
        
        BigFeatures = new List<WorldFeature>();
        terrain = FindObjectOfType<Terrain>();
        tWidth = terrain.terrainData.size.x;
        tLength = terrain.terrainData.size.z;


        SpawnBase();
        

        SpawnHills();

        //Fill Respos Array with the positions created
        ResPositions = FindObjectsOfType<ResPos>();

        FillResources();

        SpawnTrees();

        SpawnCreepPositions();
        Debug.Log("hey");

        buildNavmesh();
        /*
        usedNumbers.Add(100000);
        FillResources();
        ActivateSavageCamp();
        //I clear used numbers to recicle in SpawnCreeps()
        usedNumbers.Clear();
        SpawnCreeps();
        SpawnWorkers();
        SpawnArtifacts();
        */
        
    }

    private void buildNavmesh()
    {

        // Use this for initialization

        surfaces = FindObjectsOfType<NavMeshSurface>();



        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    
}

    private void SpawnCreepPositions()
    {
        for (int i = 0; i < numberOfBigFeatures * 15; i++)
        {
            Vector3 PosToSpawn = GetPosToSpawn();
            if (checkDistances(PosToSpawn, SpawnPosition.GetComponent<WorldFeature>()))
            {
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(PosToSpawn.x, 20, PosToSpawn.z), Vector3.down, out hit, Mathf.Infinity))
                {
                    var newHill = Instantiate(SpawnPosition, hit.point, Quaternion.Euler(0, UnityEngine.Random.Range(0, 180), 0));
                }
            }else
            {
                i--;
            }
            
        }
    }

    private void SpawnTrees()
    {
        for (int i = 0; i < numberOfBigFeatures*15; i++)
        {
            var hillToSpawn = Trees[UnityEngine.Random.Range(0, Trees.Length)];
            bool SpotFound = false;
            
                //get a randomSpot in the terrain and 
                Vector3 PosToSpawn = GetPosToSpawn();
                PosToSpawn = FindCloseTrees(PosToSpawn);
           
                RaycastHit hit;
                if (Physics.Raycast(new Vector3(PosToSpawn.x, 20, PosToSpawn.z), Vector3.down, out hit, Mathf.Infinity))
                {
                    var newHill = Instantiate(hillToSpawn, hit.point, Quaternion.Euler(0, UnityEngine.Random.Range(0, 180), 0));

                }
        }
    }

    private Vector3 FindCloseTrees(Vector3 _posToSpawn)
    {
        float closeRange = 50;
        int xOffset = 2, zOffset = 2;
        Collider[] hitColliders = Physics.OverlapSphere(_posToSpawn, closeRange);
        int i = 0;
        while (i < hitColliders.Length)
        {
            WorldSmallFeature SF = hitColliders[i].GetComponent<WorldSmallFeature>();
            if (SF != null)
            {
                if (Vector3.Distance(_posToSpawn, hitColliders[i].transform.position) < closeRange)
                {
                    closeRange = Vector3.Distance(_posToSpawn, hitColliders[i].transform.position);
                    _posToSpawn = new Vector3(hitColliders[i].transform.position.x + xOffset, hitColliders[i].transform.position.y, hitColliders[i].transform.position.z + zOffset);
                    int SafetyNet = 0;
                    while (!IsPosFree(_posToSpawn))
                    {
                        xOffset = xOffset + (int)UnityEngine.Random.Range(-2,2);
                        zOffset = zOffset + (int)UnityEngine.Random.Range(-2, 2);
                        Debug.Log("changed pos");
                        SafetyNet++;
                        if (SafetyNet>10)
                        {
                            break;
                        }
                    }
                    
                }
            }
            i++;
        }
        return _posToSpawn;
    }

    private bool IsPosFree(Vector3 _posToSpawn)
    {
        Collider[] hitColliders = Physics.OverlapSphere(_posToSpawn, 3);
        int i = 0;
        while (i < hitColliders.Length)
        {
            WorldFeature WF = hitColliders[i].GetComponent<WorldFeature>();
            if (WF != null)
            {

                Debug.Log(WF.transform.name);
                return false;

            }
            i++;
        }
        return true;
    }
    
    public void SpawnCreep()
    {

    }
   

    private void SpawnHills()
    {
        for (int i = 0; i < numberOfBigFeatures; i++)
        {
            var hillToSpawn = Hills[UnityEngine.Random.Range(0, Hills.Length)];
            bool SpotFound = false;
            int safetyNet = 0;
            while (!SpotFound)
            {
                //get a randomSpot in the terrain and 
                Vector3 PosToSpawn = GetPosToSpawn(hillToSpawn.GetComponent<WorldFeature>());
                if (safetyNet<300)
                {

                    if (checkDistances(PosToSpawn, hillToSpawn.GetComponent<WorldFeature>()))
                    {
                        var newHill = Instantiate(hillToSpawn, PosToSpawn, Quaternion.Euler(0,UnityEngine.Random.Range(0, 180), 0));
                        BigFeatures.Add(newHill.GetComponent<WorldFeature>());
                        SpotFound = true;
                    }

                }else
                {
                    break;
                }
                safetyNet++;
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
    private Vector3 GetPosToSpawn(WorldFeature _WF)
    {
        return new Vector3(UnityEngine.Random.Range(-tWidth / 2 +_WF.Rad, tWidth / 2 - _WF.Rad), 0, UnityEngine.Random.Range(-tLength / 2 + _WF.Rad, tLength / 2 - _WF.Rad));
    }

    private void SpawnBase()
    {
        var mainBase  = (GameObject) Instantiate(BasePrefab, new Vector3(0, 0 , 0), BasePrefab.transform.rotation);
        BigFeatures.Add( (WorldFeature )mainBase.GetComponent<WorldFeature>());
    }

   
    void FillResources()
    {
        for (int i = 0; i < ResPositions.Length/2; i++)
        {
            var posNumber = UnityEngine.Random.Range(0,ResPositions.Length );
            if (!usedNumbers.Contains(posNumber) || usedNumbers == null)
            {
                usedNumbers.Add(posNumber);
                
                var ress = Instantiate(ResourcePrefab[UnityEngine.Random.Range(0, ResourcePrefab.Length)],ResPositions[posNumber].transform.position, ResPositions[posNumber].transform.rotation);
                Res.Add(ress);
            }
            else
            {
                i--;
            }
        }
    }
   
}
