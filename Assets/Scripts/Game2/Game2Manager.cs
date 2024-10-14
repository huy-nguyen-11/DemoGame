using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Game2Manager : MonoBehaviour
{
    public List<GameObject> listFurnitures;
    public List<Sprite> listSprite;
    public List<string> listVoice;
    public GameObject itemSelect;
    public GameObject panelReady , panelVictory;

    // Start is called before the first frame update
    void Start()
    {
        panelReady.SetActive(true);
        panelVictory.SetActive(false);
        itemSelect.transform.localScale = Vector3.zero;
        foreach(GameObject go in listFurnitures)
        {
            go.GetComponent<Button>().enabled = false;
        }
    }

    public void PlayGame()
    {
        panelReady.SetActive(false);
        StartCoroutine(Create(1));
        AudioManager.instance.PlaySFX("blink");
    }

    public void Creating()
    {
        StartCoroutine(Create(1));
    }

    IEnumerator Create( int timeDelay)
    {
        if (itemSelect.transform.localScale.x != 0)
        {
            itemSelect.transform.DOScale(0, 0.5f);
        }
        if (listFurnitures.Count> 0)
        {
            yield return new WaitForSeconds(timeDelay);
            AudioManager.instance.PlaySFX("spawn");
            int index = Random.RandomRange(0, listFurnitures.Count);
            itemSelect.GetComponent<Image>().sprite = listSprite[index];
            itemSelect.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                AudioManager.instance.PlaySFX($"{listVoice[index]}");
                listFurnitures[index].GetComponent<Button>().enabled = true;
                listFurnitures.RemoveAt(index);
                listSprite.RemoveAt(index);
                listVoice.RemoveAt(index);
            });
        }
        else
        {
            Debug.Log("victory!");
            yield return new WaitForSeconds(1f);
            AudioManager.instance.PlaySFX("win");
            panelVictory.SetActive(true);
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene("Game2");
        }
    }

}
