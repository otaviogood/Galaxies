using System;
using System.Diagnostics;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

namespace Galaxies
{
    internal static class Program
    {
		[STAThread]
        private static void Main()
        {
            RenderForm form = new RenderForm("Galaxies - test");
			Global._G.form = form;
			form.ClientSize = new System.Drawing.Size(1280, 720);// 2540, 1550);
			//form.ClientSize = new System.Drawing.Size(2540, 1550);
			form.MouseDown += new System.Windows.Forms.MouseEventHandler(form_MouseDown);
			form.MouseUp += new System.Windows.Forms.MouseEventHandler(form_MouseUp);
			form.MouseMove += new System.Windows.Forms.MouseEventHandler(form_MouseMove);
			form.MouseWheel += new System.Windows.Forms.MouseEventHandler(form_MouseWheel);
			form.Resize += new EventHandler(form_Resize);

			Global._G.camera = new Camera(1, form.ClientSize.Width, form.ClientSize.Height);
#if DEBUG
            Configuration.EnableObjectTracking = true;	// some sharpDX COM object tracking that slows performance.
#endif

			Global._G.InitDX(form);

			Mesh mesh = new Mesh();
			mesh.MakeLatLonSphere2(64, 64);

			// Prepare All the stages
			Global._G.context.Rasterizer.SetViewports(new Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height, 0.0f, 1.0f));
			Global._G.context.OutputMerger.SetTargets(Global._G.depthView, Global._G.renderView);

			Global._G.shaderParams.sunDir = new Vector4(Vector3.Normalize(new Vector3(-1, -1, 1)), 1);
			Global._G.shaderParams.sunColor = new Vector4(5.25f, 4.0f, 2.75f, 1);
			Global._G.camera.SetupCamera();


			// Use clock
			var clock = new Stopwatch();
			clock.Start();

			// Main loop
			RenderLoop.Run(form, () =>
			{
				var time = clock.ElapsedMilliseconds / 1000.0f;

				// Clear views
				Global._G.context.ClearDepthStencilView(Global._G.depthView, DepthStencilClearFlags.Depth, 1.0f, 0);
				Global._G.context.ClearRenderTargetView(Global._G.renderView, SharpDX.Color.Black);

				Global._G.shaderParams.world = Matrix.RotationY(time * 0.02f);// Matrix.Identity;// Matrix.RotationX(time) * Matrix.RotationY(time * 2) * Matrix.RotationZ(time * .7f);
				Global._G.shaderParams.world.Transpose();
				Global._G.context.UpdateSubresource(ref Global._G.shaderParams, Global._G.constantBuffer);

				// Draw the cube
				mesh.Render();

				// Present!
				Global._G.swapChain.Present(0, PresentFlags.None);
			});

			// Release all resources
			Global._G.ShutdownDX();
		}

		static void form_Resize(object sender, EventArgs e)
		{
			Global._G.camera.Resize(1, Global._G.form.ClientSize.Width, Global._G.form.ClientSize.Height);
		}

		static void form_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Global._G.camera.MouseWheel(sender, e);
		}

		static void form_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Global._G.camera.MouseMove(sender, e);
		}

		static void form_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Global._G.camera.MouseUp(sender, e);
		}

		static void form_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Global._G.camera.MouseDown(sender, e);
		}

    }
}

