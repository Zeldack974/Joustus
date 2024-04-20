using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public static LevelLoader instance;

    void Awake()
    {
        instance = this;
    }

    public void LoadLevel(int levelIndex)
    {
        StartCoroutine(LoadLevelCouroutine(levelIndex));
    }

    private IEnumerator LoadLevelCouroutine(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
