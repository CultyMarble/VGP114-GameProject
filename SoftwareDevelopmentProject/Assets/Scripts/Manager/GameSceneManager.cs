using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : SingletonMonobehaviour<GameSceneManager>
{
    public enum EnumScene
    {
        GameScene,
        MainMenuScene,
    }

    //===========================================================================
    protected override void Awake()
    {
        Singleton();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //===========================================================================
    public static void Load(EnumScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
