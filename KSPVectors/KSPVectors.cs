using System;
using System.IO;
using UnityEngine;
namespace KSPVectors
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class KSPVectors : MonoBehaviour
    {
        private Rect windowRect = new Rect(100, 100, 400, 400);
        int fileID = 0;

        public void Start()
        {
            while (File.Exists(Path.Combine(KSPUtil.ApplicationRootPath, "vectors-" + fileID + ".txt")))
            {
                fileID++;
            }
        }

        public void OnGUI()
        {
            windowRect = GUILayout.Window(157891152, windowRect, Draw, "KSPVectors");
        }

        private void Draw(int windowID)
        {
            GUI.DragWindow(new Rect(0, 0, 10000, 30));
            if (FlightGlobals.ready && FlightGlobals.fetch.activeVessel != null)
            {
                Vessel aV = FlightGlobals.fetch.activeVessel;
                CelestialBody body = FlightGlobals.fetch.activeVessel.mainBody;
                GUILayout.Label("V_srfRelRot: " + aV.srfRelRotation);
                GUILayout.Label("VRefT_Rot: " + aV.ReferenceTransform.rotation);
                GUILayout.Label("VRefT_LocalRot: " + aV.ReferenceTransform.localRotation);
                GUILayout.Label("B_Rot: " + body.rotation);
                GUILayout.Label("BTran_Rot: " + body.transform.rotation);
                GUILayout.Label("BBTran_Rot: " + body.bodyTransform.rotation);
                GUILayout.Label("V_terrNorm: " + aV.terrainNormal);
                GUILayout.Label("V_up: " + aV.up);
                GUILayout.Label("V_forward: " + aV.GetFwdVector());
                GUILayout.Label("B_surfNVector: " + body.GetSurfaceNVector(aV.latitude, aV.longitude));
                GUILayout.Label("B_relSurfNVector: " + body.GetRelSurfaceNVector(aV.latitude, aV.longitude));
                if (GUILayout.Button("Save vectors"))
                {


                    ConfigNode saveNode = new ConfigNode();
                    //this.srfRelRotation = Quaternion.Inverse (this.mainBody.bodyTransform.rotation) * this.vesselTransform.rotation;
                    saveNode.AddValue("V_srfRelRot", aV.srfRelRotation);
                    saveNode.AddValue("VRefT_Rot", aV.ReferenceTransform.rotation);
                    saveNode.AddValue("VRefT_LocalRot", aV.ReferenceTransform.localRotation);
                    saveNode.AddValue("B_Rot", body.rotation);
                    saveNode.AddValue("BTran_Rot", body.transform.rotation);
                    saveNode.AddValue("BBTran_Rot", body.bodyTransform.rotation);
                    saveNode.AddValue("V_terrNorm", aV.terrainNormal);
                    saveNode.AddValue("V_up", aV.up);
                    saveNode.AddValue("V_forward", aV.GetFwdVector());
                    saveNode.AddValue("B_surfNVector", body.GetSurfaceNVector(aV.latitude, aV.longitude));
                    saveNode.AddValue("B_relSurfNVector", body.GetRelSurfaceNVector(aV.latitude, aV.longitude));
                    saveNode.Save(Path.Combine(KSPUtil.ApplicationRootPath, "vectors-" + fileID + ".txt"));
                    fileID++;
                }
            }
            else
            {
                GUILayout.Label("Flightglobals not ready");
            }
        }
    }
}
