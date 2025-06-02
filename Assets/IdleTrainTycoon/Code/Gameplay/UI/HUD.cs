using IdleTrainTycoon.Code.Gameplay.World.WorldTimer;
using IdleTrainTycoon.Code.Systems.BankSystem;
using IdleTrainTycoon.Code.Systems.GameSpeedSystem;
using UnityEngine;
using UnityEngine.UI;

namespace IdleTrainTycoon.Code.Gameplay.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Button spawnTrainButton;
        [SerializeField] private GameSpeedUI gameSpeed;
        [SerializeField] private StationsPanelUI stationsPanel;
        [SerializeField] private BankUI bankUI;
        [SerializeField] private WorldTimeUI worldTime;
        public Button SpawnTrainButton => spawnTrainButton;
        public GameSpeedUI GameSpeed => gameSpeed;
        public StationsPanelUI StationsPanel => stationsPanel;
        public BankUI Bank => bankUI;
        public WorldTimeUI WorldTime => worldTime;
    }
}