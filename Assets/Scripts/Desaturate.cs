using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace DesaturateEffect
{
    [System.Serializable]
    [PostProcess(typeof(DesatureRenderer), PostProcessEvent.AfterStack, "Desaturate")]
    public class Desaturate : PostProcessEffectSettings
    {
        [Range(0f, 1f)]
        public FloatParameter Intensity = new FloatParameter { value = 0 };
    }

    public sealed class DesatureRenderer : PostProcessEffectRenderer<Desaturate>
    {
        const string ShaderPath = "Hidden/PostProcessing/Desaturate";

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
            propertySheet.properties.SetFloat("_Intensity", settings.Intensity);

            context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
        }
    }
}