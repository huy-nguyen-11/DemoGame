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
        yield return new WaitForSeconds(0.5f); // Delay for 1 second

        if (colorHead == colorBody && colorBody == colorTail && colorHead == colorTail)
        {
            Debug.Log("Victory");
            panelVictory.SetActive(true);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("Game4");
        }else if (colorHead == colorBody || colorHead == colorTail || colorBody == colorTail)
        {

        }
        Debug.Log("checked!");
    }
}
