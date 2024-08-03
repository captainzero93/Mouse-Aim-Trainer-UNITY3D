using UnityEngine;
using TMPro;
using System;

public class FontApply : MonoBehaviour
{
    [Serializable]
    public class FontMapping
    {
        public string targetTag = "Untagged";
        public TMP_FontAsset font;
    }

    public FontMapping[] fontMappings;

    [Header("Default Font")]
    public TMP_FontAsset defaultFont;

    void Start()
    {
        ApplyFonts();
    }

    public void ApplyFonts()
    {
        // Find all TextMeshProUGUI components in the scene
        TextMeshProUGUI[] textComponents = FindObjectsOfType<TextMeshProUGUI>();

        foreach (TextMeshProUGUI text in textComponents)
        {
            // Check if there's a specific font mapping for this text component's tag
            TMP_FontAsset mappedFont = GetMappedFont(text.gameObject.tag);

            // Apply the mapped font or the default font if no mapping exists
            if (mappedFont != null)
            {
                text.font = mappedFont;
            }
            else if (defaultFont != null)
            {
                text.font = defaultFont;
            }
            else
            {
                Debug.LogWarning("No font assigned for " + text.gameObject.name);
            }
        }
    }

    private TMP_FontAsset GetMappedFont(string tag)
    {
        foreach (FontMapping mapping in fontMappings)
        {
            if (mapping.targetTag == tag && mapping.font != null)
            {
                return mapping.font;
            }
        }
        return null;
    }
}