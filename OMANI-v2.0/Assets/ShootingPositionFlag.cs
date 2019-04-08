using UnityEngine;

public class ShootingPositionFlag : MonoBehaviour
{
    Transform parent;
    [SerializeField] float rad;
    [SerializeField] float speed;
    Robot myRobot;

    public Robot MyRobot { get => myRobot; set => myRobot = value; }


    // Use this for initialization
    void Start()
    {
        parent = transform.parent;
        transform.position = parent.position + new Vector3(rad, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject enemy = null;
        if (MyRobot != null)
        {
            enemy = MyRobot.getDeployEnemy();
        }
        if (enemy != null)
        {

            var newmiradapos = new Vector3(enemy.transform.position.x, parent.position.y, enemy.transform.position.z);
            var posToGo = (parent.position + ((newmiradapos - parent.position).normalized * rad));

            transform.LookAt(parent.position);

            if (transform.InverseTransformPoint(posToGo).x > 0)
            {
                transform.RotateAround(parent.position, Vector3.up, -Time.deltaTime * speed);
            }
            else
            {
                transform.RotateAround(parent.position, Vector3.up, Time.deltaTime * speed);
            }

        }

    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }


}
