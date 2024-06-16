using System.Collections;
using System.IO;
using UnityEngine;

public class OpenViBEFileReader : MonoBehaviour
{
    public string filePath = "";
    public float speed = 1.0f; // Speed factor to control the movement speed
    private Vector3 movement;

    void Start()
    {
        StartCoroutine(ReadData());
    }

    IEnumerator ReadData()
    {
        while (true)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    // Use FileStream with FileShare.ReadWrite
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        string content = reader.ReadToEnd();
                        string[] lines = content.Split('\n');
                        if (lines.Length > 0)
                        {
                            string[] lastLine = lines[lines.Length - 1].Split(',');
                            if (lastLine.Length >= 3) // Ensure there are enough values
                            {
                                float x = float.Parse(lastLine[0]);
                                float y = float.Parse(lastLine[1]);
                                float z = float.Parse(lastLine[2]);
                                movement = new Vector3(x, y, z);
                            }
                        }
                    }
                }
                catch (IOException e)
                {
                    Debug.LogError("Error reading file: " + e.Message);
                }
            }
            yield return new WaitForSeconds(1f); // Read data every second
        }
    }

    void Update()
    {
        transform.position += movement * speed * Time.deltaTime;
    }
}
