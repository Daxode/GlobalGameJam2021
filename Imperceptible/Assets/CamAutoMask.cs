using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CamAutoMask : MonoBehaviour {
	[Header("Culling Masks")] [SerializeField]
	private LayerMask noPlats;

	[SerializeField] private LayerMask fromWinston;
	[SerializeField] private LayerMask fromSparrow;
	[SerializeField] private LayerMask fromPortal;

	private List<Camera> allPortalCameras = new List<Camera>();
	private CamSwap      _camSwap;
	private Camera       thisCam;

	void Start() {
		thisCam = GetComponent<Camera>();
		var allPortals = GameObject.FindGameObjectsWithTag("PortalTag");

		foreach (var portal in allPortals) {
			allPortalCameras.Add(portal.GetComponentInChildren<Camera>());
		}

		_camSwap = GetComponent<CamSwap>();
		_camSwap.onViewChange += OnCamSwapONViewChange;
	}

	void OnCamSwapONViewChange(ViewActive viewActive) {
		switch (viewActive) {
			case ViewActive.SparrowView:
				thisCam.cullingMask = fromSparrow;
				allPortalCameras.ForEach((camera => camera.cullingMask = fromPortal));
				break;
			case ViewActive.WinstonView:
				thisCam.cullingMask = fromWinston;
				allPortalCameras.ForEach((camera => camera.cullingMask = fromWinston));
				break;
			case ViewActive.DrawSparrowView:
				thisCam.cullingMask = noPlats;
				allPortalCameras.ForEach((camera => camera.cullingMask = noPlats));
				break;
		}
	}
}