using UnityEngine;
using Code.Infrastructure;
using Code.Infrastructure.Services;
using Code.Infrastructure.StateMachine;
using Code.Infrastructure.StateMachine.States;

public class LoadMainSceneByKey : MonoBehaviour
{
#if UNITY_EDITOR
    public KeyCode Key;

    private IStateMachine _stateMachine;

    private void Start()
    {
        _stateMachine = AllServices.Container.Single<IStateMachine>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(Key))
            _stateMachine.Enter<LoadLevelState, string>(SceneNames.Main);
    }
#endif
}