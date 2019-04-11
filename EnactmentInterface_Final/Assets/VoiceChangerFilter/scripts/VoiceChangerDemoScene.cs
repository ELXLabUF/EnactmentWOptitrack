using UnityEngine;
using System.Collections;

public class VoiceChangerDemoScene : MonoBehaviour {

    bool useMicrophone = true;
    public AudioClip sampleVoiceClip;

    public AudioSource targetAudioSource;
    public VoiceChangerFilter targetFilter;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0f, 0f, Screen.width / 2, Screen.height));
        GUILayout.Label("Voice Changer Demo Scene.\nYou can change pitch and formant.");

        GUILayout.Space(30f);

        GUILayout.Label("Pitch: " + targetFilter._pitch);
        targetFilter._pitch = GUILayout.HorizontalSlider(targetFilter._pitch, 0.3f, 3f);

        GUILayout.Label("Formant: " + targetFilter._formant);
        targetFilter._formant = GUILayout.HorizontalSlider(targetFilter._formant, 0f, 3f);

        if (useMicrophone)
        {
            if (Microphone.devices.Length == 0)
            {
                GUILayout.Label("Can't find any microphone. Please check connect correctly.");
            }
            else
            {
                GUILayout.Label("Using microphone. Please speaking something to microphone.");
            }

            GUILayout.Space(20f);
            if (GUILayout.Button("Use sample voice"))
            {
                targetAudioSource.clip = sampleVoiceClip;
                targetAudioSource.Play();
                useMicrophone = false;
            }
        }
        else
        {
            GUILayout.Label("Using sample voice.");

            GUILayout.Space(20f);
            if (GUILayout.Button("Use microphone"))
            {
                if (Microphone.devices.Length != 0)
                {
                    targetAudioSource.clip = Microphone.Start(null, true, 5, AudioSettings.outputSampleRate);
                    while (Microphone.GetPosition(null) <= 0)
                    {}
                    targetAudioSource.Play();
                }
                else
                {
                    targetAudioSource.clip = null;
                }
                useMicrophone = true;
            }
        }

        GUILayout.Space(30f);

        GUILayout.Label("Preset");
        if (GUILayout.Button("Male to Female"))
        {
            targetFilter._pitch = 2.0f;
            targetFilter._formant = 1.2f;
        }
        if (GUILayout.Button("Female to Male"))
        {
            targetFilter._pitch = 0.5f;
            targetFilter._formant = 0.82f;
        }

        GUILayout.EndArea();
    }
}
