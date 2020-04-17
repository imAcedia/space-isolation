﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayData", menuName = "DayData")]
public class DayData : ScriptableObject
{
    public bool oxGenBroken = false;
    public bool navBroken = false;
    public bool materializerBroken = false;
}
