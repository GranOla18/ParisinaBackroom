using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)] 
    public string[] sentences; 
    //public DialoguePoses[] sentences;

    public void SetName(string newName)
    {
        name = newName;
    }

    public string GetName()
    {
        return name;
    }
}

public class DialoguePoses
{
    [TextArea(3, 10)]
    public string sentence;

    public Animation pose;
}
