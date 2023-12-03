namespace Data
{
    [System.Serializable]
    public class PersistantData 
    {
        public PersistantData(PersistantData data)
        {
            // exit, if data is null
            if (data == null) return;

            // synchronize data
            score = data.score;
            raceAmount = data.raceAmount;
        }

        public int score;
        public int raceAmount;
    }
}