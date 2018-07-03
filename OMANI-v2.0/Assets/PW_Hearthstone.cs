using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PW_Hearthstone : Power
{
    public bool energy, planted, teleporting;
    private float timeToTeleport = 0, sizeToTeleport = 7, cost = 25;
    [SerializeField]
    public GameObject hearthstoneUI;
    Powers powers;
    [SerializeField]
    Transform teleportBasePosition;
    // Use this for initialization
    void Start()
    {
        powers = this.GetComponent<Powers>();
    }

    public override void CastPower()
    {
        if (teleporting == true)
        {
            teleporting = false;
        }
        else
        {
            teleporting = true;
        }
        powers.reducePower((int)cost);
    }

    // Update is called once per frame
    void Update()
    {
        if (teleporting == true)
        {
            timeToTeleport += Time.deltaTime;
            hearthstoneUI.SetActive(true);
            hearthstoneUI.transform.Rotate(Vector3.up * Time.deltaTime * 5, Space.World);

            if (timeToTeleport > 5)
            {
                timeToTeleport = 0;
                teleporting = false;
                LoadingTeleport();
            }

        }
        else
        {
            hearthstoneUI.SetActive(false);
            timeToTeleport = 0;
        }

    }

    private void LoadingTeleport()
    {
        Collider[] objectsInArea = null;
        objectsInArea = Physics.OverlapSphere(transform.position, sizeToTeleport, 1 << 9);

        List<NavMeshAgent> peoples = new List<NavMeshAgent>();
        GameObject player = null;
        //Checks if there are possible interactions.
        if (objectsInArea.Length > 1)
        {
            {
                NavMeshAgent people;
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].tag == "Player")
                    {
                        player = objectsInArea[i].gameObject;
                    }

                    else
                    {
                        people = objectsInArea[i].GetComponent<NavMeshAgent>();

                        if (people != null)
                        {
                            peoples.Add(people);
                        }
                    }
                }
            }

            Teleport(peoples, player);

        }
    }


    public void Teleport(List<NavMeshAgent> _people, GameObject _barroboy)
    {
        foreach (NavMeshAgent people in _people)
        {
            people.Warp(new Vector3(teleportBasePosition.position.x + Random.Range(2, sizeToTeleport), teleportBasePosition.position.y, teleportBasePosition.position.z + Random.Range(2, sizeToTeleport)));
        }

        Vector3 randomPos = new Vector3(teleportBasePosition.position.x + Random.Range(2, sizeToTeleport), teleportBasePosition.position.y, teleportBasePosition.position.z + Random.Range(2, sizeToTeleport));
        _barroboy.transform.position = randomPos;
    }
}
