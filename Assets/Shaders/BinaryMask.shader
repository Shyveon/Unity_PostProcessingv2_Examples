Shader "Hidden/PostProcessing/BinaryMask"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
    TEXTURE2D_SAMPLER2D(_Mask, sampler_Mask);

    float4 BinaryMaskFrag(VaryingsDefault i): SV_TARGET
    {
        float2 uv = i.texcoord;

        float3 mask = SAMPLE_TEXTURE2D(_Mask, sampler_Mask, uv).rgb;

        if (mask.r == 0.0 && mask.g == 0.0 && mask.b == 0.0) {
            return float4(mask, 1);
        } else {
            float4 pixelColour = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
            return pixelColour;
        }
    }

    ENDHLSL

    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment BinaryMaskFrag

            ENDHLSL
        }
    }
}
