using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour, IPlayerSystem
{
    #region Param_block

    private Animator _animController;
    private NavMeshAgent _navMesh;
    private int _healthAmount = 100;
    private bool _kick = false;
    private AudioSource _audioSource;
    
    public Image imageProgressBar;
    public Text textHealth;
    public Text textInfoExp;
    public Text textLevelInfo;
    public Slider expSlider;
    public AudioClip[] soundsAC;
    public LayerMask whatIsChest;
    public int Health { get; set; }
    public int Experience { get; set; }
    public int NeedExpToLevel { get; set; }
    public int CurrentLevel { get; set; }

    #endregion
    

    #region UnityBlock

    static PlayerController instance;
    
    public void Awake()
    {
        if(instance == null)
        {    
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        if(instance != this) Destroy(gameObject);
    }
    
    private void Start()
    {
        NeedExpToLevel = 500;
        Health = 100;
        Experience = 0;
        CurrentLevel = 1;
        GameManager.GMinstance.playerLevel = CurrentLevel;
        _animController = GetComponent<Animator>();
        _navMesh = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        //MOVE via navmesh
        MoveToPoint();

        if (Input.GetMouseButtonDown(1))
        {
            ReciveDamage(10);
        }
        
    }

    #endregion
    
    public void PlaySound() //Play by event animation "kick"
    {
        _audioSource.PlayOneShot(soundsAC[Random.Range(0, 3)], 1);
    }

    public void SetKickState() //Set by event animation "Kick"
    {
        _kick = false;
    }

    private void MoveToPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, whatIsChest))
            {
                if (hit.collider.CompareTag("Chest"))
                {
                    if (hit.collider.GetComponent<ChestController>().RequiredLevel > CurrentLevel)
                    {
                        return;
                    }
                    //ANIMATION - Kick
                    if (Vector3.Distance(hit.point, transform.position) < 1.5f)
                    {
                        _kick = true;
                        transform.LookAt(hit.point);
                        _animController.SetTrigger("Kick");
                        
                        //Open chest
                        if (hit.collider.GetComponent<ChestController>().WhatKindOfObj != TypeOfInteractable.destroy)
                        {
                            hit.collider.GetComponent<Animator>().SetTrigger("SisamOpen");
                        }
                        hit.collider.GetComponent<ChestController>().DoSomething(gameObject);
                    }
                }

                if (!_kick)
                {
                    _navMesh.SetDestination(hit.point);
                }
            }
        }

        //ANIMATION - Stay / run
        _animController.SetBool("IsRunning", (_navMesh.velocity.sqrMagnitude == 0f) ? false : true);
    }
    

    public void ReciveDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, 100);
        //Health = Health < 0 ? Health = 0 : 0;

        if (Health == 0)
        {
            GameManager.GMinstance.GameOverUI();
        }
        RefreshUIHealth();
    }

    private void LevelUp()
    {
        var CurrentExp = Experience - NeedExpToLevel;
        expSlider.value = CurrentExp;
        Experience = CurrentExp;
        NeedExpToLevel = (int) (expSlider.maxValue * 1.2f);
        expSlider.maxValue = NeedExpToLevel;

        CurrentLevel++;
        GameManager.GMinstance.playerLevel = CurrentLevel;
        
        UpdateSliderExp();
        UpdateLevelUI();
    }
    
    #region UI_block

    private void RefreshUIHealth()
    {
        imageProgressBar.fillAmount = (float)Health/_healthAmount;
        textHealth.text = Health.ToString();
    }
    
    public void UpdateSliderExp()
    {
        textInfoExp.text = Experience.ToString() + " /" + NeedExpToLevel;
        expSlider.value = (float)Experience;

        //LEVEL UP
        if (NeedExpToLevel <= Experience)
        {
            LevelUp();
        }
    }

    public void UpdateLevelUI()
    {
        textLevelInfo.text = "LVL:\n" + CurrentLevel.ToString();
    }
    
    #endregion
    
}
