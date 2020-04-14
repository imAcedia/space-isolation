using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogues/CreateNewDialogueSequences", order = 1)]
public class Dialogue : ScriptableObject
{
    public string sequenceName = "Default";
    [TextArea(1, 3)]
    public string[] sentences;
}
