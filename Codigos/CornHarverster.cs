using UnityEngine;
using UnityEngine.UI;

public class CornHarvester : MonoBehaviour
{
    [SerializeField]
    private float storageCapacityWeight = 1000f; // Capacidad en peso
    [SerializeField]
    private float storageCapacityVolume = 3f; // Capacidad en volumen

    [SerializeField]
     private Text totalWeightText;
[    SerializeField]
     private Text totalVolumeText;
    [SerializeField]
    private Text totalCobsText;


    private float totalWeight = 0f;
    private float totalVolume = 0f;
    private int totalCobs = 0;

    public float StorageLoadFactor => Mathf.Max(totalWeight / storageCapacityWeight, totalVolume / storageCapacityVolume);

    public void HarvestPlant(CornPlant plant)
    {
        if (IsStorageFull())
        {
            Debug.Log("Almacenamiento lleno. No se puede cosechar más.");
            return;
        }

        CornCob[] cobs = plant.HarvestCobs();
        foreach (var cob in cobs)
        {
            if (IsStorageFull())
            {
                Debug.Log("Almacenamiento lleno durante la cosecha.");
                break;
            }
            StoreCornCob(cob);
        }
    }

private void StoreCornCob(CornCob cob)
{
    totalWeight += cob.weight;
    totalVolume += cob.volume;
    totalCobs++;

    // Actualizar UI
    if (totalWeightText != null) totalWeightText.text = $"Peso total: {totalWeight} kg";
    if (totalVolumeText != null) totalVolumeText.text = $"Volumen total: {totalVolume} m³";
    if (totalCobsText != null) totalCobsText.text = $"Mazorcas totales: {totalCobs}";

    Debug.Log($"Mazorca almacenada. Peso total: {totalWeight} kg, Volumen total: {totalVolume} m³, Mazorcas totales: {totalCobs}");
}

    private bool IsStorageFull()
    {
        return totalWeight >= storageCapacityWeight || totalVolume >= storageCapacityVolume;
    }
}
