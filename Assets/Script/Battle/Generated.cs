using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generated : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public static Unit playerUnit, enemyUnit;
    public static Generated instance;
    public Transform playerBattleStation;
    public Transform enemyBattleStation;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);//Œª÷√
        playerUnit = playerGo.GetComponent<Unit>();
        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);//Œª÷√
        enemyUnit = enemyGo.GetComponent<Unit>();
    }

}
