using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room{
   public  Vector2 relativePos;
   public  Vector3 RealPos;
   public  bool? up, down, left, right;
    public string role;
    public bool connected ;

    public Room(Vector2 relativePos, Vector3 realPos, bool connected)
    {
        this.relativePos = relativePos;
        RealPos = realPos;
        this.connected = connected;
        this.up = null;

        this.down = null;

        this.left = null;

        this.right = null;
        this.role = null;

    }

    public void AddConnection()
    {
        List<bool?> falseDirections = new List<bool?>();
        if (up == false)
        {
            falseDirections.Add(up);
        }
        if (down == false)
        {
            falseDirections.Add(up);
        }
        if (left == false)
        {
            falseDirections.Add(up);
        }
        if (right == false)
        {
            falseDirections.Add(up);
        }
        if (falseDirections.Count > 0)
        {
            falseDirections[Random.Range(0, falseDirections.Count)] = true;
        }
    }
}
