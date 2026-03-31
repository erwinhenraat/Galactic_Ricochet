using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;


public class PlaySounds : MonoBehaviour
{
    public enum SoundType { Bumper, Combo, GameOver, ExtraBall, BallLost, Loadup, RailEnter, RailRoll, FlipperHit, Pain1, Pain2, Pain3 }

    private Dictionary<SoundType, AudioSource> soundSources = new Dictionary<SoundType, AudioSource>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length < 12)
        {
            Debug.LogError("PlaySounds requires 12 AudioSource components!");
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
        soundSources[SoundType.Pain1] = sources[9];
        soundSources[SoundType.Pain2] = sources[10];
        soundSources[SoundType.Pain3] = sources[11];

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
    }
    private void PlayBumper(Transform _, int __)
    {
        int rnd = Random.Range(1, 4);
        if (rnd == 1) 
        {
            soundSources[SoundType.Pain1].Play();
        }
        if (rnd == 2)
        {
            soundSources[SoundType.Pain2].Play();
        }
        if (rnd == 3)
        {
            soundSources[SoundType.Pain3].Play();
        }
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
    private void PlayRailEnter(bool bl)
    {
        soundSources[SoundType.RailEnter].Play();
    }
    private void PlayRailRoll(bool bl)
    {
        soundSources[SoundType.RailRoll].Play();
    }
    private void StopRailRoll(bool bl)
    {
        soundSources[SoundType.RailRoll].Stop();
    }
    private void PlayFlipperHit(bool bl)
    {
        soundSources[SoundType.FlipperHit].Play();
    }
}
