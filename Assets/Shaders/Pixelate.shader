// Referência https://luka712.github.io/2018/07/01/Pixelate-it-Shadertoy-Unity/

Shader "Hidden/PostProcessing/Pixelate"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

    float _Width;
    float _Height;

    float4 PixelateFrag(VaryingsDefault i): SV_TARGET
    {
        float2 uv = i.texcoord;

        float screenWidth = _ScreenParams.x;
        float screenHeight = _ScreenParams.y;

        float X = screenWidth / _Width;
        float Y = screenHeight / _Height;

        float4 pixelColour = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(floor(X * uv.x) / X, floor(Y * uv.y) / Y));

        return pixelColour;
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
            #pragma fragment PixelateFrag

            ENDHLSL
        }
    }
}
