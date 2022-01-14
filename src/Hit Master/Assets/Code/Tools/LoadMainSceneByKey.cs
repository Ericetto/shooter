using Code.Infrastructure;
using UnityEngine;
using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;
using UnityEngine.SceneManagement;

public class LoadMainSceneByKey : MonoBehaviour
{
#if UNITY_EDITOR
    public KeyCode Key;

    private IGameStateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = AllServices.Container.Single<IGameStateMachine>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key))
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
    }
#endif
}