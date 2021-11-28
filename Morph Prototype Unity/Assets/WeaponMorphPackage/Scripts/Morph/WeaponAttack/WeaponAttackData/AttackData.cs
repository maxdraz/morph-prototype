using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : ScriptableObject
{
   [Header("Timing")]
   [SerializeField] private float duration = 1;
   [SerializeField] private float inputWindowDuringAttack = 0.5f;
   [SerializeField] private float inputWindowAfterAttack = 0.5f;
   
   [Header("Particles")]
   [SerializeField] private GameObject onStartParticles ;
   [SerializeField] private GameObject onHitParticles;
   [SerializeField] private GameObject onEndParticles;
   
   public float Duration => duration;
   public float InputWindowDuringAttack => inputWindowDuringAttack;
   public float InputWindowAfterAttack => inputWindowAfterAttack;
   public GameObject OnStartParticles=> onStartParticles;
   public GameObject OnHitParticles=> onHitParticles;
   public GameObject OnEndParticles=> onEndParticles;
   
   
}
