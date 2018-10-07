using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPosition : MonoBehaviour {
    LookDirectionsAndOrder LDAO;
    Transform parent;
    [SerializeField]float rad;
	// Use this for initialization
	void Start () {
        LDAO = FindObjectOfType<LookDirectionsAndOrder>();
        parent = transform.parent;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = (parent.position + ((LDAO.miradaposition - parent.position).normalized * rad));
	}
}
