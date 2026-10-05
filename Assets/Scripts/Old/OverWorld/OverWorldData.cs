using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OverworldData", menuName = "Scriptable Objects/OverworldData")]
public class OverWorldData : ScriptableObject
{
    Dictionary<Vector3, RoomData> roomData = new Dictionary<Vector3, RoomData>();
}

[Serializable]
public class RoomData
{
    public List<Enemy> enemies = new List<Enemy>();

    public List<GameObject> chests = new List<GameObject>();
    public List<GameObject> Chests => chests;

    public bool isDiscovered = false;
}
