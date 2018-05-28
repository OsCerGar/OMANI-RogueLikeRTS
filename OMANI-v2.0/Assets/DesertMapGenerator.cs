using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertMapGenerator : MonoBehaviour {

    [SerializeField] List<GameObject> Obstacle3x3 = new List<GameObject>();
    [SerializeField] List<GameObject> Obstacle2x4 = new List<GameObject>();
    WorldFeature[] TerrainPieces;
     List<GameObject> InstanciatedTerrain = new List<GameObject>();
    public void Clean()
    {
        if (InstanciatedTerrain.Count > 0)
        {
            foreach (var item in InstanciatedTerrain)
            {
                DestroyImmediate(item, false);
            }
        }
    }
    public void Generate()
    {
        TerrainPieces = FindObjectsOfType<WorldFeature>();
        foreach (var item in TerrainPieces)
        {
            if (item.GetType() == typeof(Feature_2x4))
            {
                GenerateObstacle(item.transform,Obstacle2x4[Random.Range(0, Obstacle2x4.Count)]);

            } else if (item.GetType() == typeof(Feature_3x3))
            {
                GenerateObstacle(item.transform, Obstacle3x3[Random.Range(0, Obstacle3x3.Count)]);
            }
        }
    }
    public void GenerateObstacle(Transform where, GameObject newObj)
    {
        var newT = Instantiate(newObj,where.transform.position,where.transform.rotation);
        DestroyImmediate(where.gameObject,false);
        newT.isStatic = true;
    }
}
