using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Furniture : MonoBehaviour
{
    public Transform targetPos;
    public Game2Manager game2Manager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Moving);
    }

    void Moving()
    {
        int rate = Random.RandomRange(1, 4);
        if (rate == 1)
        {
            AudioManager.instance.PlaySFX("welldone");
        }
        else if (rate == 2)
        {
            AudioManager.instance.PlaySFX("goodjob");
        }
        else if (rate == 3)
        {
            AudioManager.instance.PlaySFX("correct");
        }
        else
        {
            AudioManager.instance.PlaySFX("excellent");
        }

        transform.DOMove(targetPos.position, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            //AudioManager.instance.PlaySFX("spawn");
            float posX = Random.Range(-2f, 2f);
            float posY = Random.Range(-1f, 1f);
            transform.DOMove(new Vector3(transform.position.x + posX, transform.position.y + 3 + posY, 0), 0.5f).SetEase(Ease.InBack);

            float randomRotationZ = Random.Range(0, 2) == 0 ? -20f : 20f;
            transform.DORotate(new Vector3(0, 0, randomRotationZ), 0.5f).OnComplete(() =>
            {
                GetComponent<Button>().enabled = false;
                game2Manager.Creating();
            });
        });
    }
}
