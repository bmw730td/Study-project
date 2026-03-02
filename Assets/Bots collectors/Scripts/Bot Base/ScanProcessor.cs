using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]

public class ScanProcessor : MonoBehaviour
{
    private ResourceScanner _scanner;

    private Dictionary<ResourceType, List<Resource>> _knownResources;
    private List<Resource> _blacklist;

    private void Awake()
    {
        _scanner = GetComponent<ResourceScanner>();

        _knownResources = new();
        _blacklist = new();
    }

    private void OnEnable()
    {
        _scanner.ScanCompleted += ProcessScanResults;
    }

    private void OnDisable()
    {
        _scanner.ScanCompleted -= ProcessScanResults;

        ClearKnownResources();
        _blacklist.Clear();
    }

    public Resource GetKnownResource(ResourceType type)
    {
        Resource resourceToGive;

        if (_knownResources.ContainsKey(type))
        {
            resourceToGive = _knownResources[type].FirstOrDefault();

            if (resourceToGive != null)
            {
                _knownResources[type].Remove(resourceToGive);
                _blacklist.Add(resourceToGive);

                return resourceToGive;
            }
        }

        return null;
    }

    private void ProcessScanResults(List<Resource> results)
    {
        foreach (Resource resourse in results)
        {
            if (_blacklist.Contains(resourse) == false)
            {
                if (_knownResources.ContainsKey(resourse.Type) == false)
                {
                    _knownResources.Add(resourse.Type, new() { resourse });
                    resourse.Disabled += RemoveResourceOnDisable;
                }
                else if (_knownResources[resourse.Type].Contains(resourse) == false)
                {
                    _knownResources[resourse.Type].Add(resourse);
                    resourse.Disabled += RemoveResourceOnDisable;
                }
            }
        }
    }

    private void RemoveResourceOnDisable(Resource resource)
    {
        resource.Disabled -= RemoveResourceOnDisable;
        
        if (_blacklist.Contains(resource))
            _blacklist.Remove(resource);
    }

    private void ClearKnownResources()
    {
        foreach (List<Resource> resourseList in _knownResources.Values)
        {
            foreach (Resource resource in resourseList)
            {
                resource.Disabled -= RemoveResourceOnDisable;
            }
        }
        
        _knownResources.Clear();
    }
}
