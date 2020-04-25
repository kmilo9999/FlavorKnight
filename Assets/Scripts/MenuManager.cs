using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCursor;
    public Vector3[] menuPositions;

    private int currentPosition;
    

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentPosition < 0)
        {
            currentPosition = 0;
        }
        else if (currentPosition >= menuPositions.Length)
        {
            currentPosition = menuPositions.Length - 1;
        }

        menuCursor.transform.position = menuPositions[currentPosition];
        if (Input.GetKeyDown(KeyCode.UpArrow ) ||
            Input.GetKeyDown(KeyCode.W) )
        {
            currentPosition--;
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.S))
        {
            currentPosition++;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) )
        {
            switch (currentPosition )
            {
                case 0:
                    SceneManager.LoadScene("BrynnScene 1", LoadSceneMode.Single);
                    break;
                case 1:
                    break;
                case 2:
                    Application.Quit();
                    break;
                default:
                    break;
            }
            
        }
    }
}
