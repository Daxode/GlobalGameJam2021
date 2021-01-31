using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Mirror : MonoBehaviour, IPortal {
	[Header("Main Settings")] public  MeshRenderer screen;
	public                            int          recursionLimit = 5;

	// Private variables
	RenderTexture viewTexture;
	Camera        portalCam;
	Camera        playerCam;
	Material      firstRecursionMat;

	private Plane _plane;

	void Awake() {
		playerCam = Camera.main;
		portalCam = GetComponentInChildren<Camera>();
		portalCam.enabled = false;
		screen.material.SetInt("displayMask", 0);
		_plane = new Plane(screen.transform.forward, screen.transform.position);
	}

	// Called before any mirror cameras are rendered for the current frame
	public void PrePortalRender() { }
	
	public void Render() {
		CreateViewTexture();

		//int startIndex = 0;
		portalCam.projectionMatrix = playerCam.projectionMatrix;

		// Hide screen so that camera can see through mirror
		screen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
		var   playerForward = playerCam.transform.forward;
		float enter         = 0.0f;
		var   ray           = new Ray(playerCam.transform.position, playerForward);
		if (!_plane.Raycast(ray, out enter)) {
			ray = new Ray(playerCam.transform.position, playerCam.transform.right);
			if (!_plane.Raycast(ray, out enter)) {
				ray = new Ray(playerCam.transform.position, -playerCam.transform.right);
				if (!_plane.Raycast(ray, out enter)) {
					return;
				}
			}
		}
		Vector3 hitPoint = ray.GetPoint(enter);
		
		float   distance      = Vector3.Distance(playerCam.transform.position, hitPoint);
		Vector3 n             = -screen.transform.right;
		Vector3 reflect       = ray.direction-2*Vector3.Dot(ray.direction, n)*n;
		portalCam.transform.position = playerCam.transform.position + ray.direction * distance + reflect*distance;
		
		//var lookatdir = playerCam.transform.forward + playerCam.transform.right;
		portalCam.transform.rotation = Quaternion.LookRotation(-reflect, playerCam.transform.up);
		
		if (ray.direction == playerCam.transform.right) {
			portalCam.transform.Rotate(portalCam.transform.up, 90);
		} else if (ray.direction == -playerCam.transform.right) {
			portalCam.transform.Rotate(portalCam.transform.up, -90);
		}
		
		portalCam.Render();

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