cbuffer MatrixBuffer
{
	matrix worldMat;
	matrix viewMat;
	matrix projMat;
	float4 cameraPos;
	float4 sunPos;
}
//Texture2D txDiffuse : register( t0 );
Texture2D <float4> txDiffuse;
SamplerState mySampler;// : register( s0 );
//sampler samLinear;

struct VS_IN
{
	float3 pos : POSITION;
	float3 normal : NORMAL;
	float2 tex : TEXCOORD;
};

struct PS_IN
{
	float4 pos : SV_POSITION;
	//float4 col : COLOR;
	float2 tex : TEXCOORD0;
	float3 worldPos : TEXCOORD1;
	float3 normal : NORMAL;
};

PS_IN VS( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	//input.pos.w = 1.0f;
//	output.pos = mul( float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat );
	output.pos = mul( float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat );
	output.pos = mul( output.pos, viewMat );
	output.pos = mul( output.pos, projMat );
	output.worldPos = input.pos;
	//output.worldPos = mul(float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat).xyz;
	output.normal = input.normal;
	output.tex = input.tex;
	return output;
}

PS_IN VS2( VS_IN input )
{
	PS_IN output = (PS_IN)0;
	//input.pos.w = 1.0f;
	float scale = 1.05f;
	//output.pos = mul( float4(input.pos.x*scale, input.pos.y*scale, input.pos.z*scale, 1), worldMat );
	output.pos = mul( float4(input.pos.x*scale, input.pos.y*scale, input.pos.z*scale, 1), worldMat );
	output.pos = mul( output.pos, viewMat );
	output.pos = mul( output.pos, projMat );
	output.worldPos = input.pos;
	//output.worldPos = mul(float4(input.pos.x, input.pos.y, input.pos.z, 1), worldMat).xyz;
	output.normal = input.normal;
	output.tex = input.tex;
	return output;
}

float4 PS2( PS_IN input ) : SV_Target
{
	float3 tex = txDiffuse.Sample( mySampler, input.tex );
	//return float4(tex.x, tex.y, tex.z, 0.5);
	float3 camTrans = mul(cameraPos, transpose(worldMat)).xyz;
	float3 eyeVec = -normalize(input.worldPos - camTrans.xyz);
	float light = dot(input.normal, sunPos.xyz);
	float temp = dot(input.normal, eyeVec);
	float3 finalColor = float3(0.3f, 0.6f, 0.99f)*0.35f * light;
	float alpha = 1.0f-temp.x;
	return float4(finalColor.x, finalColor.y, finalColor.z, alpha);
}

float4 PS( PS_IN input ) : SV_Target
{
	float light = dot(input.normal, sunPos.xyz);
	float3 tex = txDiffuse.Sample( mySampler, input.tex );
	float3 finalColor = float3(0.3f, 0.6f, 0.99f) * tex * light;
	return float4(finalColor.x, finalColor.y, finalColor.z, 1.0f);
}

BlendState AlphaBlending
{
	AlphaToCoverageEnable = FALSE;
	BlendEnable[0] = TRUE;
	SrcBlend = SRC_ALPHA;
	DestBlend = INV_SRC_ALPHA;
	BlendOp = ADD;
	SrcBlendAlpha = ONE;
	DestBlendAlpha = ONE;
	BlendOpAlpha = ADD;
	RenderTargetWriteMask[0] = 0x0F;
};

BlendState NoBlending
{
	AlphaToCoverageEnable = FALSE;
	BlendEnable[0] = FALSE;
};
technique11 Render
{
	pass P0
	{
		SetBlendState( NoBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		//SetBlendState( AlphaBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		//AlphaBlendEnable = true;
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetPixelShader( CompileShader( ps_5_0, PS() ) );
	}
	pass P1
	{
		//SetBlendState( NoBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		SetBlendState( AlphaBlending, float4( 0.0f, 0.0f, 0.0f, 0.0f ), 0xFFFFFFFF );
		//AlphaBlendEnable = true;
		SetGeometryShader( 0 );
		SetVertexShader( CompileShader( vs_5_0, VS2() ) );
		SetPixelShader( CompileShader( ps_5_0, PS2() ) );
	}
}


