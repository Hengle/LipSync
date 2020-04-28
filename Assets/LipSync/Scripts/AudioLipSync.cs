﻿using UnityEngine;

namespace LipSync
{
    public enum ELipSyncMethod { Runtime, Baked }
    
    public class AudioLipSync : LipSync
    {
        public ELipSyncMethod lipSyncMethod;
        public AudioSource audioSource;
        public FFTWindow fftWindow = FFTWindow.Hamming;
       
        #region Fields for Baked LipSync
        public Animator targetAnimator;
        private int lastTimeSamples;
        #endregion
        

        void Start()
        {
            InitializeRecognizer();
           // av = new AudioVisualization(runtimeRecognizer.playingAudioSpectrum);
        }

        void Update()
        {
            if (lipSyncMethod == ELipSyncMethod.Runtime)
            {
                if (Input.GetKeyDown(KeyCode.Space)) audioSource.Play();

                recognizeResult = runtimeRecognizer.RecognizeByAudioSource(audioSource, fftWindow);
                UpdateForward();
            }
            else if (lipSyncMethod == ELipSyncMethod.Baked)
            {
                if (audioSource.timeSamples < lastTimeSamples)
                {
                    if (audioSource.isPlaying)
                    {
                        targetAnimator.CrossFade(audioSource.clip.name + "_anim", 0f);
                    }
                }
                lastTimeSamples = audioSource.timeSamples;
            }
        }
        
    }
    
}
