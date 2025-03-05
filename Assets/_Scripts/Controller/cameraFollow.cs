using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Référence du joueur à suivre
    [SerializeField] private float smoothSpeed = 5f; // Vitesse de suivi
    [SerializeField] private Vector2 offset = new Vector2(0f, 2f); // Décalage de la caméra
    [SerializeField] private float maxHeight = 10f; // Hauteur maximale

    private void LateUpdate()
    {
        if (player == null) return;

        // Position cible de la caméra (suivant le joueur)
        Vector3 targetPosition = new Vector3(player.position.x + offset.x, 
                                             player.position.y + offset.y, 
                                             transform.position.z);

        // Appliquer la limite de hauteur
        targetPosition.y = Mathf.Min(targetPosition.y, maxHeight);

        // Lissage du mouvement de la caméra
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
