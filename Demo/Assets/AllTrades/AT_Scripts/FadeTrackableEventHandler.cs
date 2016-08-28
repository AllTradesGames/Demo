/*==============================================================================
Copyright (c) 2016 AllTrades Games, Ltd.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System.Collections;

namespace Vuforia
{
    /// <summary>
    /// A custom event handler that fades in/out augmented game and UI elements. 
    /// Game elements and game UI are faded out when trackers are lost, while the bracket UI is faded in. 
    /// </summary>
    public class FadeTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
    {
        #region PUBLIC_MEMBER_VARIABLES

        public float fadeSpeed = 0.1f;
        public Material gameRenderMaterial;

        #endregion // PUBLIC_MEMBER_VARIABLES


        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;
        private int firstLoss = 0;
        private GameObject bracketCanvas;
        private GameObject gameCanvas;

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Awake()
        {
            bracketCanvas = GameObject.FindGameObjectWithTag("BracketUICanvas");
            gameCanvas = GameObject.FindGameObjectWithTag("GameUICanvas");
        }

        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Time.timeScale = 1f;
            bracketCanvas.SetActive(false);
            gameCanvas.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Fade(1f));

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            if (firstLoss < 2)
            {
                firstLoss++;
            }
            else
            {
                Time.timeScale = 0f;
            }
            bracketCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Fade(0f));

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }


        IEnumerator Fade(float alphaTarget)
        {
            while(Mathf.Abs(alphaTarget - gameRenderMaterial.color.a) > 0.01f )
            {
                gameRenderMaterial.color = new Color(gameRenderMaterial.color.r, gameRenderMaterial.color.g, gameRenderMaterial.color.b, Mathf.Lerp(gameRenderMaterial.color.a, alphaTarget, fadeSpeed*Time.fixedDeltaTime));

                yield return null;
            }

            gameRenderMaterial.color = new Color(gameRenderMaterial.color.r, gameRenderMaterial.color.g, gameRenderMaterial.color.b, alphaTarget);
        }

            #endregion // PRIVATE_METHODS
        }
}
