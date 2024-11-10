using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SwipeControl : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float time;
    [SerializeField] DG.Tweening.Ease typeTween;
    [SerializeField] Button nextBtn, previuosBtn;
    int currentPage;
    Vector3 targetPos;
    float dragThreshold;

    private void OnEnable()
    {

        currentPage = 1;
        levelPagesRect.localPosition = Vector3.zero;
        targetPos = levelPagesRect.localPosition;
        dragThreshold = Screen.height / 15;
        Debug.Log(dragThreshold);
        UpdateButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            Move();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            Move();
        }
    }

    public void Move()
    {
        levelPagesRect.DOLocalMove(targetPos, time).SetEase(typeTween);
        UpdateButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.y - eventData.pressPosition.y) > dragThreshold)
        {
            if (eventData.position.y > eventData.pressPosition.y)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
        else
        {
            Move();
        }
    }

    void UpdateButton()
    {
        nextBtn.interactable = true;
        previuosBtn.interactable = true;
        if (currentPage == 1) previuosBtn.interactable = false;
        else if (currentPage == maxPage) nextBtn.interactable = false;
    }
}
