﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using osu.Game.Rulesets.Mods;
using osu.Framework.Timing;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;

namespace osu.Game.Beatmaps
{
    public abstract class DifficultyCalculator
    {
        protected readonly IBeatmap Beatmap;
        protected readonly Mod[] Mods;

        protected double TimeRate = 1;

        protected DifficultyCalculator(IBeatmap beatmap, Mod[] mods = null)
        {
            Beatmap = beatmap;
            Mods = mods ?? new Mod[0];

            ApplyMods(Mods);
        }

        protected virtual void ApplyMods(Mod[] mods)
        {
            var clock = new StopwatchClock();
            mods.OfType<IApplicableToClock>().ForEach(m => m.ApplyToClock(clock));
            TimeRate = clock.Rate;
        }

        protected virtual void PreprocessHitObjects()
        {
        }

        public abstract double Calculate(Dictionary<string, double> categoryDifficulty = null);
    }
}
