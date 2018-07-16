using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour {
    [SerializeField] Transform Follow;
    [SerializeField] bool position,rotation;
    // Update is called once per frame
    void Update () {
        if (position)
        {
            transform.position = Follow.position;
        }
        if (rotation)
        {
            transform.rotation = Follow.rotation;

        }
	}
}
