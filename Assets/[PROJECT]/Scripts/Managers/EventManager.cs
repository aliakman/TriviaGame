using System;

public static class EventManager
{
    public static Func<LeaderboardGeneralData> LeaderboardGeneralData;
    public static Func<ScrollBehaviour> ScrollBehaviour;
    public static Func<QuestionSlotBehaviour> QuestionSlotBehaviour;
    public static Func<CompetitionBehaviour> CompetitionBehaviour;

    public static Action ToEndCompetition;

    public static Action<int> CorrectAnswerGiven;
    public static Action<int, int> WrongAnswerGiven;
    public static Action QuestionNotReplied;

    public static Action QuestionReady;


}
