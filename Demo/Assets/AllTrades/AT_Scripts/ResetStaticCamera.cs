using UnityEngine;
using System.Collections;

public class ResetStaticCamera : MonoBehaviour
{
    public float timeDelay;
    public Material gameRenderMaterial;
    public float gameRenderScale = 100f;

    private Vector3 origPos;
    private Quaternion origRot;
    private Vector3 origScale;
    private Transform backgroundPlane;
    private Transform gameRenderPlane;
    private Camera thisCamera;
    private RenderTexture gameTexture;


    void Start()
    {        
        backgroundPlane = Camera.main.transform.FindChild("BackgroundPlane").transform;
        gameRenderPlane = GameObject.FindGameObjectWithTag("GameRender").transform;
        thisCamera = GetComponent<Camera>();
        StartCoroutine("ResetCamera");
    }

    IEnumerator ResetCamera()
    {
        yield return new WaitForSecondsRealtime(timeDelay);

        thisCamera.projectionMatrix = Camera.main.projectionMatrix;

        // RenderTextures only render a square subset of the target camera by default, this changes the texture to render the whole camera view
        gameTexture = new RenderTexture(Camera.main.pixelWidth, Camera.main.pixelHeight, 24);
        gameRenderMaterial.mainTexture = gameTexture;

        // Record background plane's position relative to ARCamera
        origPos = backgroundPlane.localPosition;
        origRot = backgroundPlane.localRotation;
        origScale = backgroundPlane.localScale;

        // Parent background plane to Static Camera with same relative position
        backgroundPlane.parent = transform;
        backgroundPlane.localPosition = origPos;
        backgroundPlane.localRotation = origRot;
        backgroundPlane.localScale = origScale;

        // Match Static Camera's settings to ARCamera's
        thisCamera.aspect = Camera.main.aspect;
        thisCamera.fieldOfView = Camera.main.fieldOfView;
        thisCamera.farClipPlane = Camera.main.farClipPlane;
        thisCamera.nearClipPlane = Camera.main.nearClipPlane;

        // Adjust game render plane to fit Static Camera's view exactly
        gameRenderPlane.localScale = new Vector3(gameRenderScale, 1f, gameRenderScale / thisCamera.aspect);
        gameRenderPlane.localPosition = new Vector3(0f, 0f, (gameRenderScale/2f) / (Mathf.Tan(thisCamera.fieldOfView/2f*Mathf.Rad2Deg)) * 27.5f);        

        // Make ARCamera render its view to the game render plane
        Camera.main.targetTexture = gameTexture;

        Time.timeScale = 0f;
    }

	
}
