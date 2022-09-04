Shader "Hidden/PostProcessing/Desaturate"
{
    HLSLINCLUDE
    
    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

    float _Intensity;

    float4 DesaturateFrag(VaryingsDefault i): SV_TARGET
    {
        float3 pixelColour = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord).rgb;

        pixelColour = lerp(pixelColour, dot(pixelColour, float3(0.3, 0.59, 0.11)), _Intensity);

        return float4(pixelColour, 1);
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
            #pragma fragment DesaturateFrag

            ENDHLSL
        }
    }
}
