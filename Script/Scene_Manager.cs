using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    GameManager gameManager;
    public void GameStart() // ���ӽ���
    {        
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
        Datamanager.instance.nowPlayer.Gameclear = false;
        Datamanager.instance.SaveData();

    }
    
    public void Lobby() // �κ�� ���ư���
    {
        SceneManager.LoadScene("Lobby");
        Time.timeScale = 1;
    }

    public void GameExit() // ��������
    {
        Application.Quit();

    }


}
