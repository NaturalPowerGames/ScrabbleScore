using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TouchKeyboardDisplacementController : MonoBehaviour
{
    private RectTransform canvasTransform; // Reference to your Canvas RectTransform
    private RectTransform inputFieldTransform; // Reference to the InputField RectTransform
    private Vector2 originalPosition;
    private bool isKeyboardVisible = false;
	[SerializeField]
	private float displacement;

    private void Awake()
	{
        canvasTransform = GetComponent<RectTransform>();
	}

	private void OnEnable()
	{
        UIEvents.OnInputFieldInteractionBegan += OnInputFieldInteractionBegan;
        UIEvents.OnInputFieldInteractionEnded += OnInputFieldInteractionEnded;
    }

    private void OnDisable()
    {
        UIEvents.OnInputFieldInteractionBegan -= OnInputFieldInteractionBegan;
        UIEvents.OnInputFieldInteractionEnded -= OnInputFieldInteractionEnded;
    }

    private void OnInputFieldInteractionBegan(RectTransform rect)
	{
        inputFieldTransform = rect;
	}

	private void OnInputFieldInteractionEnded(RectTransform rect)
	{
        inputFieldTransform = null;
	}

	private void Start()
    {
        originalPosition = canvasTransform.anchoredPosition;
    }

    private void Update()
	{
		MoveCanvasDependingOnKeyboardHeight();
	}

	private void MoveCanvasDependingOnKeyboardHeight()
	{
		if (TouchScreenKeyboard.visible && !isKeyboardVisible)
		{
			isKeyboardVisible = true;
			Vector2 targetPosition = originalPosition + new Vector2(0, displacement);
			canvasTransform.DOAnchorPos(targetPosition, 0.3f).SetEase(Ease.OutQuad);
		}
		else if (!TouchScreenKeyboard.visible && isKeyboardVisible)
		{
			isKeyboardVisible = false;
			canvasTransform.DOAnchorPos(originalPosition, 0.3f).SetEase(Ease.OutQuad);
		}
	}

}
