using UnityEngine;

public class EnuScreen : MonoBehaviour
{

    Animator animator;
    BU_Energy_CityDistricts EnergyDistrict;
    float animationValue;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        EnergyDistrict = this.transform.parent.GetComponentInParent<BU_Energy_CityDistricts>();
    }

    // Update is called once per frame
    void Update()
    {
        animationValue = Mathf.Lerp(animationValue, 0.02f * EnergyDistrict.totalEnergyReturn(), 0.75f * Time.deltaTime);

        animator.Play("EnuScreen", 0, Mathf.Clamp(animationValue, 0, 1));

    }
}
