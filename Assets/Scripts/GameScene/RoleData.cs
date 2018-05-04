using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData : MonoBehaviour
{
    public PlayerInfo PlayerInfo { get; private set; }

    public PlayerMove PlayerMove { get; set; }
    public PlayerAttack PlayerAttack { get; set; }

    public RoleData OnInit(PlayerData _playerData)
    {
        PlayerInfo = new PlayerInfo().OnInit(_playerData);

        return this;
    }


    public RoleData SetCurrentPlayer(Arrow _arrow, PlayerData _PlayerData)
    {
        PlayerMove = gameObject.AddComponent<PlayerMove>();
        PlayerAttack = gameObject.AddComponent<PlayerAttack>().OnInit(_arrow, _PlayerData);
        Camera.main.GetComponent<CameraFollowTarget>().OnInit(transform);

        return this;
    }
}
