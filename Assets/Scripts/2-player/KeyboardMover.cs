using UnityEngine;

/**
 * This component allows the player to move by clicking the arrow keys.
 */
public class KeyboardMover : MonoBehaviour {

    [SerializeField]public float timeBetweenSteps = 1f;
    public float nextStep;
    protected Vector3 NewPosition() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            nextStep = Time.time + timeBetweenSteps;
            return transform.position + Vector3.left;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            nextStep = Time.time + timeBetweenSteps;
            return transform.position + Vector3.right;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            nextStep = Time.time + timeBetweenSteps;
            return transform.position + Vector3.down;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            nextStep = Time.time + timeBetweenSteps;
            return transform.position + Vector3.up;
        } else {
            return transform.position;
        }
    }


    void Update()  {
        transform.position = NewPosition();
    }
}
