using System.Collections;
using UnityEngine;

public class SurkaTutorialAnimations : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool ZD;
    float ZDF;

    public void Update()
    {

        if (ZD) {
            ZDF = Mathf.Clamp(ZDF - (Time.deltaTime / 2), 0, 1);
            Debug.Log(ZDF);
            anim.SetFloat("Z", ZDF);
        }
    }


    public void ZDown()
    {
        ZD = true;
        ZDF = anim.GetFloat("Z");
        StartCoroutine("Z");

    }
    public void Yell()
    {
        anim.SetFloat("Z", 0f);
        anim.SetTrigger("Yell");
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Z()
    {
        yield return new WaitForSeconds(2f);
        anim.SetFloat("Z", 1f);
        ZDF = 0;
        ZD = false;
    }

}
