using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffects : MonoBehaviour
{
    public ParticleSystem[] particleEffects;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (gameObject.GetComponentInParent<ChestController>().WhatKindOfObj)
            {
                case TypeOfInteractable.exp:
                    particleEffects[0].Play();
                    break;
                case TypeOfInteractable.damage:
                    particleEffects[1].Play();
                    break;
                case TypeOfInteractable.destroy:
                    particleEffects[2].Play();
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < particleEffects.Length; i++)
        {
            particleEffects[i].Stop();
        }
    }
}
