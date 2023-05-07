using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoisonArch;

[CreateAssetMenu(fileName = nameof(ItemPickedEvent),
        menuName = "PoisonArch/" + nameof(ItemPickedEvent))]
public class ItemPickedEvent : AbstractGameEvent
{
    [HideInInspector]
    public int Count = -1;

    public override void Reset()
    {
        Count = -1;
    }
}

namespace PoisonArch
{
    /// <summary>
    /// Fires an ItemPickedEvent when the player enters the trigger collider attached to this game object.
    /// </summary>
    public class ItemPickupTrigger : MonoBehaviour
    {
        public string m_PlayerTag = "Player";

        public ItemPickedEvent m_Event;

        public int m_Count;

        void OnTriggerEnter(Collider col)
        {
            if (!col.CompareTag(m_PlayerTag))
                return;

            m_Event.Count = m_Count;
            m_Event.Raise();
            gameObject.SetActive(false);
        }
    }
}
