using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnuiParabola : MonoBehaviour
{

    public Vector3 TargetPos;
    private Vector3 startPos;
    private bool moving;
    float x = 0;
    EnnuiSpawnerManager ennui;


    private void Start()
    {
        ennui = FindObjectOfType<EnnuiSpawnerManager>();

    }
    private void Update()
    {
        if (moving)
        {
            x += (Time.deltaTime * 1f);
            transform.position = MathParabola.Parabola(startPos, TargetPos, 0.5f, x);
            if (x > 0.95)
            {
                x = 0;
                moving = false;

                ennui.SpawnEnnui(this.transform);
                this.transform.gameObject.SetActive(false);
            }
        }
    }
    private void OnEnable()
    {
        SetTargetPos(ShootRaycast(new Vector3(transform.position.x + Random.Range(-3, 3), transform.position.y, transform.position.z + Random.Range(-3, 3))));
    }
    public void SetTargetPos(Vector3 _targetPos)
    {
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        TargetPos = _targetPos;
        x = 0;
        moving = true;
    }
    Vector3 ShootRaycast(Vector3 tr)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(tr.x, tr.y + 10, tr.z), -Vector3.up, out hit))
        {
            return new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }
        return transform.position;
    }
}
