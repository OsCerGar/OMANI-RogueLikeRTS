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

    private void Start()
    {
        initializePieces();
        representRoles();

    }

    private void representRoles()
    {
        for (int i = 0; i < Pieces.GetLength(0); i++)
        {
            for (int z = 0; z < Pieces.GetLength(1); z++)
            {

                Pieces[i, z].RepresentWithCube();
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
