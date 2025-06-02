using IdleTrainTycoon.Code.Gameplay.Trains;

namespace IdleTrainTycoon.Code.Gameplay.Towers.Data
{
    [System.Serializable]
    internal class MineSchedule
    {
        public Train train;
        public float start;
        public float end;

        public MineSchedule(Train train, float start, float end)
        {
            this.train = train;
            this.start = start;
            this.end = end;
        }
    }
}