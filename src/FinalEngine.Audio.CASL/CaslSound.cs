// <copyright file="OpenALSound.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio;

using System;
using CASL;
using FinalEngine.Audio.Sounds;

internal sealed class CaslSound : ISound
{
    private bool isDisposed;

    private IAudio? sound;

    internal CaslSound(IAudio sound)
    {
        this.sound = sound ?? throw new ArgumentNullException(nameof(sound));
    }

    public bool IsLooping
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
            return this.sound!.IsLooping;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
            this.sound!.IsLooping = value;
        }
    }

    public float Volume
    {
        get
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
            return this.sound!.Volume;
        }

        set
        {
            ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
            this.sound!.Volume = value;
        }
    }

    public void Dispose()
    {
        if (this.isDisposed)
        {
            return;
        }

        if (this.sound != null)
        {
            this.sound.Dispose();
            this.sound = null;
        }

        this.isDisposed = true;
    }

    public void Pause()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
        this.sound!.Pause();
    }

    public void Play()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
        this.sound!.Play();
    }

    public void Reset()
    {
        ObjectDisposedException.ThrowIf(this.isDisposed, typeof(CaslSound));
        this.sound!.Reset();
    }
}
