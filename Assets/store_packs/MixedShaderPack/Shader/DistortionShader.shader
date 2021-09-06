
Shader "Custom/DistortionShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "black" {}
		_Color("Main Color", Color) =  (1,1,1,1)

		_DistTex("Distortion", 2D) = "grey" {}
		_DistMask("Distorion Mask", 2D) = "black" {}
		_Speed("Distortion Speed", float) = 1
		_Rotation("Distortion Rotation/Direction", Range(0,360)) = 0
		_DistMultiplier("Distortion Multiplier", Range(0,0.1)) = 0.01

		_OverlayTex("Overlay Texture", 2D) = "white" {}
		_OverlayColor("Overlay Color", Color) = (1,1,1,0)
		_OverlayRotation("Overlay Rotation/Direction", Range(0, 360)) = 0
		_OverlayScrollSpeed("Overlay Scroll Speed", float) = 1
	}

	SubShader
	{
		Tags{
		"Queue" = "Transparent"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
	}
		LOD 100
		ZWrite Off
		AlphaToMask On
		Lighting On

		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex, _DistTex, _DistMask, _OverlayTex;
			float4 _Color, _OverlayColor;
			float _Rotation, _DistMultiplier, _Speed, _DirectionX, _DirectionY, _OverlayRotation, _OverlayScrollSpeed, _ScrollDirectionX, _ScrollDirectionY;
	#define _PI 3.1415926535897932384626433832795


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv2 = v.uv;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//Divided by 180 because slider is between 0-360 and
				//2*pi = 360 degrees.
				_DirectionY = -sin(_Rotation * _PI / 180);
				_DirectionX = -cos(_Rotation * _PI / 180);

				float2 distScroll = float2(_Time.x * _DirectionX, _Time.x * _DirectionY);
				fixed2 dist = (tex2D(_DistTex, i.uv + distScroll * _Speed) - 0.5) * 2; //Constants is for offsetting.
				fixed distMask = tex2D(_DistMask, i.uv)[0];
				fixed4 maintex = _Color * tex2D(_MainTex, i.uv + dist * distMask * _DistMultiplier);

				_ScrollDirectionY = -sin(_OverlayRotation * _PI / 180);
				_ScrollDirectionX = -cos(_OverlayRotation * _PI / 180);

				float2 overlayScroll = float2(_Time.x * _ScrollDirectionX, _Time.x * _ScrollDirectionY);
				fixed4 overlay = _OverlayColor * tex2D(_OverlayTex, i.uv2 + (dist * distMask * _DistMultiplier) + overlayScroll * _OverlayScrollSpeed);

				maintex = lerp(maintex, overlay, overlay.a);

				return maintex;
			}
			ENDCG
		}
	}
}