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
        Tags { "RenderType" = "Opaque" }
        LOD 250
        Cull Off


        CGPROGRAM
        #pragma surface surf MobileBlinnPhong noforwardadd nolightmap noforwardadd

        inline fixed4 LightingMobileBlinnPhong(SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
        {
            fixed diff = max(0, dot(s.Normal, lightDir));
            fixed nh = max(0, dot(s.Normal, halfDir));
            fixed spec = pow(nh, s.Specular * 128) * s.Gloss;

            fixed4 c;
            c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * atten;
            UNITY_OPAQUE_ALPHA(c.a);
            return c;
        }

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
            o.Alpha = c.a;
            o.Gloss = _Glossiness;
            o.Specular = _Specular;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
        }
        ENDCG
    }

        FallBack "Mobile/Diffuse"
}
