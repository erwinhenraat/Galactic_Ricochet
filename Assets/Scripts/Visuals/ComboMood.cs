using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ComboMood : MonoBehaviour
{
    [SerializeField] private VolumeProfile vol;
    [SerializeField] private Color noColorFilter;
    [SerializeField] private Color colorFilter;
    [SerializeField] private float effectPower = 0.2f;
    [SerializeField] private float effectTime = 0.02f;
    private void Start()
    {
        Combo.onComboAchieved += AddComboEffects;
        Combo.onComboLost += AddComboLoseEffects;
        Lives.onGameOver += AddGameOverEffects;
        ColorAdjustments col;
        if (vol.TryGet<ColorAdjustments>(out col))
        {
            col.saturation.value = 0;
            col.colorFilter.value = noColorFilter;
        }

        ChromaticAberration ca;
        if (vol.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.value = 0;
        }
        LensDistortion ld;
        if (vol.TryGet<LensDistortion>(out ld))
        {
            ld.intensity.value = 0;
        }
    }

    private void OnDisable()
    {
        Combo.onComboAchieved -= AddComboEffects;
        Combo.onComboLost -= AddComboLoseEffects;
        Lives.onGameOver -= AddGameOverEffects;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ChromaticAberration ca;
        if (vol.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.value -= effectTime;
        }
        LensDistortion ld;
        if (vol.TryGet<LensDistortion>(out ld))
        {
            ld.intensity.value = math.clamp(ld.intensity.value - effectTime,0,1);
        }
        ColorAdjustments col;
        if (vol.TryGet<ColorAdjustments>(out col))
        {
            col.colorFilter.value = Color.Lerp(col.colorFilter.value, noColorFilter, 0.02f);
        }
    }

    private void AddComboEffects(int _, string __)
    {
        ChromaticAberration ca;
        LensDistortion ld;
        if(vol.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.value += effectPower;
        }
        if (vol.TryGet<LensDistortion>(out ld))
        {
            ld.intensity.value += effectPower;
        }
    }

    private void AddComboLoseEffects(int combo, string __)
    {
        ColorAdjustments col;
        if (vol.TryGet<ColorAdjustments>(out col) && combo != 2)
        {
            col.colorFilter.value = colorFilter;
        }
    }

    private void AddGameOverEffects(string _)
    {
        ColorAdjustments col;
        if (vol.TryGet<ColorAdjustments>(out col))
        {
            col.saturation.value = -100;
        }
    }
}
