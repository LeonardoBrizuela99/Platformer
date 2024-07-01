using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicManager : MonoBehaviour
{
    public AudioClip sceneMusic;

    private void Start()
    {
        if (AudioManager.Instance != null && sceneMusic != null)
        {
            AudioManager.Instance.PlayMusic(sceneMusic);
        }
    }
}