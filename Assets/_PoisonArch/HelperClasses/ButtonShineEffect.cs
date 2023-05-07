using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButtonShineEffect : MonoBehaviour
{
    [SerializeField] Transform shine;
    [SerializeField] float offset = 100f;
    [SerializeField] float speed = 1.5f;
    [SerializeField] float minDelay = 0.3f;
    [SerializeField] float maxDelay =0.5f;

    private void Start()
    {
        MyTween.Instance.ButtonShineEffect(shine, offset, speed, minDelay, maxDelay);
    }
}
