using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

[Serializable]
public struct SaveData
{
    public int herbs;
    public int honey;
    public int money;
    public bool sword;
    public bool armor;
}

public class Belongings : MonoBehaviour
{
    const string SAVE_FILE_PATH = "save.txt";

    public static int NumberOfHerbs = 0;
    public static int NumberOfHoney = 0;
    public static int Money = 1000;
    public static bool Sword = false;
    public static bool Armor = false;

    GameObject SE;
    SE_Control SE_Control;
    float Volume;

    Status Status;
    int HP;
    int MP;
    int CurrentHP;
    int CurrentMP;

    GameObject MessageText;
    BattleText BattleText;
    bool EnemyDefeat = false;
    int coin;
    bool GetCoin = false;

    public Button SwordButton;
    public Button ArmorButton;

    public AudioClip sound;
    public AudioClip FailureSound;
    AudioSource audioSource;

    [SerializeField] Text HerbsText;
    [SerializeField] Text HoneyText;
    [SerializeField] Text MoneyText;
    [SerializeField] Text MerchantText;
    [SerializeField] Text Merchant2Text;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SE = GameObject.Find("SE");
        SE_Control = SE.GetComponent<SE_Control>();
        Volume = SE_Control.Volume;

        HP = Status.HP;
        MP = Status.MP;

        if(audioSource != null)
            audioSource.volume = Volume;

        if (MerchantText != null)
            MerchantText.text = "へへ、いらっしゃい";

        if (Merchant2Text != null)
            Merchant2Text.text = "いらっしゃい！";

        if (SceneManager.GetActiveScene().name == "StartMenuScene")
        {
            var info = new FileInfo(Application.persistentDataPath + "/" + SAVE_FILE_PATH);
            var reader = new StreamReader(info.OpenRead());
            var json = reader.ReadToEnd();
            var data = JsonUtility.FromJson<SaveData>(json);
            NumberOfHerbs = data.herbs;
            NumberOfHoney = data.honey;
            Money = data.money;
            Sword = data.sword;
            Armor = data.armor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHP = Status.CurrentHp;
        CurrentMP = Status.CurrentMp;

        if (coin > 9990)
            coin = 9990;

        MessageText = GameObject.Find("MessageText");
        if (MessageText != null)
        {
            BattleText = MessageText.GetComponent<BattleText>();
            if (BattleText != null)
            {
                EnemyDefeat = BattleText.EnemyDefeat;
                coin = BattleText.coin;
                if (coin == 1000)
                    coin = 2000; 
                if (EnemyDefeat && !GetCoin)
                {
                    Debug.Log(coin);
                    Money += coin;
                    Save();
                    GetCoin = true;
                }
            }
        }

        if (MoneyText != null)
        {
            MoneyText.text = "×" + Money;
        }

        if (HerbsText != null)
        {
            HerbsText.text = "×" + NumberOfHerbs;
        }

        if (HoneyText != null)
        {
            HoneyText.text = "×" + NumberOfHoney;
        }

        if(SwordButton != null)
        {
            if(Sword)
                SwordButton.interactable = false;
        }

        if (ArmorButton != null)
        {
            if (Armor)
                ArmorButton.interactable = false;
        }
    }

    public void BuyMedicinalHerbs()
    {
        if (Money >= 200 && NumberOfHerbs <= 99)
        {
            audioSource.PlayOneShot(sound);
            NumberOfHerbs++;
            Money -= 200;
            Save();
            Merchant2Text.text = "まいど！";
        }

        else
        {
            audioSource.PlayOneShot(FailureSound);
            Merchant2Text.text = "金が足りないよ";
        }
    }

    public void BuyMedicinalHerbs_E()
    {
        if (Money >= 100 && NumberOfHerbs <= 99)
        {
            audioSource.PlayOneShot(sound);
            NumberOfHerbs++;
            Money -= 100;
            Save();
            MerchantText.text = "へへ、まいど";
        }

        else
        {
            audioSource.PlayOneShot(FailureSound);
            MerchantText.text = "ん？金が足りないぞ";
        }
    }

    public void BuyMedicinalHoney()
    {
        if (Money >= 200 && NumberOfHoney <= 99)
        {
            audioSource.PlayOneShot(sound);
            NumberOfHoney++;
            Money -= 200;
            Save();
            Merchant2Text.text = "まいど！";
        }

        else
        {
            audioSource.PlayOneShot(FailureSound);
            Merchant2Text.text = "金が足りないよ";
        }
    }

    public void BuyMedicinalHoney_E()
    {
        if (Money >= 100 && NumberOfHoney <= 99)
        {
            audioSource.PlayOneShot(sound);
            NumberOfHoney++;
            Money -= 100;
            Save();
            MerchantText.text = "へへ、まいど";
        }

        else
        {
            audioSource.PlayOneShot(FailureSound);
            MerchantText.text = "ん？金が足りないぞ";
        }
    }

    public void BuyMedicinalSword()
    {
        if (Money >= 2000)
        {
            audioSource.PlayOneShot(sound);
            Sword = true;
            Money -= 1000;
            Save();
            Merchant2Text.text = "まいど！";
        }

        else
        {
            audioSource.PlayOneShot(FailureSound);
            Merchant2Text.text = "金が足りないよ";
        }
    }

    public void BuyMedicinalArmor()
    {
        if (Money >= 2000)
        {
            audioSource.PlayOneShot(sound);
            Armor = true;
            Money -= 1000;
            Save();
            Merchant2Text.text = "まいど！";
        }

        else
        {
            audioSource.PlayOneShot(FailureSound);
            Merchant2Text.text = "金が足りないよ";
        }
    }

    public void UseHerbs()
    {
        if (NumberOfHerbs > 0 && CurrentHP != HP)
        {
            NumberOfHerbs--;
            audioSource.PlayOneShot(sound);
            Save();
        }

        if (NumberOfHerbs == 0 || CurrentHP == HP)
        {
            audioSource.PlayOneShot(FailureSound);
        }
    }

    public void UseHoney()
    {
        if(NumberOfHoney > 0 && CurrentMP != MP)
        {
            NumberOfHoney--;
            audioSource.PlayOneShot(sound);
            Save();
        }

        if (NumberOfHoney == 0 || CurrentMP == MP)
        {
            audioSource.PlayOneShot(FailureSound);
        }
    }

    public void EnterTheInn()
    {
        if(Money >= 100)
        {
            Money -= 100;
            Save();
        }
    }

    void Save()
    {
        var data = new SaveData();
        data.herbs = NumberOfHerbs;
        data.honey = NumberOfHoney;
        data.money = Money;
        data.sword = Sword;
        data.armor = Armor;
        // JSONにシリアライズ
        var json = JsonUtility.ToJson(data);
        // Assetsフォルダに保存する
        var path = Application.persistentDataPath + "/" + SAVE_FILE_PATH;
        var writer = new StreamWriter(path, false); // 上書き
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }
}
