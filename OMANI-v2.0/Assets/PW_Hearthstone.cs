using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PW_Hearthstone : Power
{
    public bool energy, planted, teleporting;
    private float timeToTeleport, sizeToTeleport = 10, cost = 25, timer, timerToTeleport = 1.5f;

    private Image hearthstoneUI;
    private ParticleSystem hearthstoneExplosionUI;
    [SerializeField]
    Transform teleportBasePosition;

    public override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        hearthstoneUI = this.transform.Find("Player_UI/GUI_Teleport").GetComponent<Image>();
        hearthstoneExplosionUI = hearthstoneUI.transform.GetChild(0).GetComponent<ParticleSystem>();
        teleportBasePosition = FindObjectOfType<Base>().teleportationField;
    }

    public override void CastPower()
    {
        timer += Time.deltaTime;

        if (timer > timerToTeleport)
        {
            timer = 0;

            //Costs
            powers.reducePower((int)cost);
            teleporting = true;
        }

    }

    public void StopCast()
    {
        timer = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (teleporting == true)
        {
            //Disables Movement
            player.enabled = false;
            timeToTeleport += Time.deltaTime;
            hearthstoneUI.enabled = true;
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
            timeToTeleport = 0;
        }

    }
    private void LoadingTeleport()
    {
        Collider[] objectsInArea = null;
        objectsInArea = Physics.OverlapSphere(transform.position, sizeToTeleport, 1 << 9);

        List<NavMeshAgent> peoples = new List<NavMeshAgent>();
        //Checks if there are possible interactions.
        if (objectsInArea.Length > 1)
        {
            {
                NavMeshAgent people;
                for (int i = 0; i < objectsInArea.Length; i++)
                {
                    if (objectsInArea[i].tag != "Player")
                    {
                        people = objectsInArea[i].GetComponent<NavMeshAgent>();

                        if (people != null)
                        {
                            peoples.Add(people);
                        }
                    }
                }
            }
        }
        Teleport(peoples, powers.gameObject);
    }
    public void Teleport(List<NavMeshAgent> _people, GameObject _barroboy)
    {
        //UI disable
        hearthstoneUI.enabled = false;
        if (_people.Count > 0)
        {
            foreach (NavMeshAgent people in _people)
            {
                people.Warp(new Vector3(teleportBasePosition.position.x + Random.Range(5, sizeToTeleport), teleportBasePosition.position.y, teleportBasePosition.position.z + Random.Range(5, sizeToTeleport)));
            }
        }
        Vector3 randomPos = new Vector3(teleportBasePosition.position.x + Random.Range(2, sizeToTeleport), teleportBasePosition.position.y, teleportBasePosition.position.z + Random.Range(2, sizeToTeleport));
        _barroboy.transform.position = randomPos;

        //UI EXPLOSION
        hearthstoneExplosionUI.Play();

        //Restores movement
        player.enabled = true;

    }
}
