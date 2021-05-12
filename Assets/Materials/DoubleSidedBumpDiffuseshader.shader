Shader "Custom/Doublesided Bumped Diffuse" 
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        [NoScaleOffset] _BumpMap("Normalmap", 2D) = "bump" {}
        _Glossiness("Glossiness", Range(0,1)) = 0
        _Specular("Specular", Range(0,5)) = 0
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque"  }
        Cull Off
        
        CGPROGRAM
        #pragma surface surf Lambert noforwardadd nolightmap noforwardadd 
        #pragma target 3.0


        sampler2D _MainTex;
        sampler2D _BumpMap;
        float _Glossiness;
        float _Specular;

        struct Input {
            float2 uv_MainTex;
        };
         
        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            clip(c.a - 0.01);
            o.Gloss = _Glossiness;
            o.Specular = _Specular;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
        }
        ENDCG
    }

        FallBack "Mobile/Diffuse"
}
