using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class PortalEnabler : MonoBehaviour {
	IPortal[] portals;

	void Awake() {
		var portals = new List<IPortal>();
		var ss      = FindObjectsOfType<MonoBehaviour>().OfType<IPortal>();
		foreach (IPortal s in ss) {
			portals.Add (s);
		}
		
		//portals = FindObjectsOfType<IPortal>();
		this.portals = portals.ToArray();
	}

	void OnPreCull() {
		for (int i = 0; i < portals.Length; i++) {
			portals[i].PrePortalRender();
		}

		for (int i = 0; i < portals.Length; i++) {
			portals[i].Render();
		}

		for (int i = 0; i < portals.Length; i++) {
			portals[i].PostPortalRender();
		}
	}
}