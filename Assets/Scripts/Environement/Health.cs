using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class Health : MonoBehaviour
{
  [SerializeField] protected AudioClip TakeSound = default;

  protected AudioSource AudioSource;

  private void Start()
  {
    AudioSource = GetComponent<AudioSource>();
  }

  protected abstract void OnTriggerEnter2D(Collider2D collision);
  
}
