using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Model;
using Common.Code;

public class Game_PlayerManager : PlayerManager
{
    private GameScene gameScene;
    private Transform roleStartPositions;
    private Dictionary<RoleType, RoleData> playerRoleDataArray;

    private static RoleType currentRoleType;

    public override PlayerManager OnInit()
    {
        gameScene = GameObject.Find(ObjectNames.gameScene).GetComponent<GameScene>();
        roleStartPositions = GameObject.Find(ObjectNames.gamePositions).transform;
        playerRoleDataArray = new Dictionary<RoleType, RoleData>();
        SpawnRole();

        //通知服务器加载完成了
        GameFacade.Instance.SendRequest(RequestCode.Battle,ActionCode.Battle_Enter, "");
        return this;
    }

    private void SpawnRole()
    {
        string currentName = currentRoleType.ToString().ToUpper();
        foreach (var playerData in gameScene.PlayerDataList.playerDataList)
        {
            var role = Object.Instantiate(playerData.PlayerPrefab
                , GetPosByRoleType(playerData.RoleType), Quaternion.identity);
            var roleData = role.AddComponent<RoleData>().OnInit(playerData);
            playerRoleDataArray.Add(playerData.RoleType, roleData);
        }
    }

    public static void SetCurrentRoleType(RoleType _type)
    {
        currentRoleType = _type;
    }

    public void SetCurrentRoleData()
    {
        Debug.Log(1);
        foreach (var playerData in gameScene.PlayerDataList.playerDataList)
        {
            Debug.Log(2);
            if (playerData.RoleType == currentRoleType)
            {
                Debug.Log(3);
                GetRoleData(currentRoleType).SetCurrentPlayer(gameScene.Arrow, playerData);
            }
        }
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
