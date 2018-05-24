using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece
{
    int size;
    public Vector2 position; // Relative position to other MapPieces (0,0 top-left)
    Vector3 realPosition;
    public Room[,] Rooms = new Room[3, 3];
    public List<Vector2> entrances = new List<Vector2>();
    Vector3 bottomLeftCorner;
    float separation;
    public string role;
    public bool exitRight, exitLeft, exitUp, exitDown;
    public Map Map;

    //Delete this !!
    public List<GameObject> TerrainPrefabs = new List<GameObject>();

    public void InstanciateTerrain()
    {
        foreach (Room room in Rooms)
        {
            GameObject terrain;
            if (room.up == true && room.down == true && room.left == true && room.right == true)
            { //X
                terrain = Map.Instantiate(Map.X[0], room.RealPos, Quaternion.Euler(0, 0, 0));

            }
            else if (room.up == true && room.down == true && room.left == true)
            {//T from the left

                terrain = Map.Instantiate(Map.T[0], room.RealPos, Quaternion.Euler(0, 90, 0));
            }
            else if (room.up == true && room.down == true && room.right == true)
            {//T from the right

                terrain = Map.Instantiate(Map.T[0], room.RealPos, Quaternion.Euler(0, 270, 0));
            }
            else if (room.up == true && room.left == true && room.right == true)
            {//T reversed

                terrain = Map.Instantiate(Map.T[0], room.RealPos, Quaternion.Euler(0, 180, 0));
            }
            else if (room.left == true && room.down == true && room.right == true)
            {//T normal

                terrain = Map.Instantiate(Map.T[0], room.RealPos, Quaternion.Euler(0, 0, 0));
            }
            else if (room.up == true && room.down == true)
            {//I

                terrain = Map.Instantiate(Map.I[0], room.RealPos, Quaternion.Euler(0, 0, 0));
            }
            else if (room.left == true && room.right == true)
            {//I Horizontal

                terrain = Map.Instantiate(Map.I[0], room.RealPos, Quaternion.Euler(0, 90, 0));
            }
            else if (room.up == true && room.right == true)
            {//L

                terrain = Map.Instantiate(Map.L[0], room.RealPos, Quaternion.Euler(0, 0, 0));
            }
            else if (room.right == true && room.down == true)
            {//L 90

                terrain = Map.Instantiate(Map.L[0], room.RealPos, Quaternion.Euler(0, 90, 0));
            }
            else if (room.down == true && room.left == true)
            {//L 180

                terrain = Map.Instantiate(Map.L[0], room.RealPos, Quaternion.Euler(0, 180, 0));
            }
            else if (room.up == true && room.left == true)
            {//L 270

                terrain = Map.Instantiate(Map.L[0], room.RealPos, Quaternion.Euler(0, 270, 0));
            }
            else if (room.up == true)
            {

                terrain = Map.Instantiate(Map.End[0], room.RealPos, Quaternion.Euler(0, 180, 0));
            }
            else if (room.down == true)
            {

                terrain = Map.Instantiate(Map.End[0], room.RealPos, Quaternion.Euler(0, 0, 0));
            }
            else if (room.left == true)
            {

                terrain = Map.Instantiate(Map.End[0], room.RealPos, Quaternion.Euler(0, 90, 0));
            }
            else if (room.right == true)
            {

                terrain = Map.Instantiate(Map.End[0], room.RealPos, Quaternion.Euler(0, 270, 0));
            }
            else
            {
                terrain = Map.Instantiate(Map.Mist[0], room.RealPos, Quaternion.Euler(0, 270, 0));
            }
            terrain.name = room.relativePos + " left : " + room.left + " || right : " + room.right + " up : " + room.up + " || down : " + room.down;
            TerrainPrefabs.Add(terrain);
        }
    }
    public void ChooseEntrance()
    { //make it with a loop 
        if (role != "Base")
        {
            foreach (var item in entrances)
            {
                CreatePath(item);
            }
        }
        

    }
    public void CheckSurroundings()
    {


        List<Room> validSurroundings = new List<Room>();
        for (int i = 0; i < Rooms.GetLength(0); i++)
        {
            for (int z = 0; z < Rooms.GetLength(1); z++)
            {

                validSurroundings.Clear();


                if (i != 0 && Rooms[i - 1, z].role == "Connection") //not on the left of the map && his role is connection
                {
                    validSurroundings.Add((Rooms[i - 1, z]));

                }

                if (i != Rooms.GetLength(0) - 1 && Rooms[i + 1, z].role == "Connection") //not on the right of the map && his role is connection
                {

                    validSurroundings.Add((Rooms[i + 1, z]));
                }
                if (z != Rooms.GetLength(0) - 1 && Rooms[i , z +1 ].role == "Connection") //not up of the map && his role is connection
                {

                    validSurroundings.Add((Rooms[i, z + 1]));

                }
                if (z != 0 && Rooms[i , z -1 ].role == "Connection") //not Down of the map && his role is connection
                {

                    validSurroundings.Add((Rooms[i, z - 1]));

                }


                if (validSurroundings.Count >= 3) //Picking a random connection to set the entrance from
                {

                    Rooms[i, z].AddConnection();

                }

            }
        }
    }
    public void CleanPaths()
        {
        for (int i = 0; i < Rooms.GetLength(0); i++)
        {
            for (int z = 0; z < Rooms.GetLength(1); z++)
            {
                var roomToSet = Rooms[i, z];
                var roomToSetPos = roomToSet.relativePos;

                //CheckConectivity
                if (roomToSet.up == true)
                {
                    if (roomToSetPos.y + 1 < 3)
                    {
                        Rooms[i, z +1].down = true;

                    }
                }
                if (roomToSet.down == true)
                {
                    if (roomToSetPos.y - 1 >= 0)
                    {
                        Rooms[i , z -1].up = true;
                    }
                }
                if (roomToSet.left == true)
                {
                    if (roomToSetPos.x - 1 >= 0)
                    {
                        Rooms[i - 1, z].right = true;
                    }
                }
                if (roomToSet.right == true)
                {
                    if (roomToSetPos.x + 1 < 3)
                    {
                        Rooms[i + 1, z].left = true;
                    }
                }
            }
        }
        
        
    }
    public void CreatePath(Vector2 RoomToSet)
    {//make it with a loop
        Room roomToSet = Rooms[(int)RoomToSet.x, (int)RoomToSet.y];
        if (roomToSet.connected == false)
        {
            Debug.Log(roomToSet.role);
            roomToSet.connected = true;
            bool chosing = true;
            while (chosing)
            {

                if (roomToSet.left == null)
                {
                    Boolean boolValue = (UnityEngine.Random.Range(0, 2) == 0);
                    if (boolValue)
                    {
                        roomToSet.left = true;
                        chosing = false;
                    }
                }
                if (roomToSet.up == null)
                {
                    Boolean boolValue = (UnityEngine.Random.Range(0, 2) == 0);
                    if (boolValue)
                    {
                        roomToSet.up = true;
                        chosing = false;
                    }
                }
                if (roomToSet.right == null)
                {
                    Boolean boolValue = (UnityEngine.Random.Range(0, 2) == 0);
                    if (boolValue)
                    {
                        roomToSet.right = true;
                        chosing = false;
                    }
                }
                if (roomToSet.down == null)
                {
                    Boolean boolValue = (UnityEngine.Random.Range(0, 2) == 0);
                    if (boolValue)
                    {
                        roomToSet.down = true;
                        chosing = false;
                    }
                }
                if (roomToSet.down != null && roomToSet.up != null && roomToSet.left != null && roomToSet.right != null)
                {
                    chosing = false;
                }
            }
            if (roomToSet.left == null)
            {
                roomToSet.left = false;
            }
            if (roomToSet.right == null)
            {
                roomToSet.right = false;
            }
            if (roomToSet.down == null)
            {
                roomToSet.down = false;
            }
            if (roomToSet.up == null)
            {
                roomToSet.up = false;
            }

            //4 if statements to set false neighbours
            if (roomToSet.up == false)
            {
                if (RoomToSet.y + 1 < 3)
                {
                    Room UpRoom = Rooms[(int)RoomToSet.x, (int)RoomToSet.y + 1];
                    UpRoom.down = false;

                }
            }
            if (roomToSet.down == false)
            {
                if (RoomToSet.y - 1 >= 0)
                {
                    Room DownRoom = Rooms[(int)RoomToSet.x, (int)RoomToSet.y - 1];
                    DownRoom.up = false;

                }
            }
            if (roomToSet.left == false)
            {
                if (RoomToSet.x - 1 >= 0)
                {
                    Room LeftRoom = Rooms[(int)RoomToSet.x - 1, (int)RoomToSet.y];
                    LeftRoom.right = false;
                }
            }
            if (roomToSet.right == false)
            {
                if (RoomToSet.x + 1 < 3)
                {
                    Room RightRoom = Rooms[(int)RoomToSet.x + 1, (int)RoomToSet.y];
                    RightRoom.left = false;
                }
            }
            //Create followup rooms
            if (roomToSet.up == true)
            {
                if (RoomToSet.y + 1 < 3)
                {
                    Room UpRoom = Rooms[(int)RoomToSet.x, (int)RoomToSet.y + 1];
                    UpRoom.down = true;

                    if (UpRoom.connected == false)
                    {
                        CreatePath(UpRoom.relativePos);
                    }
                }
            }
            if (roomToSet.down == true)
            {
                if (RoomToSet.y - 1 >= 0)
                {
                    Room DownRoom = Rooms[(int)RoomToSet.x, (int)RoomToSet.y - 1];
                    DownRoom.up = true;
                    if (DownRoom.connected == false)
                    {
                        CreatePath(DownRoom.relativePos);
                    }
                }
            }
            if (roomToSet.left == true)
            {
                if (RoomToSet.x - 1 >= 0)
                {
                    Room LeftRoom = Rooms[(int)RoomToSet.x - 1, (int)RoomToSet.y];
                    LeftRoom.right = true;
                    if (LeftRoom.connected == false)
                    {
                        CreatePath(LeftRoom.relativePos);
                    }
                }
            }
            if (roomToSet.right == true)
            {
                if (RoomToSet.x + 1 < 3)
                {
                    Room RightRoom = Rooms[(int)RoomToSet.x + 1, (int)RoomToSet.y];
                    RightRoom.left = true;
                    if (RightRoom.connected == false)
                    {
                        CreatePath(RightRoom.relativePos);
                    }
                }
            }


        }
        else {

            Debug.Log("already connected");
        }


    }
    public void CreateConnections()
    {
        bool finished = false;
        while (!finished)
        {

            foreach (Vector2 CheckingEntrances in entrances)
            {
                int i = 0;
                if (Rooms[(int)CheckingEntrances.x, (int)CheckingEntrances.y].connected)
                {
                    i++;
                }
                if (i == entrances.Count)
                {
                    finished = true;
                }
            }
        }
    }

    public void instanciateRooms()
    {
        for (int i = 0; i < Rooms.GetLength(0); i++)
        {
            for (int z = 0; z < Rooms.GetLength(1); z++)
            {

                var RoomPos = new Vector2(i, z);

                var CurrentRoom = Rooms[i, z];
                CurrentRoom = new Room(RoomPos, GetRealRoomPos(RoomPos), false); //Set room position,realposition, and not connected

                foreach (Vector2 entr in entrances)//We check each entrance to compare
                {
                    if (RoomPos == entr)
                    {
                        CurrentRoom.role = "Entrance"; //if the position of the room is one of the entrances, then, set his role right

                        //Next we set the direction Out
                        if (i == 0)
                        {
                            CurrentRoom.left = true;
                        }
                        else if (i == 2)
                        {
                            CurrentRoom.right = true;
                        }
                        if (z == 0)
                        {
                            CurrentRoom.down = true;
                        }
                        else if (z == 2)
                        {
                            CurrentRoom.up = true;
                        }
                    }
                }
                if (CurrentRoom.role != "Entrance") // else just make it a connection
                {
                    CurrentRoom.role = "Connection";
                    //And set the borders to false;
                    if (i == 0)
                    {
                        CurrentRoom.left = false;
                    }
                    else if (i == 2)
                    {
                        CurrentRoom.right = false;
                    }
                    if (z == 0)
                    {
                        CurrentRoom.down = false;
                    }
                    else if (z == 2)
                    {
                        CurrentRoom.up = false;
                    }
                }

                Rooms[i, z] = CurrentRoom;



            }
        }
    }
    public void SetUpVariables()
    {

        realPosition = new Vector3(separation + size * (position.x), 0, separation + size * (position.y));
        bottomLeftCorner = new Vector3(realPosition.x - size / 2, 0, realPosition.z - size / 2);
        separation = size / 3;

    }

    private Vector3 GetRealRoomPos(Vector2 item)
    {
        return new Vector3((bottomLeftCorner.x + (separation / 2)) + (separation * item.x), 0, (bottomLeftCorner.z + (separation / 2)) + (separation * item.y));
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

        } else if (_extreme == "Baseleft")
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
        return new Vector2(choosex, choosey);

    }

    public void RepresentWithCube()
    {
        var separation = size / 2;

        var CubeRepresentation = GameObject.CreatePrimitive(PrimitiveType.Cube);
        CubeRepresentation.name = "cube  :" + position.x + " " + position.y;

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
        CubeRepresentation.transform.localScale = new Vector3(size, 2, size);
        realPosition = new Vector3(separation + size * (position.x), 0, separation + size * (position.y));
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
    public void  CleanRoomPrefabs()
    {
        foreach (var item in TerrainPrefabs)
        {
            MonoBehaviour.DestroyImmediate(item,false);
        }
        TerrainPrefabs.Clear();
    }

}
