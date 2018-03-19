﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.UserInterface;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Game.Screens.Edit.Screens.Compose.BeatSnap
{
    public class BeatSnapVisualiser : CompositeDrawable
    {
        public readonly Bindable<int> Divisor = new Bindable<int>(1);

        private TickContainer tickContainer;

        [BackgroundDependencyLoader]
        private void load(OsuColour colours)
        {
            Size = new Vector2(100, 110);
            Masking = true;
            CornerRadius = 5;

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },
                new GridContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Content = new[]
                    {
                        new Drawable[]
                        {
                            tickContainer = new TickContainer(1, 2, 3, 4, 6, 8, 12, 16)
                            {
                                RelativeSizeAxes = Axes.Both,
                                Padding = new MarginPadding { Horizontal = 5 }
                            }
                        },
                        new Drawable[]
                        {
                            new Container
                            {
                                RelativeSizeAxes = Axes.Both,
                                Children = new Drawable[]
                                {
                                    new Box
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Colour = colours.Gray4
                                    },
                                    new Container
                                    {
                                        RelativeSizeAxes = Axes.Both,
                                        Padding = new MarginPadding { Horizontal = 5 },
                                        Child = new GridContainer
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Content = new[]
                                            {
                                                new Drawable[]
                                                {
                                                    new DivisorButton
                                                    {
                                                        Icon = FontAwesome.fa_chevron_left,
                                                    },
                                                    null,
                                                    new DivisorButton
                                                    {
                                                        Icon = FontAwesome.fa_chevron_right,
                                                    }
                                                },
                                                new Drawable[]
                                                {
                                                    null,
                                                    new TextFlowContainer(s => s.TextSize = 12)
                                                    {
                                                        Text = "beat snap divisor",
                                                        RelativeSizeAxes = Axes.X,
                                                        TextAnchor = Anchor.TopCentre
                                                    },
                                                },
                                            },
                                            ColumnDimensions = new[]
                                            {
                                                new Dimension(GridSizeMode.Absolute, 20),
                                                new Dimension(),
                                                new Dimension(GridSizeMode.Absolute, 20)
                                            }
                                        }
                                    }
                                }
                            }
                        },
                    },
                    RowDimensions = new[]
                    {
                        new Dimension(GridSizeMode.Absolute, 35),
                    }
                }
            };

            tickContainer.Divisor.BindTo(Divisor);
        }

        private class DivisorButton : IconButton
        {
            public DivisorButton()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                ButtonSize = new Vector2(20);
                IconScale = new Vector2(0.7f);
            }

            [BackgroundDependencyLoader]
            private void load(OsuColour colours)
            {
                IconColour = Color4.Black;
                HoverColour = colours.Gray7;
                FlashColour = colours.Gray9;
            }
        }
    }
}
