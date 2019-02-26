using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

    /*New Story Scene should be set in Unity as scene 2 - Load Story is scene 3*/

    public void goToNewStory() {
        SceneManager.LoadScene(2);
    }

    public void goToLoadStory() {
        SceneManager.LoadScene(3);
    }
}
