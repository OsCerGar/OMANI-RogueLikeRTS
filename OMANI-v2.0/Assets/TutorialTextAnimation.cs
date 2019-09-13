using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextAnimation : MonoBehaviour
{
    public List<Image> dyingEffect = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dyingEffect[0].transform.Rotate(0, 0, 2f * Time.deltaTime, Space.Self);
        dyingEffect[1].transform.Rotate(0, 0, -2f * Time.deltaTime, Space.Self);
        dyingEffect[2].transform.Rotate(0, 0, 2f * Time.deltaTime, Space.Self);

    }
}
