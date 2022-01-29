using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideConfig : MonoBehaviour
{
    
}

[System.Serializable]
public class GuideProperty
{
    [SerializeField] private int _level;
    [SerializeField] private string _guideText;
}