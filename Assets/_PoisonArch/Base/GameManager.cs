using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PoisonArch
{
    public enum GameState { Menu, Play, Finish, Lose, Pause}

    public class GameManager : AbstractSingleton<GameManager>
    {
        GameState _gameState;
        public GameState GameState { get { return _gameState; } }

        public event Action EventMenu;
        public event Action EventPlay;
        public event Action EventFinish;
        public event Action EventLose;
        public event Action EventPause;

        void Start()
        {
            LevelManager.Instance.EventNextLevel += OnNextLevel;
            SetMenu();
        }
        public void SetMenu()
        {
            _gameState = GameState.Menu;
            EventMenu?.Invoke();
        }

        public void SetPlay()
        {
            _gameState = GameState.Play;
            EventPlay?.Invoke();
        }
        public void SetFinish()
        {
            _gameState = GameState.Finish;
            EventFinish?.Invoke();
        }

        public void SetLose()
        {
            _gameState = GameState.Lose;
            EventLose?.Invoke();
        }
        public void SetPause()
        {
            _gameState = GameState.Pause;
            EventPause?.Invoke();
        }

        public void OnNextLevel()
        {
            SetMenu();
        }
    }

}
