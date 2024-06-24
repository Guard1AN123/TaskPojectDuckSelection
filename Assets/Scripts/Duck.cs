using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Duck : MonoBehaviour
{
    [field: SerializeField] public Image bodyImage { get; private set; }
    [field: SerializeField] public RectTransform rectTransform { get; private set; }
    [field: SerializeField] public GameObject shadowGameObject { get; private set; }


    public int selectionIndex { get; private set; }

    private DuckState _currentDragState = DuckState.NotDragable;
    private Vector2 _initialPos;

    public void InitializeBusket(Color basketColor, int index, RectTransform goTo)
    {
        bodyImage.color = basketColor;
        selectionIndex = index;
        rectTransform.DOMove(goTo.position, ConfigsManager.Instance.ducksConfig.duckMoveInTime).SetEase(ConfigsManager.Instance.ducksConfig.duckMoveInEase).OnComplete(() => { ChangeDuckState(DuckState.Dragable); });
        _initialPos = goTo.anchoredPosition;
    }
    public void OnClickDown()
    {
        if(_currentDragState == DuckState.Dragable)
        {
            DragManager.Instance.StartDrag(this);
            AudioManager.Instance.PlayAudio(SoundType.Duck);
            ChangeDuckState(DuckState.NotDragable);
        }
    }
    public void OnClickUp()
    {
        if(_currentDragState == DuckState.NotDragable)
        {
            DragManager.Instance.FinishDrag();
        }
    }
    public void ChangeDuckState(DuckState state)
    {
        if(state == DuckState.NotDragable)
        {
            _currentDragState = DuckState.NotDragable;
            shadowGameObject.SetActive(false);
        }
        else if(state == DuckState.Dragable)
        {
            _currentDragState = DuckState.Dragable;
            shadowGameObject.SetActive(true);
        }
    }
    public void ReturnToInitialPos()
    {
        rectTransform.DOAnchorPos(_initialPos, 0.25f).SetEase(Ease.Linear).OnComplete(() => { ChangeDuckState(DuckState.Dragable);});
    }
}
