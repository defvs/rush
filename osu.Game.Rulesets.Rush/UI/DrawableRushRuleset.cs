﻿// Copyright (c) Shane Woolcock. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Input.Handlers;
using osu.Game.Replays;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Rush.Objects;
using osu.Game.Rulesets.Rush.Replays;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.UI.Scrolling;
using osu.Game.Scoring;

namespace osu.Game.Rulesets.Rush.UI
{
    [Cached]
    public class DrawableRushRuleset : DrawableScrollingRuleset<RushHitObject>
    {
        protected override bool UserScrollSpeedAdjustment => true;

        protected override ScrollVisualisationMethod VisualisationMethod => ScrollVisualisationMethod.Constant;

        public DrawableRushRuleset(RushRuleset ruleset, IBeatmap beatmap, IReadOnlyList<Mod> mods = null)
            : base(ruleset, beatmap, mods)
        {
            Direction.Value = ScrollingDirection.Left;
            TimeRange.Value = 800;
        }

        public bool PlayerCollidesWith(HitObject hitObject) => Playfield.PlayerSprite.CollidesWith(hitObject);

        protected override Playfield CreatePlayfield() => new RushPlayfield();

        protected override ReplayInputHandler CreateReplayInputHandler(Replay replay) => new RushFramedReplayInputHandler(replay);

        protected override ReplayRecorder CreateReplayRecorder(Score score) => new RushReplayRecorder(score);

        public new RushPlayfield Playfield => (RushPlayfield)base.Playfield;

        public override DrawableHitObject<RushHitObject> CreateDrawableRepresentation(RushHitObject h) => null;

        protected override PassThroughInputManager CreateInputManager() => new RushInputManager(Ruleset?.RulesetInfo);
    }
}
