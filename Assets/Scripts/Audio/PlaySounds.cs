using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Splines;

public class PlaySounds : MonoBehaviour
{
    public enum SoundType { Bumper, Combo, GameOver, ExtraBall, BallLost, Loadup, RailEnter, RailRoll }

    private Dictionary<SoundType, AudioSource> soundSources = new Dictionary<SoundType, AudioSource>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length < 8)
        {
            Debug.LogError("PlaySounds requires 8 AudioSource components!");
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
}
