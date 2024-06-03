using System.Collections;
using UnityEngine;

public class AIHunterController : MonoBehaviour
{
    [Header("Components")]
    private AIHunterAgent _hunter;
    private MeshRenderer _meshRenderer;
    public Material _yellowMat;
    public Material _redMat;
    public Material _greenMat;

    [Header("Values")]
    public float maxEnergy = 100f;
    public float currentEnergy = 0;
    public float energyCooldownRecovery = 5f;
    public bool energyRecovering = true;
    public float energyRecoveryCooldown = 5f;
    public bool isPatrolling = true;
    
    private Vector3 _lastPos;

    private void Start()
    {
        currentEnergy = maxEnergy;
        _lastPos = transform.position;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (currentEnergy > 0 && !isNearBoid())
        {
            isPatrolling = true;
            SetAgent<PatrolBehaviour>();
            currentEnergy -= Time.deltaTime;
            _lastPos = transform.position;
            SetMaterial(_yellowMat);
        }
        else if (currentEnergy <= 0)
        {
            StartCoroutine(EnergyRecovery());
        }

        if (currentEnergy > 0 && isNearBoid())
        {
            isPatrolling = false;
            SetAgent<PursueBehaviour>();
            currentEnergy -= Time.deltaTime;
            _lastPos = transform.position;
            SetMaterial(_redMat); ;

        }
        else if (currentEnergy <= 0)
        {
            StartCoroutine(EnergyRecovery());
        }
    }

    private IEnumerator EnergyRecovery()
    {
        currentEnergy = 0;
        Idle(_lastPos);
        yield return new WaitForSeconds(energyRecoveryCooldown);
        currentEnergy = maxEnergy;
    }
    private void SetAgent<T>() where T : AIHunterAgent
    {
        if (_hunter != null)
        {
            Destroy(_hunter.gameObject);
        }

        GameObject hunterObject = new GameObject("AIHunterAgent");
        _hunter = hunterObject.AddComponent<T>();
    }

    private void Idle(Vector3 lastPos)
    {
        transform.position = lastPos;
        SetMaterial(_greenMat);
    }

    private bool isNearBoid()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Boid") && collider != this)
            {
                return true;
            }
        }
        return false;
    }

    private void SetMaterial(Material material)
    {
        Material[] newMaterials = new Material[_meshRenderer.materials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = material;
        }

        _meshRenderer.materials = newMaterials;
    }
}