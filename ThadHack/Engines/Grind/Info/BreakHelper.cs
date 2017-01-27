using System;
using ZzukBot.Mem;
using ZzukBot.Settings;

namespace ZzukBot.Engines.Grind.Info
{
    internal class _BreakHelper
    {
        private bool _NeedToBreak;
        private int BreakAt;

        private readonly Random ran = new Random();
        private int ResumeAt;
        private bool SetResumeTime;

        internal _BreakHelper()
        {
            BreakAt = 0;
            ResumeAt = 0;
            _NeedToBreak = false;
            SetResumeTime = false;
        }

        internal bool NeedToBreak
        {
            get
            {
                if (!(Options.BreakFor != 0 && Options.ForceBreakAfter != 0)) return false;
                if (_NeedToBreak)
                {
                    if (!ObjectManager.EnumObjects())
                    {
                        if (SetResumeTime)
                        {
                            SetResumeAt(0);
                            SetResumeTime = false;
                        }
                        else if (NeedToResume)
                        {
                            SetBreakAt(240000);
                            _NeedToBreak = false;
                        }
                    }
                    return true;
                }
                if (Environment.TickCount > BreakAt)
                {
                    _NeedToBreak = true;
                    return true;
                }
                SetResumeTime = true;
                return false;
            }
        }

        private bool NeedToResume => Environment.TickCount > ResumeAt;

        internal void SetBreakAt(int parModifier)
        {
            if (Options.ForceBreakAfter < 5)
                Options.ForceBreakAfter = 5;

            BreakAt = Environment.TickCount + Options.ForceBreakAfter*60*1000
                      + ran.Next(-120000, 120000) + parModifier;
        }

        private void SetResumeAt(int parModifier)
        {
            if (Options.BreakFor < 5)
                Options.BreakFor = 5;

            ResumeAt = Environment.TickCount + Options.BreakFor*60*1000
                       + ran.Next(-120000, 120000) + parModifier;
        }
    }
}