using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EASY()
    {
        SceneManager.LoadScene(2);
    }
    public void NORMAL()
    {
        SceneManager.LoadScene(3);
    }

    public void HARD()
    {
        SceneManager.LoadScene(4);
    }

}
