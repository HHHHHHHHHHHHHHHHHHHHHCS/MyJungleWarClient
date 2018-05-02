using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Model;

public class Game_PlayerManager : PlayerManager
{
    private GameScene gameScene;
    private Transform roleStartPositions;

    public override PlayerManager OnInit()
    {
        gameScene = GameObject.Find(ObjectNames.gameScene).GetComponent<GameScene>();
        roleStartPositions = GameObject.Find(ObjectNames.gamePositions).transform;
        SpawnRole();
        return this;
    }

    private void SpawnRole()
    {
        bool isFirst = true;
        foreach (var item in gameScene.PlayerDataList.playerDataList)
        {
            var role = Object.Instantiate(item.PlayerPrefab
                , GetPosByRoleType(item.RoleType), Quaternion.identity);
            if (isFirst)
            {
                role.AddComponent<PlayerMove>();
                role.AddComponent<PlayerAttack>().OnInit(gameScene.Arrow, item.RoleType, item.Color);
                Camera.main.GetComponent<CameraFollowTarget>().OnInit(role.transform);
                isFirst = false;
            }
        }
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
