using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace PoisonArch
{
    public class UIManager : AbstractSingleton<UIManager>, IEventScripts
    {
        [SerializeField] Transform _menuPanel;
        [SerializeField] Transform _mainPanel;
        [SerializeField] Transform _winPanel;
        [SerializeField] Transform _losePanel;

        [SerializeField] Transform _playButton;
        [SerializeField] TMP_Text _levelText;

        public Transform MenuPanel { get { return _menuPanel; } }
        public Transform MainPanel { get { return _mainPanel; } }
        public Transform WinPanel { get { return _winPanel; } }
        public Transform LosePanel { get { return _losePanel; } }
        public Transform PlayButton { get { return _playButton; } }

        [SerializeField]
        SoundID m_Sound = SoundID.None;
        void Start()
        {
            GameManager.Instance.EventMenu += OnMenu;
            GameManager.Instance.EventPlay += OnPlay;
            GameManager.Instance.EventFinish += OnFinish;
            GameManager.Instance.EventLose += OnLose;
            GameManager.Instance.EventPause += OnPause;
        }
        public void OnMenu()
        {
            MenuPanel.gameObject.SetActive(true);
            WinPanel.gameObject.SetActive(false);
            LosePanel.gameObject.SetActive(false);
            _levelText.text = "Level " + (LevelManager.Instance.CurrentLevel + 1).ToString();

            Debug.Log("uı menu event - menu panel açılacak");

            AudioManager.Instance.PlayMusicForStart(m_Sound, MusicSourceID.MenuMusicSource);
        }

        public void OnPlay()
        {
            MenuPanel.gameObject.SetActive(false);
            MainPanel.gameObject.SetActive(true);
            _levelText.text = "Level " + (LevelManager.Instance.CurrentLevel + 1).ToString();

            

            Debug.Log("uı menu event - main panel açılacak");
        }

        public void OnFinish()
        {
            Invoke("Finish", 4f);

            Debug.Log("uı menu event - win fail paneller açılacak");
        }
        public void OnLose()
        {
            Invoke("Lose", 4f);
        }
        public void OnPause()
        {

        }
        public void Lose()
        {
            MainPanel.gameObject.SetActive(false);
            OpenPanelSmoothly(LosePanel);
        }
        public void Finish()
        {
            MainPanel.gameObject.SetActive(false);
            OpenPanelSmoothly(WinPanel);
        }
        private void OpenPanelSmoothly(Transform panel)
        {
            panel.gameObject.SetActive(true);
            panel.GetChild(0).GetComponent<Image>().DOFade(0.7f, 1.5f).From(0f);
            panel.GetChild(1).DOScale(1, 1.5f).SetEase(Ease.OutBounce).From(0f);
            panel.GetChild(2).DOScale(1, 1.5f).SetEase(Ease.OutBounce).From(0f);
            panel.GetChild(3).DOScale(1, 1.5f).SetEase(Ease.OutBounce).From(0f);
        }
    }
}

