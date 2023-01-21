using UnityEngine;

[CreateAssetMenu(fileName = "Competition Data", menuName = "DATA/Competition Data", order = 1)]
public class CompetitionData : ScriptableObject
{
    public int countdownTime = 20;
    public int correctAnswerPoint = 10;
    public int wrongAnswerPoint = -5;
    public int notReplyInTimePoint = -3;
}
