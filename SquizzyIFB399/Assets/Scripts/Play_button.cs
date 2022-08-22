using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
code taken from a youtube tutorial by GameDevTraum. https://www.youtube.com/watch?v=05OfmBIf5os
*/
public class Play_button : MonoBehaviour
{
    public void LoadScene (string scene){
        SceneManager.LoadScene(scene); // checks if there is a scene with the same name as
    }
}
