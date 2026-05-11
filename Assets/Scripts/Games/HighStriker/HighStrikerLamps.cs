using UnityEngine;

public class HighStrikerLamps : MonoBehaviour
{
    public void SetValueForLamps(float value)
    {
        float newValue = 1 - value;

        Renderer ren = GetComponent<Renderer>();

        if(ren == null ) return;

        foreach (Material mat in ren.materials )
        {
            if (mat.shader.name == "Shader Graphs/LightSlider")
            {
                mat.SetFloat("_LightSlider_UV", newValue);
            }
        }
    }


}
