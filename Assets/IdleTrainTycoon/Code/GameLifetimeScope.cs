using IdleTrainTycoon.Code.Gameplay.Towers;
using IdleTrainTycoon.Code.Gameplay.Trains;
using IdleTrainTycoon.Code.Gameplay.UI;
using IdleTrainTycoon.Code.Gameplay.World.Maps;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Systems.BankSystem;
using IdleTrainTycoon.Code.Systems.GameSpeedSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace IdleTrainTycoon.Code
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Space(20)]
        [SerializeField] private GameSO config;
        [SerializeField] private GameSpeed gameSpeed;
        [SerializeField] private Bank bank;
        [SerializeField] private GameUI ui;
        [Space(10)]
        [SerializeField] private Map map;
        [SerializeField] private DispatcherTower dispatcherTower;
        [SerializeField] private MineTower mineTower;
        [SerializeField] private StationTower stationTower;
        [SerializeField] private TrainFactory trainFactory;
        [Space(20)]
        [SerializeField] private Station testTrainSpawnPoint;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(config);
            builder.RegisterComponent(gameSpeed);
            builder.RegisterComponent(bank);

            builder.RegisterComponent(ui);
            builder.RegisterComponent(ui.HUD.Bank);

            builder.RegisterComponent(map);
            builder.RegisterComponent(trainFactory);

            builder.RegisterComponent(dispatcherTower);
            builder.RegisterComponent(mineTower);
            builder.RegisterComponent(stationTower);

            builder.RegisterEntryPoint<Game>().WithParameter("spawnAt", testTrainSpawnPoint);
        }
    }
}