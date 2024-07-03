using System.Collections;
using System.IO;
using UnityEngine;

public class OpenViBEZSignalMovement : MonoBehaviour
{
    public string filePath = "D:/Personal Projects/Virtual Reality Development/Unity/IIT KGP/output/sinusoidal";
    public float speed = 1.0f; // Speed factor to control the movement speed
    private float zMovement = 0f;

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
                            if (lastLine.Length > 2) // Ensure there are at least three values
                            {
                                if (float.TryParse(lastLine[2], out float signalValue)) // Read the Z value
                                {
                                    zMovement = signalValue; // Use the signal value directly
                                    Debug.Log("Signal value: " + signalValue); // Debug the signal value
                                }
                                else
                                {
                                    Debug.LogWarning("Failed to parse the signal value: " + lastLine[2]);
                                }
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
        // Move the GameObject up or down based on the signal value
        transform.position += new Vector3(0, zMovement * speed * Time.deltaTime, 0);
    }
}
