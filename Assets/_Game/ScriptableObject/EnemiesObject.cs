using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesData", menuName = "Config data / Enemies")]
public class EnemiesObject : ScriptableObject
{
    public Definition.TypeMonster type;
    public int level;
    public int hp;
    public int maxCapacity;
    public int numberOfSpawn;
    public int spawnCountDown;
    public int startTime;
    public int durationTime;
    public int expDrop;
    public float baseSeep;
    public float maxSpeed;
    public float acceleration;
}
