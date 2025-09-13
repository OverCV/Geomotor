using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RoundedButtonHelper : MonoBehaviour
{
    [Header("Rounded Button Settings")]
    [Range(0f, 50f)]
    public float cornerRadius = 15f;
    
    private Image buttonImage;
    
    void Start()
    {
        buttonImage = GetComponent<Image>();
        CreateRoundedSprite();
    }
    
    void CreateRoundedSprite()
    {
        // Create a simple rounded rectangle texture
        int textureSize = 100;
        Texture2D texture = new Texture2D(textureSize, textureSize);
        
        Color transparent = new Color(1f, 1f, 1f, 0f);
        Color white = Color.white;
        
        // Fill the texture with rounded corners
        for (int x = 0; x < textureSize; x++)
        {
            for (int y = 0; y < textureSize; y++)
            {
                float normalizedX = (float)x / textureSize;
                float normalizedY = (float)y / textureSize;
                
                // Calculate distance from corners
                float cornerSize = cornerRadius / textureSize;
                
                bool isInside = true;
                
                // Check each corner
                if (normalizedX < cornerSize && normalizedY < cornerSize)
                {
                    // Bottom-left corner
                    float dist = Vector2.Distance(new Vector2(normalizedX, normalizedY), new Vector2(cornerSize, cornerSize));
                    isInside = dist <= cornerSize;
                }
                else if (normalizedX > (1f - cornerSize) && normalizedY < cornerSize)
                {
                    // Bottom-right corner
                    float dist = Vector2.Distance(new Vector2(normalizedX, normalizedY), new Vector2(1f - cornerSize, cornerSize));
                    isInside = dist <= cornerSize;
                }
                else if (normalizedX < cornerSize && normalizedY > (1f - cornerSize))
                {
                    // Top-left corner
                    float dist = Vector2.Distance(new Vector2(normalizedX, normalizedY), new Vector2(cornerSize, 1f - cornerSize));
                    isInside = dist <= cornerSize;
                }
                else if (normalizedX > (1f - cornerSize) && normalizedY > (1f - cornerSize))
                {
                    // Top-right corner
                    float dist = Vector2.Distance(new Vector2(normalizedX, normalizedY), new Vector2(1f - cornerSize, 1f - cornerSize));
                    isInside = dist <= cornerSize;
                }
                
                texture.SetPixel(x, y, isInside ? white : transparent);
            }
        }
        
        texture.Apply();
        
        // Create sprite from texture
        Sprite roundedSprite = Sprite.Create(texture, new Rect(0, 0, textureSize, textureSize), new Vector2(0.5f, 0.5f));
        
        // Apply to button
        if (buttonImage != null)
        {
            buttonImage.sprite = roundedSprite;
            buttonImage.type = Image.Type.Sliced;
        }
    }
}