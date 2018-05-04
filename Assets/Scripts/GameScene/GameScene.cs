using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private Arrow arrow;
    public Arrow Arrow { get { return arrow; } }

    [SerializeField]
    private PlayerDataList playerDataList;
    public PlayerDataList PlayerDataList { get { return playerDataList; } }

}
