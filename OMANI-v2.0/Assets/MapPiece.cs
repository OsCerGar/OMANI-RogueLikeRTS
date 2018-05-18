using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece  {
    int size; 
    Vector2 position; // Relative position to other MapPieces (0,0 top-left)
    public Room[,] Rooms = new Room[3, 3];
    List<Vector2> exits;
    Vector2 entrance;
    public string role;

    public void RepresentWithCube()
    {
        var separation = size / 2;
        
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "cube  :"+position.x+" "+position.y;
        if (role == "Mist")
        {
            cube.GetComponent<Renderer>().material.color = Color.red;

        }else if (role == "POI")
        {
            cube.GetComponent<Renderer>().material.color = Color.green;

        }else if (role == "Base")
        {
            cube.GetComponent<Renderer>().material.color = Color.blue;

        }else if (role == "Connection")
        {
            cube.GetComponent<Renderer>().material.color = Color.grey;

        }
        cube.transform.localScale = new Vector3(135,2,135);
        cube.transform.position = new Vector3( separation + size * (position.x),0, separation + size * (position.y));
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
        this.entrance = entrance;
        this.role = role;
    }

    public MapPiece(int size, Vector2 position, Vector2 entrance, string role)
    {
        this.size = size;
        this.position = position;
        this.entrance = entrance;
        this.role = role;
    }
    #endregion
}
