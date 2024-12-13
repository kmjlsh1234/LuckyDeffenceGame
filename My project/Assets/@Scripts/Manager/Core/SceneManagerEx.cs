using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : SingletonBase<SceneManagerEx>
{
    public SceneName _currentSceneName;
    public SceneBase _currentScene;

    public void Init()
    {
        _currentSceneName = SceneName.SplashScene;
        
    }
    void OnEnable()
    {
        // 씬 로드 완료 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 이벤트 등록 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"씬 로드됨: {scene.name}, 로드 모드: {mode}");
        _currentSceneName = (SceneName)System.Enum.Parse(typeof(SceneName), scene.name);
        UIManager.Instance.Clear();
        switch (_currentSceneName)
        {
            case SceneName.SplashScene:
                _currentScene = GameObject.Find("@SplashScene")?.GetComponent<SceneBase>();
                if (_currentScene == null)
                {
                    GameObject go = new GameObject() { name = $"@{SceneName.SplashScene.ToString()}" };
                    _currentScene = go.AddComponent<SplashScene>();

                }
                _currentScene.Init();
                break;
            case SceneName.MainScene:
                _currentScene = GameObject.Find("@MainScene")?.GetComponent<SceneBase>();
                if(_currentScene == null)
                {
                    GameObject go = new GameObject() { name = $"@{SceneName.MainScene.ToString()}" };
                    _currentScene = go.AddComponent<MainScene>();
                    
                }
                _currentScene.Init();
                break;
            case SceneName.GameScene:
                _currentScene = GameObject.Find("@GameScene")?.GetComponent<SceneBase>();
                if (_currentScene == null)
                {
                    GameObject go = new GameObject() { name = $"@{SceneName.GameScene.ToString()}" };
                    _currentScene = go.AddComponent<GameScene>();

                }
                _currentScene.Init();
                break;
        }    
    }

}
