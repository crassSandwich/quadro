﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// requires that TimingSource has a clip of 4 seconds of silence (one 4/4 measure at 60 BPM) set to loop
public class RealtimeDriver : ADriver
{
    public AudioSource TimingSource;

    public int TimingClipBPM, TimingClipMeasureLength;

    float inverseAudioFrequency;

    void Awake ()
    {
        // TODO: move this initialize call to some kind of scene manager
        Initialize();
    }

    public override void Initialize ()
    {
        base.Initialize();

        TimingSource.Play();
    }

    void Update ()
    {
        if (!initialized) return;

        Player.Track.CurrentPositionInMeasure = (float) TimingSource.timeSamples * TimingClipBPM / 60 / TimingSource.clip.frequency;
        TimingSource.pitch = (float) Player.Track.BPM / TimingClipBPM * 4 / Track.BEATS_PER_MEASURE;

        var input = getInput();

        if (!input.FrameIsEmpty) Player.DoInput(input);
    }
}
