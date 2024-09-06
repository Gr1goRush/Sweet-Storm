using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

namespace Unity.Advertisement.IosSupport.Samples
{
    public class ContextScreenManager : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadSS());
        }

        private IEnumerator LoadSS()
        {
#if UNITY_IOS && !UNITY_EDITOR
            var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
 
            if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
                
                if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
                    PlayerPrefs.SetInt("idfaSS", 1);
            }
#endif
            SceneManager.LoadScene(1);
            yield return null;
        }
    }
}