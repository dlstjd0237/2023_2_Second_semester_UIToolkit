using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/StatData")]
public class StatSO : ScriptableObject
{
    public string charName;
    public int dex;
    public int str;
    public int hp;
    public int wis;
}
