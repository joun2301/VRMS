using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public StarCamera starCamera;
    public Transform[] celestialBodies;

    public void MoveToMercury() => starCamera.MoveToTarget(celestialBodies[0]);
    public void MoveToVenus() => starCamera.MoveToTarget(celestialBodies[1]);
    public void MoveToEarth() => starCamera.MoveToTarget(celestialBodies[2]);
    public void MoveToMars() => starCamera.MoveToTarget(celestialBodies[3]);
    public void MoveToJupiter() => starCamera.MoveToTarget(celestialBodies[4]);
    public void MoveToSaturn() => starCamera.MoveToTarget(celestialBodies[5]);
    public void MoveToUranus() => starCamera.MoveToTarget(celestialBodies[6]);
    public void MoveToNeptune() => starCamera.MoveToTarget(celestialBodies[7]);
}
