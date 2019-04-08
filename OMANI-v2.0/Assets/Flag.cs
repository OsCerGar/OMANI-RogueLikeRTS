using UnityEngine;

public class Flag : MonoBehaviour
{
    Powers powers;
    int armor;
    ShootingPositionFlag shootingPositionFlag;

    private void Start()
    {
        shootingPositionFlag = transform.FindDeepChild("ShootingPosition").GetComponent<ShootingPositionFlag>();
    }

    public void Thrown(Robot _robotToThrow)
    {
        shootingPositionFlag.MyRobot = _robotToThrow;
        _robotToThrow.Deploy(shootingPositionFlag.gameObject);
    }
}
