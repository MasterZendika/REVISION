using UnityEngine;

public class CornPlant : MonoBehaviour
{
    [SerializeField]
    private float minWeight = 0.2f; // Peso mínimo de la mazorca
    [SerializeField]
    private float maxWeight = 0.3f; // Peso máximo de la mazorca
    [SerializeField]
    private float minVolume = 0.001f; // Volumen mínimo de la mazorca
    [SerializeField]
    private float maxVolume = 0.002f; // Volumen máximo de la mazorca

    public CornCob[] HarvestCobs()
    {
        int numberOfCobs = Random.Range(2, 4); // Genera de 2 a 3 mazorcas
        CornCob[] cobs = new CornCob[numberOfCobs];

        for (int i = 0; i < numberOfCobs; i++)
        {
            cobs[i] = new CornCob
            {
                weight = Random.Range(minWeight, maxWeight),
                volume = Random.Range(minVolume, maxVolume)
            };
        }

        return cobs;
    }
}

public class CornCob
{
    public float weight;
    public float volume;
}
