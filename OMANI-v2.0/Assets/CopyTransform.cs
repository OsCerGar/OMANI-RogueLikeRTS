using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour {
    [SerializeField] Transform Follow;
    [SerializeField] bool position,rotation;
    [SerializeField] Vector3 positionOffset = Vector3.zero;
    // Update is called once per frame
    void Update () {
        if (position)
        {
            transform.position = new Vector3(Follow.position.x + positionOffset.x, Follow.position.y + positionOffset.y, Follow.position.z + positionOffset.z);
        }
        if (rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion (transform.rotation.x, Follow.rotation.y, transform.rotation.z, transform.rotation.w), Time.unscaledDeltaTime);

        }
	}
}
