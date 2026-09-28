using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class BounceController : MonoBehaviour
{
    int bounces = 0;
    [HideInInspector] public UnityEvent onBounceOffGround = new UnityEvent();
    public int GetBounces() { return bounces; }

    public void SaveBounces()
    {
        Debug.Log("=== INICIO GUARDADO ===");
        string saveFolder = Path.Combine(Application.persistentDataPath, "SavedBounces");
        Debug.Log("Ruta de guardado: " + saveFolder);

        if (!Directory.Exists(saveFolder)) { Directory.CreateDirectory(saveFolder); 
                                             Debug.Log("La carpeta no existía. Se ha creado."); }
        else { Debug.Log("La carpeta ya existía."); }

        BouncesData data = new BouncesData();
        data.bounces = bounces;
        Debug.Log("Datos preparados. Rebotes: " + data.bounces);

        string json = JsonUtility.ToJson(data);
        Debug.Log("JSON generado: " + json);

        string saveFile = Path.Combine(saveFolder, "savedBounces.json");
        Debug.Log("Archivo de guardado: " + saveFile);

        File.WriteAllText(saveFile, json);
        Debug.Log("Archivo JSON guardado correctamente.");
    }

    public void LoadBounces()
    {
        string saveFolder = Path.Combine(Application.persistentDataPath, "SavedBounces");

        if (!Directory.Exists(saveFolder)) { Debug.Log("Carpeta de gaurdado no existe"); }

        string saveFile = Path.Combine(saveFolder, "savedBounces.json");

        if (!File.Exists(saveFile)) { Debug.Log("El archivo json no existe"); }

        string json = File.ReadAllText(saveFile);
        Debug.Log("json leído " + json);

        BouncesData data = JsonUtility.FromJson<BouncesData>(json);
        Debug.Log("Guardado cargado " + data.bounces);

        bounces = data.bounces;

        onBounceOffGround.Invoke();
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        bounces++;
        onBounceOffGround.Invoke();

        
    }


}
