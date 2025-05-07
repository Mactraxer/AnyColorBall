using AnyColorBall.Infrastructure;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private ISaveLoadService _saveLoadService;

    private void Awake()
    {
        _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _saveLoadService.SaveProgress();
        Debug.Log("Progress saved");
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (_collider == null) { return; }

        Gizmos.color = new Color32(30 ,200, 30, 100);
        Gizmos.DrawCube(transform.position, _collider.size);
    }
}
