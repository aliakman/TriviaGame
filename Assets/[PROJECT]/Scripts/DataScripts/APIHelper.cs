using UnityEngine;
using System.Net;
using System.IO;

public static class APIHelper
{
    public static bool CheckNetConnection()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }



    public static LeaderboardData GetLeaderboardData(string _url)
    {
        HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(_url);
        HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
        StreamReader _reader = new StreamReader(_response.GetResponseStream());
        string _json = _reader.ReadToEnd();
        return JsonUtility.FromJson<LeaderboardData>(_json);
    }

}
