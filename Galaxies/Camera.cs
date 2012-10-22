using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Otavio.Math;

namespace Galaxies
{
	public class Camera
	{
		//public SharpDX.Matrix worldMatrix;
		//public SharpDX.Matrix viewMatrix;
		//public SharpDX.Matrix projMatrix;

		public float3 m_cameraPos, m_cameraLookat, m_cameraUp;
		public float aspectRatio = 1.0f;
		public float viewWidth = 1280.0f;
		public float viewHeight = 720.0f;
		public float farClip = 1000.0f;
		public float nearClip = 0.1f;
		public float fovAngle = (float)Math.PI / 4.0f;

		public Camera(float aspect, int tempWidth, int tempHeight)
		{
			viewWidth = tempWidth;
			viewHeight = tempHeight;
			aspectRatio = aspect;
			m_cameraPos = new float3(0, 0, -5);
			m_cameraLookat = new float3(0, 0, 0);
			m_cameraUp = new float3(0, 1, 0);
		}

		public void SetupCamera()
		{
			Global._G.shaderParams.view = SharpDX.Matrix.LookAtLH(new Vector3(m_cameraPos.ToArray()), new Vector3(m_cameraLookat.ToArray()), new Vector3(m_cameraUp.ToArray()));
			Global._G.shaderParams.proj = SharpDX.Matrix.PerspectiveFovLH(fovAngle, viewWidth / (float)viewHeight, nearClip, farClip);
			Global._G.shaderParams.view.Transpose();
			Global._G.shaderParams.proj.Transpose();

			Global._G.shaderParams.cameraPos = new Vector4(m_cameraPos.x, m_cameraPos.y, m_cameraPos.z, 1);
		}


	}
}
