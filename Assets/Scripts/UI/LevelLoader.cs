#pragma warning disable 649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float timeForTransition = 1;
    [SerializeField] private Animator transition;

    // Start is called before the first frame update

    public void startLoder(int sceneNum)
    {
        StartCoroutine(LoadLevel(sceneNum));

    }

    IEnumerator LoadLevel(int sceneNum)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(timeForTransition);
        SceneManager.LoadScene(sceneNum);
    }
}