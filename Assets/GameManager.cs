using System.Collections;
using Audio.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void StartLoadScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    
    private IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            yield return null;
        }
        op.allowSceneActivation = true;
        while (!op.isDone)
        {
            Debug.Log($"Loading: {op.progress:P0}");
            yield return null;
        }
        Music.OnMusicAudioEvent?.Invoke("Base");
        //display instructions
    }
}
