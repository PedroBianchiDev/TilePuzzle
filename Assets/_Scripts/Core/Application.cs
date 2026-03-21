using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TilePuzzle.Core
{
    public class Application : MonoBehaviour
    {
        public static Application Instance;

        [SerializeField]
        private List<MonoBehaviourService> Services = new List<MonoBehaviourService>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeServices();
        }

        private void InitializeServices()
        {
            Services = GetComponentsInChildren<MonoBehaviourService>().ToList();

            foreach (MonoBehaviourService service in Services)
            {
                service.Initialize();
            }
        }

        public T GetService<T>() where T : MonoBehaviourService
        {
            return Services.OfType<T>().FirstOrDefault();
        }
    }
}