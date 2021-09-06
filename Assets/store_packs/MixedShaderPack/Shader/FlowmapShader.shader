Shader "Custom/FlowmapShader"
{
	Properties
	{
		_MainTex("Main Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		_FlowMap("Flow Map", 2D) = "white" {}
		_FlowSpeed("Flow Speed", float) = 0.05
		_FlowMultiplier("Flow Multiplier", float) = 0.5
		_DispMap("Displacement Map", 2D) = "white" {}
		_Displacement("Displacement Multiplier", float) = 0
		_DisplacementSpeed("Displacement Speed", float) = 1
		_DisplacementFreq("Displacement Frequency", float) = 1

		[Toggle(X_DISPLACEMENT)]
		_XDisplacement("Displace X-axis", float) = 0
		[Toggle(Y_DISPLACEMENT)]
		_YDisplacement("Displace Y-axis", float) = 1
		[Toggle(Z_DISPLACEMENT)]
		_ZDisplacement("Displace Z-axis", float) = 0

		_MaskTex("Mask Texture", 2D) = "white" {}
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True" 
		"RenderType" = "Transparent"
	}

	Blend One OneMinusSrcAlpha

	ZWrite On
		Cull Off

	Pass
	{
		CGPROGRAM

		#pragma shader_feature X_DISPLACEMENT
		#pragma shader_feature Y_DISPLACEMENT
		#pragma shader_feature Z_DISPLACEMENT

		#pragma vertex vert 
		#pragma fragment frag 
		#include "UnityCG.cginc"

		struct appdata_t
		{
			float4 vertex   : POSITION;
			float4 color    : COLOR;
			float2 texcoord : TEXCOORD0;
		};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		half2 texcoord  : TEXCOORD0;
	};

	fixed4 _Color;
	sampler2D _DispMap;
	float _Displacement;
	float _DisplacementSpeed;
	float _DisplacementFreq;

	sampler2D _MainTex;
	sampler2D _FlowMap;
	sampler2D _MaskTex;
	float _FlowSpeed;
	float _FlowMultiplier;

	v2f vert(appdata_full i)
	{
		v2f o;

		o.vertex = UnityObjectToClipPos(i.vertex);
		o.texcoord = i.texcoord;
		o.color = i.color * _Color;


		float3 flowDir = tex2Dlod(_FlowMap, float4(i.texcoord.xy,0,0)) * 2.0f - 1.0f;
		flowDir *= _FlowMultiplier;

		fixed mask = tex2Dlod(_MaskTex, float4(i.texcoord.xy,0,0))[3];

		float phase0 = frac(_Time.x * _DisplacementSpeed * 0.5f + 0.5f); 
		float phase1 = frac(_Time.x * _DisplacementSpeed * 0.5f + 1.0f);

		fixed4 tex0 = tex2Dlod(_DispMap, float4(i.texcoord.xy  * phase0 * mask,0,0)); 
		fixed4 tex1 = tex2Dlod(_DispMap, float4(i.texcoord.xy * phase1 * mask,0,0)); 

		float flowLerp = abs((0.5f - phase0) / 0.5f);
		float4 finalColor = lerp(tex0, tex1, flowLerp);

#ifdef Y_DISPLACEMENT
				o.vertex.y += sin(finalColor[3] * _DisplacementFreq) * _Displacement;
#endif
#ifdef X_DISPLACEMENT
				o.vertex.x += sin(finalColor[3] * _DisplacementFreq) * _Displacement;
#endif
#ifdef Z_DISPLACEMENT
				o.vertex.z += sin(finalColor[3] * _DisplacementFreq) * _Displacement;
#endif

				return o;
			}


	fixed4 frag(v2f i) : SV_Target
	{
		float3 flowDir = tex2D(_FlowMap, i.texcoord) * 2.0f - 1.0f;
		flowDir *= _FlowMultiplier;

		fixed mask = tex2D(_MaskTex, i.texcoord)[0];

		float phase0 = frac(_Time[1] * _FlowSpeed * 0.5f + 0.5f);
		float phase1 = frac(_Time[1] * _FlowSpeed * 0.5f + 1.0f);

		fixed4 tex0 = tex2D(_MainTex, i.texcoord + flowDir.xy * phase0 * mask); 
		fixed4 tex1 = tex2D(_MainTex, i.texcoord + flowDir.xy * phase1 * mask);

		float flowLerp = abs((0.5f - phase0) / 0.5f);
		fixed4 finalColor = lerp(tex0, tex1, flowLerp);

		fixed4 c = float4(finalColor)* i.color;
		c.rgb *= c.a;
		return c;
	}
	ENDCG
}
	}
}