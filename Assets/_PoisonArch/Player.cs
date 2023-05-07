using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PoisonArch;
public enum PlayerState { Idle, Run, Win }
public class Player : MonoBehaviour, IAnimationScripts, IEventScripts
{
    public static Player Instance => s_Instance;
    static Player s_Instance;

    public PlayerState _playerState;

    Animator _animator;

    void Awake()
    {
        SetupInstance();
    }

    void OnEnable()
    {
        SetupInstance();
    }

    void SetupInstance()
    {
        if (s_Instance != null && s_Instance != this)
        {
            if (Application.isPlaying)
            {
                Destroy(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            return;
        }

        s_Instance = this;
    }
    void Start()
    {
        _animator = GetComponent<Animator>();

        GameManager.Instance.EventMenu += OnMenu;
        GameManager.Instance.EventPlay += OnPlay;
        GameManager.Instance.EventFinish += OnFinish;
        GameManager.Instance.EventPause += OnPause;
    }
    public void OnMenu()
    {
        SetTriggerAnimation(PlayerState.Idle);
        transform.position = Vector3.zero;
        Debug.Log("player menu event");
    }

    public void OnPlay()
    {
        SetTriggerAnimation(PlayerState.Run);
    }

    public void OnFinish()
    {
        SetTriggerAnimation(PlayerState.Win);
    }

    public void OnLose()
    {

    }
    public void OnPause()
    {

    }
    public void SetTriggerAnimation(Enum _enum)
    {
        //situation
        _playerState = (PlayerState)_enum;

        _animator.SetTrigger(Enum.GetName(typeof(PlayerState), _enum));
    }

    public void SetBoolAnimation(Enum _enum, bool _state)
    {
        //situation
        _playerState = (PlayerState)_enum;

        _animator.SetBool(Enum.GetName(typeof(PlayerState), _enum), _state);
    }
}
