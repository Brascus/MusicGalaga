using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void restartlevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public static void menu()
    {

        SceneManager.LoadScene(0);

    }

}
