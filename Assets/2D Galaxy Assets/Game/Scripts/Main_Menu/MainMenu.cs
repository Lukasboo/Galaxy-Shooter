using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void loadSinglePlayerGame() {
        Debug.Log("carregando single player");
        SceneManager.LoadScene("Single_Player");

    }

    public void loadCoOpGame() {
        Debug.Log("carregando co op");
        SceneManager.LoadScene("Co-Op_Mode");
    }

}
