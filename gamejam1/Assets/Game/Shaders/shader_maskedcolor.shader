Shader "Custom/shader_maskedcolor"{
	Properties{
		_MainTex("Main Texture", 2D) = "white" {}
		_MaskPrimary("Primary Mask", 2D) = "white" {}
		_MaskSecondary("Secondary Mask", 2D) = "white" {}

		_ColorPrimary("Primary Tint", Color) = (0, 0, 0, 1)
		_ColorSecondary("Secondary Tint", Color) = (0, 0, 0, 1)
	}

SubShader{
	Tags{
		"RenderType" = "Transparent"
		"Queue" = "Transparent"
	}

	Blend SrcAlpha OneMinusSrcAlpha

	ZWrite off
	Cull off

	Pass{

		CGPROGRAM

		#include "UnityCG.cginc"

		#pragma vertex vert
		#pragma fragment frag

			sampler2D _MainTex;
			sampler2D _MaskPrimary;
			sampler2D _MaskSecondary;

			float4 _MainTex_ST;

			fixed4 _ColorPrimary;
			fixed4 _ColorSecondary;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			v2f vert(appdata v) {
				v2f o;
				o.position = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag(v2f i) : SV_TARGET{

				float maskPrimary = tex2D(_MaskPrimary, i.uv).a;
				float maskPrimaryMult = tex2D(_MaskPrimary, i.uv);

				float maskSecondary = tex2D(_MaskSecondary, i.uv).a;
				float maskSecondaryMult = tex2D(_MaskSecondary, i.uv);

				fixed4 col = tex2D(_MainTex, i.uv) * (1 - maskPrimary) * (1 - maskSecondary);
				col += tex2D(_MainTex, i.uv) * _ColorPrimary * maskPrimary * maskPrimaryMult;
				col += tex2D(_MainTex, i.uv) * _ColorSecondary * maskSecondary * maskSecondaryMult;
				col *= i.color;

				return col;
			}

			ENDCG
		}
	}
}
