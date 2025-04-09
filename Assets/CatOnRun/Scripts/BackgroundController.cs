using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float bgSpeed;
    public Renderer mainBackground;
    public managerVars vars;
    private bool backgroundUpdated;

    void OnEnable()
    {
        vars = Resources.Load<managerVars>("managerVarsContainer");
    }

    void Start()
    {
        SetBackground();
    }

    void Update()
    {
        if (GameManager.instance.tileSetChanged && !backgroundUpdated)
        {
            backgroundUpdated = true;
            SetBackground();
        }

        ScrollBG(bgSpeed, mainBackground);
    }

    void ScrollBG(float scrollSpeed, Renderer rend)
    {
        float offset = Camera.main.transform.position.x * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, -0.001f));
    }

    void SetBackground()
    {
        if (GameManager.instance.selectedTheme == 0)
        {
            mainBackground.material.mainTextureScale = new Vector2(6, 6);
        }
        else
        {
            mainBackground.material.mainTextureScale = new Vector2(1, 1);
        }

        mainBackground.material.mainTexture = vars.themeData[GameManager.instance.selectedTheme].backgroundTexture;
    }
}
