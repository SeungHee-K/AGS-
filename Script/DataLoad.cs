using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class DataLoad : MonoBehaviour
{

    bool savefile =false;
    public bool NewGameBool = false;

    public GameObject WarringdeletPanel;

    public StoreManager storeManager;

    void Start()
    {
            if (File.Exists(Datamanager.instance.path + $"0"))	// save0 데이터가 있는 경우
            {
                savefile = true;			// 해당 슬롯 번호의 bool배열 true로 변환
                Datamanager.instance.nowSlot = 0;	// 슬롯 번호 저장
                Datamanager.instance.LoadData();	// 해당 슬롯 데이터 불러옴
            }
            else	// 데이터가 없는 경우
            {
            Debug.Log("데이터 없음");

            }
        Datamanager.instance.DataClear();
        start();
    }

    public void start()	
    {
        Datamanager.instance.nowSlot = 0;
        if (savefile)	// bool 배열에서 현재 true라면 = 데이터 존재한다는 뜻
        {
            Datamanager.instance.LoadData();	// 데이터를 로드하고
            GoGame();	
        }
        else	// bool 배열에서 현재 false라면 데이터가 없다는 뜻
        {
            GoGame();
        }
    }

    public void DeleteDataPanel()
    {
        Datamanager.instance.nowSlot = 0;	

        if (savefile)	// bool 배열에서 현재 true라면 = 데이터 존재한다는 뜻
        {
            Datamanager.instance.deleteReady = true;
            Datamanager.instance.deletenum = 0;
            WarringdeletPanel.SetActive(true);
        }
        else
        {
            Debug.Log("삭제할 데이터가 없습니다.");
        }
    }

    public void DeleteSaveFile()// 데이터 삭제
    {
        Datamanager.instance.DeleteData();
        savefile = false;
        Datamanager.instance.deletenum = -1;
        start();
    }

    public void GoGame()	// 게임씬으로 이동
    {
        if (!savefile)
        {
            Datamanager.instance.SaveData(); // 현재 정보를 저장함.
        }
        if (SceneManager.GetActiveScene().name=="Lobby")
        {
            storeManager.Load();
        }
        
       // SceneManager.LoadScene(Datamanager.instance.nowPlayer.); // 게임씬으로 이동
    }
}
