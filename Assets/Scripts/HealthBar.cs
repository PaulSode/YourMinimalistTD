using UnityEngine;

/// <summary>
/// Displays a configurable health bar for any object with a Damageable as a parent
/// </summary>
public class HealthBar : MonoBehaviour {

    MaterialPropertyBlock matBlock;
    MeshRenderer meshRenderer;
    Camera mainCamera;
    Enemy damageable;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        // get the Enemy script to get its life
        damageable = transform.parent.GetChild(0).GetComponent<Enemy>();
    }

    private void Start() {
        // Cache since Camera.main is super slow
        mainCamera = Camera.main;
    }

    private void Update() {
        // Only display on partial health
        if (damageable.hp < damageable.mHp) {
            meshRenderer.enabled = true;
            UpdateParams();
            StayUp();
        } else {
            meshRenderer.enabled = false;
        }
    }

    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        var percent = (float)(damageable.hp / damageable.mHp);
        matBlock.SetFloat("_Fill", percent);
        meshRenderer.SetPropertyBlock(matBlock);
    }
    
    private void StayUp() {
        var camXform = mainCamera.transform;
        var forward = transform.position - camXform.position;
        forward.Normalize();
        var up = Vector3.Cross(forward, camXform.right);
        transform.rotation = Quaternion.LookRotation(forward, up);
    }

}