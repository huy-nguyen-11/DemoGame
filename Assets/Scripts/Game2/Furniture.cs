using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Furniture : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform targetPos;
    public Game2Manager game2Manager;
    Vector3 _startPos;
    Vector3 offset;
    public bool isSelectDrag = false;
    public bool isEnableDrag = true;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.localPosition;  // Store the initial position
        isSelectDrag = false;
        isEnableDrag = true;
    }


    CanvasGroup canvasGroup;
    public string destinationTag = "DropArea";

    void Awake()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (game2Manager.isDragObject && isEnableDrag)
        {


            //offset = transform.localPosition - Input.mousePosition;
            //canvasGroup.alpha = 0.75f;
            //canvasGroup.blocksRaycasts = false;


            RectTransform rectTransform = GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                Input.mousePosition,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );
            Vector3 newLocalPoint = new Vector3(localPoint.x, localPoint.y, transform.localPosition.z); 
            offset = transform.localPosition - newLocalPoint;

            canvasGroup.alpha = 0.75f;
            canvasGroup.blocksRaycasts = false;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (game2Manager.isDragObject && isEnableDrag)
        {
            //transform.localPosition = Input.mousePosition + offset;


            RectTransform rectTransform = GetComponent<RectTransform>();


            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                Input.mousePosition,
                eventData.pressEventCamera,
                out Vector2 localPoint
            );


            Vector3 newLocalPoint = new Vector3(localPoint.x, localPoint.y, transform.localPosition.z);
            transform.localPosition = newLocalPoint + offset;
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
                            int num = Random.Range(-5, 5);   
                            float posX = Random.Range(-200f, 200f);
                            float posY = Random.Range(-1f, 1f);
                            float randomRotationZ = Random.Range(0, 2) == 0 ? -20f : 20f;
                            transform.DORotate(new Vector3(0, 0, randomRotationZ), 0.5f);

                            transform.DOLocalMove(new Vector3(num * 25 , posY, 0), 0.5f).SetEase(Ease.InOutBack).OnComplete(() =>
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
