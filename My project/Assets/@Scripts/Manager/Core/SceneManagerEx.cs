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
        // �� �ε� �Ϸ� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // �̺�Ʈ ��� ���� (�޸� ���� ����)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"�� �ε��: {scene.name}, �ε� ���: {mode}");
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
