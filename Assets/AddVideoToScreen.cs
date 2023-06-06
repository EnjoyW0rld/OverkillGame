using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

public class AddVideoToScreen : MonoBehaviour
{
    [SerializeField] string videoNames;

    [SerializeField] VideoClip clip;
    [SerializeField] float glitchCooldown = 7f;
    [SerializeField] float glitchAmount = .5f;
    [SerializeField] bool fishEye = false;


    [Header("Textures for setting up")]
    [SerializeField] RenderTexture texture;
    [SerializeField] Material material;


    public void AddClipToScripts()
    {
        /*
        if (videoNames == "")
        {
            Debug.LogError("NO NAME IN :" + videoNames);
            return;
        }

        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.clip = clip;
        videoPlayer.isLooping = true;

        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        if (texture == null)
        {
            RenderTexture texture = new RenderTexture();

            string localPath = "Assets/Videos/Render Textures/" + videoNames + ".renderTexture";
            EditorUtility.SetDirty()

        }
        if (material == null)
        {
            //Make new Material
        }
        */


    }
}
