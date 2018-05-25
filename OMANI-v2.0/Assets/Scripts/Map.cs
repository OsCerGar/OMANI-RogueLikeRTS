using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int size = 3;
    int mapPieceSize = 135;
    MapPiece[,] Pieces; // do in method
    [SerializeField]bool cornerless,diamond;

    public GameObject[] I;
    public GameObject[] L;
    public GameObject[] T;
    public GameObject[] X;
    public GameObject[] End;
    public GameObject[] Mist;
    public GameObject Base;

    public void GenerateMap()
    {
        CleanMap();
        initializePieces();
        SetUpVars();
        SetEntrances();
        AddExtraEntrances();
        ConnectEntrances();
        InstanciateRooms();
        CreatePaths();
        InstanciateTerrain();

    }

    private void InstanciateRooms()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                Pieces[i, z].instanciateRooms();
            }
        }
    }

    private void CreatePaths()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                Pieces[i, z].ChooseEntrance();
            }
        }
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                Pieces[i, z].CheckSurroundings();
            }
        }
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                Pieces[i, z].CleanPaths();
            }
        }

    }
    private void InstanciateTerrain()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                Pieces[i, z].InstanciateTerrain();


            }
        }


    }

    private void ConnectEntrances()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                List<Vector2> nearEntrances;

                if (i+1 < size) //Check MapPiece to the right
                {
                    nearEntrances = Pieces[i + 1, z].entrances; //The piece on the right
                    if (nearEntrances != null)
                    {
                        foreach (Vector2 item in nearEntrances)
                        {
                            if (item.x == 0) // if it has an entrance on the left
                            {
                                Pieces[i, z].entrances.Add(new Vector2(2, item.y)); //we connect it by putting an entrance to the right, same Y
                            }
                        }
                    }
                }
                if (i - 1 >= 0) //Check MapPiece to the left
                {
                    nearEntrances = Pieces[i - 1, z].entrances; 
                    if (nearEntrances != null)
                    {
                        foreach (Vector2 item in nearEntrances)
                        {
                            if (item.x == 2) 
                            {
                                Pieces[i, z].entrances.Add(new Vector2(0, item.y)); 
                            }
                        }
                    }
                }
                if (z - 1 >= 0) //Check MapPiece to the down
                {
                    nearEntrances = Pieces[i, z -1].entrances;
                    if (nearEntrances != null)
                    {
                        foreach (Vector2 item in nearEntrances)
                        {
                            if (item.y == 2)
                            {
                                Pieces[i, z].entrances.Add(new Vector2(item.x, 0));
                            }
                        }
                    }
                }
                if (z + 1 < size) //Check MapPiece to the up
                {
                    nearEntrances = Pieces[i, z + 1].entrances;
                    if (nearEntrances != null)
                    if (nearEntrances != null)
                    {
                        foreach (Vector2 item in nearEntrances)
                        {
                            if (item.y == 0)
                            {
                                Pieces[i, z].entrances.Add(new Vector2(item.x, 2));
                            }
                        }
                    }
                }


            }
        }
    }

    private void AddExtraEntrances()
    {

        List<MapPiece> validSurroundings = new List<MapPiece>();
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {

                validSurroundings.Clear();


                if (i != 0 && CheckRole(Pieces[i - 1, z])) //not on the left of the map && his role is connection
                {
                    validSurroundings.Add((Pieces[i - 1, z]));

                }

                if (i != Pieces.GetLength(0) - 1 && CheckRole(Pieces[i + 1, z])) //not on the right of the map && his role is connection
                {

                    validSurroundings.Add((Pieces[i + 1, z]));
                }
                if (z != Pieces.GetLength(0) - 1 && CheckRole(Pieces[i, z + 1])) //not up of the map && his role is connection
                {

                    validSurroundings.Add((Pieces[i, z + 1]));

                }
                if (z != 0 && CheckRole(Pieces[i, z - 1])) //not Down of the map && his role is connection
                {

                    validSurroundings.Add((Pieces[i, z - 1]));

                }
                MapPiece removeBase = null;
                foreach (MapPiece item in validSurroundings) //If any of the surrounding mapPieces is the Base;
                {

                    if (item.role == "Base")
                    {
                        removeBase = item;
                    }

                }
                if (removeBase != null)
                {
                    validSurroundings.Remove(removeBase);
                }


                if (validSurroundings.Count > 3) //Picking a random connection to set the entrance from
                {

                    Pieces[i, z].AddExtraEntrance();

                }

            }
        }
    }

    private void representEntrances()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {

                Pieces[i, z].SetUpVariables();
            }
        }
    }

    private void CleanMap()
    {
        if (Pieces != null)
        {
            foreach (var item in Pieces)
            {
                item.CleanRoomPrefabs();
            }
        }
    }

    private void SetEntrances()
    {
        List<MapPiece> validSurroundings = new List<MapPiece>();
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {   

                validSurroundings.Clear();


                if (i!=0 && CheckRole(Pieces[i - 1, z])) //not on the left of the map && his role is connection
                {
                    validSurroundings.Add((Pieces[i - 1, z]));
                    
                }
                
                if (i != Pieces.GetLength(0)-1 && CheckRole(Pieces[i + 1, z])) //not on the right of the map && his role is connection
                {
                    
                    validSurroundings.Add((Pieces[i + 1, z]));
                }
                if (z != Pieces.GetLength(0) - 1 && CheckRole(Pieces[i, z + 1])) //not up of the map && his role is connection
                {
                    
                    validSurroundings.Add((Pieces[i , z + 1 ]));

                }
                if (z != 0  && CheckRole(Pieces[i, z -1])) //not Down of the map && his role is connection
                {

                    validSurroundings.Add((Pieces[i , z - 1]));

                }

                bool baseFound = false;
                foreach (MapPiece item in validSurroundings) //If any of the surrounding mapPieces is the Base, then we force the entrance to be there
                {
                    
                    if (item.role == "Base")
                    {
                        baseFound = true;
                        Pieces[i, z].SetEntrance(CheckRelativePosition(Pieces[i, z], item, "Base"));
                    }
                    
                }
                if (baseFound) //Base found so forget about the randompicking
                {
                    validSurroundings.Clear();
                }


                if (validSurroundings.Count > 0) //Picking a random connection to set the entrance from
                {
                    MapPiece chosen = validSurroundings[UnityEngine.Random.Range(0, validSurroundings.Count - 1)];
                    validSurroundings.Clear();
                    Pieces[i, z].SetEntrance(CheckRelativePosition(Pieces[i, z], chosen, "NotBase"));
                    
                }
               
            }
        }
    }

    private string CheckRelativePosition(MapPiece currentPiece, MapPiece neighboor,String _role)
    {
        String returnString = null;
        if (neighboor.position.x == currentPiece.position.x-1)
        {
            returnString =  "left";
        }
        else if (neighboor.position.x == currentPiece.position.x + 1)
        {
            returnString =  "right";
        }
        else if (neighboor.position.y == currentPiece.position.y + 1)
        {
            returnString = "up";
        }
        else if (neighboor.position.y == currentPiece.position.y - 1)
        {
            returnString = "down";
        }
        if (returnString != null)
        {
            if (_role == "Base")
            {
                returnString = "Base" + returnString;
            }
        }
        return returnString;


    }

    private bool CheckRole(MapPiece mapPiece)
    {
        if (mapPiece.role == "Mist")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void SetUpVars()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {

                Pieces[i, z].Map = this;
                Pieces[i, z].SetUpVariables();
            }
        }
    }

    private void PrintRoles()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {

                Debug.Log(i + "   " + z + Pieces[i, z].role);
            }
        }
    }

    void initializePieces() //creates An Array of MapPieces, establishing general roles and conections 
    {
        Pieces = new MapPiece[size, size];
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {
                Pieces[i,z] = InstanciateMapPiece(i,z);
            }
        }
    }

    private MapPiece InstanciateMapPiece(int _i,int _z) //Long if tree, to control the roles of the mapPieces
    {
        if (diamond) //cut Corners and uncentered extremes
        {
            if (_i == size - 1 || _i == 0) // if in extremes
            {
                if (_z != size/2) // and not in center
                {
                    return new MapPiece(mapPieceSize,new Vector2(_i,_z),"Mist"); // MIST means outside of the map
                }
                else
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "POI"); //Important Point (Boss or similar)
                }
            }
            else if (_z == size - 1 || _z == 0) //in z extremes
                {
                        if (_i != size / 2) // and not in center
                        {
                            return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Mist"); // MIST means outside of the map
                        }
                        else
                        {
                            return new MapPiece(mapPieceSize, new Vector2(_i, _z), "POI"); //Important Point (Boss or similar)
                        }
                        
                    
                }
                else
                {
                    if (_i == size / 2 && _z == size / 2) //if in the center of the map
                    {
                        return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Base"); //The base will be here

                    }
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Connection"); // Connection MapPiece
                }
            
        }
        else if (cornerless) // cut corners
        {
            if (_i == size - 1 || _i == 0) // if in extremes
            {
                if (_z == size - 1 || _z == 0) // and not in center
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Mist"); // MIST means outside of the map
                }
                else
                {
                    if (_z != size / 2) // and not in center
                    {
                        return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Connection"); // Connection MapPiece
                    }
                    else
                    {
                        return new MapPiece(mapPieceSize, new Vector2(_i, _z), "POI"); //Important Point (Boss or similar)
                    }
                }
            }
            else //not in extremes
            {
                if (_i == size / 2 && _z == size / 2) //if in the center of the map
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Base"); //The base will be here

                }
                else if (_z == size - 1 || _z == 0) //in the middle z wise
                {
                    if (_i != size / 2) // and not in center
                    {
                        return new MapPiece(mapPieceSize, new Vector2(_i, _z), "POI"); //Important Point (Boss or similar)
                    }
                    else
                    {
                        return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Connection");
                    }
                }
                else 
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Connection"); 
                }
            }
        }
        else
        {
            if (_i == size - 1 || _i == 0) // if in extremes
            {
                if (_z == size - 1 || _z == 0) // and in corner
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "POI"); //Important Point (Boss or similar)
                }
                else
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Connection"); 

                }
            }
            else //not in extremes
            {
                if (_i == size / 2 && _z == size / 2) //if in the center of the map
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Base"); //The base will be here

                }
                else
                {
                    return new MapPiece(mapPieceSize, new Vector2(_i, _z), "Connection"); // Connection MapPiece
                }
            }
        }
    }
}
