using UnityAddressableImporter.Helper;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Trains.Data
{
    [CreateAssetMenu(fileName = "NewTrain", menuName = "Game/Train SO")]
    public class TrainSO : ScriptableObject
    {
        [SerializeField] [Label(("Name"))] private string nameLabel;
        [SerializeField] [Min(0)] private float speed = 1f;
        [SerializeField] [Min(0)] private float harvestDurationSec = 10f;
        public float Speed => speed;
        public float HarvestDurationSec => harvestDurationSec;
        public string Name  => nameLabel;
    }
}