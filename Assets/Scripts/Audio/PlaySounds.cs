using UnityEngine;
using System.Collections.Generic;

public class PlaySounds : MonoBehaviour
{
    public enum SoundType { Bumper, Combo, GameOver, ExtraBall, BallLost, Loadup, RailEnter, RailRoll, FlipperHit, RNGBumper }

    private Dictionary<SoundType, AudioSource> soundSources = new Dictionary<SoundType, AudioSource>();

    void Start()
    {
        // Initialize all audio sources from components
        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length < 9)
        {
            Debug.LogError("PlaySounds requires 10 AudioSource components!");
            return;
        }

        soundSources[SoundType.Bumper] = sources[0];
        soundSources[SoundType.Combo] = sources[1];
        soundSources[SoundType.GameOver] = sources[2];
        soundSources[SoundType.ExtraBall] = sources[3];
        soundSources[SoundType.BallLost] = sources[4];
        soundSources[SoundType.Loadup] = sources[5];
        soundSources[SoundType.RailEnter] = sources[6];
        soundSources[SoundType.RailRoll] = sources[7];
        soundSources[SoundType.FlipperHit] = sources[8];
        soundSources[SoundType.RNGBumper] = sources[9];


        HitBumper.onHitBumper += PlayBumper;
        Combo.onComboAchieved += PlayCombo;
        Lives.onGameOver += PlayGameOver;
        ExtraBall.onExtraBall += PlayExtraBall;
        PlayArea.onBallLost += PlayBallLost;
        Shoot.onPress += PlayLoadup;
        Shoot.onRelease += StopLoadup;
        BallToRailConnector.onRailPlaySound += PlayRailEnter;
        BallToRailConnector.onRailPlaySound += PlayRailRoll;
        BallToRailConnector.onRailStopSound += StopRailRoll;
        FlipperController.onFlipperPlaySound += PlayFlipperHit;
        RNGHitBumper.onHitRNGBumper += PlayRNGBumper;
        sources = GetComponents<AudioSource>();

    }

    private void OnDisable()
    {
        HitBumper.onHitBumper -= PlayBumper;
        Combo.onComboAchieved -= PlayCombo;
        Lives.onGameOver -= PlayGameOver;
        ExtraBall.onExtraBall -= PlayExtraBall;
        PlayArea.onBallLost -= PlayBallLost;
        Shoot.onPress -= PlayLoadup;
        Shoot.onRelease -= StopLoadup;
        BallToRailConnector.onRailPlaySound -= PlayRailEnter;
        BallToRailConnector.onRailPlaySound -= PlayRailRoll;
        BallToRailConnector.onRailStopSound -= StopRailRoll;
        FlipperController.onFlipperPlaySound -= PlayFlipperHit;
        RNGHitBumper.onHitRNGBumper -= PlayRNGBumper;
    }

    private void PlayRNGBumper(Transform _, int __)
    {
        soundSources[SoundType.RNGBumper].pitch = Random.Range(0.5f, 1.5f);
        soundSources[SoundType.RNGBumper].Play();
    }

    private void PlayBumper(Transform _, int __)
    {
        soundSources[SoundType.Bumper].pitch = Random.Range(0.5f, 1.5f);
        soundSources[SoundType.Bumper].Play();
    }

    private void PlayCombo(int value, string _)
    {
        soundSources[SoundType.Combo].pitch = 1 + value / 10;
        soundSources[SoundType.Combo].Play();
    }

    private void PlayGameOver(string _)
    {
        soundSources[SoundType.GameOver].Play();
    }

    private void PlayExtraBall(string _)
    {
        soundSources[SoundType.ExtraBall].Play();
    }

    private void PlayBallLost()
    {
        soundSources[SoundType.BallLost].Play();
    }

    private void PlayLoadup()
    {
        soundSources[SoundType.Loadup].Play();
    }

    private void StopLoadup()
    {
        soundSources[SoundType.Loadup].Stop();
    }
}
