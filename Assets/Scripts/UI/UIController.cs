using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WFC.ScriptableObjects;

namespace WFC.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Button _generationButton;
        [SerializeField] private LevelChannelSO _levelChannel;

        private void Awake()
        {
            _generationButton.onClick.AddListener(Generate);
        }

        private void Generate()
        {
            _levelChannel.RaiseGenerationEvent();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Generate();
            }
        }
    }
}