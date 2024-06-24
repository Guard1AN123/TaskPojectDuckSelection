using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [field: SerializeField] public ParticleSystem appearparticles { get; private set; }
    [field: SerializeField] public Image bodyImage { get; private set; }
    [field: SerializeField] public RectTransform rectTransform { get; private set; }
    public int selectionIndex { get; private set; }

    public void InitializeBusket(Color basketColor, int index)
    {
        bodyImage.color = basketColor;
        selectionIndex = index;
        rectTransform.DOScale(1f, ConfigsManager.Instance.basketsConfig.basketAppearTime).SetEase(ConfigsManager.Instance.basketsConfig.basketAppearEase).OnComplete(() => { AppearParticle(); });
    }
    public void AppearParticle()
    {
        appearparticles.Play();
    }
    public void CallComparison()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Duck"))
        {
            DragManager.Instance.EnteredCollision(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Duck"))
        {
            DragManager.Instance.ExitedCollision(this);
        }
    }
}
