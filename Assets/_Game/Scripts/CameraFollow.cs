using Unity.Cinemachine;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [SerializeField] private CinemachineCamera camWin;
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
    [SerializeField] private CinemachineCamera camGamePlay;
    private float rotateSpeed = 10f;
    private bool isWin = false;
    public void OnWin()
    {
        camWin.transform.position = camGamePlay.transform.position;

        camGamePlay.Priority = 0; 
        camWin.Priority = 20; 
        isWin = true ; 
    }
    public void OnInit()
    {
        camGamePlay.Priority = 20; 
        camWin.Priority = 0;
        isWin = false; 
    }
    void LateUpdate()
    {
        if (isWin)
        {
            orbitalFollow.HorizontalAxis.Value += rotateSpeed * Time.deltaTime;
        }
    }


}