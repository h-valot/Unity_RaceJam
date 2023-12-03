namespace Data
{
    [System.Serializable]
    public class PersistantData 
    {
        public PersistantData(PersistantData data)
        {
            // exit, if there is no data to load
            if (data == null) return;

            // sync data
            score = data.score;
            raceAmount = data.raceAmount;
        }

        public int score;
        public int raceAmount;
    }
}