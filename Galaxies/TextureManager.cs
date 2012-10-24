using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;

namespace Galaxies
{
	public class TextureManager
	{
		public Dictionary<string, Texture> texDict = new Dictionary<string, Texture>();

		public TextureManager()
		{
			string[] allfiles = Directory.GetFiles(@"Content", "*.png");
			allfiles = allfiles.Concat(Directory.GetFiles(@"Content", "*.jpg")).ToArray();
			allfiles = allfiles.Concat(Directory.GetFiles(@"Content", "*.bmp")).ToArray();
			foreach (string s in allfiles)
			{
				string chopped = s.Replace(".png", "");
				chopped = chopped.Replace(".jpg", "");
				chopped = chopped.Replace(".bmp", "");
				chopped = chopped.Replace(".dds", "");
				chopped = chopped.Replace(".tiff", "");
				chopped = chopped.Replace(".tif", "");
				string[] split = chopped.Split(@"\/".ToCharArray());
				chopped = split[split.Length - 1];
				Texture texTemp = new Texture();

				//var ili = new ImageLoadInformation();
				//ili.OptionFlags = ResourceOptionFlags.
				SharpDX.Direct3D11.Resource tex = Texture2D.FromFile<Texture2D>(Global._G.device, s);
				//SharpDX.Direct3D11.Texture2D tex = Texture2D.FromFile<Texture2D>(Global._G.device, s);
				texTemp.textureView = new ShaderResourceView(Global._G.device, tex);
				tex.Dispose();
				texDict.Add(chopped, texTemp);
			}
		}
	}
}
