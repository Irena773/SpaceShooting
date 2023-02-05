using UnityEngine;
using System;
using System.Linq;
using System.IO;
using TMPro;

//ランキング処理について
public class Ranking : MonoBehaviour
{
    
    private string path = "./Assets/Data/Score.txt";
    private int rankingSize = 5;

    private GameDirector gameDirector;

    private TextMeshPro RankingIndex;
    private TextMeshPro RankingTable;
    private string playerScore;
    private TextMeshPro score;
    private GameObject RankingUI; 

    private void OnEnable()
    {

        RankingUI = GameObject.Find("RankingUI");
        RankingIndex = RankingUI.transform.Find("Ranking/RankingIndex").gameObject.GetComponent<TextMeshPro>();
        RankingTable = RankingUI.transform.Find("Ranking/RankingTable").gameObject.GetComponent<TextMeshPro>();
        
        score = RankingUI.transform.Find("Ranking/ScoreValue").gameObject.GetComponent<TextMeshPro>();
        gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        playerScore = gameDirector.GetScore().ToString();
        
        //scoresTextにスコアを入れる
        string scoresText = File.ReadAllText(path);
        //データの先頭に追加
        scoresText = playerScore + "\n" + scoresText;
        //データ上書き保存
        File.WriteAllText(path, scoresText);

        //ランキング作る
        string[] scores = File.ReadAllLines(path);
        int[] intScores = scores.Select(int.Parse).ToArray();
        
        Array.Sort(intScores, (a, b) => b - a);
        //表示する分のスコアを入れる配列
        int[] topScore = new int[rankingSize];
        string[] topIndex = new string[rankingSize];
        //足りないとき
        if (intScores.Length < rankingSize)      
        {
            for (int i = 0; i < intScores.Length; i++)
            {
                topScore[i] = intScores[i];
            }
        }
        else
        {
            for (int i = 0; i < rankingSize; i++)
            {
                topScore[i] = intScores[i];
            }
        }
        for (int i = 0; i < rankingSize; i++)
        {
            string str = (i + 1).ToString();
            switch (i+1){
                case 1:
                    str += "st";
                    break;
                case 2:
                    str += "nd";
                    break;
                case 3:
                    str += "rd";
                    break;
                default:
                    str += "th";
                    break;
            }
            topIndex[i] = str;

        }
        //配列を改行で区切って1つのテキストに
        string rankingText = string.Join("\n", topScore);
        string indexText = string.Join("\n",topIndex);
        //ランキングをUIに表示
        RankingTable.text = rankingText;
        RankingIndex.text = indexText;
        //プレイヤースコアをUIに表示
        score.text = playerScore;

        
    }

    
}
