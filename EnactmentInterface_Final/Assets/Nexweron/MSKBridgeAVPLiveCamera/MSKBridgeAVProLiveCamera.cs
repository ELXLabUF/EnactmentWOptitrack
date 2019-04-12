using UnityEngine;
using Nexweron.Core.MSK;
using RenderHeads.Media.AVProLiveCamera; //uncomment for new version

public class MSKBridgeAVProLiveCamera : TargetRenderBase
{
	public AVProLiveCamera avpLiveCamera;
	public MSKController mskController;

	private RenderTexture _texture;
	public RenderTexture texture {
		get { return _texture; }
	}

	private Texture _sourceTexture;
	private void SetSourceTexture(Texture value, bool isForce = false) {
		if (_sourceTexture != value || isForce) {
			if (mskController != null) {
				_sourceTexture = value;
				mskController.SetSourceTexture(_sourceTexture);
			} else {
				Debug.LogError("MSKBridgeAVPLiveCamera | mskController = null");
			}
		}
	}

	private int _framesCounter = 0;

	void OnEnable() {
		UpdateTarget();
	}

	void Update() {
		if(avpLiveCamera != null) {
			var framesCounter = avpLiveCamera.Device.FramesTotal;
			if (framesCounter > 0) {
				if (_framesCounter != framesCounter) {
					_framesCounter = framesCounter;

					SetSourceTexture(avpLiveCamera.OutputTexture);
					Render();
				}
			}
		} else {
			Debug.LogError("MSKBridgeAVPLiveCamera | avpLiveCamera = null");
		}
	}

	private void Render() {
		_texture = mskController.RenderIn();
		UpdateTargetTexture(_texture);
	}
}
