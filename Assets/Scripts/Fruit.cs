using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fruit : MonoBehaviour
{
    [SerializeField] private List<Sprite> listFruits;
    public GameManager gameManager;

    private void OnEnable()
    {
        Appear();
    }

    public void Appear()
    {
        StartCoroutine(Moving());
    }

    IEnumerator Moving()
    {
        yield return new WaitForSeconds(0.2f);
        //GetComponent<SpriteRenderer>().sprite = listFruits[0];
        transform.DOScale(0, 0.25f).SetEase(Ease.Linear);
        transform.DORotate(new Vector3(0, 0, -359), 0.25f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(2, LoopType.Restart);
        transform.DOMoveY(3f, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            transform.localScale = Vector3.one;
            gameManager.isHaveMole = false;
        });
    }
}
