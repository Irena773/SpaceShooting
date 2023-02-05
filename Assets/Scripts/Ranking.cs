using UnityEngine;
using System;
using System.Linq;
using System.IO;
using TMPro;

//�����L���O�����ɂ���
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
        
        //scoresText�ɃX�R�A������
        string scoresText = File.ReadAllText(path);
        //�f�[�^�̐擪�ɒǉ�
        scoresText = playerScore + "\n" + scoresText;
        //�f�[�^�㏑���ۑ�
        File.WriteAllText(path, scoresText);

        //�����L���O���
        string[] scores = File.ReadAllLines(path);
        int[] intScores = scores.Select(int.Parse).ToArray();
        
        Array.Sort(intScores, (a, b) => b - a);
        //�\�����镪�̃X�R�A������z��
        int[] topScore = new int[rankingSize];
        string[] topIndex = new string[rankingSize];
        //����Ȃ��Ƃ�
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
        //�z������s�ŋ�؂���1�̃e�L�X�g��
        string rankingText = string.Join("\n", topScore);
        string indexText = string.Join("\n",topIndex);
        //�����L���O��UI�ɕ\��
        RankingTable.text = rankingText;
        RankingIndex.text = indexText;
        //�v���C���[�X�R�A��UI�ɕ\��
        score.text = playerScore;

        
    }

    
}
