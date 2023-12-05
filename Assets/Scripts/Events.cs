using System;

public static class Events
{
    public static Action<EndSituation> onCircuitEnded;
}

public enum EndSituation
{
    PLAYER_WINS = 0,
    AI_WINS,
    PLAYER_DEAD
}