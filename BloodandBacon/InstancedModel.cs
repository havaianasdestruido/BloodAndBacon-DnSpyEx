using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000011 RID: 17
	public class InstancedModel
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0002AF38 File Offset: 0x00029138
		internal InstancedModel(ContentReader input)
		{
			this.graphicsDevice = InstancedModel.GetGraphicsDevice(input);
			int num = input.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				this.modelParts.Add(new InstancedModelPart(input, this.graphicsDevice));
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0002AF98 File Offset: 0x00029198
		private static GraphicsDevice GetGraphicsDevice(ContentReader input)
		{
			IServiceProvider serviceProvider = input.ContentManager.ServiceProvider;
			IGraphicsDeviceService graphicsDeviceService = (IGraphicsDeviceService)serviceProvider.GetService(typeof(IGraphicsDeviceService));
			return graphicsDeviceService.GraphicsDevice;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0002AFD0 File Offset: 0x000291D0
		public void DrawGems(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, Vector3 vpos)
		{
			if (this.show.Count == 0)
			{
				return;
			}
			this.modelParts[0].show = this.show;
			this.modelParts[0].DrawGems(ref instanceTransforms, view, projection, light, amb, diffu, vpos);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0002B01E File Offset: 0x0002921E
		public void DrawBoulders(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, int count)
		{
			if (count == 0)
			{
				return;
			}
			this.modelParts[0].boulders(ref instanceTransforms, view, projection, light, amb, diffu, count);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0002B041 File Offset: 0x00029241
		public void DrawBreak(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, int count)
		{
			if (count == 0)
			{
				return;
			}
			this.modelParts[0].breakage(ref instanceTransforms, view, projection, light, amb, diffu, count);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0002B064 File Offset: 0x00029264
		public void DrawSmallGems(ref Matrix[] instanceTransforms, int count, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu)
		{
			if (count == 0)
			{
				return;
			}
			this.modelParts[0].smallGems(ref instanceTransforms, view, projection, light, amb, diffu);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0002B085 File Offset: 0x00029285
		public void DrawChain(ref Matrix[] instanceTransforms, Matrix view, Matrix projection, Vector3 light, Vector3 amb, Vector3 diffu, int start)
		{
			if (instanceTransforms.Length == 0)
			{
				return;
			}
			this.modelParts[0].chain(ref instanceTransforms, view, projection, light, amb, diffu, start);
		}

		// Token: 0x040005B0 RID: 1456
		private List<InstancedModelPart> modelParts = new List<InstancedModelPart>();

		// Token: 0x040005B1 RID: 1457
		private GraphicsDevice graphicsDevice;

		// Token: 0x040005B2 RID: 1458
		public List<int> show = new List<int>();
	}
}
