using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour {
    [SerializeField]
    int currentScene;

    public void LoadNextLevel() {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadLastLevel() {
        SceneManager.LoadScene(currentScene - 1);
    }
}
