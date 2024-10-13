using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game2Manager : MonoBehaviour
{
    public List<GameObject> listFurnitures;
    public List<Sprite> listSprite;
    public List<string> listVoice;
    public GameObject itemSelect;

    // Start is called before the first frame update
    void Start()
    {

        itemSelect.transform.localScale = Vector3.zero;
        foreach(GameObject go in listFurnitures)
        {
            go.GetComponent<Button>().enabled = false;
        }

        StartCoroutine(Create(2));
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
            int index = Random.RandomRange(0, listFurnitures.Count);
            itemSelect.GetComponent<Image>().sprite = listSprite[index];
            itemSelect.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
            AudioManager.instance.PlaySFX($"{listVoice[index]}");
            listFurnitures[index].GetComponent<Button>().enabled = true;
            listFurnitures.RemoveAt(index);
            listSprite.RemoveAt(index);
            listVoice.RemoveAt(index);
        }
        else
        {
            Debug.Log("victory!");
        }

    }

}
