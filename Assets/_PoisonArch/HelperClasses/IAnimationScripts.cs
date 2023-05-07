using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAnimationScripts
{
    void SetTriggerAnimation(Enum _enum);

    void SetBoolAnimation(Enum _enum, bool _state);
}
