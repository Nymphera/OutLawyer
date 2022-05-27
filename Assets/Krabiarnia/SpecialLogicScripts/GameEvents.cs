using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }
    public event Action<int,bool> onDoorMouseClick;
    public event Action <int>onOfficeClick;
    public event Action onNegotiationsStarted;
    public event Action <Line>onBurnLines;
    public event Action<Line> onLineCreated;
    public void DoorMouseClick(int id,bool doorState)
    {
        onDoorMouseClick(id,doorState);
    }
    public void OfficeClick(int id)
    {
        onOfficeClick(id);
    }
    public void TriggerNegotiations()
    {
        onNegotiationsStarted();
    }
    public void TriggerBurnLines(Line line)
    {
        onBurnLines?.Invoke(line);
    }
    public void TriggerLineCreated(Line line)
    {
        onLineCreated?.Invoke(line);
    }
    
}
