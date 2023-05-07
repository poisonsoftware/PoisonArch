using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoisonArch;
using System;
using MoreMountains.NiceVibrations;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    SoundID m_Sound = SoundID.None;

    const string k_PlayerTag = "Player";

    public int m_Count;

    Renderer[] m_Renderers;

    void Awake()
    {
        m_Renderers = gameObject.GetComponentsInChildren<Renderer>();
    }
    private void Start()
    {
        MyTween.Instance.CollactableRotate(transform, .8f);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(k_PlayerTag))
        {
            Collect();
        }
    }

    void Collect()
    {
        for (int i = 0; i < m_Renderers.Length; i++)
        {
            m_Renderers[i].enabled = false;
        }

        AudioManager.Instance.PlayEffect(m_Sound, EffectSourceID.CoinEffectSource);

        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }
}
