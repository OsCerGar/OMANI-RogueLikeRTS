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
}
