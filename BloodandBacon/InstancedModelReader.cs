using System;
using Microsoft.Xna.Framework.Content;

namespace Blood
{
	// Token: 0x02000050 RID: 80
	public class InstancedModelReader : ContentTypeReader<InstancedModel>
	{
		// Token: 0x06000324 RID: 804 RVA: 0x000CF025 File Offset: 0x000CD225
		protected override InstancedModel Read(ContentReader input, InstancedModel existingInstance)
		{
			return new InstancedModel(input);
		}
	}
}
