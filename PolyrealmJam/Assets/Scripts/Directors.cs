using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "DirectorsData", menuName = "Directors/CreateNewDirectorSequences", order = 1)]
public class Directors : ScriptableObject
{
    public string directorName;
    public List<PlayableDirector> directors;
}
