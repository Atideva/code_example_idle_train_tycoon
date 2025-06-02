using System;
using IdleTrainTycoon.Code.Gameplay.Towers;
using IdleTrainTycoon.Code.Gameplay.Trains;
using IdleTrainTycoon.Code.Gameplay.UI;
using IdleTrainTycoon.Code.Gameplay.World.Maps;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using IdleTrainTycoon.Code.Systems.BankSystem;
using IdleTrainTycoon.Code.Systems.GameSpeedSystem;
using VContainer.Unity;

namespace IdleTrainTycoon.Code
{
    public class Game : IStartable, IDisposable
    {
        private readonly GameSO _config;
        private readonly GameSpeed _gameSpeed;
        private readonly GameUI _ui;
        private readonly Bank _bank;

        private readonly Map _map;
        private readonly TrainFactory _factory;
        private readonly DispatcherTower _dispatcherTower;
        private readonly MineTower _mineTower;
        private readonly StationTower _stationTower;
        private readonly Station _spawnAt;

        public Game(
            GameSO config,
            GameSpeed gameSpeed,
            Bank bank,
            GameUI ui,
            
            Map map,
            DispatcherTower dispatcherTower,
            MineTower mineTower,
            StationTower stationTower,
            
            TrainFactory factory,
            Station spawnAt)
        {
            _config = config;
            _gameSpeed = gameSpeed;
            _bank = bank;
            _ui = ui;
            
            _map = map;
            _dispatcherTower = dispatcherTower;
            _mineTower = mineTower;
            _stationTower = stationTower;
            
            _factory = factory;
            _spawnAt = spawnAt;
        }

        public void Start()
        {
            _gameSpeed.Init(_ui.HUD.GameSpeed);

            _map.Init();
            _dispatcherTower.Init(_map, _mineTower, _bank);
            _mineTower.Init(_map);
            _stationTower.Init(_map, _config.SuppliesDatabase);

            _factory.Init(_config.TrainDatabase);
            _factory.OnSpawn += Register;

            _ui.HUD.SpawnTrainButton.OnClick(SpawnTrain);
            _ui.HUD.StationsPanel.Init();
            _ui.HUD.StationsPanel.Create(_map.Stations);
        }

        private void Register(Train train)
        {
            _dispatcherTower.Observe(train);
        }

        private void SpawnTrain()
        {
            var train = _factory.SpawnRandom();
            var targetStation = _config.SpawnTrainsAtRandomStations
                ? _map.GetRandomStation()
                : _spawnAt;

            train.PutInto(targetStation);
            train.ReadyToWork();
        }

        public void Dispose()
        {
            _factory.OnSpawn -= Register;
        }
    }
}