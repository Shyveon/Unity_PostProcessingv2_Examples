using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace BinaryMaskEffect
{
    [System.Serializable]
    [PostProcess(typeof(BinaryMaskRenderer), PostProcessEvent.AfterStack, "BinaryMask")]
    public class BinaryMask : PostProcessEffectSettings
    {
        [Tooltip("Binary Mask")]
        public TextureParameter Mask = new TextureParameter();
    }

    public sealed class BinaryMaskRenderer : PostProcessEffectRenderer<BinaryMask>
    {
        const string ShaderPath = "Hidden/PostProcessing/BinaryMask";

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
            propertySheet.properties.SetTexture("_Mask", settings.Mask);

            context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
        }
    }
}
