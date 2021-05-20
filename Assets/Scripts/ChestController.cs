using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ChestController : MonoBehaviour, IInteractable
{
    #region Param_block

    private TypeOfInteractable _WhatKindOfObj
    {
        get => WhatKindOfObj;
        set
        {
            WhatKindOfObj = value;
            GetInfo(WhatKindOfObj);
        }
    }
    private GameObject _canvasParent;
    private Text _textInfoObj;
    private AudioSource _audioSource;
    private bool _isActiveOver = true;

    public Text prefabTextInfoObject;
    public GameObject textPosition;
    public AudioClip[] soundsAC;
    public int RequiredLevel { get; set; }
    public int clickToDestroy = 1;
    public string Description { get; set; }
    public TypeOfInteractable WhatKindOfObj { get; set; }

    //Set description for different types
    public void GetInfo(TypeOfInteractable whatis)
    {
        switch (whatis)
        {
            case TypeOfInteractable.exp:
                Description = "I will give experience\n" + "Required level - " + RequiredLevel.ToString();
                break;
            case TypeOfInteractable.damage:
                Description = "I will do the damage\n" + "Required level - " + RequiredLevel.ToString();
                break;
            case TypeOfInteractable.destroy:
                Description = "HP(" +clickToDestroy.ToString()+") I will destroy myself\n" + "Required level - " + RequiredLevel.ToString();
                break;
            default:
                Description = "I am something else \n" + "Required level - " + RequiredLevel.ToString();
                break;
        }
        
    }

    #endregion
    
    public void DoSomething(GameObject obj)
    {
        //describe what each chest does
        switch (WhatKindOfObj)
        {
            case TypeOfInteractable.exp:
                obj.GetComponent<PlayerController>().Experience += 100;
                break;
            case TypeOfInteractable.damage:
                obj.GetComponent<PlayerController>().ReciveDamage(20);
                break;
            case TypeOfInteractable.destroy:
                //ПО ТЗ УСЛОВИЕ НА КОЛИЧЕСТВО КЛИКОВ для уничтожения
                clickToDestroy--;
                
                if (clickToDestroy == 0)
                {
                    GetComponent<Animator>().SetTrigger("SisamOpen");
                    //gameObject.GetComponent<Collider>().enabled = false;
                    Description = "DESTROYED";
                    Destroy(this.gameObject, 2f);
                    Destroy(_textInfoObj.gameObject,2f);
                    _isActiveOver = false;
                }
                else
                {
                    Description = "HP(" +clickToDestroy.ToString()+") I will destroy myself\n" + "Required level - " + RequiredLevel.ToString();
                    if (_textInfoObj)
                    { 
                        _textInfoObj.GetComponent<Text>().text = Description;   
                    }
                    
                }

                break;
            default:
                break;
        }
        obj.GetComponent<PlayerController>().UpdateSliderExp();
    }
    public void PlaySound() //Play via animation
    {
        _audioSource.PlayOneShot(soundsAC[0], 0.7f);
    }

    public void SetupChestsFromOtherScript(int variation)
    {
        switch (variation)
        {
            case 1:
                _WhatKindOfObj = TypeOfInteractable.exp;
                break;
            case 2:
                _WhatKindOfObj = TypeOfInteractable.destroy;
                break;
            case 3:
                _WhatKindOfObj = TypeOfInteractable.damage;
                break;
        }
    }

    #region Unity_block

    private void Start()
    {
        _canvasParent = GameObject.FindGameObjectWithTag("UITag");
        _audioSource = GetComponent<AudioSource>();
        var component = GetComponent<SetupChests>();
        clickToDestroy = Random.Range(1, 6);
        if (component.overrideProperty) //Если хочется ручных натсроек без лишнего рандома
        {
            component.OverrideProprety();
        }
        else //Рандом на свойства сундуков
        {
            //Random level for chests
            RequiredLevel = Random.Range(1, 6);
            //Random type of chests
            int temp = Random.Range(0, 3);
            switch (temp)
            {
                case 0: 
                    _WhatKindOfObj = TypeOfInteractable.exp;
                    break;
                case 1:
                    _WhatKindOfObj = TypeOfInteractable.damage;
                    break;
                case 2:
                    _WhatKindOfObj = TypeOfInteractable.destroy;
                    break;
            }
        }
        
        
    }

    //Show info of chests on mouse over
    private void OnMouseOver()
    {
        //if (Input.GetMouseButtonDown(0)) DoSomething();
        if (!_isActiveOver)
        {
            return;
        }
        if (!_textInfoObj)
        {
            //Create UI text
            _textInfoObj = Instantiate(prefabTextInfoObject, Vector3.zero, Quaternion.identity,
                _canvasParent.transform);
            _textInfoObj.GetComponent<Text>().text = Description;
            
            //Glow object
            var materials = GetComponentsInChildren<MeshRenderer>();
            foreach (var VARIABLE in materials)
            {
                if (RequiredLevel > GameManager.GMinstance.playerLevel)
                {
                    var newColor = new Color32(243, 29, 29, 255);
                    VARIABLE.materials[0].SetColor("_EmissionColor", newColor);
                }
                else
                {
                    var newColor = new Color32(147, 147, 147, 255);
                    VARIABLE.materials[0].SetColor("_EmissionColor", newColor);
                }
                VARIABLE.materials[0].EnableKeyword("_EMISSION");
            }
        }

        if (_textInfoObj)
        {
            //Move UI text
            Vector3 namePose = Camera.main.WorldToScreenPoint(textPosition.transform.position);
            _textInfoObj.transform.position = namePose;
        }
    }

    //Remove info of chests
    private void OnMouseExit()
    {
        //Destroy UI text
        if (_canvasParent.transform.childCount > 0)
            for (int i = 0; i < _canvasParent.transform.childCount; i++)
            {
                Destroy(_canvasParent.transform.GetChild(i).gameObject);
            }
        //if (_textInfoObj) Destroy(_textInfoObj.gameObject);
        //Remove Glow from object
        var materials = GetComponentsInChildren<MeshRenderer>();
        foreach (var VARIABLE in materials)
        {
           VARIABLE.materials[0].DisableKeyword("_EMISSION");
        }
    }

    #endregion
    
    
}
