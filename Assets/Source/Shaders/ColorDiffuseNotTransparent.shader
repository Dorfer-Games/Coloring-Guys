﻿Shader "Custom/ColorDiffuseNotTransparent"
{
    Properties 
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.9
    }

    SubShader 
    {
        Tags { "RenderType"="Geometry " }
        LOD 200
        CGPROGRAM

        // Physically based Standard lighting model, and enable shadows on all light types
       #pragma surface surf Standard fullforwardshadows
        // Use Shader model 3.0 target
        #pragma target 3.0
        sampler2D _MainTex;
        struct Input 
        {
            float2 uv_MainTex;
        };
        half _Glossiness;
        UNITY_INSTANCING_BUFFER_START(Props)
           UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
        UNITY_INSTANCING_BUFFER_END(Props)
        void surf (Input IN, inout SurfaceOutputStandard o) 
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
            o.Albedo = c.rgb;
            o.Smoothness = _Glossiness;
        }

        ENDCG
    }
    FallBack "Diffuse"
}