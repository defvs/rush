// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Backgrounds;
using osu.Game.Rulesets.Dash.UI;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Dash.Objects.Drawables
{
    public class DrawableOrb : DrawableLanedHit<Orb>
    {
        private const double rotation_time = 1000;

        private readonly SpriteIcon orbSpriteIcon;
        private readonly Box background;
        private readonly Triangles triangles;

        public DrawableOrb(Orb hitObject)
            : base(hitObject)
        {
            Size = new Vector2(DashPlayfield.HIT_TARGET_SIZE);

            Content.AddRange(new Drawable[]
            {
                orbSpriteIcon = new SpriteIcon
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Icon = FontAwesome.Solid.Cog
                },
                new CircularContainer
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Size = new Vector2(DashPlayfield.HIT_TARGET_SIZE),
                    Scale = new Vector2(0.7f),
                    BorderThickness = DashPlayfield.HIT_TARGET_SIZE * 0.1f,
                    BorderColour = Color4.White,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        background = new Box { RelativeSizeAxes = Axes.Both },
                        triangles = new Triangles { RelativeSizeAxes = Axes.Both }
                    }
                },
            });

            AccentColour.ValueChanged += evt => updateColours(evt.NewValue);
        }

        private void updateColours(Color4 colour)
        {
            background.Colour = colour.Darken(0.5f);
            triangles.Colour = colour;
            triangles.Alpha = 0.8f;
            orbSpriteIcon.Colour = colour.Lighten(0.5f);
        }

        public void UpdateResult() => base.UpdateResult(true);

        public override bool OnPressed(DashAction action) => false;

        public override void OnReleased(DashAction action)
        {
        }

        protected override void Update()
        {
            base.Update();

            orbSpriteIcon.Rotation = (float)(Time.Current % rotation_time / rotation_time) * 360f;
        }
    }
}
