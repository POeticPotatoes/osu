﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Game.Graphics.Containers;
using osuTK;

namespace osu.Game.Graphics.UserInterface
{
    public abstract class LoadingButton : OsuHoverContainer
    {
        private const float fade_duration = 200;

        private bool isLoading;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;

                Enabled.Value = !isLoading;

                if (value)
                {
                    loading.Show();
                    content.FadeOut(fade_duration, Easing.OutQuint);
                    OnLoadingStart();
                }
                else
                {
                    loading.Hide();
                    content.FadeIn(fade_duration, Easing.OutQuint);
                    OnLoadingFinished();
                }
            }
        }

        public Vector2 LoadingAnimationSize
        {
            get => loading.Size;
            set => loading.Size = value;
        }

        private readonly LoadingAnimation loading;
        private readonly Drawable content;

        protected LoadingButton()
        {
            Container background;

            Child = background = CreateBackground();

            background.AddRange(new[]
            {
                content = CreateContent(),
                loading = new LoadingAnimation
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(12)
                }
            });
        }

        protected override bool OnClick(ClickEvent e)
        {
            if (!Enabled.Value)
                return false;

            try
            {
                return base.OnClick(e);
            }
            finally
            {
                // run afterwards as this will disable this button.
                IsLoading = true;
            }
        }

        protected virtual void OnLoadingStart()
        {
        }

        protected virtual void OnLoadingFinished()
        {
        }

        protected abstract Container CreateBackground();

        protected abstract Drawable CreateContent();
    }
}
