using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Model;
using Common.Code;

public class Game_PlayerManager : PlayerManager
{
    private GameScene gameScene;
    private Transform roleStartPositions;
    private RoleType currentRoleType;
    private Dictionary<RoleType, RoleData> playerRoleDataArray;

    private GameObject currentRole;
    private static RoleType roleType;

    public override PlayerManager OnInit()
    {
        gameScene = GameObject.Find(ObjectNames.gameScene).GetComponent<GameScene>();
        roleStartPositions = GameObject.Find(ObjectNames.gamePositions).transform;
        playerRoleDataArray = new Dictionary<RoleType, RoleData>();
        SpawnRole();

        //通知服务器加载完成了
        GameFacade.Instance.SendRequest(ActionCode.Game_Enter, "");
        return this;
    }

    private void SpawnRole()
    {
        string currentName = roleType.ToString().ToUpper();
        foreach (var playerData in gameScene.PlayerDataList.playerDataList)
        {
            var role = Object.Instantiate(playerData.PlayerPrefab
                , GetPosByRoleType(playerData.RoleType), Quaternion.identity);
            var roleData = role.AddComponent<RoleData>().OnInit(playerData);
            if (role.name.IndexOf(currentName) > 0)
            {
                roleData.SetCurrentPlayer(gameScene.Arrow, playerData);
            }

            playerRoleDataArray.Add(playerData.RoleType, roleData);
        }
    }

    public static void SetCurrentRoleType(RoleType _type)
    {
        roleType = _type;
    }

    public RoleData GetRoleData(RoleType _roleType)
    {
        RoleData rd = null;
        playerRoleDataArray.TryGetValue(_roleType, out rd);
        return rd;
    }

    private Vector3 GetPosByRoleType(RoleType _type)
    {
        foreach (Transform item in roleStartPositions)
        {
            if (item.name.IndexOf(_type.ToString()) >= 0)
            {
                return item.position;
            }
        }
        return Vector3.zero;
    }
}
