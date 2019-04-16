using System.Collections;
using DigitalSalmon;
using UnityEngine;

public class ObsExample : MonoBehaviour {

	/// <summary>
	/// Start recording to a custom collection/scene
	/// </summary>
	public void StartRecording() {
		// You can record with a custom config by creating an ObsConfigInfo.
		OpenBroadcastStudio.ObsConfigInfo configInfo =
			new OpenBroadcastStudio.ObsConfigInfo {
				AllowOpenGl = true,
				AlwaysOnTop = true,
				CollectionName = "My Collection Name",
				SceneName = "My Scene Name", Verbose = true
			};

		OpenBroadcastStudio.StartRecording(configInfo);
	}

	/// <summary>
	/// Record for a fixed duration
	/// </summary>
	/// <param name="duration"></param>
	public void RecordForDuration(float duration) {
		// You can modify the active config
		OpenBroadcastStudio.ActiveConfig.AllowOpenGl = false;
		OpenBroadcastStudio.ActiveConfig.MinimizeToTray = true;

		// And record using the active config, but not defining a custom config.
		OpenBroadcastStudio.StartRecording();

		// Using a coroutine, you can stop the recording after a fixed duration.
		StartCoroutine(StopOpenBroadcastStudioAfterSeconds(5));
	}
	
	/// <summary>
	/// Start the OBS Studio Replay Buffer.
	/// </summary>
	public void StartReplayBuffer()
	{
		// As above, can be called with/without a config override.
		OpenBroadcastStudio.StartReplayBuffer();
	}

	/// <summary>
	/// Start OBS Studio and stream.
	/// </summary>
	public void StartStreaming()
	{
		// To configure streaming, open OpenBroadcastStudio (The program), and configure Settings.
		OpenBroadcastStudio.StartStreaming();
	}

	/// <summary>
	/// Stop recording
	/// </summary>
	public void StopRecording() {
		OpenBroadcastStudio.Stop();
	}

	private IEnumerator StopOpenBroadcastStudioAfterSeconds(float seconds) {
		yield return new WaitForSeconds(seconds);
		StopRecording();
	}
}