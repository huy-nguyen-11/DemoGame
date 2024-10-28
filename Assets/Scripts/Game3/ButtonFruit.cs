using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonFruit : MonoBehaviour
{
    public string nameFruit;
    public Transform targetPos;
    public Game3Manager game3Manager;
    Vector3 _startPos;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Moving);
        _startPos = transform.position;
    }

    void Moving()
    {
        Instantiate(effect , transform.position, Quaternion.identity);

        int rate = Random.RandomRange(1, 4);
        if (rate == 1)
        {
            AudioManager.instance.PlaySFX("coolbe");
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

        //moving
        float randomRotation = Random.Range(-40f, 40f);
        transform.DORotate(new Vector3(0, 0, randomRotation), 1f, RotateMode.FastBeyond360);

        float count = Random.Range(-0.7f, 1f);
        Vector3 targetPosition = new Vector3(targetPos.position.x + count, targetPos.position.y, 0);

        Vector3 peakPoint = new Vector3(transform.position.x - 2 , transform.position.y , 0); // Vị trí hiện tại
        peakPoint.y += 2f;

        Vector3[] path = { transform.position, peakPoint, targetPosition };
        transform.DOPath(path, 1.2f, PathType.CatmullRom)
                 .SetEase(Ease.OutQuad)
                 .OnComplete(() =>
                 {
                     GetComponent<Button>().enabled = false;
                     game3Manager.listFruits.RemoveAt(game3Manager.indexSelected);
                     game3Manager.listAudioFruits.RemoveAt(game3Manager.indexSelected);

                     game3Manager.Create();
                 });

    }
}