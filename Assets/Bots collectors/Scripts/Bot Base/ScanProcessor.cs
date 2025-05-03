using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]

public class ScanProcessor : MonoBehaviour
{
    private ResourceScanner _scanner;

    public Dictionary<EnumResourceType, List<Resource>> KnownResources { get; private set; }

    private void Awake()
    {
        _scanner = GetComponent<ResourceScanner>();

        KnownResources = new();
    }

    private void OnEnable()
    {
        _scanner.ScanCompleted += ProcessScanResults;
    }

    private void OnDisable()
    {
        _scanner.ScanCompleted -= ProcessScanResults;

        ClearKnownResources();
    }

    private void ProcessScanResults()
    {
        foreach (Resource resourse in _scanner.LastResults)
        {
            if (KnownResources.ContainsKey(resourse.Type) == false)
            {
                KnownResources.Add(resourse.Type, new() { resourse });
                resourse.Disabled += RemoveResourceOnDisable;
            }
            else if (KnownResources[resourse.Type].Contains(resourse) == false)
            {
                KnownResources[resourse.Type].Add(resourse);
                resourse.Disabled += RemoveResourceOnDisable;
            }
        }
    }

    private void RemoveResourceOnDisable(Resource resource)
    {
        resource.Disabled -= RemoveResourceOnDisable;
        KnownResources[resource.Type].Remove(resource);
    }

    private void ClearKnownResources()
    {
        foreach (List<Resource> resourseList in KnownResources.Values)
        {
            foreach (Resource resource in resourseList)
            {
                resource.Disabled -= RemoveResourceOnDisable;
            }
        }
        
        KnownResources.Clear();
    }
}
