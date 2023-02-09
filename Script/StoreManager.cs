using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public int coin = 0;
    public bool[] Weapons = new bool[4];
    public int RegenHP = 0;
    public int MaxHP = 0;
    public int ATTUp = 0;
    public int GoldUp = 0; //골드 희득량 증가/
    public int ExpUp = 0;  //경험치 희득량 증가/
    public int PlusAmmo = 0;

    public bool missile=false;

    public int Price = 200;

    public GameObject[] CheckBox;

    public Text Mycoin;

    public int[] UP;
    public void Load()//데이터 불러오기
    {
        coin = Datamanager.instance.nowPlayer.coin;
        Weapons = Datamanager.instance.nowPlayer.Weapons;
        RegenHP = Datamanager.instance.nowPlayer.RegenHP;
        MaxHP = Datamanager.instance.nowPlayer.MaxHP;
        ATTUp = Datamanager.instance.nowPlayer.ATTUp;
        GoldUp = Datamanager.instance.nowPlayer.GoldUp;
        ExpUp = Datamanager.instance.nowPlayer.ExpUp;
        PlusAmmo = Datamanager.instance.nowPlayer.PlusAmmo;
        UP = new int[] { ATTUp, MaxHP, RegenHP, GoldUp, ExpUp, PlusAmmo};
        missile = Datamanager.instance.nowPlayer.missile;
        UpgradeImage();
    }

    public void Save()
    {
        Datamanager.instance.nowPlayer.coin = coin;
        Datamanager.instance.nowPlayer.Weapons = Weapons;
        Datamanager.instance.nowPlayer.RegenHP = RegenHP;
        Datamanager.instance.nowPlayer.MaxHP = MaxHP;
        Datamanager.instance.nowPlayer.ATTUp = ATTUp;
        Datamanager.instance.nowPlayer.GoldUp = GoldUp;
        Datamanager.instance.nowPlayer.ExpUp = ExpUp;
        Datamanager.instance.nowPlayer.PlusAmmo = PlusAmmo;
        Datamanager.instance.nowPlayer.missile = missile;

        Datamanager.instance.SaveData();
        
    }

    public void BuyGun(int num)
    {
        if (num == 1 && coin >= 1000&& !Weapons[num])
        {
            Weapons[num] = true;
            coin-=1000;

            GameObject check = CheckBox[6].transform.GetChild(2).gameObject;
            check.transform.GetChild(0).gameObject.SetActive(true);
            Save();
        }
        else if (num == 2 && coin >= 2000 && !Weapons[num])
        {
            Weapons[num] = true;
            coin -= 2000;
            GameObject check = CheckBox[7].transform.GetChild(2).gameObject;
            check.transform.GetChild(0).gameObject.SetActive(true);
            Save();
        }
        else if (num == 3 && coin >= 3000 && !Weapons[num])
        {
            Weapons[num] = true;
            coin -= 3000;
            GameObject check = CheckBox[8].transform.GetChild(2).gameObject;
            check.transform.GetChild(0).gameObject.SetActive(true);
            Save();
        }
        else if (num == 4 && coin >= 10000 && !missile)
        {
            missile = true;
            coin -= 10000;
            GameObject check = CheckBox[9].transform.GetChild(2).gameObject;
            check.transform.GetChild(0).gameObject.SetActive(true);
            Save();
        }
    }
   

    public void Upgrade(int num)
    {
        #region 기본능력강화
        if (num == 0)
        {
            if (ATTUp < 5)
            {
                if (coin >= Price + Price * ATTUp)
                {
                    coin -= Price + Price * ATTUp;
                    ATTUp++;
                    BoxcheckUpdate(num, ATTUp);
                    Save();
                }
                else { print("잔액부족"); }
            }
        }
        else if (num == 1)
        {
            if (MaxHP < 5)
            {
                if (coin >= Price + Price * MaxHP)
                {
                    coin -= Price + Price * MaxHP;
                    MaxHP++;
                    BoxcheckUpdate(num, MaxHP);
                    Save();
                }
                else { print("잔액부족"); }
            }
        }
        else if (num == 2)
        {
            if (RegenHP < 5)
            {
                if (coin >= Price + Price * RegenHP)
                {
                    coin -= Price + Price * RegenHP;
                    RegenHP++;
                    BoxcheckUpdate(num, RegenHP);
                    Save();
                }
                else { print("잔액부족"); }
            }
        }
        else if (num == 3)
        {
            if (GoldUp < 5)
            {
                if (coin >= Price + Price * GoldUp)
                {
                    coin -= Price + Price * GoldUp;
                    GoldUp++;
                    BoxcheckUpdate(num, GoldUp);
                    Save();
                }
                else { print("잔액부족"); }
            }
        }
        else if (num == 4)
        {
            if (ExpUp < 5)
            {
                if (coin >= Price + Price * ExpUp)
                {
                    coin -= Price + Price * ExpUp;
                    ExpUp++;
                    BoxcheckUpdate(num, ExpUp);
                    Save();
                }
                else { print("잔액부족"); }
            }
        }
        else if (num == 5)
        {
            if (PlusAmmo < 5)
            {
                if (coin >= Price + Price * PlusAmmo)
                {
                    coin -= Price + Price * PlusAmmo;
                    PlusAmmo++;
                    BoxcheckUpdate(num, PlusAmmo);
                    Save();
                }
                else { print("잔액부족"); }
            }
        }
        #endregion
    }

    private void BoxcheckUpdate(int num, int UPtaget)
    {
        GameObject check = CheckBox[num].transform.GetChild(2).gameObject;
        for (int i = 0; i < UPtaget; i++)
        {
            check.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (UPtaget>=5)
        {
            CheckBox[num].transform.GetChild(3).gameObject.GetComponent<Text>().text = "Max";
        }
        else
        {
            CheckBox[num].transform.GetChild(3).gameObject.GetComponent<Text>().text = (Price + (UPtaget * Price)).ToString();
        }
        Save();
    }
    private void RefundUpdate(int num)
    {
        GameObject check = CheckBox[num].transform.GetChild(2).gameObject;
        for (int i = 0; i < check.transform.childCount; i++)
        {
            check.transform.GetChild(i).gameObject.SetActive(false);
        }
        CheckBox[num].transform.GetChild(3).gameObject.GetComponent<Text>().text = Price.ToString();
    }
    public int RefunPrice(int product)
    {
        if (product <= 0)
        {
            return product; 
        }
        Debug.Log(product);
            return product + RefunPrice(product - 1);
        
    }
    public void Refund()
    {
        coin += (RefunPrice(RegenHP) + RefunPrice(MaxHP) + RefunPrice(ATTUp) + RefunPrice(GoldUp) + RefunPrice(ExpUp) + RefunPrice(PlusAmmo))*Price;

        RegenHP = 0;
        MaxHP = 0;
        ATTUp = 0;
        GoldUp = 0;
        ExpUp = 0; 
        PlusAmmo = 0;
        for (int i = 0; i < 6; i++)
        {
            RefundUpdate(i);
        }
        for (int i = 1; i < Weapons.Length; i++)
        {
            if (Weapons[i])
            {
                coin += i * 1000;
                GameObject check = CheckBox[i + 5].transform.GetChild(2).gameObject;
                check.transform.GetChild(0).gameObject.SetActive(false);
            }
            
            Weapons[i] = false;
        }
        if (missile)
        {
            missile = false;
            coin += 10000;
            GameObject check = CheckBox[9].transform.GetChild(2).gameObject;
            check.transform.GetChild(0).gameObject.SetActive(false);
        }
        Save();
    }

    private void UpgradeImage()
    {
        for (int i = 0; i < 6; i++)
        {
            BoxcheckUpdate(i, UP[i]);
        }

        //총  체크
        for (int i = 1; i < 4; i++)
        {
            if (Weapons[i])
            {
                GameObject check = CheckBox[i+5].transform.GetChild(2).gameObject;
                check.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (missile)
        {
            GameObject check = CheckBox[9].transform.GetChild(2).gameObject;
            check.transform.GetChild(0).gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        Mycoin.text = coin.ToString() ;
        Debug.Log(coin);
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    
}
