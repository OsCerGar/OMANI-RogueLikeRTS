using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece  {
    int size; 
    public Vector2 position; // Relative position to other MapPieces (0,0 top-left)
    Vector3 realPosition;
    public Room[,] Rooms = new Room[3, 3];
    List<Vector2> exits = new List<Vector2>();
    List<Vector2>  entrances = new List<Vector2>();
    public string role;
    public bool exitRight, exitLeft, exitUp, exitDown;
    public GameObject CubeRepresentation;
    public GameObject EntranceCubeRepresentation;
    public void RepresentEntranceRoom()
    {
        foreach (var item in entrances)
        {

            Debug.Log(position);
            var bottomLeftCorner = new Vector3(realPosition.x - size/2,0, realPosition.z - size / 2);
            var separation = size / 3;
            //calculation of the real place where the entrance should be !!
            var realEntrancePosition = new Vector3((bottomLeftCorner.x + (separation/2)) + (separation * item.x), 0, (bottomLeftCorner.z + (separation / 2)) +(separation * item.y));

            EntranceCubeRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
            EntranceCubeRepresentation.name = "Entrance  :" + realEntrancePosition.x + " " + realEntrancePosition.z;

                var tempMaterial = new Material(EntranceCubeRepresentation.GetComponent<Renderer>().sharedMaterial);
                tempMaterial.color = Color.white;
            EntranceCubeRepresentation.GetComponent<Renderer>().material = tempMaterial;


            EntranceCubeRepresentation.transform.localScale = new Vector3(separation, 10, separation);
            EntranceCubeRepresentation.transform.position = realEntrancePosition;
            EntranceCubeRepresentation.transform.tag = "Terrain";

        }
    }

    public void SetEntrance(string _extreme) //Here we define entrances to the mapPiece depending on it's neighbours (checked on Map script)
    {
        Vector2 entrance = new Vector2();
        if (_extreme == "left")
        {
            entrance = new Vector2(0, (int)UnityEngine.Random.Range(0, 2));
        } else if (_extreme == "right")
        {
            entrance = new Vector2(2, (int)UnityEngine.Random.Range(0, 2));
        }
        else if (_extreme == "up")
        {
            entrance = new Vector2((int)UnityEngine.Random.Range(0, 2), 2);

        }
        else if (_extreme == "down")
        {
            entrance = new Vector2((int)UnityEngine.Random.Range(0, 2), 0);

        }else if (_extreme == "Baseleft")
        {
            entrance = new Vector2(0, 1);
        }
        else if (_extreme == "Baseright")
        {
            entrance = new Vector2(2, 1);
        }
        else if (_extreme == "Baseup")
        {
            entrance = new Vector2(1, 2);

        }
        else if (_extreme == "Basedown")
        {
            entrance = new Vector2(1, 0);

        }
        entrances.Add(entrance);
        
    }
    public void AddExtraEntrance()
    {
        if (entrances.Count > 0)
        {
            entrances.Add(getAdditionalEntrance(entrances[0]));
        }
    }

    private Vector2 getAdditionalEntrance(Vector2 _firstEntr)
    {
        List<int> xnumbers = new List<int>();
        List<int> ynumbers = new List<int>();
        xnumbers.Add(0);
        xnumbers.Add(1);
        xnumbers.Add(2);
        ynumbers.Add(0);
        ynumbers.Add(1);
        ynumbers.Add(2);

        xnumbers.Remove((int)_firstEntr.x);
        ynumbers.Remove((int)_firstEntr.y);
        var choosex = xnumbers[UnityEngine.Random.Range(0, xnumbers.Count)];

        if (choosex == 1)
        {
            ynumbers.Remove(1);
        }

        var choosey = ynumbers[UnityEngine.Random.Range(0, ynumbers.Count)];
        return new Vector2(choosex,choosey);

    }

    public void RepresentWithCube()
    {
        var separation = size / 2;
        
        CubeRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
        CubeRepresentation.name = "cube  :"+position.x+" "+position.y;
       
        if (role == "Mist")
        {
            var tempMaterial = new Material(CubeRepresentation.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = Color.red;
            CubeRepresentation.GetComponent<Renderer>().material = tempMaterial;

        }
        else if (role == "POI")
        {
            var tempMaterial = new Material(CubeRepresentation.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = Color.green;
            CubeRepresentation.GetComponent<Renderer>().material = tempMaterial;

        }
        else if (role == "Base")
        {
            var tempMaterial = new Material(CubeRepresentation.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = Color.blue;
            CubeRepresentation.GetComponent<Renderer>().material = tempMaterial;

        }
        else if (role == "Connection")
        {
            var tempMaterial = new Material(CubeRepresentation.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = Color.grey;
            CubeRepresentation.GetComponent<Renderer>().material = tempMaterial;

        }
        CubeRepresentation.transform.localScale = new Vector3(size,2,size);
        realPosition = new Vector3(separation + size * (position.x),0, separation + size * (position.y));
        CubeRepresentation.transform.position = realPosition;
        CubeRepresentation.transform.tag = "Terrain";
    }

    #region Constructors


    public MapPiece(string role)
    {
        this.role = role;
    }

    public MapPiece(int size, Vector2 position, string role)
    {
        this.size = size;
        this.position = position;
        this.role = role;
    }

    public MapPiece(int size, Vector2 position, List<Vector2> exits, Vector2 entrance, string role)
    {
        this.size = size;
        this.position = position;
        this.exits = exits;
        this.entrances.Add(entrance);
        this.role = role;
    }

    public MapPiece(int size, Vector2 position, Vector2 entrance, string role)
    {
        this.size = size;
        this.position = position;
        this.entrances.Add(entrance);
        this.role = role;
    }
    #endregion
}
