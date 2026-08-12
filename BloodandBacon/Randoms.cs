using System;
using Microsoft.Xna.Framework;

namespace Blood
{
	// Token: 0x0200013A RID: 314
	public class Randoms
	{
		// Token: 0x06000B45 RID: 2885 RVA: 0x002E4697 File Offset: 0x002E2897
		public Randoms(int start)
		{
			this.seedIndex = start % this.maximum;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x002E46B8 File Offset: 0x002E28B8
		public void changeSeed(int start)
		{
			this.seedIndex = start % this.maximum;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x002E46C8 File Offset: 0x002E28C8
		public static void initRandom(int seed)
		{
			Random random = new Random(seed);
			Randoms.vals = new float[10000];
			for (int i = 0; i < 10000; i++)
			{
				Randoms.vals[i] = (float)random.NextDouble();
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x002E470C File Offset: 0x002E290C
		public int Next(int min, int max)
		{
			this.seedIndex++;
			if (this.seedIndex > this.maximum - 1)
			{
				this.seedIndex = 0;
			}
			float num = Randoms.vals[this.seedIndex];
			return (int)MathHelper.Lerp((float)min, (float)max, num);
		}

		// Token: 0x04002E49 RID: 11849
		public int seedIndex;

		// Token: 0x04002E4A RID: 11850
		public static float[] vals;

		// Token: 0x04002E4B RID: 11851
		private int maximum = 10000;
	}
}
