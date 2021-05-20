using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupChests : MonoBehaviour
{
    public MyEnum typeOfChests = MyEnum.exp;
    public int requiredLevel = 1;
    public bool overrideProperty = false;
    
    private ChestController _chestController;
    public enum MyEnum
    {
        exp, damage, destroy
    }

    public void OverrideProprety()
    {
        if (!overrideProperty)
        {
            return;
        }
        _chestController = GetComponent<ChestController>();

        _chestController.RequiredLevel = requiredLevel;
        switch (typeOfChests)
        {
            case MyEnum.exp:
                _chestController.SetupChestsFromOtherScript(1);
                break;
            case MyEnum.destroy:
                _chestController.SetupChestsFromOtherScript(2);
                break;
            case MyEnum.damage:
                _chestController.SetupChestsFromOtherScript(3);
                break;
        }
        
    }
}
