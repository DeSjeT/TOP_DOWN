using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
   int RequiredLevel { get; set; }
   string Description { get; set; }
   TypeOfInteractable WhatKindOfObj { get; set; }
   void GetInfo(TypeOfInteractable whatis);
   void DoSomething(GameObject obj);

}
public enum TypeOfInteractable
{
   exp,
   damage,
   destroy
}
