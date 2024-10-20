using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Furniture : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //public Transform targetPos;
    //public Game2Manager game2Manager;
    //Vector3 _startPos;
    //Vector3 offset;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    GetComponent<Button>().onClick.AddListener(Moving);
    //}

    //void Moving()
    //{
    //    int rate = Random.RandomRange(1, 4);
    //    if (rate == 1)
    //    {
    //        AudioManager.instance.PlaySFX("welldone");
    //    }
    //    else if (rate == 2)
    //    {
    //        AudioManager.instance.PlaySFX("goodjob");
    //    }
    //    else if (rate == 3)
    //    {
    //        AudioManager.instance.PlaySFX("correct");
    //    }
    //    else
    //    {
    //        AudioManager.instance.PlaySFX("excellent");
    //    }

    //    transform.DOMove(targetPos.position, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
    //    {
    //        //AudioManager.instance.PlaySFX("spawn");
    //        float posX = Random.Range(-2f, 2f);
    //        float posY = Random.Range(-1f, 1f);
    //        transform.DOMove(new Vector3(transform.position.x + posX, transform.position.y + 3 + posY, 0), 0.5f).SetEase(Ease.InBack);

    //        float randomRotationZ = Random.Range(0, 2) == 0 ? -20f : 20f;
    //        transform.DORotate(new Vector3(0, 0, randomRotationZ), 0.5f).OnComplete(() =>
    //        {
    //            GetComponent<Button>().enabled = false;
    //            game2Manager.Creating();
    //        });
    //    });
    //}

    //CanvasGroup canvasGroup;
    //public string destinationTag = "DropArea";

    //void Awake()
    //{
    //    if (gameObject.GetComponent<CanvasGroup>() == null)
    //        gameObject.AddComponent<CanvasGroup>();
    //    canvasGroup = gameObject.GetComponent<CanvasGroup>();
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.localPosition = Input.mousePosition + offset;
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    offset = transform.localPosition - Input.mousePosition;
    //    canvasGroup.alpha = 0.5f;
    //    canvasGroup.blocksRaycasts = false;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    RaycastResult raycastResult = eventData.pointerCurrentRaycast;
    //    if (raycastResult.gameObject?.tag == destinationTag)
    //    {
    //        transform.localPosition = raycastResult.gameObject.transform.localPosition;
    //    }
    //    canvasGroup.alpha = 1;
    //    canvasGroup.blocksRaycasts = true;
    //}
    public Transform targetPos;
    public Game2Manager game2Manager;
    Vector3 _startPos;
    Vector3 offset;
    public bool isSelectDrag = false;
    public bool isEnableDrag = true;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Button>().onClick.AddListener(Moving);
        _startPos = transform.localPosition;  // Store the initial position
        isSelectDrag = false;
        isEnableDrag = true;
    }

    //void Moving()
    //{
    //    int rate = Random.Range(1, 4);
    //    if (rate == 1)
    //    {
    //        AudioManager.instance.PlaySFX("welldone");
    //    }
    //    else if (rate == 2)
    //    {
    //        AudioManager.instance.PlaySFX("goodjob");
    //    }
    //    else if (rate == 3)
    //    {
    //        AudioManager.instance.PlaySFX("correct");
    //    }
    //    else
    //    {
    //        AudioManager.instance.PlaySFX("excellent");
    //    }

    //    transform.DOMove(targetPos.position, 0.8f).SetEase(Ease.OutBack).OnComplete(() =>
    //    {
    //        float posX = Random.Range(-2f, 2f);
    //        float posY = Random.Range(-1f, 1f);
    //        transform.DOMove(new Vector3(transform.position.x + posX, transform.position.y + 3 + posY, 0), 0.5f).SetEase(Ease.InBack);

    //        float randomRotationZ = Random.Range(0, 2) == 0 ? -20f : 20f;
    //        transform.DORotate(new Vector3(0, 0, randomRotationZ), 0.5f).OnComplete(() =>
    //        {
    //            GetComponent<Button>().enabled = false;
    //            game2Manager.Creating();
    //        });
    //    });
    //}

    CanvasGroup canvasGroup;
    public string destinationTag = "DropArea";

    void Awake()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (game2Manager.isDragObject && isEnableDrag)
        {
            transform.localPosition = Input.mousePosition + offset;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (game2Manager.isDragObject && isEnableDrag)
        {
            //_startPos = transform.localPosition;  // Store the initial position
            offset = transform.localPosition - Input.mousePosition;
            canvasGroup.alpha = 0.75f;
            canvasGroup.blocksRaycasts = false;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (game2Manager.isDragObject && isEnableDrag)
        {
            RaycastResult raycastResult = eventData.pointerCurrentRaycast;
            if (raycastResult.gameObject?.tag == destinationTag && isSelectDrag)
            {
                game2Manager.isDragObject = false;

                int rate = Random.Range(1, 4);
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
                transform.DOLocalMove(raycastResult.gameObject.transform.localPosition + new Vector3(-30, -370, 0), 0.5f).SetEase(Ease.OutBack)
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(0.3f, () =>
                        {
                            transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutBack).OnComplete(() =>
                            {
                                isEnableDrag = false;
                                game2Manager.Creating();
                            });

                        });

                    });
            }
            else
            {
                // If the drop area is invalid, move back to the starting position
                AudioManager.instance.PlaySFX("wrong");
                transform.DOLocalMove(_startPos, 0.5f).SetEase(Ease.OutBack);
            }

            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
      
    }
}
