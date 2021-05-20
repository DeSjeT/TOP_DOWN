using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSystem
{
    int Health { get; set; }
    int CurrentLevel { get; set; }
    int Experience { get; set; }
    int NeedExpToLevel { get; set; }
}
