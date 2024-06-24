using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoSingleton<HintManager>
{
    [field:SerializeField] public RectTransform hintHandRectTransform {  get; private set; }

    private Coroutine _hintCoroutine = null;
    private bool _hintInitialized = false;
    private void Update()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            ResetHintTimer();
        }
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase != TouchPhase.Began)
        {
            DisableHint();
        }
    }

    public void InitializeHints()
    {
        _hintCoroutine = StartCoroutine(InteractionTimer());
        _hintInitialized = true;
    }
    public IEnumerator InteractionTimer()
    {
        Debug.Log("dfsdfsdfsdf");
        yield return new WaitForSeconds(ConfigsManager.Instance.gameplayConfig.hintWaitTime);
        ShowHint();
        _hintCoroutine = null;
    }
    public void ShowHint()
    {
        Duck hintDuck = GameManager.Instance.generatedDucks[Random.Range(0, GameManager.Instance.generatedDucks.Count)];
        Basket hintBasket = null;
        foreach (var basket in GameManager.Instance.generatedBaskets)
        {
            if (hintDuck.selectionIndex == basket.selectionIndex)
            {
                hintBasket = basket;
            }
        }
        hintHandRectTransform.position = hintDuck.transform.position;
        hintHandRectTransform.gameObject.SetActive(true);
        hintHandRectTransform.DOMove(hintBasket.transform.position, ConfigsManager.Instance.gameplayConfig.hintMoveTime).SetEase(ConfigsManager.Instance.gameplayConfig.hintMoveEase).SetLoops(-1,LoopType.Restart);
    }
    public void DisableHint()
    {
        if (_hintCoroutine != null)
        {
            StopCoroutine(_hintCoroutine);
            _hintCoroutine = null;
        }
        hintHandRectTransform.gameObject.SetActive(false);
        hintHandRectTransform.DOKill();
    }
    public void ResetHintTimer()
    {
        if(_hintCoroutine != null)
        {
            StopCoroutine(_hintCoroutine);
            _hintCoroutine = null;
        }
        _hintCoroutine = StartCoroutine(InteractionTimer());
    }
}
