using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CamAutoMask : MonoBehaviour {
    [Header("Culling Masks")]
    [SerializeField] private LayerMask noPlats;
    [SerializeField] private LayerMask fromWinston;
    [SerializeField] private LayerMask fromPortal;
    
    private Camera[] allPortalCameras;
    private CamSwap  _camSwap;
    private Camera   thisCam;
    
    void Start() {
        thisCam = GetComponent<Camera>();
        var allPortals          = GameObject.FindGameObjectsWithTag("PortalTag");
        var allPortalCameraList = new List<Camera>();
        
        foreach (var portal in allPortals) {
            allPortalCameraList.Add(portal.GetComponentInChildren<Camera>());
        }

        allPortalCameras = allPortalCameraList.ToArray();
        _camSwap = GetComponent<CamSwap>();
        _camSwap.onViewChange += (viewActive) => {
            switch (viewActive) {
                case ViewActive.SparrowView:
                    thisCam.cullingMask = noPlats;
                    allPortalCameraList.ForEach((camera => camera.cullingMask = fromPortal));
                    break;
                case ViewActive.WinstonView:
                    thisCam.cullingMask = fromWinston;
                    allPortalCameraList.ForEach((camera => camera.cullingMask = fromWinston));
                    break;
                case ViewActive.DrawView:
                    thisCam.cullingMask = noPlats;
                    allPortalCameraList.ForEach((camera => camera.cullingMask = noPlats));
                    break;
            }
        };
    }
}
