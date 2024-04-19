using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

public class Loader : MonoBehaviour
{
    public static bool loaded = false;

    public static Loader instance;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        StartCoroutine(LoadAll());
    }

    private IEnumerator LoadAll()
    {
        LoadResources();

        yield return StartCoroutine(
            LoadCards("C:\\Users\\Utilisateur\\Documents\\Programmation\\Projets JavaScript\\Test\\img-splitter\\output")
        );

        loaded = true;
        Game.instance.Ready();
    }

    public static IEnumerator LoadCards(string folderPath)
    {
        string[] files = Directory.GetFiles(folderPath, "*.png", SearchOption.TopDirectoryOnly);

        foreach (string file in files)
        {
            string url = $"file:///{file}";

            // Create a WWW object to load the file
            UnityWebRequest webRequest = new(url);
            DownloadHandlerTexture textureDownloader = new(true);
            webRequest.downloadHandler = textureDownloader;

            // Wait for the file to be completely downloaded
            yield return webRequest.SendWebRequest();

            // Check for errors
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Create a new Texture2D and load the downloaded data into it
                Texture2D texture = textureDownloader.texture;
                texture.filterMode = FilterMode.Point;

                // Add the loaded texture to the list
                SpritesReferences.instance.sprites.TryAdd(
                    $"card_{files.ToList().IndexOf(file)}",
                    Sprite.Create(texture, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f), 100f, 0, SpriteMeshType.Tight)
                );
            }
            else
            {
                Debug.LogError("Error loading file from " + url + ": " + webRequest.error);
            }
        }
    }

    public static void LoadResources()
    {
        Debug.Log("Loading resources");

        Texture2D[] textures = Resources.LoadAll("Sprites", typeof(Texture2D)).Cast<Texture2D>().ToArray();

        foreach(var texture in textures)
        {
            texture.filterMode = FilterMode.Point;
            Debug.Log($"Loading : {texture.name}");
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            SpritesReferences.instance.sprites.TryAdd(texture.name, sprite);
        }
        
        Debug.Log("Finished loading resources");
        loaded = true;
    }
}
