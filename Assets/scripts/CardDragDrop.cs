using UnityEngine;

public class CardDragDrop : MonoBehaviour {

    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector3 offset;

    private void Update() {
        if (isDragging) {
            // Update the position of the dragged card based on the mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition + offset;
        }
    }

    private void OnMouseDown() {

        Debug.Log("OnMouseDown");

        // Start dragging the card
        isDragging = true;
        initialPosition = transform.position;

        // Calculate the offset between the mouse position and the card's position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
    }

    private void OnMouseUp() {

        Debug.Log("OnMouseUp");

        // Stop dragging the card
        isDragging = false;

        // Check if the card was dropped onto a valid drop zone (e.g., a card slot)
        // Implement your logic here for determining the valid drop zones and handling the card placement accordingly
        // You can use Raycasting or Collider-based detection to check for valid drop zones
    }

    private void OnMouseExit() {

        Debug.Log("OnMouseExit");

        // Reset the card's position if it's not being dropped onto a valid drop zone
        if (isDragging) {
            transform.position = initialPosition;
        }
    }
}
