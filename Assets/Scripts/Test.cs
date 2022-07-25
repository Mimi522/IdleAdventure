using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptablesObjects/Events/Event Channel")]
public class EventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;
}
