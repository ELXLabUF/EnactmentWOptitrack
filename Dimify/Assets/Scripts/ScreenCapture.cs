using UnityEngine;
using System.Collections;
using System.IO;

public enum CaptureMethod
{
    AppCapture_Asynch,
    AppCapture_Synch,
    ReadPixels_Asynch,
    ReadPixels_Synch,
    RenderToTex_Asynch,
    RenderToTex_Synch
}

public class ScreenCapture : MonoBehaviour
{
     
    private bool isTextureLoaded;
    WWW www;
    IEnumerator fetchTexture(string url)
    {
        print("Textur e  NOT loaded");
        www = new WWW(url);
        yield return www;
        isTextureLoaded = true;
        print("Texture loaded");       
    }
    Object[] textures;
    Texture2D texture; 
    void Start()
    {
      /*  string url = "file://C://Users//Kumar//Documents//Dime//Assets//Resources//Textures//screen5.png";
        textures = Resources.LoadAll ("Textures");
        texture = (Texture2D)textures[0];
       // StartCoroutine(fetchTexture(url));
        Directory.CreateDirectory(Application.dataPath + "/Resources/Textures");*/
        print("Outside function");
    }
    void OnGUI() //For testing
    {
        GUI.Button(new Rect(0f,0f, Screen.width, Screen.height), texture);
      /*
        if(GUI.Button(new Rect(100 * 0, 0, 100, 30), "AppCapture_Asynch"))
            SaveScreenshot(CaptureMethod.AppCapture_Asynch, Application.dataPath + "/screen1.png");
        else if(GUI.Button(new Rect(100 * 1, 0, 100, 30), "AppCapture_Synch"))
            SaveScreenshot(CaptureMethod.AppCapture_Synch, Application.dataPath + "/screen2.png");
        else if(GUI.Button(new Rect(100 * 2, 0, 100, 30), "ReadPixels_Asynch"))
            SaveScreenshot(CaptureMethod.ReadPixels_Asynch, Application.dataPath + "/screen3.png");
        else if(GUI.Button(new Rect(100 * 3, 0, 100, 30), "ReadPixels_Synch"))
            SaveScreenshot(CaptureMethod.ReadPixels_Synch, Application.dataPath + "/screen4.png");
        //WITHOUT GUI MESS 
        else if(GUI.Button(new Rect(100 * 4, 0, 100, 30), "RenderToTex_Asynch"))
            SaveScreenshot(CaptureMethod.RenderToTex_Asynch, Application.dataPath + "/Resources/Textures/screen5.png");
        else if(GUI.Button(new Rect(100 * 5, 0, 100, 30), "RenderToTex_Synch"))
            SaveScreenshot(CaptureMethod.RenderToTex_Synch, Application.dataPath + "/screen6.png");
       */
    }

    public void SaveScreenshot(CaptureMethod method, string filePath)
    {
        if (method == CaptureMethod.AppCapture_Asynch)
        {
            Application.CaptureScreenshot(filePath);
        }
        else if (method == CaptureMethod.AppCapture_Synch)
        {
            Texture2D texture = GetScreenshot(CaptureMethod.AppCapture_Synch);
            byte[] bytes = texture.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
        }
        else if (method == CaptureMethod.ReadPixels_Asynch)
        {
            StartCoroutine(SaveScreenshot_ReadPixelsAsynch(filePath));
        }
        else if (method == CaptureMethod.ReadPixels_Synch)
        {
            Texture2D texture = GetScreenshot(CaptureMethod.ReadPixels_Synch);

            byte[] bytes = texture.EncodeToPNG();

            //Save our test image (could also upload to WWW)
            File.WriteAllBytes(filePath, bytes);

            //Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
            DestroyObject(texture);
        }
        else if (method == CaptureMethod.RenderToTex_Asynch)
        {
            StartCoroutine(SaveScreenshot_RenderToTexAsynch(filePath));
        }
        else
        {
            Texture2D screenShot = GetScreenshot(CaptureMethod.RenderToTex_Synch);
            byte[] bytes = screenShot.EncodeToPNG();
            File.WriteAllBytes(filePath, bytes);
        }
    }

    private IEnumerator SaveScreenshot_ReadPixelsAsynch(string filePath)
    {
        //Wait for graphics to render
        yield return new WaitForEndOfFrame();

        //Create a texture to pass to encoding
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        //Put buffer into texture
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

        //Split the process up--ReadPixels() and the GetPixels() call inside of the encoder are both pretty heavy
        yield return 0;

        byte[] bytes = texture.EncodeToPNG();

        //Save our test image (could also upload to WWW)
        File.WriteAllBytes(filePath, bytes);

        //Tell unity to delete the texture, by default it seems to keep hold of it and memory crashes will occur after too many screenshots.
        DestroyObject(texture);
    }

    private IEnumerator SaveScreenshot_RenderToTexAsynch(string filePath)
    {
        //Wait for graphics to render
        yield return new WaitForEndOfFrame();
           
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        //Camera.main.targetTexture = rt;
        //Camera.main.Render();

        //Render from all!
        foreach (Camera cam in Camera.allCameras)
        {
            cam.targetTexture = rt;
            cam.Render();
            cam.targetTexture = null;
        }

        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; //Added to avoid errors
        Destroy(rt);

        //Split the process up
        yield return 0;

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);
    }

    private static int tempFileCount = 0;
    ///<summary>Must use a Synch capture type to work.</summary>
    public Texture2D GetScreenshot(CaptureMethod method)
    {
        if (method == CaptureMethod.AppCapture_Synch)
        {
            string tempFilePath = System.Environment.GetEnvironmentVariable("TEMP") + "/screenshotBuffer" + tempFileCount + ".png";
            tempFileCount++;
            Application.CaptureScreenshot(tempFilePath);
            WWW www = new WWW("file://" + tempFilePath.Replace(Path.DirectorySeparatorChar.ToString(), "/"));

            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            while (!www.isDone) { }
            www.LoadImageIntoTexture((Texture2D)texture);
            File.Delete(tempFilePath); //Can delete now

            return texture;
        }
        else if (method == CaptureMethod.ReadPixels_Synch)
        {
            //Create a texture to pass to encoding
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            //Put buffer into texture
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); //Unity complains about this line's call being made "while not inside drawing frame", but it works just fine.*

            return texture;
        }
        else if (method == CaptureMethod.RenderToTex_Synch)
        {
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
            Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

            //Camera.main.targetTexture = rt;
            //Camera.main.Render();

            //Render from all!
            foreach (Camera cam in Camera.allCameras)
            {
                cam.targetTexture = rt;
                cam.Render();
                cam.targetTexture = null;
            }

            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0);
            Camera.main.targetTexture = null;
            RenderTexture.active = null; //Added to avoid errors
            Destroy(rt);

            return screenShot;
        }
        else
            return null;
    }
}