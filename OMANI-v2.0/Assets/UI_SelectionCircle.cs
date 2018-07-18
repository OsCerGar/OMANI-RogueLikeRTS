using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectionCircle : MonoBehaviour
{

    [SerializeField]
    GameObject circle;
    [SerializeField]
    ParticleSystem firstAnimationParticleSystem, fadeAnimationPArticleSystem;

    void OnEnable()
    {
        firstAnimationParticleSystem.Play();
        circle.SetActive(true);
    }
    void OnDisable()
    {
        //fadeAnimationPArticleSystem.Play();
        circle.SetActive(false);
    }
}
