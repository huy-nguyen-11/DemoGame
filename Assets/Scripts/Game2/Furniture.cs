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
        transform.DOMove(targetPos.position, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
        {
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
