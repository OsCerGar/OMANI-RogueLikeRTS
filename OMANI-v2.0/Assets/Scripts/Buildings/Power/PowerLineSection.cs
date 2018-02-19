using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A class that will hold information about each rope section
public struct PowerLineSection
{
    public Vector3 pos;
    public Vector3 vel;

    //To write RopeSection.zero
    public static readonly PowerLineSection zero = new PowerLineSection(Vector3.zero);

    public PowerLineSection(Vector3 pos)
    {
        this.pos = pos;

        this.vel = Vector3.zero;
    }
}