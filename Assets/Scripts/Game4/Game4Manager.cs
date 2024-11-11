using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirFishLab.ScrollingList.Demo;
using AirFishLab.ScrollingList;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game4Manager : MonoBehaviour
{
    public static Game4Manager instance;

    private string colorHead, colorBody, colorTail;
    public ListEventDemo listEventDemo1, listEventDemo2, listEventDemo3;
    public GameObject panelVictory;

    bool is12, is23, is13;


    private void Awake()
    {
        instance = this;
        //listEventDemo = new ListEventDemo();
    }

    // Start is called before the first frame update
    void Start()
    {
        panelVictory.SetActive(false);

        // Subscribe to the OnColorEventChanged callback
        listEventDemo1.OnHeadColorEventChanged += HandleHeadColorEventChanged;//color head
        listEventDemo2.OnBodyColorEventChanged += HandleBodyColorEventChanged;//color body
        listEventDemo3.OnTailColorEventChanged += HandleTailColorEventChanged;//color tail

        colorHead = listEventDemo1.HeadColorEvent;
        colorBody = listEventDemo2.BodyColorEvent;
        colorTail = listEventDemo3.TailColorEvent;
        StartCoroutine(CheckVictory());

        is12 = false;
        is13 = false;
        is23 = false;
    }


    private void HandleHeadColorEventChanged(string newHeadColorEvent)
    {
        // Debug.Log("Head Color: " + newHeadColorEvent);
        colorHead = newHeadColorEvent;
        StartCoroutine(CheckVictory());
    }

    private void HandleBodyColorEventChanged(string newBodyColorEvent)
    {
        // Debug.Log("Body Color: " + newBodyColorEvent);
        colorBody = newBodyColorEvent;
        StartCoroutine(CheckVictory());
    }

    private void HandleTailColorEventChanged(string newTailColorEvent)
    {
        // Debug.Log("Tail Color: " + newTailColorEvent);
        colorTail = newTailColorEvent;
        StartCoroutine(CheckVictory());
    }

    private IEnumerator CheckVictory()
    {
        AudioManager.instance.PlaySFX("pop");
        yield return new WaitForSeconds(1f); // Delay for 1 second

        if (colorHead == colorBody && colorBody == colorTail && colorHead == colorTail)
        {
            Debug.Log("Victory");
            AudioManager.instance.PlaySFX("yeah");
            panelVictory.SetActive(true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Game4");
        }else if (colorHead == colorBody && !is12)
        {
            is12 = true;
            AudioManager.instance.PlaySFX("blink");
        }else if (colorHead == colorTail && !is13)
        {
            is13 = true;
            AudioManager.instance.PlaySFX("blink");
        }else if (colorBody == colorTail && !is23)
        {
            is23 = true;
            AudioManager.instance.PlaySFX("blink");
        }
            Debug.Log("checked!");
    }
}
