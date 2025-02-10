using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SuctionToolInteraction : MonoBehaviour
{
    public XRGrabInteractable suctionTool; // Reference to the XR Grab Interactable
    public GameObject phoneScreen;         // Reference to the phone screen
    private bool isAttached = false;       // Check if screen is attached to the suction tool

    private void OnTriggerEnter(Collider other)
    {
        // Check if the suction tool collides with the phone screen
        if (other.CompareTag("PhoneScreen") && !isAttached) // Only interact if not already attached
        {
            StartCoroutine(DetachAndAttachScreen(other.gameObject));
        }
    }

    IEnumerator DetachAndAttachScreen(GameObject screen)
    {
        // Detach the screen from the phone (disabling XR Grab)
        screen.GetComponent<XRGrabInteractable>().enabled = false; // Disable grabbing for the screen
        screen.GetComponent<Collider>().enabled = false; // Disable the collider so it doesn’t interact further

        // Parent the screen to the suction tool for 3 seconds
        screen.transform.parent = suctionTool.transform; // Attach the screen to the suction tool
        isAttached = true;

        // Optional: Add a sound or effect here when suction tool attaches
        // PlaySuctionSound();

        // Wait for 3 seconds while the screen is attached to the suction tool
        yield return new WaitForSeconds(3f);

        // After 3 seconds, remove the screen from the suction tool
        screen.transform.parent = null; // Unparent the screen from the suction tool
        screen.GetComponent<Collider>().enabled = true; // Re-enable the screen collider
        screen.GetComponent<XRGrabInteractable>().enabled = true; // Re-enable XR grabbing

        isAttached = false;

        // Optional: Add a sound or effect here when screen detaches
        // PlayDetachmentSound();
    }

    // You can add sound effects or particle effects here if you want.
    // Example: Play a sound effect when the suction tool is applied.
    // private void PlaySuctionSound()
    // {
    //     AudioSource.PlayClipAtPoint(suctionSound, transform.position);
    // }
}
