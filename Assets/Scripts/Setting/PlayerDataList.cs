using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataList : ScriptableObject
{
    public List<PlayerData> playerDataList;
}

[System.Serializable]
public class PlayerData 
{
    [SerializeField]
    private RoleType roleType;
    public RoleType RoleType
    {
        get
        {
            return roleType;
        }
    }

    [SerializeField]
    private GameObject playerPrefab;
    public GameObject PlayerPrefab
    {
        get
        {
            return playerPrefab;
        }
    }

    [SerializeField]
    private Color color;
    public Color Color
    {
        get
        {
            return color;
        }
    }
}
