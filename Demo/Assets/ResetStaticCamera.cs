using UnityEngine;
using System.Collections;

public class ResetStaticCamera : MonoBehaviour
{
    public float timeDelay;
    public RenderTexture gameTexture;

    private Vector3 origPos;
    private Quaternion origRot;
    private Vector3 origScale;
    private Transform backgroundPlane;

    private bool set = false;

    void Start()
    {
        backgroundPlane = Camera.main.transform.FindChild("BackgroundPlane").transform;


    }

    void Update()
    {
        if (!set && (Time.time > timeDelay))
        {
            GetComponent<Camera>().projectionMatrix = Camera.main.projectionMatrix;

            origPos = backgroundPlane.localPosition;
            origRot = backgroundPlane.localRotation;
            origScale = backgroundPlane.localScale;
            backgroundPlane.parent = transform;

            backgroundPlane.localPosition = origPos;
            backgroundPlane.localRotation = origRot;
            backgroundPlane.localScale = origScale;

            Camera.main.targetTexture = gameTexture;

            set = true;
        }
    }

	
}
