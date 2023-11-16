using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    public int dungeonCount;

    readonly string DungeonBaseName = "Dungeon_";

    private void Start()
    {
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        int count = 65;
        AsyncOperation[] asyncs = new AsyncOperation[dungeonCount];

        for (int i = 0; i < dungeonCount; i++)
        {
            asyncs[i] = SceneManager.LoadSceneAsync($"{DungeonBaseName}{(char)(count + i)}", LoadSceneMode.Additive);

            asyncs[i].allowSceneActivation = false;

            while (asyncs[i].progress < 0.9f)
            {
                yield return null;
            }
        }

        foreach(AsyncOperation async in asyncs)
        {
            async.allowSceneActivation = true;
        }
    }
}
