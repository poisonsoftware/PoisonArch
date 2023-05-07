using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PoisonArch
{
    /// <summary>
    /// comment statements for version without scene loading
    /// </summary>
    public class LevelManager : AbstractSingleton<LevelManager>
    {
        [SerializeField] List<GameObject> _levels;

        [SerializeField] int _currentLevel = 0;
        int _maxLevel;

        public List<GameObject> levels { get { return _levels; } }
        public int CurrentLevel { get { return _currentLevel; } }
        public int MaxLevel { get { return _maxLevel; } }

        public event Action EventNextLevel;
        void Start()
        {
            // _levels = GetTopLevelChildren(transform);
            _maxLevel = _levels.Count;
            _currentLevel = PlayerPrefs.GetInt("level", 0);
            for (int i = 0; i < levels.Count; i++)
            {
                _levels[i].gameObject.SetActive(false);
            }
            _levels[_currentLevel % _maxLevel].gameObject.SetActive(true);
        }
        public void RestartLevel()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void NextLevel() // for button
        {
            // _levels[_currentLevel % _maxLevel].gameObject.SetActive(false);
            // _levels[++_currentLevel % _maxLevel].gameObject.SetActive(true);
            _currentLevel += 1;
            PlayerPrefs.SetInt("level", _currentLevel);
            RestartLevel();
            // EventNextLevel?.Invoke();
        }

        public List<Transform> GetTopLevelChildren(Transform Parent)
        {
            List<Transform> Children = new List<Transform>(Parent.childCount);
            for (int i = 0; i < Parent.childCount; i++)
            {
                Children[i] = Parent.GetChild(i);
            }
            return Children;
        }
        public GameObject GetCurrentLevel()
        {
            return _levels[_currentLevel % _maxLevel];
        }

        [ContextMenu("ResetPrefs")]
        void ResetPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
