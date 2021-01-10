using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialChoice : MonoBehaviour
{
   public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToLevelOne()
    {
        SceneManager.LoadScene("Level 1 Proper");
    }
}
