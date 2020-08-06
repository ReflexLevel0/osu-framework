// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.Timing;

namespace osu.Framework.Graphics.Audio
{
    /// <summary>
    /// A <see cref="Track"/> wrapper to allow insertion in the draw hierarchy to allow transforms, lifetime management etc.
    /// </summary>
    public class DrawableTrack : DrawableAudioWrapper, ITrack, IAdjustableClock
    {
        private readonly Track track;

        /// <summary>
        /// Construct a new drawable track instance.
        /// </summary>
        /// <param name="track">The audio track to wrap.</param>
        /// <param name="disposeTrackOnDisposal">Whether the track should be automatically disposed on drawable disposal/expiry.</param>
        public DrawableTrack(Track track, bool disposeTrackOnDisposal = true)
            : base(track, disposeTrackOnDisposal)
        {
            this.track = track;
        }

        public event Action Completed
        {
            add => track.Completed += value;
            remove => track.Completed -= value;
        }

        public event Action Failed
        {
            add => track.Failed += value;
            remove => track.Failed -= value;
        }

        public bool Looping
        {
            get => track.Looping;
            set => track.Looping = value;
        }

        public bool IsDummyDevice => track.IsDummyDevice;

        public double RestartPoint
        {
            get => track.RestartPoint;
            set => track.RestartPoint = value;
        }

        public double CurrentTime => track.CurrentTime;

        public double Rate
        {
            get => track.Rate;
            set => track.Rate = value;
        }

        public double Length
        {
            get => track.Length;
            set => track.Length = value;
        }

        public int? Bitrate => track.Bitrate;

        public bool IsRunning => track.IsRunning;

        public bool IsReversed => track.IsReversed;

        public bool HasCompleted => track.HasCompleted;

        public void Reset()
        {
            Volume.Value = 1;

            ResetSpeedAdjustments();

            Stop();
            Seek(0);
        }

        public void Restart() => track.Restart();

        public void ResetSpeedAdjustments()
        {
            RemoveAllAdjustments(AdjustableProperty.Frequency);
            RemoveAllAdjustments(AdjustableProperty.Tempo);
        }

        public bool Seek(double seek) => track.Seek(seek);

        public void Start() => track.Start();

        public void Stop() => track.Stop();

        public ChannelAmplitudes CurrentAmplitudes => track.CurrentAmplitudes;
    }
}
