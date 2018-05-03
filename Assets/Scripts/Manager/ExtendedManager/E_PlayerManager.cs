using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class E_PlayerManager
{
    public static void SetCurrentRole(this GameFacade facade
    , RoleType roleType)
    {
        Game_PlayerManager.SetCurrentRoleType(roleType);
    }
}
