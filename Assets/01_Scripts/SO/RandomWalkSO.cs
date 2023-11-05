using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/RandomWalkData", fileName = "RandomWalkData")]
public class RandomWalkSO : ScriptableObject
{
    public int iteration = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
    public bool fillRoom;
    public bool smoothLine = false; 
}
