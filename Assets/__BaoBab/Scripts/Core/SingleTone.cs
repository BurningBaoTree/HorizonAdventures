using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

/// <summary>
/// MonoBehaviour를 상속받는 Singleton<T>.
/// </summary>
/// <typeparam name="T"> T는 Component를 포함하는 Type</typeparam>
public class SingleTone<T> : MonoBehaviour where T : Component
{
    private static bool isShutDonw = false;
    private static bool initialized = false;
    static T instance;
    public static T Inst
    {
        get
        {
            if (isShutDonw)
            {
                Debug.LogWarning($"{typeof(T).Name}현재 프로그램이 종료되어 싱글톤에 접근할수 없사옵니다.");
                return null;
            }
            if (instance == null)
            {
                T singleton = FindObjectOfType<T>();
                if (singleton == null)
                {
                    GameObject gameobj = new GameObject();
                    gameobj.name = $"{typeof(T).Name} Singleton";
                    singleton = gameobj.AddComponent<T>();
                }
                instance = singleton;
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(!initialized)
        {
            initialized = true;
            OnPreInitialize();
        }
        if(arg1 != LoadSceneMode.Additive)
        {
            OnInitialize();
        }
    }

    private void OnPreInitialize()
    {

    }
    private void OnInitialize()
    {

    }

    void OnApplicationQuit()
    {
        isShutDonw = true;
    }
}

