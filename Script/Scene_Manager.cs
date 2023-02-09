using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    GameManager gameManager;
    public void GameStart() // 게임시작
    {        
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
        Datamanager.instance.nowPlayer.Gameclear = false;
        Datamanager.instance.SaveData();

    }
    
    public void Lobby() // 로비로 돌아가기
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1;
    }

    public void GameExit() // 게임종료
    {
        Application.Quit();

    }


}
