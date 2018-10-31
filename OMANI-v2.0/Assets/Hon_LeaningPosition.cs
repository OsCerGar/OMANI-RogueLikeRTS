using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hon_LeaningPosition : MonoBehaviour {
    Transform player, hon;
    [SerializeField] float playerRadius, honRadius;
    private IEnumerator coroutine;
    // Use this for initialization
    void Start () {

        player = FindObjectOfType<Player>().transform;
        hon = FindObjectOfType<Hon>().transform;

        coroutine = WaitAndCheck(5.0f);
        StartCoroutine(coroutine);

    }

    private IEnumerator WaitAndCheck(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            CheckDistances();
        }
    }

    private void CheckDistances()
    {
        if (Vector3.Distance(transform.position, player.position) < playerRadius)
        {

            SetScreenColor(Color.yellow);
            if (Vector3.Distance(transform.position, hon.position) < honRadius)
            {

                SetScreenColor(Color.red);
                hon.GetComponent<Hon>().GoAttack(this.transform);
            }
        }
        else
        {
            SetScreenColor(Color.green);
        }
    }
    private void SetScreenColor(Color _color)
    {
        Renderer rend = GetComponentInChildren<Renderer>();
        

        //Set the main Color of the Material to green
        rend.material.color = _color;
       
    }
}
