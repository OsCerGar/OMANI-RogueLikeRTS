using UnityEngine;

public class EnuScreen : MonoBehaviour
{

    Animator animator;
    BU_Energy_CityDistricts EnergyDistrict;
    AudioSource sound;
    float animationValue, lastanimationValue;
    bool play;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        EnergyDistrict = transform.parent.GetComponentInParent<BU_Energy_CityDistricts>();
    }

    // Update is called once per frame
    void Update()
    {
        //animationValue = Mathf.Lerp(animationValue, 0.02f * EnergyDistrict.totalEnergyReturn(), 1.25f * Time.deltaTime);
        if (animationValue < 0.02f * EnergyDistrict.totalEnergyReturn())
        {
            animationValue += 0.05f * Time.deltaTime;

            if (animationValue % 0.02f < 0.004f)
            {            // Aqui muchas veces

                if (play)
                {                // Aqui solo entrará una vez

                    play = false;
                    sound.Play();
                }
            }

            else { play = true; }
        }
        else if (animationValue > 0.02f * EnergyDistrict.totalEnergyReturn())
        {
            animationValue -= 0.05f * Time.deltaTime;

            if (animationValue % 0.02f < 0.004f)
            {            // Aqui muchas veces

                if (play)
                {                // Aqui solo entrará una vez

                    play = false;
                    sound.Play();
                }
            }

            else { play = true; }

        }

        animator.Play("EnuScreen", 0, Mathf.Clamp(animationValue, 0, 1));

    }

    public void Sound()
    {
        sound.Play();
    }
}
