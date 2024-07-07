using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Technique3_Text : MonoBehaviour
{
    GameObject Player;
    Status Status;
    public Text Text;
    string Technique;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Status = Player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        Technique = Status.Technique3;
        Text.text = Technique;
        TextColor(Technique);
    }

    void TextColor(string Technique)
    {
        switch (Technique)
        {
            case "ファイアーパンチ":
            case "ファイアースラッシュ":
            case "ヒートヘイズ":
            case "サーマル":
            case "サーマルパワー":
            case "イグニッション":
            case "エクスプロージョン":
            case "イラプション":
            case "プロミネンス":
                Text.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                break;

            case "ウォーターパンチ":
            case "ウォータースラッシュ":
            case "エクストラマー":
            case "ハイドロ":
            case "ハイドロパワー":
            case "オーバーキャスト":
            case "アシッドレイン":
            case "ブライニクル":
            case "アブソリュートゼロ":
                Text.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
                break;

            case "リーフパンチ":
            case "リーフスラッシュ":
            case "スケアクロウ":
            case "ウィンド":
            case "ウィンドパワー":
            case "ホトシンセシス":
            case "ダストストーム":
            case "ドリフトウッド":
            case "ローカストプレイグ":
                Text.color = new Color(0.0f, 0.5f, 0.0f, 1.0f);
                break;

            case "ライトニングパンチ":
            case "ライトニングスラッシュ":
            case "フラッシュ":
            case "チャージ":
            case "エクシードチャージ":
            case "シャイニング":
            case "マグネティックフィールド":
            case "レールガン":
            case "スターバースト":
                Text.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                break;
        }
    }
}
