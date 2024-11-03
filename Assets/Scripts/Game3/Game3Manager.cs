using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game3Manager : MonoBehaviour
{
    public List<Transform> listObjects;
    public List<Transform> listPositions;
    public List<Sprite> listFruits;
    public List<string> listAudioFruits;
    public GameObject girl, popObject , fruit;
    public GameObject panelReady, panelVictory;
    public int indexSelected;

    private List<Sprite> originalListFruits;
    private List<string> originalListAudioFruits;

    private void Start()
    {
        //copy list
        originalListFruits = new List<Sprite>(listFruits);
        originalListAudioFruits = new List<string>(listAudioFruits);

        if (listObjects.Count != listPositions.Count || listObjects.Count != 9)
        {
            return;
        }

        foreach (Transform go in listObjects)
        {
            go.localScale = Vector3.zero;
            go.GetComponent<Button>().enabled = false;
        }

        fruit.transform.localScale = Vector3.zero;


        girl.transform.localScale = Vector3.zero;
        popObject.transform.localScale = Vector3.zero;

        panelReady.SetActive(true);
        panelVictory.SetActive(false);
    }

    public void PlayGame()
    {
        panelReady.SetActive(false);
        ShuffleObjects();
        StartCoroutine(SetPosObject());
    }

    private void ShuffleObjects()
    {
        for (int i = listObjects.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = listObjects[i];
            listObjects[i] = listObjects[randomIndex];
            listObjects[randomIndex] = temp;
        }
    }


    private IEnumerator SetPosObject()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySFX("pop");
        girl.transform.DOScale(1, 0.2f).OnComplete(() =>
        {
            AudioManager.instance.PlaySFX("hello");
        });

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < listPositions.Count; i++)
        {
            AudioManager.instance.PlaySFX("pop");
            listObjects[i].position = listPositions[i].position;
            listObjects[i].DOScale(1, 0.2f).SetEase(Ease.OutBounce);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        
        popObject.transform.DOScale(1, 0.3f).OnComplete(() =>
        {
            AudioManager.instance.PlaySFX("spawn");

            DOVirtual.DelayedCall(0.5f, Create);
        });
    }

    public void Create()
    {
        if (listFruits.Count > 0)
        {
            int randomIndex = Random.Range(0, listFruits.Count);
            indexSelected = randomIndex;
            fruit.GetComponent<Image>().sprite = listFruits[randomIndex];
            fruit.transform.DOScale(1, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                //play sound fruit.
                AudioManager.instance.PlaySFX(listAudioFruits[randomIndex]);
                Debug.Log("chi soo index la:" + randomIndex);

                foreach(Transform fruitGO in listObjects)
                {
                    if(fruitGO.GetComponent<ButtonFruit>().nameFruit == listAudioFruits[randomIndex])
                    {
                        fruitGO.GetComponent<Button>().enabled = true;
                    }
                }

            });

            //listFruits.RemoveAt(randomIndex);
            //listAudioFruits.RemoveAt(randomIndex);
        }
        else
        {
            Debug.Log("win!");
            panelVictory.SetActive(true);
            AudioManager.instance.PlaySFX("yeah");
            DOVirtual.DelayedCall(2, Reset);
        }

    }

    private void Reset()
    {
        listFruits = new List<Sprite>(originalListFruits);
        listAudioFruits = new List<string>(originalListAudioFruits);

 
        foreach (Transform go in listObjects)
        {
            go.localScale = Vector3.zero;
            go.GetComponent<Button>().enabled = false;
            go.transform.rotation = Quaternion.identity;
        }

        fruit.transform.localScale = Vector3.zero;
        panelVictory.SetActive(false);

        ShuffleObjects();
        StartCoroutine(SetPosObject());
    }
}
