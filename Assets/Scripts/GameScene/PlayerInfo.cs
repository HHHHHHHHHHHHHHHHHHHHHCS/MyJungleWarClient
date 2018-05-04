using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo 
{
    public RoleType roleType;

    public PlayerInfo OnInit(PlayerData _playerData)
    {
        roleType = _playerData.RoleType;

        return this;
    }
}
