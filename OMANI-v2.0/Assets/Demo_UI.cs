using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_UI : MonoBehaviour
{
    List<GameObject> elements = new List<GameObject>();
    GameObject backgroundUI, ps4UI, messageUI, keyboardUI;

    int element = 1;
    // Use this for initialization
    void Start()
    {
        foreach (Transform child in this.transform)
        {
            if (child.name.Equals("Background"))
            {
                backgroundUI = child.gameObject;
            }
            if (child.name.Equals("Ps4"))
            {
                ps4UI = child.gameObject;
            }
            if (child.name.Equals("Keyboard"))
            {
                keyboardUI = child.gameObject;
            }
            if (child.name.Equals("Message"))
            {
                messageUI = child.gameObject;
            }
        }
        elements.Add(messageUI);
        elements.Add(keyboardUI);
        elements.Add(ps4UI);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 9"))
        {
            switch (element)
            {
                case 0:
                    MessageUI();
                    element++;
                    break;
                case 1:
                    KeyboardUI();
                    element++;

                    break;
                case 2:
                    Ps4();
                    element++;
              
                    break;
                case 3:
                    RemoveUI();
                    element = 0;
                    break;
            }
        }
    }

    void MessageUI()
    {
        backgroundUI.SetActive(true);
        messageUI.SetActive(true);
    }
    void KeyboardUI()
    {
        messageUI.SetActive(false);
        keyboardUI.SetActive(true);

    }
    void Ps4()
    {
        keyboardUI.SetActive(false);
        ps4UI.SetActive(true);
    }
    void RemoveUI()
    {
        ps4UI.SetActive(false);
        backgroundUI.SetActive(false);
    }
}
