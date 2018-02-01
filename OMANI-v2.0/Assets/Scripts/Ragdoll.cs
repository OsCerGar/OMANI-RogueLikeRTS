using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Declare a class that will hold useful information for each body part
public class BodyPart
{
    public Transform transform;
    public Vector3 storedPosition;
    public Quaternion storedRotation;
}

public class Ragdoll : MonoBehaviour
{
    /* 
     * Ragdoll searches for the components in children in charge of the Ragdoll system
     * and desactivates or activates them.
     * To do that it disables or enables the kinematic function in rigidbody and disables or enables the colliders. 
     */
    public Component[] rigidbodies;
    public Component[] colliders;
    public Component[] boxcolliders;


    public bool startRagdoll;

    //Declare a list of body parts, initialized in Start()
    public List<BodyPart> bodyParts = new List<BodyPart>();

    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<CapsuleCollider>();
        boxcolliders = GetComponentsInChildren<BoxCollider>();



        //Find all the transforms in the character, assuming that this script is attached to the root
        Component[] components = GetComponentsInChildren(typeof(Transform));

        //For each of the transforms, create a BodyPart instance and store the transform 
        foreach (Component c in components)
        {
            BodyPart bodyPart = new BodyPart();
            bodyPart.transform = c as Transform;
            bodyParts.Add(bodyPart);
        }
    }

    private void FixedUpdate()
    {
    }

    void Start()
    {
        if (startRagdoll == true) {
            ragdollTrue();
        }
        else 
        {
            ragdollFalse();
        }
    }

    public void ragdollFalse()
    {
        foreach (Rigidbody rigi in rigidbodies)
            rigi.isKinematic = true;

        foreach (CapsuleCollider capsule in colliders)
            capsule.enabled = false;

        foreach (BoxCollider box in boxcolliders)
            box.enabled = false;



        //Store the ragdolled position for blending
        foreach (BodyPart b in bodyParts)
        {
            b.storedRotation = b.transform.rotation;
            b.storedPosition = b.transform.position;
        }

    }

    public void ragdollTrue()
    {

        foreach (Rigidbody rigi in rigidbodies)
            rigi.isKinematic = false;

        foreach (CapsuleCollider capsule in colliders)
            capsule.enabled = true;

        foreach (BoxCollider box in boxcolliders)
            box.enabled = true;

    }

    public void ragdolledPickup()
    {
        foreach (Rigidbody rigi in rigidbodies)
        rigi.mass = 0.1f;
        this.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = true;

    }

    public IEnumerator ragdollTrueNPC(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);

        this.gameObject.transform.parent.GetComponent<Animator>().enabled = false;
        this.gameObject.transform.parent.GetComponent<Collider>().enabled = false;

        foreach (Rigidbody rigi in rigidbodies)
            rigi.isKinematic = false;

        foreach (CapsuleCollider capsule in colliders)
            capsule.enabled = true;

        foreach (BoxCollider box in boxcolliders)
            box.enabled = true;

        this.gameObject.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 240f);
    }

    public void ragdollTrueNPC()
    {

        this.gameObject.transform.parent.GetComponent<Animator>().enabled = false;
        this.gameObject.transform.parent.GetComponent<Collider>().enabled = false;

        gameObject.transform.parent.gameObject.layer = 15;

        foreach (Rigidbody rigi in rigidbodies) {
            rigi.isKinematic = false;
            rigi.transform.gameObject.layer = 15; }

        foreach (CapsuleCollider capsule in colliders)
            capsule.enabled = true;

        foreach (BoxCollider box in boxcolliders)
            box.enabled = true;

        this.gameObject.transform.parent.GetComponent<Rigidbody>().isKinematic = true;

    }

}


