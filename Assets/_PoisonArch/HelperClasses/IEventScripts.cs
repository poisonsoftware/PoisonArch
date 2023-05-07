using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventScripts
{
    void OnMenu();
    void OnPlay();
    void OnFinish();
    void OnLose();
    void OnPause();
}
