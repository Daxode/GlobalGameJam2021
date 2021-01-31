using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Mirror : MonoBehaviour, IPortal {
	[SerializeField]          private LayerMask    mirrorsOnlyLayer;
	[Header("Main Settings")] public  MeshRenderer screen;
	public                            int          recursionLimit = 5;

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
		print("test0");
		CreateViewTexture();

		//int startIndex = 0;
		portalCam.projectionMatrix = playerCam.projectionMatrix;

		// Hide screen so that camera can see through mirror
		screen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		//screen.material.SetInt("displayMask", 0);
		print("test1");

		RaycastHit hit;
		if (!Physics.Raycast(playerCam.transform.position, playerCam.transform.forward,out hit,
		                     float.MaxValue, mirrorsOnlyLayer)) return;
		print("test2");
		
		float   distance      = Vector3.Distance(playerCam.transform.position, hit.point);
		var     playerForward = playerCam.transform.forward;
		Vector3 n             = -screen.transform.right;
		Vector3 reflect       = playerForward-2*Vector3.Dot(playerForward, n)*n;
		portalCam.transform.position = playerCam.transform.position + playerCam.transform.forward * distance + reflect*distance;
		portalCam.transform.LookAt(hit.point, playerCam.transform.up);
		portalCam.Render();
		print("test3");

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

	private void OnDrawGizmosSelected() {
		if (playerCam != null) {
			Gizmos.DrawRay(playerCam.transform.position, playerCam.transform.forward);
		}
	}
}