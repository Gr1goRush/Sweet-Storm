using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class PostBuildStep
{
    // Set the IDFA request description:
    static string k_TrackingDescription = Application.productName.ToString() + " requests permission to track user data for analytics, aiming to improve the game by understanding when players usually close it.";
    static string k_LocationDescription = Application.productName.ToString() + " requests access geolocation data for its analytics to enhance the game, gathering this information to identify popular regions.";

    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            AddPListValues(pathToXcode);
        }
    }

    // Implement a function to read and write values to the plist file:
    static void AddPListValues(string pathToXcode)
    {
        // Retrieve the plist file from the Xcode project directory:
        string plistPath = pathToXcode + "/Info.plist";
        PlistDocument plistObj = new PlistDocument();


        // Read the values from the plist file:
        plistObj.ReadFromString(File.ReadAllText(plistPath));

        // Set values from the root object:
        PlistElementDict plistRoot = plistObj.root;

        // Set the description key-value in the plist:
        plistRoot.SetString("NSUserTrackingUsageDescription", k_TrackingDescription);
        plistRoot.SetString("NSLocationWhenInUseUsageDescription", k_LocationDescription);
        plistRoot.SetString("NSPhotoLibraryAddUsageDescription", "The app requires access to Photos to save media to it.");
        plistRoot.SetString("NSPhotoLibraryUsageDescription", "The app requires access to Photos to save media to it.");
        plistRoot.SetString("NSAdvertisingAttributionReportEndpoint", "https://appsflyer-skadnetwork.com/");

        // Save changes to the plist:
        File.WriteAllText(plistPath, plistObj.WriteToString());
    }
}
