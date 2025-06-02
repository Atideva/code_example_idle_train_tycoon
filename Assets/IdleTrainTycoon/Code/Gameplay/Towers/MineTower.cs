using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using IdleTrainTycoon.Code.Gameplay.Towers.Data;
using IdleTrainTycoon.Code.Gameplay.Trains;
using IdleTrainTycoon.Code.Gameplay.World.Maps;
using IdleTrainTycoon.Code.Gameplay.World.MapsWaypoints;
using Sirenix.OdinInspector;
using UnityEngine;

namespace IdleTrainTycoon.Code.Gameplay.Towers
{
    public class MineTower : SerializedMonoBehaviour
    {
        private Map map;
        public IReadOnlyList<Mine> Mines => map.Mines;
        [SerializeField] private Dictionary<Mine, List<MineSchedule>> _mineSchedules = new();

        public void Init(Map newMap)
        {
            _mineSchedules = new Dictionary<Mine, List<MineSchedule>>();
            map = newMap;
            StartAutoCleanup();
        }

        public void WillBeBusyAt(Mine mine, Train byTrain, float secFromNow, float duration)
        {
            var now = Time.time;
            var start = now + Mathf.Max(0f, secFromNow);
            var end = start + duration;

            var schedule = GetSchedule(mine);
            schedule.RemoveAll(s => s.end <= now);

            var note = new MineSchedule(byTrain, start, end);
            var index = schedule.FindIndex(slot => slot.start > start);
            if (index < 0) schedule.Add(note);
            else schedule.Insert(index, note);
        }

        [Button]
        public float GetIdleDuration(Mine mine)
        {
            var now = Time.time;
            var schedule = GetSchedule(mine);

            if (schedule.Count > 0)
            {
                //Debug.Log(mine.name + " have schedule. Delay: " + schedule[^1].end);
                return schedule[^1].end - now;
            }

            Debug.LogWarning(mine.name + " have no schedule");
            return 0f;
        }

        private List<MineSchedule> GetSchedule(Mine mine)
        {
            if (!_mineSchedules.ContainsKey(mine))
                _mineSchedules.Add(mine, new List<MineSchedule>());
            return _mineSchedules[mine];
        }

        private void StartAutoCleanup(float intervalSec = 0.1f)
        {
            _ = CleanupSchedule(intervalSec, this.GetCancellationTokenOnDestroy());
        }

        private async UniTask CleanupSchedule(float intervalSec, CancellationToken ct)
        {
            // remove expired  
            while (!ct.IsCancellationRequested)
            {
                var now = Time.time;

                foreach (var schedule in _mineSchedules.Values)
                    schedule.RemoveAll(slot => slot.end <= now);

                await UniTask.Delay(TimeSpan.FromSeconds(intervalSec),
                    DelayType.DeltaTime,
                    PlayerLoopTiming.Update, ct);
            }
        }
    }
}