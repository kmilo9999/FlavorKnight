using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuCursor;
    public GameObject mainMenu;
    public GameObject credits;
    public Vector3[] menuPositions;

    private bool creditsUp = false;

    private int currentPosition;

    public string levelOne;
    

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
        menuCursor.SetActive(true);
        mainMenu.SetActive(true);
        currentPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (creditsUp && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))) {
            credits.SetActive(false);
            menuCursor.SetActive(true);
            mainMenu.SetActive(true);
            creditsUp = false;
        } else {

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
                        SceneManager.LoadScene(levelOne, LoadSceneMode.Single);
                        break;
                    case 1:
                        credits.SetActive(true);
                        menuCursor.SetActive(false);
                        mainMenu.SetActive(false);
                        creditsUp = true;
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
}
