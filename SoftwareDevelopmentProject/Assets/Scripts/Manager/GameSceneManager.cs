using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : SingletonMonobehaviour<GameSceneManager>
{
    public enum Scene
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
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
