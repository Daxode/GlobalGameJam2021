using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Mirror : MonoBehaviour, IPortal {
	[Header("Main Settings")] public MeshRenderer screen;
	public                           int          recursionLimit = 5;

	// Private variables
	RenderTexture viewTexture;
	Camera        portalCam;
	Camera        playerCam;
	Material      firstRecursionMat;

	void Awake() {
		playerCam = Camera.main;
		portalCam = GetComponentInChildren<Camera>();
		portalCam.enabled = false;
		screen.material.SetInt("displayMask", 1);
	}

	// Called before any mirror cameras are rendered for the current frame
	public void PrePortalRender() { }
	
	public void Render() {
		CreateViewTexture();

		int startIndex = 0;
		portalCam.projectionMatrix = playerCam.projectionMatrix;

		// Hide screen so that camera can see through mirror
		screen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		//screen.material.SetInt("displayMask", 0);
		portalCam.transform.rotation = transform.rotation;
		portalCam.Render();

		/*for (int i = startIndex; i < recursionLimit; i++) {
			portalCam.transform.rotation = transform.rotation;
			//SetNearClipPlane ();
			portalCam.Render();

			if (i == startIndex) {
				screen.material.SetInt("displayMask", 1);
			}
		}*/

		// Unhide objects hidden at start of render
		screen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
	}
	public void PostPortalRender() { }

	void CreateViewTexture() {
		if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height) {
			if (viewTexture != null) {
				viewTexture.Release();
			}

			viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
			// Render the view from the portal camera to the view texture
			portalCam.targetTexture = viewTexture;
			// Display the view texture on the screen of the linked portal
			screen.material.SetTexture("_MainTex", viewTexture);
		}
	}
}