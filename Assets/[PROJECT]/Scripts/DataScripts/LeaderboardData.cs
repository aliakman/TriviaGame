using System.Collections.Generic;

[System.Serializable]
public class LeaderboardData
{
    public int page;
    public bool is_last;
    public List<PersonalData> data;
}

[System.Serializable]
public class PersonalData
{
    public int rank;
    public string nickname;
    public int score;
}