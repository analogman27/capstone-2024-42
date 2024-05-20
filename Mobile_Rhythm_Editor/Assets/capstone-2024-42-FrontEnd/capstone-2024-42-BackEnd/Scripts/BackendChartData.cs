using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using Unity.VisualScripting;
using Unity.Burst.Intrinsics;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class LevelChartData
{
    public int level;
    public int maxExperience;
    public int rewardGold;
}

public class SongChartData
{
    public int songID;
    public string songName;
    public int bpm;
    public string artist;
    public int songLevel;
}

public class CharacterChartData
{
    public int characterID;
    public string characterName;
    public string characterProfile;
    public string characterTeam;
}

public class BackendChartData : MonoBehaviour
{
    public static List<LevelChartData> levelChart;
    public static List<SongChartData> songChart;
    public static List<CharacterChartData> characterChart;

    static BackendChartData()
    {
        levelChart = new List<LevelChartData>();
        songChart = new List<SongChartData>();
        characterChart = new List<CharacterChartData>();
    }

    public static void LoadAllChart()
    {
        LoadSongChart();
//        LoadCharacterChart();
        LoadLevelChart();
    }

    public static void LoadLevelChart()
    {
        Backend.Chart.GetChartContents(Constants.LEVEL_CHART, callback => { 
            if(callback.IsSuccess())
            {
                try
                {
                    LitJson.JsonData jsonData = callback.FlattenRows();

                    if (jsonData.Count <= 0)
                    {
                        Debug.LogWarning("������ ������");
                    }
                    else
                    {
                        for(int i = 0; i < jsonData.Count; ++i) { 
                            LevelChartData newChart = new LevelChartData();
                            newChart.level = int.Parse(jsonData[i]["level"].ToString());
                            newChart.maxExperience = int.Parse(jsonData[i]["maxExperience"].ToString());
                            newChart.rewardGold = int.Parse(jsonData[i]["rewardGold"].ToString());

                            levelChart.Add(newChart);

                            Debug.Log($"Level : {newChart.level}, Max Exp : {newChart.maxExperience}, Reward Gold : {newChart.rewardGold}");
                        }
                    }
                }
                catch(System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            else
            {
                Debug.LogError($"{Constants.LEVEL_CHART}��Ʈ �ҷ����� ���� : {callback}");
            }
        });
    }

    public static void LoadSongChart()
    {
        Backend.Chart.GetChartContents(Constants.SONG_CHART, callback => {
            if (callback.IsSuccess())
            {
                try
                {
                    LitJson.JsonData jsonData = callback.FlattenRows();

                    if (jsonData.Count <= 0)
                    {
                        Debug.LogWarning("������ ������");
                    }
                    else
                    {
                        for (int i = 0; i < jsonData.Count; ++i)
                        {
                            SongChartData newChart = new SongChartData();

                            newChart.songID = int.Parse(jsonData[i]["SongID"].ToString());
                            newChart.songName = jsonData[i]["SongName"].ToString();
                            newChart.songLevel = int.Parse(jsonData[i]["SongLevel"].ToString());
                            newChart.bpm = int.Parse(jsonData[i]["BPM"].ToString());
                            newChart.artist = jsonData[i]["Artist"].ToString();
                            songChart.Add(newChart);

                            Debug.Log($"SongID : {newChart.songID}, SongName : {newChart.songName}, BPM : {newChart.bpm}, Artist : {newChart.artist}, SongLevel : {newChart.songLevel}");
                        }
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError(e);
                }
            }
            else
            {
                Debug.LogError($"{Constants.SONG_CHART}��Ʈ �ҷ����� ���� : {callback}");
            }
        });
    }

    public static void LoadCharacterChart()
    {

    }
}