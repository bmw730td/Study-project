using UnityEngine;

public class ResourceCarrier : MonoBehaviour
{
    private Resource _heldResource;

    public void GrabResource(Resource target)
    {
        if (_heldResource != null)
            ReleaseResource();

        _heldResource = target;
        target.transform.SetParent(transform);
    }

    public Resource ReleaseResource()
    {
        Resource resourceToRelease = _heldResource;

        _heldResource = null;
        
        resourceToRelease.transform.SetParent(null);

        return resourceToRelease;
    }
}
