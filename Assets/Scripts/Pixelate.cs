using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PixelateEffect
{
    [System.Serializable]
    [PostProcess(typeof(PixelateRenderer), PostProcessEvent.AfterStack, "Pixelate")]
    public class Pixelate : PostProcessEffectSettings
    {
        [UnityEngine.Rendering.PostProcessing.Min(1f), DisplayName("X")]
        public FloatParameter width = new FloatParameter { value = 1f };

        [UnityEngine.Rendering.PostProcessing.Min(1f), DisplayName("Y")]
        public FloatParameter heigth = new FloatParameter { value = 1f };
    }

    public sealed class PixelateRenderer : PostProcessEffectRenderer<Pixelate>
    {
        const string ShaderPath = "Hidden/PostProcessing/Pixelate";


        public override void Render(PostProcessRenderContext context)
        {
            // Tenta pegar o shader
            var shader = Shader.Find(ShaderPath);
            if (shader == null)
            {
                Debug.LogError("Failed to find shader: " + ShaderPath);
                return;
            }

            // Tenta pegar as propriedades
            var propertySheet = context.propertySheets.Get(shader);
            if (propertySheet == null)
            {
                Debug.LogError("Falhei em pegar as propriedades do shader: " + ShaderPath);
                return;
            }

            // Configurar shader
            propertySheet.properties.SetFloat("_Width", settings.width);
            propertySheet.properties.SetFloat("_Height", settings.heigth);

            context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
        }
    }
}
