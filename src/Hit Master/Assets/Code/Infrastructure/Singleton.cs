using UnityEngine;

namespace Code.Infrastructure
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField] protected bool _singleSceneInstance = false;
        
        private static T instance;

        public static T Instance => instance;

        public static bool IsInited => instance != null;

        public virtual void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning($"[{this.GetType().Name}]: Trying to instantiate a second instance of a singleton class.");
                Destroy(gameObject);
            }
            else
            {
                instance = (T) this;

                if (_singleSceneInstance == false)
                    DontDestroyOnLoad(gameObject);

                Init();
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        protected virtual void Init() { }
    }
}
