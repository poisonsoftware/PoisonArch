using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoisonArch
{
    public class CameraManager : AbstractSingleton<CameraManager>, IEventScripts
    {
        public static CameraManager Instance;
        [SerializeField] List<Transform> _cameras;

        [SerializeField] Transform _menuCam;

        public Transform _playCam;
        public Transform _finalCam;

        public Transform MainUsedCamera;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        void Start()
        {
            GameManager.Instance.EventMenu += OnMenu;
            GameManager.Instance.EventPlay += OnPlay;
            GameManager.Instance.EventFinish += OnFinish;
            GameManager.Instance.EventLose += OnLose;
            GameManager.Instance.EventPause += OnLose;

            //getCamera
            for (int i = 0; i < transform.childCount; i++)
            {
                _cameras.Add(transform.GetChild(i));
            }
        }

        public void SetActiveCamera(Transform activeCamera)
        {
            int _priority = _cameras.Count;
            for (int i = 0; i < _cameras.Count; i++)
            {
                if (_cameras[i] == transform)
                    continue;

                if (_cameras[i] == activeCamera)
                {
                    _cameras[i].GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = _cameras.Count;
                    _priority--;
                }
                else
                {
                    _priority--;
                    _cameras[i].GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Priority = _priority;
                }

                MainUsedCamera = activeCamera.GetComponent<Transform>();
            }
        }

        public void OnMenu()
        {
            SetActiveCamera(_menuCam);

            //_finalCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = AfterFinal.Instance.LikeButton;
            //_finalCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().LookAt = AfterFinal.Instance.LikeButton;
        }

        public void OnPlay()
        {
            SetActiveCamera(_playCam);
        }

        public void OnFinish()
        {
            SetActiveCamera(_finalCam);
        }
        public void OnLose()
        {
            SetActiveCamera(_finalCam);
        }
        public void OnPause()
        {
            SetActiveCamera(_playCam);
        }
    }
}
