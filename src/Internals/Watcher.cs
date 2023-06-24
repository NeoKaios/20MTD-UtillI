using UnityEngine;
using System.Threading;
using System.Collections.Generic;

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
        public Registration reg { get; }
        public int lastDisplayed { get; set; }
        public bool isDisplayed { get; set; }
    }
    class Watcher : MonoBehaviour
    {
        private ModTextPanel mtp = null;
        private readonly int watcherInterval = 2000;
        private readonly int watchCycleLength = 2;
        private int watchCycle = 0;
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
                wr.Add(new WatchedRegistration(reg));
            }
            currentlyDisplayedWRegIdx = (-1, -1);
        }

        private bool isOlderThanCurrentlyDisplayed(WatchedRegistration wreg)
        {
            var current = currentlyDisplayedWRegIdx.Item1;
            if (wreg.reg.pos == PanelPosition.BottomRight)
            {
                current = currentlyDisplayedWRegIdx.Item2;
            }
            return current == -1 || wreg.lastDisplayed > wr[current].lastDisplayed;
        }

        private bool canBeDisplayedNow(WatchedRegistration wreg)
        {
            return (
                wreg.reg.rule == DisplayRule.Always ||
                wreg.reg.rule == DisplayRule.PauseOnly && Patch.isPaused ||
                wreg.reg.rule == DisplayRule.CombatOnly && !Patch.isPaused);
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
        private void SetAsDisplayed(PanelPosition pos, int index)
        {
            var wreg = wr[index];
            wreg.isDisplayed = true;
            wreg.lastDisplayed = 0;
            wr[index] = wreg;
            var displayText = canBeDisplayedNow(wreg) ? wreg.reg.updater.GetUpdatedText() : "";
            mtp.SetText(pos, displayText);
        }

        public void MainWatch()
        {
            WatchedRegistration wreg;
            if (watchCycle % watchCycleLength == 0)
            {
                // Change displayed reg
                for (var i = 0; i < wr.Count; i++)
                {
                    if (wr[i].isDisplayed) continue;
                    wreg = wr[i];
                    wreg.lastDisplayed++;
                    wr[i] = wreg;
                    if (isOlderThanCurrentlyDisplayed(wreg) && canBeDisplayedNow(wreg))
                    {
                        SetCurrentlyDisplayed(wreg.reg.pos, i);
                    }
                }
            }
            if (currentlyDisplayedWRegIdx.Item1 != -1)
            {
                SetAsDisplayed(PanelPosition.BottomLeft, currentlyDisplayedWRegIdx.Item1);
            }
            if (currentlyDisplayedWRegIdx.Item1 != -1)
            {
                SetAsDisplayed(PanelPosition.BottomRight, currentlyDisplayedWRegIdx.Item2);
            }
            watchCycle++;
        }
        private void SetupWatcher()
        {
            watcher = new System.Threading.Timer(
                e => (e as Watcher).MainWatch(),
                this,
                Timeout.Infinite,
                Timeout.Infinite);
        }

        public void StartWatcher()
        {
            watcher.Change(watcherInterval, watcherInterval);
        }

        public void StopWatcher()
        {
            watcher.Change(Timeout.Infinite, Timeout.Infinite);
        }
    }
}