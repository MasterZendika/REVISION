using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Arreglo de cámaras

    void Start()
    {
        // Desactivar todas las cámaras excepto la primera
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Verificar si se presiona alguna tecla numérica
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SwitchCamera(0); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SwitchCamera(1); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SwitchCamera(2); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SwitchCamera(3); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SwitchCamera(4); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { SwitchCamera(5); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { SwitchCamera(6); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { SwitchCamera(7); }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { SwitchCamera(8); }
    }

    void SwitchCamera(int index)
    {
        // Asegurarse de que el índice esté dentro del rango del arreglo
        if (index < 0 || index >= cameras.Length) return;

        // Activar la cámara seleccionada y desactivar las demás
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
        }
    }
}
