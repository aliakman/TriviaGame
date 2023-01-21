using UnityEngine;
using UnityEngine.UI;

public class LeaderboardSlot : MonoBehaviour
{
    public int rank;
    public Text rankText;
    public Text nameText;
    public Text scoreText;

    public void Init (string _rank, string _name, string _score)
    {
        rankText.text = _rank.ToString();
        //rank = _rank;
        nameText.text = _name;
        scoreText.text = _score;
    }
}
