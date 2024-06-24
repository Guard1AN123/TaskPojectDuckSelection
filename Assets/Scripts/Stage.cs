using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [field:SerializeField] public GameObject onGameObject { get; private set; }
    [field:SerializeField] public GameObject offGameObject { get; private set; }
    [field:SerializeField] public ParticleSystem completeParticle { get; private set; }

    public void CompleteStage()
    {
        offGameObject.SetActive(false);
        onGameObject.SetActive(true);
        completeParticle.Play();
    }
} 
