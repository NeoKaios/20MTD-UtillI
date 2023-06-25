using System.Threading;
using System.Collections.Generic;
using UnityEngine;

namespace UtillI.Internals
{
    struct WatchedRegistration
    {
        public WatchedRegistration(Registration reg)
        {
            this.reg = reg;
            lastDisplayed = 0;
            isDisplayed = false;
        }
        private Registration reg { get; }
        public DisplayRule rule { get { return reg.rule; } }
        public PanelPosition pos { get { return reg.pos; } }
        public string newText { get { return reg.GetUpdatedText(); } }
        public int lastDisplayed { get; set; }
        public bool isDisplayed { get; set; }
    }
    class Watcher : MonoBehaviour
    {
        private ModTextPanel mtp = null;
        private bool isPaused = false;
        private readonly int WATCHER_INTERVAL = 2000;
        private readonly int WATCHER_CYCLE_LENGTH = 2;
        private int watcherCycle = 0;
        private System.Threading.Timer watcher = null;
        private (int, int) currentlyDisplayedWRegIdx;
        private List<WatchedRegistration> wr = new List<WatchedRegistration>();

        void Awake()
        {
            mtp = gameObject.AddComponent<ModTextPanel>();
            SetupWatcher();
            StartWatcher();
            foreach (var reg in UtillIRegister.registrations)
            {
                reg.Init();
                wr.Add(new WatchedRegistration(reg));
            }
            currentlyDisplayedWRegIdx = (-1, -1);
        }
        private bool newIsOlderAndCurrentIsOldEnough(WatchedRegistration wreg)
        {
            var current = GetCurrentlyDisplayed(wreg.pos);
            return current == -1 || (wreg.lastDisplayed >= wr[current].lastDisplayed && wr[current].lastDisplayed >= WATCHER_CYCLE_LENGTH);
        }
        private bool canBeDisplayedNow(DisplayRule rule)
        {
            return (
                rule == DisplayRule.Always ||
                rule == DisplayRule.PauseOnly && isPaused ||
                rule == DisplayRule.CombatOnly && !isPaused);
        }
        private void UpdateCurrentlyDisplayed(PanelPosition pos)
        {
            var index = GetCurrentlyDisplayed(pos);
            if (index == -1) return;
            var wreg = wr[index];
            if (!wreg.isDisplayed)
            {
                wreg.lastDisplayed = 0;
                wreg.isDisplayed = true;
            }
            wr[index] = wreg;
            var displayText = canBeDisplayedNow(wreg.rule) ? wreg.newText : "";
            mtp.SetText(pos, displayText);
        }
        private void UpdateDisplayedRegistration()
        {
            WatchedRegistration wreg;
            for (var i = 0; i < wr.Count; i++)
            {
                wreg = wr[i];
                if (wreg.isDisplayed) continue;
                if ((newIsOlderAndCurrentIsOldEnough(wreg) || !canBeDisplayedNow(wr[GetCurrentlyDisplayed(wreg.pos)].rule)) && canBeDisplayedNow(wreg.rule))
                {
                    SetCurrentlyDisplayed(wreg.pos, i);
                }
            }
        }
        private void ExecuteWatcherCycle(bool forceUpdate = false)
        {
            for (var i = 0; i < wr.Count; i++)
            {
                var wreg = wr[i];
                wreg.lastDisplayed++;
                wr[i] = wreg;
            }
            if (watcherCycle % WATCHER_CYCLE_LENGTH == 0 || forceUpdate) UpdateDisplayedRegistration();
            UpdateCurrentlyDisplayed(PanelPosition.BottomLeft);
            UpdateCurrentlyDisplayed(PanelPosition.BottomRight);
            watcherCycle++;
        }
        private void SetupWatcher()
        {
            watcher = new System.Threading.Timer(
                e => (e as Watcher).ExecuteWatcherCycle(),
                this,
                Timeout.Infinite,
                Timeout.Infinite);
        }
        private void StartWatcher()
        {
            watcher.Change(WATCHER_INTERVAL, WATCHER_INTERVAL);
        }
        private void StopWatcher()
        {
            watcher.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public void HideAndStopWatcher()
        {
            mtp.Hide(PanelPosition.BottomLeft);
            mtp.Hide(PanelPosition.BottomRight);
            StopWatcher();
        }
        public void SetPauseStatus(bool isPaused)
        {
            if (this.isPaused != isPaused)
            {
                this.isPaused = isPaused;
                ExecuteWatcherCycle(true);
            }
        }
        private int GetCurrentlyDisplayed(PanelPosition pos)
        {
            var index = currentlyDisplayedWRegIdx.Item1;
            if (pos == PanelPosition.BottomRight)
            {
                index = currentlyDisplayedWRegIdx.Item2;
            }
            return index;
        }
        private void SetCurrentlyDisplayed(PanelPosition pos, int index)
        {
            int old;
            if (pos == PanelPosition.BottomRight)
            {
                old = currentlyDisplayedWRegIdx.Item2;
                currentlyDisplayedWRegIdx.Item2 = index;
            }
            else
            {
                old = currentlyDisplayedWRegIdx.Item1;
                currentlyDisplayedWRegIdx.Item1 = index;
            }
            if (old != -1)
            {
                var wreg = wr[old];
                wreg.isDisplayed = false;
                wr[old] = wreg;
            }
        }
    }
}