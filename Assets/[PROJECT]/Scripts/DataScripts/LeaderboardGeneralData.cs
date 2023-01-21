using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardGeneralData : MonoBehaviour
{
    public string leaderboardFirstPageUrl;
    public string leaderboardSecondPageUrl;
    public List<PersonalData> AllPersonalData = new List<PersonalData>();
    public LeaderboardData leaderboardDataFirstPage;
    public LeaderboardData leaderboardDataSecondPage;

    private void OnEnable()
    {
        EventManager.LeaderboardGeneralData += () => this;
    }
    private void OnDisable()
    {
        EventManager.LeaderboardGeneralData -= () => this;
    }

    public void SetLeaderboardData()
    {
        leaderboardDataFirstPage = APIHelper.GetLeaderboardData(leaderboardFirstPageUrl);
        leaderboardDataSecondPage = APIHelper.GetLeaderboardData(leaderboardSecondPageUrl);

        int _count = leaderboardDataFirstPage.data.Count;

        for (int i = 0; i < _count; i++)
        {
            AllPersonalData.Add(leaderboardDataFirstPage.data[i]);
            AllPersonalData.Add(leaderboardDataSecondPage.data[i]);
        }
    }

}
