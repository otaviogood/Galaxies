using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public float3 m_cameraPos, m_cameraLookat, m_cameraUp;
		public float aspectRatio = 1.0f;
		public float viewWidth = 1280.0f;
		public float viewHeight = 720.0f;
		public float farClip = 1000.0f;
		public float nearClip = 0.01f;
		public float fovAngle = (float)Math.PI / 4.0f;

		public Camera(float aspect, int tempWidth, int tempHeight)
		{
			viewWidth = tempWidth;
			viewHeight = tempHeight;
			aspectRatio = aspect;
			m_cameraPos = new float3(0, 0, -3);
			m_cameraLookat = new float3(0, 0, 0);
			m_cameraUp = new float3(0, 1, 0);
		}

		public void Resize(float aspect, int tempWidth, int tempHeight)
		{
			viewWidth = tempWidth;
			viewHeight = tempHeight;
			aspectRatio = aspect;
		}

		public void SetupCamera()
		{
			Global._G.shaderParams.view = SharpDX.Matrix.LookAtLH(new Vector3(m_cameraPos.ToArray()), new Vector3(m_cameraLookat.ToArray()), new Vector3(m_cameraUp.ToArray()));
			Global._G.shaderParams.proj = SharpDX.Matrix.PerspectiveFovLH(fovAngle, viewWidth / (float)viewHeight, nearClip, farClip);
			Global._G.shaderParams.view.Transpose();
			Global._G.shaderParams.proj.Transpose();

			Global._G.shaderParams.cameraPos = new Vector4(m_cameraPos.x, m_cameraPos.y, m_cameraPos.z, 1);
		}

		bool leftButton = false;
		bool rightButton = false;
		int2 mousePos = new int2(0, 0);
		int2 dragStart = new int2(0, 0);
		public void MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			mousePos = new int2(e.X, e.Y);
			UpdateMouseControls();
			dragStart = new int2(e.X, e.Y);
		}

		public void MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left)) leftButton = false;
			if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Right)) rightButton = false;
		}

		public void MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Left)) leftButton = true;
			if (e.Button.HasFlag(System.Windows.Forms.MouseButtons.Right)) rightButton = true;
			mousePos = new int2(e.X, e.Y);
			dragStart = new int2(e.X, e.Y);
			UpdateMouseControls();
		}

		public void MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Dolly(e.Delta / 2.0f);
			SetupCamera();
		}

		private void Dolly(float amount)
		{
			float3 lookAtDelta = m_cameraPos - m_cameraLookat;
			// If we try to dolly too far at once, we'll go through the origin and back out the other side. So clamp it.
			if (amount > 360) amount = 360;
			if (amount < -360) amount = -360;
			// If our frail floating point numbers get too small, force a dolly outwards. Too small causes degenerate case. :(
			if (lookAtDelta.Length() < 0.0001f) amount = -Math.Abs(amount);
			m_cameraPos -= lookAtDelta * amount * 0.0015f;
		}

		public void UpdateMouseControls()
		{
			if (leftButton || rightButton)
			{
				float2 delta = new float2((mousePos - dragStart) * 2);
				delta.x *= aspectRatio;
				delta /= viewWidth;
				if (leftButton && rightButton)
				{
					Dolly(delta.x * 1000);
				}
				else
				{
					if (leftButton)
					{
						// rotation camera
						float sensitivity = 1.5f;
						float3x3 roty = float3x3.RotateY(delta.x * sensitivity);
						float3 lookAtDelta = m_cameraPos - m_cameraLookat;
						lookAtDelta = roty.Mul(lookAtDelta);

						float3 rightVector = lookAtDelta.Cross(m_cameraUp).Normalize();
						float3x3 upRot = float3x3.AxisAngle(rightVector, delta.y * sensitivity);
						lookAtDelta = upRot.Mul(lookAtDelta);
						m_cameraPos = lookAtDelta + m_cameraLookat;
						float3 newLookAtDelta = m_cameraPos - m_cameraLookat;
						float3 newRightVector = newLookAtDelta.Cross(m_cameraUp).Normalize();
						if (newRightVector.Dot(rightVector) < 0) m_cameraUp.y = -m_cameraUp.y;
					}
					else
					{
						// move camera
						float3 lookAtDelta = m_cameraPos - m_cameraLookat;
						float3 rightVector = lookAtDelta.Cross(m_cameraUp).Normalize();
						float3 upVector = rightVector.Cross(lookAtDelta).Normalize();
						float moveScale = 0.5f * lookAtDelta.Length();
						m_cameraPos -= rightVector * delta.x * moveScale;
						m_cameraLookat -= rightVector * delta.x * moveScale;
						m_cameraPos += upVector * delta.y * moveScale;
						m_cameraLookat += upVector * delta.y * moveScale;
					}

				}
			}
			SetupCamera();
		}


	}
}
