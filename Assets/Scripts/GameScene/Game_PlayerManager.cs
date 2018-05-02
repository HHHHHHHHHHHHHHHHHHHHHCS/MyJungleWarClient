using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Model;

public class Game_PlayerManager : PlayerManager
{
    private GameScene gameScene;

    public override PlayerManager OnInit()
    {
        gameScene = GameObject.Find(ObjectNames.gameScene).GetComponent<GameScene>();
        SpawnRole();
        return this;
    }

    private void SpawnRole()
    {
        bool isFirst = true;
        foreach (var item in gameScene.PlayerDataList.playerDataList)
        {
            var role = Object.Instantiate(item.PlayerPrefab, Vector3.zero, Quaternion.identity);
            if (isFirst)
            {
                role.AddComponent<PlayerMove>();
                role.AddComponent<PlayerAttack>().OnInit(gameScene.Arrow, item.RoleType, item.Color);
                Camera.main.GetComponent<CameraFollowTarget>().OnInit(role.transform);
                isFirst = false;
            }
        }
    }
}
