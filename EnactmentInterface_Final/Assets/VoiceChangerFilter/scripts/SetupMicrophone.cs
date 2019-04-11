using UnityEngine;
using System.Collections;

public class SetupMicrophone : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        var audio = GetComponent<AudioSource>();
        if (Microphone.devices.Length == 0)
            yield break;
        audio.clip = Microphone.Start(null, true, 5, AudioSettings.outputSampleRate);
        while (Microphone.GetPosition(null) <= 0) {
            yield return 0;
        }
        audio.Play();
    }
}
