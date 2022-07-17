using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneController : Singleton<SceneController>
{
    public GameObject playerPrefab;
    private GameObject player;
    private NavMeshAgent playerAgent;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    IEnumerator Transition(string sceneName)
    {
        //TODO:±£´æÊý¾Ý
        if (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield break;
        }
        else
        {
            player = GameManager.Ins.playerStats.gameObject;
            playerAgent = player.GetComponent<NavMeshAgent>();
            yield return null;
        }
    }

    public void TransitionToBattle()
    {
        StartCoroutine(Transition("Battle"));
    }

    public void TransitionToMain()
    {
        StartCoroutine(LoadMain());
    }

    public void TransitionToFirstLevel()
    {
        StartCoroutine(LoadLevel("Level 0"));
    }

    public void TransitionToLoadLevel()
    {
        StartCoroutine(LoadLevel(SaveManager.Ins.SceneName));
    }

    IEnumerator LoadLevel(string scene)
    {
        if (scene != "")
        {
            yield return SceneManager.LoadSceneAsync(scene);
            yield return player = Instantiate(playerPrefab,Vector3.zero,Quaternion.identity);

            SaveManager.Ins.SavePlayerData();
            yield break;
        }

    }

    IEnumerator LoadMain()
    {
        yield return SceneManager.LoadSceneAsync("Main");
        yield break;
    }
}