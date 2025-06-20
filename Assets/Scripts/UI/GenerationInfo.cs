// Copyright (c) Guillem Serra. All Rights Reserved.

using System;
using TMPro;
using UnityEngine;
using WFC.Generation;

namespace WFC.UI
{
    public class GenerationInfo : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private TextMeshProUGUI _generationInfoText;
        [SerializeField] private TextMeshProUGUI _inputInfoText;

        private void Awake()
        {
            //_levelGenerator.OnGenerationStartEvent += HideInfo;
            _levelGenerator.OnGeneratedEvent += UpdateInfo;
        }

        private void HideInfo()
        {
            _generationInfoText.gameObject.SetActive(false);
            _inputInfoText.gameObject.SetActive(false);
        }

        private void UpdateInfo()
        {
            Vector3Int dimensions = _levelGenerator.GridDimensions;
            _generationInfoText.SetText(
                $"Grid dimensions: {dimensions.x},{dimensions.y},{dimensions.z} ({dimensions.x * dimensions.y * dimensions.z} cells)");
        }
    }
}