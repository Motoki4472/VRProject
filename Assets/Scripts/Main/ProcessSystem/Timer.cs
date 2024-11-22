using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Main.ProcessSystem
{
    public class Timer
    {
        public enum TimerState
        {
            Stop,
            Run,
            Pause
        }
        private TimerState timerState = TimerState.Stop;
        private float time = 0;


        public Timer()
        {
            timerState = TimerState.Stop;
            time = 0;
        }

        public void ResetTimer()
        {
            time = 0;
        }

        public void StopTimer()
        {
            timerState = TimerState.Stop;
        }

        public void RunTimer()
        {
            timerState = TimerState.Run;
        }

        public void PauseTimer()
        {
            timerState = TimerState.Pause;
        }

        // ProcessSystemのUpdateで呼び出す
        public void UpdateTimer()
        {
            if (timerState == TimerState.Run)
            {
                time += Time.deltaTime;
            }
        }

        public float GetTime()
        {
            return time;
        }
    }
}
