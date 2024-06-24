using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoSingleton<DragManager>
{
    private Vector2 screenSize;
    private Vector2 duckTouchPosDelta;
    private Duck currentDragObject;
    private Basket currentCollisionObject;

    Coroutine dragCoroutine;
    private void Start()
    {
        screenSize = new Vector2 (Screen.width, Screen.height);
    }
    public void StartDrag(Duck duck)
    {
        currentDragObject = duck;
        duckTouchPosDelta = (Input.GetTouch(0).position / UIManager.Instance.mainCanvas.scaleFactor) - duck.rectTransform.anchoredPosition;
        Debug.Log(duckTouchPosDelta);
        dragCoroutine = StartCoroutine(DragCoroutine());
    }
    public IEnumerator DragCoroutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if(Input.touchCount > 0)
            {
                currentDragObject.rectTransform.anchoredPosition = (Input.GetTouch(0).position / UIManager.Instance.mainCanvas.scaleFactor) - duckTouchPosDelta;
            }
        }
    }
    public void FinishDrag()
    {
        StopCoroutine(dragCoroutine);
        if(currentCollisionObject != null)
        {
            GameManager.Instance.CheckPair(currentDragObject,currentCollisionObject);
            currentDragObject = null;
            currentCollisionObject = null;
        }
        else
        {
            currentDragObject.ReturnToInitialPos();
        }
    }

    public void EnteredCollision(Basket basket)
    {
        currentCollisionObject = basket;
    }
    public void ExitedCollision(Basket basket)
    {
        if(currentCollisionObject == basket)
        {
            currentCollisionObject = null;
        }
    }
}
