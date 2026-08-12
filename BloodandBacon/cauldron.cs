using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x0200009A RID: 154
	public class cauldron
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x001380F8 File Offset: 0x001362F8
		public cauldron(ScreenManager ss, ContentManager cc)
		{
			this.rr = new Random();
			this.content = cc;
			this.sc = ss;
			this.bloody = default(cauldron.finale);
			this.bloody.max = 0;
			this.bloody.maxCapacity = 200;
			this.bloody.index = 0;
			this.bloody.model = this.content.Load<Model>("Models//bloodything");
			this.bloody.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, this.bloody.maxCapacity, BufferUsage.WriteOnly);
			this.bloody.displayList = new cauldron.instancedObject[this.bloody.maxCapacity];
			this.bloody.dupe = new explodeDupe2[this.bloody.maxCapacity];
			for (int i = 0; i < this.bloody.maxCapacity; i++)
			{
				this.bloody.dupe[i] = new explodeDupe2(i);
			}
			this.bloody.stream = new cauldron.instancedObject[this.bloody.maxCapacity];
			this.resetExplody(false);
			this.fireball = default(cauldron.hole);
			this.fireball.stainR = new Vector4[80];
			this.fireball.stainR[0] = new Vector4(0f, 0f, 133f, 200f);
			this.fireball.stainR[1] = new Vector4(798f, 0f, 133f, 200f);
			this.fireball.stainR[2] = new Vector4(532f, 400f, 133f, 200f);
			this.fireball.stainR[3] = new Vector4(665f, 800f, 133f, 200f);
			this.fireball.stainR[4] = new Vector4(1064f, 400f, 133f, 200f);
			this.fireball.stainR[5] = new Vector4(1197f, 600f, 133f, 200f);
			this.fireball.stainR[6] = new Vector4(1463f, 800f, 133f, 200f);
			this.fireball.stainR[7] = new Vector4(931f, 1000f, 133f, 200f);
			this.fireball.stainR[8] = new Vector4(1197f, 1000f, 133f, 200f);
			this.fireball.stainR[9] = new Vector4(133f, 0f, 133f, 200f);
			this.fireball.stainR[10] = new Vector4(266f, 0f, 133f, 200f);
			this.fireball.stainR[11] = new Vector4(0f, 200f, 133f, 200f);
			this.fireball.stainR[12] = new Vector4(133f, 200f, 133f, 200f);
			this.fireball.stainR[13] = new Vector4(266f, 200f, 133f, 200f);
			this.fireball.stainR[14] = new Vector4(399f, 0f, 133f, 200f);
			this.fireball.stainR[15] = new Vector4(532f, 0f, 133f, 200f);
			this.fireball.stainR[16] = new Vector4(399f, 200f, 133f, 200f);
			this.fireball.stainR[17] = new Vector4(665f, 0f, 133f, 200f);
			this.fireball.stainR[18] = new Vector4(532f, 200f, 133f, 200f);
			this.fireball.stainR[19] = new Vector4(665f, 200f, 133f, 200f);
			this.fireball.stainR[20] = new Vector4(798f, 200f, 133f, 200f);
			this.fireball.stainR[21] = new Vector4(0f, 400f, 133f, 200f);
			this.fireball.stainR[22] = new Vector4(133f, 400f, 133f, 200f);
			this.fireball.stainR[23] = new Vector4(0f, 600f, 133f, 200f);
			this.fireball.stainR[24] = new Vector4(266f, 400f, 133f, 200f);
			this.fireball.stainR[25] = new Vector4(133f, 600f, 133f, 200f);
			this.fireball.stainR[26] = new Vector4(399f, 400f, 133f, 200f);
			this.fireball.stainR[27] = new Vector4(0f, 800f, 133f, 200f);
			this.fireball.stainR[28] = new Vector4(266f, 600f, 133f, 200f);
			this.fireball.stainR[29] = new Vector4(133f, 800f, 133f, 200f);
			this.fireball.stainR[30] = new Vector4(399f, 600f, 133f, 200f);
			this.fireball.stainR[31] = new Vector4(665f, 400f, 133f, 200f);
			this.fireball.stainR[32] = new Vector4(266f, 800f, 133f, 200f);
			this.fireball.stainR[33] = new Vector4(532f, 600f, 133f, 200f);
			this.fireball.stainR[34] = new Vector4(798f, 400f, 133f, 200f);
			this.fireball.stainR[35] = new Vector4(399f, 800f, 133f, 200f);
			this.fireball.stainR[36] = new Vector4(665f, 600f, 133f, 200f);
			this.fireball.stainR[37] = new Vector4(532f, 800f, 133f, 200f);
			this.fireball.stainR[38] = new Vector4(798f, 600f, 133f, 200f);
			this.fireball.stainR[39] = new Vector4(798f, 800f, 133f, 200f);
			this.fireball.stainR[40] = new Vector4(931f, 0f, 133f, 200f);
			this.fireball.stainR[41] = new Vector4(1064f, 0f, 133f, 200f);
			this.fireball.stainR[42] = new Vector4(931f, 200f, 133f, 200f);
			this.fireball.stainR[43] = new Vector4(1197f, 0f, 133f, 200f);
			this.fireball.stainR[44] = new Vector4(1064f, 200f, 133f, 200f);
			this.fireball.stainR[45] = new Vector4(1330f, 0f, 133f, 200f);
			this.fireball.stainR[46] = new Vector4(931f, 400f, 133f, 200f);
			this.fireball.stainR[47] = new Vector4(1197f, 200f, 133f, 200f);
			this.fireball.stainR[48] = new Vector4(1463f, 0f, 133f, 200f);
			this.fireball.stainR[49] = new Vector4(1330f, 200f, 133f, 200f);
			this.fireball.stainR[50] = new Vector4(931f, 600f, 133f, 200f);
			this.fireball.stainR[51] = new Vector4(1596f, 0f, 133f, 200f);
			this.fireball.stainR[52] = new Vector4(1197f, 400f, 133f, 200f);
			this.fireball.stainR[53] = new Vector4(1463f, 200f, 133f, 200f);
			this.fireball.stainR[54] = new Vector4(1064f, 600f, 133f, 200f);
			this.fireball.stainR[55] = new Vector4(1729f, 0f, 133f, 200f);
			this.fireball.stainR[56] = new Vector4(1330f, 400f, 133f, 200f);
			this.fireball.stainR[57] = new Vector4(931f, 800f, 133f, 200f);
			this.fireball.stainR[58] = new Vector4(1596f, 200f, 133f, 200f);
			this.fireball.stainR[59] = new Vector4(1463f, 400f, 133f, 200f);
			this.fireball.stainR[60] = new Vector4(1064f, 800f, 133f, 200f);
			this.fireball.stainR[61] = new Vector4(1729f, 200f, 133f, 200f);
			this.fireball.stainR[62] = new Vector4(1330f, 600f, 133f, 200f);
			this.fireball.stainR[63] = new Vector4(1596f, 400f, 133f, 200f);
			this.fireball.stainR[64] = new Vector4(1197f, 800f, 133f, 200f);
			this.fireball.stainR[65] = new Vector4(1463f, 600f, 133f, 200f);
			this.fireball.stainR[66] = new Vector4(1729f, 400f, 133f, 200f);
			this.fireball.stainR[67] = new Vector4(1330f, 800f, 133f, 200f);
			this.fireball.stainR[68] = new Vector4(1596f, 600f, 133f, 200f);
			this.fireball.stainR[69] = new Vector4(1729f, 600f, 133f, 200f);
			this.fireball.stainR[70] = new Vector4(1596f, 800f, 133f, 200f);
			this.fireball.stainR[71] = new Vector4(1729f, 800f, 133f, 200f);
			this.fireball.stainR[72] = new Vector4(0f, 1000f, 133f, 200f);
			this.fireball.stainR[73] = new Vector4(133f, 1000f, 133f, 200f);
			this.fireball.stainR[74] = new Vector4(266f, 1000f, 133f, 200f);
			this.fireball.stainR[75] = new Vector4(399f, 1000f, 133f, 200f);
			this.fireball.stainR[76] = new Vector4(532f, 1000f, 133f, 200f);
			this.fireball.stainR[77] = new Vector4(665f, 1000f, 133f, 200f);
			this.fireball.stainR[78] = new Vector4(798f, 1000f, 133f, 200f);
			this.fireball.stainR[79] = new Vector4(1064f, 1000f, 133f, 200f);
			this.fireball.stainMax = 0;
			this.fireball.stainIndex = 0;
			this.fireball.stainCapacity = 65;
			this.fireball.location = new Vector3[this.fireball.stainCapacity];
			this.fireball.bone = new int[this.fireball.stainCapacity];
			this.fireball.fade = new float[this.fireball.stainCapacity];
			this.fireball.frame = new int[this.fireball.stainCapacity];
			this.fireball.scale = new Matrix[this.fireball.stainCapacity];
			this.fireball.stainTrans = new cauldron.hitStream[this.fireball.stainCapacity];
			this.fireball.stainBuffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.vd2, this.fireball.stainCapacity, BufferUsage.WriteOnly);
			this.cald = default(cauldron.shell);
			this.cald.max = 0;
			this.cald.type = 0;
			this.cald.maxCapacity = 3;
			this.cald.index = 0;
			this.cald.model1 = this.content.Load<Model>("models\\cauldron");
			this.cald.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, this.cald.maxCapacity, BufferUsage.WriteOnly);
			this.cald.displayList = new cauldron.instancedObject[this.cald.maxCapacity];
			this.cald.dupe = new caldDupe[this.cald.maxCapacity];
			for (int j = 0; j < this.cald.maxCapacity; j++)
			{
				this.cald.dupe[j] = new caldDupe(j);
			}
			this.cald.stream = new cauldron.instancedObject[this.cald.maxCapacity];
			this.liquid = default(cauldron.shell);
			this.liquid.max = 0;
			this.cald.type = 0;
			this.liquid.maxCapacity = 3;
			this.liquid.index = 0;
			this.liquid.model1 = this.content.Load<Model>("models\\cauldron_fill");
			this.liquid.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, this.liquid.maxCapacity, BufferUsage.WriteOnly);
			this.liquid.displayList = new cauldron.instancedObject[this.liquid.maxCapacity];
			this.liquid.dupe = new caldDupe[this.liquid.maxCapacity];
			for (int k = 0; k < this.liquid.maxCapacity; k++)
			{
				this.liquid.dupe[k] = new caldDupe(k);
			}
			this.liquid.stream = new cauldron.instancedObject[this.liquid.maxCapacity];
			this.c_leg = default(cauldron.shell);
			this.c_leg.max = 0;
			this.c_leg.type = 1;
			this.c_leg.maxCapacity = 5;
			this.c_leg.index = 0;
			this.c_leg.model1 = this.content.Load<Model>("models\\cald_leg");
			int num = this.c_leg.maxCapacity;
			this.c_leg.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, num, BufferUsage.WriteOnly);
			this.c_leg.displayList = new cauldron.instancedObject[num];
			this.c_leg.dupe = new caldDupe[num];
			for (int l = 0; l < num; l++)
			{
				this.c_leg.dupe[l] = new caldDupe(l);
			}
			this.c_leg.stream = new cauldron.instancedObject[num];
			this.c_big = default(cauldron.shell);
			this.c_big.max = 0;
			this.c_big.type = 2;
			this.c_big.maxCapacity = 5;
			this.c_big.index = 0;
			this.c_big.model1 = this.content.Load<Model>("models\\cald_big");
			num = this.c_big.maxCapacity;
			this.c_big.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, num, BufferUsage.WriteOnly);
			this.c_big.displayList = new cauldron.instancedObject[num];
			this.c_big.dupe = new caldDupe[num];
			for (int m = 0; m < num; m++)
			{
				this.c_big.dupe[m] = new caldDupe(m);
			}
			this.c_big.stream = new cauldron.instancedObject[num];
			this.c_base = default(cauldron.shell);
			this.c_base.max = 0;
			this.c_base.type = 3;
			this.c_base.maxCapacity = 5;
			this.c_base.index = 0;
			this.c_base.model1 = this.content.Load<Model>("models\\cald_base");
			num = this.c_base.maxCapacity;
			this.c_base.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, num, BufferUsage.WriteOnly);
			this.c_base.displayList = new cauldron.instancedObject[num];
			this.c_base.dupe = new caldDupe[num];
			for (int n = 0; n < num; n++)
			{
				this.c_base.dupe[n] = new caldDupe(n);
			}
			this.c_base.stream = new cauldron.instancedObject[num];
			this.c_top = default(cauldron.shell);
			this.c_top.max = 0;
			this.c_top.type = 4;
			this.c_top.maxCapacity = 5;
			this.c_top.index = 0;
			this.c_top.model1 = this.content.Load<Model>("models\\cald_top");
			num = this.c_top.maxCapacity;
			this.c_top.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, num, BufferUsage.WriteOnly);
			this.c_top.displayList = new cauldron.instancedObject[num];
			this.c_top.dupe = new caldDupe[num];
			for (int num2 = 0; num2 < num; num2++)
			{
				this.c_top.dupe[num2] = new caldDupe(num2);
			}
			this.c_top.stream = new cauldron.instancedObject[num];
			this.c_ring = default(cauldron.shell);
			this.c_ring.max = 0;
			this.c_ring.type = 4;
			this.c_ring.maxCapacity = 5;
			this.c_ring.index = 0;
			this.c_ring.model1 = this.content.Load<Model>("models\\cald_ring");
			num = this.c_ring.maxCapacity;
			this.c_ring.buffer = new DynamicVertexBuffer(this.sc.GraphicsDevice, cauldron.instanceDec, num, BufferUsage.WriteOnly);
			this.c_ring.displayList = new cauldron.instancedObject[num];
			this.c_ring.dupe = new caldDupe[num];
			for (int num3 = 0; num3 < num; num3++)
			{
				this.c_ring.dupe[num3] = new caldDupe(num3);
			}
			this.c_ring.stream = new cauldron.instancedObject[num];
			this.sound1 = this.content.Load<SoundEffect>("audio\\caldHit");
			this.sizzle = this.content.Load<SoundEffect>("audio\\caldSizzle");
			this.sizzle2 = this.content.Load<SoundEffect>("audio\\caldSizzle2");
			this.shortboil = this.content.Load<SoundEffect>("audio\\caldShortBoil");
			this.boil = this.content.Load<SoundEffect>("audio\\caldBoil");
			this.pop2 = this.content.Load<SoundEffect>("audio\\caldexplode");
			this.explodeSound = this.content.Load<SoundEffect>("audio\\caldBlown2");
			this.cackle = this.content.Load<SoundEffect>("audio\\caldCackle");
			this.cackle2 = this.content.Load<SoundEffect>("audio\\caldCackle2");
			this.cackle3 = this.content.Load<SoundEffect>("audio\\caldCackle3");
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x001399F0 File Offset: 0x00137BF0
		public void initCauldrons(int seed, ref float[,] heights)
		{
			this.explode = false;
			this.explodeTimer = 200;
			this.sc.shitContrast = 128;
			this.isFull = false;
			this.glowOnce = 0;
			this.level = 0;
			this.oldlevel = 0;
			this.c_big.max = 0;
			this.c_big.index = 0;
			this.c_base.max = 0;
			this.c_base.index = 0;
			this.c_top.max = 0;
			this.c_top.index = 0;
			this.c_ring.max = 0;
			this.c_ring.index = 0;
			this.c_leg.max = 0;
			this.c_leg.index = 0;
			this.fireball.stainMax = 0;
			this.fireball.stainIndex = 0;
			this.liquid.max = 0;
			this.liquid.index = 0;
			this.bloody.max = 0;
			this.bloody.index = 0;
			this.bloody.maxCapacity = 200;
			this.resetExplody(false);
			this.cald.max = 0;
			this.cald.type = 0;
			this.cald.index = 0;
			this.mainSeed = seed;
			this.rx = new Random(seed);
			int num = this.rx.Next(9000, 22000);
			bool flag = true;
			this.dropcaldron(ref this.cald, num, ref heights, flag);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00139B70 File Offset: 0x00137D70
		private void dropcaldron(ref cauldron.shell sh, int seed, ref float[,] heights, bool inner)
		{
			this.exists = true;
			float num = (float)this.rx.Next(70, 100) / 100f;
			Vector3 vector = new Vector3(1f, 1f, 1f) * num;
			this.mainScale = vector;
			Vector3 vector2 = new Vector3((float)this.rx.Next(500, 5500), 0f, (float)this.rx.Next(500, 5500));
			sh.dupe[sh.index].drop = 0;
			vector2 = new Vector3(0f, 0f, 0f) + new Vector3(3000f, 0f, 3000f);
			float num2;
			Vector3 vector3;
			cauldron.GetHeightFast(ref heights, ref vector2, out num2, out vector3);
			vector2.Y = num2;
			Matrix matrix = Matrix.CreateRotationY((float)this.rx.Next(0, 8000) / 100f);
			sh.dupe[sh.index].init(vector, vector2, matrix, seed);
			sh.dupe[sh.index].tint = 100f;
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.stream[sh.index].tint = sh.dupe[sh.index].tint;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity - 1)
			{
				sh.max = sh.maxCapacity;
			}
			this.liquid.dupe[this.liquid.index].init(vector, vector2, matrix, seed);
			this.liquid.dupe[this.liquid.index].tint = 100f;
			this.liquid.stream[this.liquid.index].Trans = this.liquid.dupe[this.liquid.index].transform;
			this.liquid.stream[this.liquid.index].tint = this.liquid.dupe[this.liquid.index].tint;
			this.liquid.index = this.liquid.index + 1;
			if (this.liquid.index > this.liquid.maxCapacity - 1)
			{
				this.liquid.index = 0;
			}
			this.liquid.max = this.liquid.max + 1;
			if (this.liquid.max > this.liquid.maxCapacity - 1)
			{
				this.liquid.max = this.liquid.maxCapacity;
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00139E74 File Offset: 0x00138074
		public void dropParts(ref cauldron.shell sh, int i, Matrix m, int seed, int size)
		{
			Random random = new Random(seed);
			float num = (float)random.Next(70, 130) / 100f;
			float num2 = 50f;
			float num3 = 100f;
			float tint = this.cald.dupe[i].tint;
			int num4 = 180;
			int num5 = 230;
			float num6 = 60f;
			int num7 = -180;
			int num8 = -90;
			float num9 = 1000f;
			int num10 = 1;
			Matrix matrix = Matrix.CreateScale(this.cald.dupe[i].scale) * this.cald.dupe[i].rot * Matrix.CreateTranslation(this.cald.dupe[i].mypos);
			Matrix matrix2 = m * matrix;
			sh.dupe[sh.index].tint = this.cald.dupe[i].tint;
			Vector3 vector = new Vector3((float)random.Next(-300, 300) / 220f, (float)random.Next(num4, num5) / num6, (float)random.Next(-300, 300) / 220f);
			Vector3 vector2 = Vector3.Transform(new Vector3(0f, 0f, 0f), m);
			Vector3 vector3 = (float)random.Next(100, 300) / 30f * Vector3.Normalize(vector2);
			float num11 = (float)random.Next(500, 700) / 1000f;
			float num12 = (float)random.Next(num7, num8) / num9;
			sh.dupe[sh.index].init2(size, num10, num11, this.cald.dupe[i].scale, 0.34f, matrix2, new Vector3(vector3.X + vector.X, vector.Y, vector3.Z + vector.Z) * num, true, num12, 20, num2, num3, false, false, false, seed);
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.stream[sh.index].tint = sh.dupe[sh.index].tint;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity - 1)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x0013A11C File Offset: 0x0013831C
		public void ExplodeTrails(ref float[,] heights)
		{
			if (this.explode)
			{
				this.explodeTimer--;
				if (this.explodeTimer <= 0)
				{
					this.updateExplode(ref heights);
					return;
				}
				if (this.explodeTimer == 160)
				{
					this.explodeSound.Play(this.sc.ev, 0f, 0f);
				}
				if (this.explodeTimer == 5)
				{
					this.initExplode();
				}
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0013A190 File Offset: 0x00138390
		public void updatecaldron(ref cauldron.shell sh, float range, ref float[,] heights, Vector3 campos, Vector3 camlookpos, ref localPlayer myPlayer, ref Cursor genCursor, ref Vector2 hitVel, float headRot)
		{
			this.liquid.tempindex = 0;
			if (!this.exists)
			{
				return;
			}
			if (this.rr.Next(1, 600) < 3 && this.level >= 50)
			{
				this.shortboil.Play(this.sc.ev, (float)this.rr.Next(-20, 20) / 100f, (float)this.rr.Next(-40, 40) / 100f);
			}
			if (this.level < 40 && !this.explode && this.glowOnce == 0 && this.level > this.oldlevel)
			{
				this.glowcolor = new Vector3(1.5f, 0f, 0f);
				this.glowOnce = 1;
				this.glowInc = (float)this.rr.Next(20, 30) / 1000f;
				this.glowRamp = 0f;
				this.oldlevel = this.level;
				this.sizzle.Play(this.sc.ev, 0f, 0f);
				this.addExplosion(ref this.fireball, campos, this.discPos);
				this.addExplosion(ref this.fireball, campos, this.discPos);
				this.addExplosion(ref this.fireball, campos, this.discPos);
			}
			if (this.level > 50 && !this.explode && this.glowOnce == 0 && this.level > this.oldlevel)
			{
				this.glowcolor = new Vector3(2f, 0f, 0f);
				this.glowOnce = 1;
				this.glowInc = (float)this.rr.Next(5, 10) / 1000f;
				this.glowRamp = 0f;
				this.oldlevel = this.level;
				float num = Vector3.DistanceSquared(myPlayer.displayState.npcPosition, new Vector3(3000f, 100f, 3000f));
				float num2 = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num) - 200f) / 4000f, 0.2f, 0.8f);
				this.sizzle2.Play(this.sc.ev * num2, 0f, 0f);
				this.addExplosion(ref this.fireball, campos, this.discPos);
				this.addExplosion(ref this.fireball, campos, this.discPos);
				this.addExplosion(ref this.fireball, campos, this.discPos);
			}
			if (this.explode && this.explodeTimer < 180 && this.explodeTimer > -1000 && this.glowOnce == 0)
			{
				this.glowcolor = new Vector3((float)this.rr.Next(0, 50) / 100f, 0f, (float)this.rr.Next(80, 180) / 100f);
				this.glowOnce = 1;
				this.glowInc = (float)this.rr.Next(2, 8) / 100f;
				this.glowRamp = 0f;
			}
			Vector3 vector = Vector3.Normalize(camlookpos - campos);
			Vector3 vector2 = -vector * 100f + campos;
			range *= range;
			this.caldkinIndex = -1;
			bool flag = false;
			sh.tempindex = 0;
			this.alive = 0;
			this.soundcount--;
			bool flag2 = true;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move != -1)
				{
					if (this.explodeTimer <= -700 && this.exists)
					{
						this.explodeCauldron(0, ref myPlayer, ref heights);
					}
					if (sh.dupe[i].move == 0)
					{
						flag2 = false;
					}
					bool flag3 = false;
					this.alive++;
					float num3;
					Vector3.DistanceSquared(ref myPlayer.displayState.npcPosition, ref sh.dupe[i].mypos, out num3);
					this.lightFade = 0f;
					if (num3 < range)
					{
						Vector3 vector3 = new Vector3(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Y, sh.dupe[i].mypos.Z) - vector2;
						Vector3 vector4;
						Vector3.Normalize(ref vector3, out vector4);
						float num4 = 0f;
						Vector3.Dot(ref vector4, ref vector, out num4);
						if (num4 > this.sc.myfov)
						{
							if (myPlayer.now.flashlight)
							{
								this.lightFade = 1f - MathHelper.Clamp((num3 - 20000f) / 350000f, 0f, 1f);
								this.flashlightDir = Vector3.Normalize(sh.dupe[i].mypos - myPlayer.displayState.npcPosition);
								this.flashlightDir.Z = this.flashlightDir.Z * -1f;
							}
							if (sh.dupe[i].move > 0)
							{
								sh.dupe[i].updateCualdron();
							}
							sh.stream[i].Trans = sh.dupe[i].transform;
							sh.stream[i].tint = sh.dupe[i].tint;
							sh.displayList[sh.tempindex] = sh.stream[i];
							sh.tempindex++;
							flag3 = true;
							if (sh.dupe[i].pop)
							{
								if (this.sc.introCamera <= 0f)
								{
									float num5 = MathHelper.Clamp(1f - ((float)Math.Sqrt((double)num3) - 200f) / 4000f, 0.2f, 0.8f);
									this.boil.Play(this.sc.ev * num5, 0f, 0f);
								}
								sh.dupe[i].pop = false;
							}
							if (sh.dupe[i].kickable && !flag && !myPlayer.isDown && !myPlayer.gunFired && num3 < 10000f * sh.dupe[i].scale.Y)
							{
								flag = true;
								if (this.soundcount <= 0)
								{
									this.sound1.Play(this.sc.ev, (float)this.rr.Next(-10, 10) / 100f, 0f);
									this.soundcount = 20;
								}
								Vector2 vector5 = new Vector2(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Z) - new Vector2(myPlayer.displayState.npcPosition.X, myPlayer.displayState.npcPosition.Z);
								if (vector5.Length() > 0f)
								{
									hitVel = -Vector2.Normalize(vector5) * 3.5f;
									hitVel = Vector2.Transform(new Vector2(-hitVel.X, hitVel.Y), Matrix.CreateRotationZ(-headRot));
								}
							}
						}
					}
					if (sh.dupe[i].move > 0 && !flag3)
					{
						sh.dupe[i].updateCualdron();
					}
					sh.dupe[i].pop = false;
				}
			}
			if (this.exists && !flag2)
			{
				if (this.level >= 1 && !this.explode)
				{
					for (int j = 0; j < this.liquid.max; j++)
					{
						int num6 = Math.Min(this.level, 40);
						float num7 = MathHelper.Lerp(1f, 1.2f, (float)num6 / 40f);
						float num8 = MathHelper.Lerp(38f, 78f, (float)num6 / 40f) * this.liquid.dupe[j].scale.X;
						this.liquid.dupe[j].rot *= Matrix.CreateRotationY(0.002f);
						this.liquid.dupe[j].transform = Matrix.CreateScale(this.liquid.dupe[j].scale * num7) * this.liquid.dupe[j].rot * Matrix.CreateTranslation(this.liquid.dupe[j].mypos.X, this.liquid.dupe[j].mypos.Y + num8, this.liquid.dupe[j].mypos.Z);
						this.liquid.stream[j].Trans = this.liquid.dupe[j].transform;
						this.liquid.stream[j].tint = 100f;
						this.discPos = this.liquid.dupe[j].transform;
						this.liquid.displayList[this.liquid.tempindex] = this.liquid.stream[j];
						this.liquid.tempindex = this.liquid.tempindex + 1;
					}
					if (this.sc.myTimer % 20f == 0f && this.rr.Next(Math.Min(this.level, 40), 45) > 39)
					{
						this.addExplosion(ref this.fireball, campos, this.discPos);
					}
				}
				this.updateExplosion(ref this.fireball, campos, this.discPos);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0013ABB8 File Offset: 0x00138DB8
		public void explodeCauldron(int i, ref localPlayer myPlayer, ref float[,] heights)
		{
			this.cald.dupe[i].move = -1;
			this.exists = false;
			this.level = 0;
			this.pop2.Play(this.sc.ev, (float)this.rr.Next(-10, 0) / 100f, 0f);
			Matrix matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-14.7f)) * Matrix.CreateRotationY(MathHelper.ToRadians(26.8f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(3.5f)) * Matrix.CreateTranslation(19.6f, 24.5f, 42.6f);
			this.dropParts(ref this.c_leg, i, matrix, this.cald.dupe[i].seed, 23);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(157f)) * Matrix.CreateRotationY(MathHelper.ToRadians(36f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(175.2f)) * Matrix.CreateTranslation(25.3f, 23.8f, -33.9f);
			this.dropParts(ref this.c_leg, i, matrix, this.cald.dupe[i].seed + 1, 23);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-162.7f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-73.5f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(137f)) * Matrix.CreateTranslation(-47.7f, 24.5f, 1.57f);
			this.dropParts(ref this.c_leg, i, matrix, this.cald.dupe[i].seed + 2, 23);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(4.7f)) * Matrix.CreateRotationY(MathHelper.ToRadians(2.9f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-2.6f)) * Matrix.CreateTranslation(-0.26f, 56.3f, 26.6f);
			this.dropParts(ref this.c_big, i, matrix, this.cald.dupe[i].seed + 3, 28);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(4.7f)) * Matrix.CreateRotationY(MathHelper.ToRadians(218f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1.4f)) * Matrix.CreateTranslation(-21f, 56.3f, -42.7f);
			this.dropParts(ref this.c_big, i, matrix, this.cald.dupe[i].seed + 4, 28);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-139.5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(21.8f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(165.6f)) * Matrix.CreateTranslation(5.9f, 38.2f, -18.2f);
			this.dropParts(ref this.c_base, i, matrix, this.cald.dupe[i].seed + 5, 24);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(23.2f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-58.4f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(26f)) * Matrix.CreateTranslation(-17.7f, 38.2f, 14.3f);
			this.dropParts(ref this.c_base, i, matrix, this.cald.dupe[i].seed + 6, 24);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(55f)) * Matrix.CreateRotationY(MathHelper.ToRadians(34f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(16.1f)) * Matrix.CreateTranslation(55f, 38.2f, 19.6f);
			this.dropParts(ref this.c_base, i, matrix, this.cald.dupe[i].seed + 7, 24);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(7.4f)) * Matrix.CreateRotationY(MathHelper.ToRadians(118f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(1.8f)) * Matrix.CreateTranslation(41.2f, 67.9f, -30.8f);
			this.dropParts(ref this.c_top, i, matrix, this.cald.dupe[i].seed + 8, 19);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(6.13f)) * Matrix.CreateRotationY(MathHelper.ToRadians(158.5f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(0.9f)) * Matrix.CreateTranslation(19.8f, 67.9f, -52.5f);
			this.dropParts(ref this.c_top, i, matrix, this.cald.dupe[i].seed + 9, 19);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(20.8f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-86.6f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-15f)) * Matrix.CreateTranslation(-48.5f, 67.9f, -5.6f);
			this.dropParts(ref this.c_top, i, matrix, this.cald.dupe[i].seed + 10, 19);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(1.8f)) * Matrix.CreateRotationY(MathHelper.ToRadians(77.8f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-4f)) * Matrix.CreateTranslation(45.8f, 67.9f, 3.2f);
			this.dropParts(ref this.c_top, i, matrix, this.cald.dupe[i].seed + 11, 19);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(16.5f)) * Matrix.CreateRotationY(MathHelper.ToRadians(12.3f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-88.4f)) * Matrix.CreateTranslation(-12f, 45.5f, 36.7f);
			this.dropParts(ref this.c_ring, i, matrix, this.cald.dupe[i].seed + 12, 21);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(-98f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-3.7f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-101.8f)) * Matrix.CreateTranslation(36.9f, 45.5f, -1.4f);
			this.dropParts(ref this.c_ring, i, matrix, this.cald.dupe[i].seed + 13, 21);
			matrix = Matrix.CreateRotationX(MathHelper.ToRadians(119f)) * Matrix.CreateRotationY(MathHelper.ToRadians(-4.3f)) * Matrix.CreateRotationZ(MathHelper.ToRadians(-78.4f)) * Matrix.CreateTranslation(-29.5f, 45.5f, -20.5f);
			this.dropParts(ref this.c_ring, i, matrix, this.cald.dupe[i].seed + 14, 21);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0013B2F8 File Offset: 0x001394F8
		public void updatecaldronParts(ref cauldron.shell sh, float range, ref float[,] heights, Vector3 campos, Vector3 camlookpos, ref localPlayer myPlayer, ref Cursor genCursor)
		{
			Vector3 vector = Vector3.Normalize(camlookpos - campos);
			Vector3 vector2 = -vector * 100f + campos;
			range *= range;
			sh.tempindex = 0;
			int num = 0;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move != -1)
				{
					num++;
					bool flag = false;
					float num2;
					Vector3.DistanceSquared(ref myPlayer.displayState.npcPosition, ref sh.dupe[i].mypos, out num2);
					if (num2 < range)
					{
						Vector3 vector3 = new Vector3(sh.dupe[i].mypos.X, sh.dupe[i].mypos.Y, sh.dupe[i].mypos.Z) - vector2;
						Vector3 vector4;
						Vector3.Normalize(ref vector3, out vector4);
						float num3 = 0f;
						Vector3.Dot(ref vector4, ref vector, out num3);
						if (num3 > this.sc.myfov)
						{
							if (sh.dupe[i].move > 0)
							{
								sh.dupe[i].Update(ref heights);
							}
							sh.stream[i].Trans = sh.dupe[i].transform;
							sh.stream[i].tint = sh.dupe[i].tint;
							sh.displayList[sh.tempindex] = sh.stream[i];
							sh.tempindex++;
							flag = true;
						}
					}
					if (sh.dupe[i].move > 0 && !flag)
					{
						sh.dupe[i].Update(ref heights);
					}
				}
			}
			if (num == 0)
			{
				sh.max = 0;
				sh.index = 0;
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0013B4DC File Offset: 0x001396DC
		private void addExplosion(ref cauldron.hole hole, Vector3 campos, Matrix pp)
		{
			hole.location[hole.stainIndex] = new Vector3((float)this.rr.Next(-100, 100) / 20f, (float)this.rr.Next(-120, -60) / 10f, (float)this.rr.Next(-100, 100) / 20f);
			Matrix matrix = Matrix.CreateTranslation(hole.location[hole.stainIndex]) * pp;
			Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
			Matrix matrix2 = Matrix.CreateBillboard(vector, new Vector3(campos.X, (campos.Y - vector.Y) / 2f, campos.Z), this.view.Up, new Vector3?(this.view.Forward));
			Vector3 vector2 = new Vector3(340f, 700f, 340f) * (float)this.rr.Next(70, 160) / 600f;
			if (this.rr.Next(1, 100) < 50)
			{
				hole.scale[hole.stainIndex] = Matrix.CreateScale(vector2) * Matrix.CreateRotationZ((float)this.rr.Next(-5, 5) / 100f);
			}
			else
			{
				hole.scale[hole.stainIndex] = Matrix.CreateScale(vector2) * Matrix.CreateRotationY(3.14f) * Matrix.CreateRotationZ((float)this.rr.Next(-5, 5) / 100f);
			}
			hole.stainTrans[hole.stainIndex].Trans = hole.scale[hole.stainIndex] * matrix2;
			hole.fade[hole.stainIndex] = (float)this.rr.Next(80, 100) / 100f;
			hole.stainTrans[hole.stainIndex].Fade = hole.fade[hole.stainIndex];
			Vector4 vector3 = hole.stainR[0];
			hole.stainTrans[hole.stainIndex].Coord = new Vector4(1864f / vector3.Z, vector3.X / 1864f, 1200f / vector3.W, vector3.Y / 1200f);
			hole.frame[hole.stainIndex] = 0;
			hole.stainIndex++;
			if (hole.stainIndex > hole.stainCapacity - 1)
			{
				hole.stainIndex = 0;
			}
			hole.stainMax++;
			if (hole.stainMax > hole.stainCapacity - 1)
			{
				hole.stainMax = hole.stainCapacity;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0013B7C8 File Offset: 0x001399C8
		private void updateExplosion(ref cauldron.hole hole, Vector3 campos, Matrix pp)
		{
			bool flag = false;
			for (int i = 0; i < hole.stainMax; i++)
			{
				hole.frame[i]++;
				if (hole.frame[i] > hole.stainR.Length - 1)
				{
					hole.frame[i] = hole.stainR.Length - 1;
				}
				else
				{
					hole.location[i] += new Vector3(0f, -0.01f, 0f);
					Matrix matrix = Matrix.CreateTranslation(hole.location[i]) * pp;
					Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
					Matrix matrix2 = Matrix.CreateBillboard(vector, new Vector3(campos.X, (campos.Y - vector.Y) / 2f, campos.Z), this.view.Up, new Vector3?(this.view.Forward));
					hole.stainTrans[i].Trans = hole.scale[i] * matrix2;
					Vector4 vector2 = hole.stainR[hole.frame[i]];
					hole.stainTrans[i].Coord = new Vector4(1864f / vector2.Z, vector2.X / 1864f, 1200f / vector2.W, vector2.Y / 1200f);
					hole.stainTrans[i].Fade = hole.fade[i];
					flag = true;
				}
			}
			if (!flag)
			{
				hole.stainIndex = 0;
				hole.stainMax = 0;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0013B990 File Offset: 0x00139B90
		public void draw(Matrix view, Matrix proj)
		{
			this.view = view;
			this.proj = proj;
			Vector3 vector = new Vector3(0.8f, 0.8f, 0.8f);
			Vector3 vector2 = new Vector3(0.2f, 0.2f, 0.2f);
			Vector3 vector3 = vector2;
			if (this.glowOnce > 0)
			{
				vector3 = Vector3.Lerp(vector2, this.glowcolor, this.glowRamp);
				this.glowRamp += this.glowInc;
				if (this.glowRamp >= 1f)
				{
					this.glowOnce = 2;
					this.glowInc *= -1f;
				}
				if (this.glowRamp < 0f)
				{
					this.glowOnce = 0;
				}
			}
			if (this.exists)
			{
				if (this.lightFade == 0f)
				{
					this.DrawInstance(ref this.cald, "fastShader2", vector, vector3);
				}
				else
				{
					this.DrawInstanceLight(ref this.cald, "cauldron", vector, vector3);
				}
				if (!this.explode)
				{
					this.DrawInstance(ref this.liquid, "brightshader", vector, vector2);
				}
			}
			this.DrawInstance(ref this.c_leg, "fastShader2", vector, vector2);
			this.DrawInstance(ref this.c_big, "fastShader2", vector, vector2);
			this.DrawInstance(ref this.c_base, "fastShader2", vector, vector2);
			this.DrawInstance(ref this.c_top, "fastShader2", vector, vector2);
			this.DrawInstance(ref this.c_ring, "fastShader2", vector, vector2);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0013BAFC File Offset: 0x00139CFC
		private void DrawInstanceLight(ref cauldron.shell shell, string tech, Vector3 diff, Vector3 amb)
		{
			int tempindex = shell.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = shell.model1.Meshes[0].MeshParts[0];
			shell.buffer.SetData<cauldron.instancedObject>(shell.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques[tech];
			Vector3 vector = Vector3.Normalize(this.sc.moontype);
			vector.Y = -0.8f;
			effect.Parameters["FlashLightDirection"].SetValue(this.flashlightDir);
			effect.Parameters["dimmer"].SetValue(this.lightFade);
			effect.Parameters["LightDirection2"].SetValue(vector);
			effect.Parameters["diff2"].SetValue(diff);
			effect.Parameters["amb2"].SetValue(amb);
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(shell.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0013BCD4 File Offset: 0x00139ED4
		private void DrawInstance(ref cauldron.shell shell, string tech, Vector3 diff, Vector3 amb)
		{
			int tempindex = shell.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = shell.model1.Meshes[0].MeshParts[0];
			shell.buffer.SetData<cauldron.instancedObject>(shell.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques[tech];
			Vector3 vector = Vector3.Normalize(this.sc.moontype);
			vector.Y = -0.8f;
			effect.Parameters["LightDirection2"].SetValue(vector);
			effect.Parameters["diff2"].SetValue(diff);
			effect.Parameters["amb2"].SetValue(amb);
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(shell.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0013BE78 File Offset: 0x0013A078
		public void DrawFireBall()
		{
			if (this.exists && !this.explode)
			{
				this.sc.GraphicsDevice.BlendState = BlendState.Additive;
				this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
				this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;
				this.DrawGrenadeExplosion(ref this.fireball, Matrix.Identity);
				this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0013BEFC File Offset: 0x0013A0FC
		private void DrawGrenadeExplosion(ref cauldron.hole hole, Matrix world)
		{
			int stainMax = hole.stainMax;
			if (stainMax < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = this.sc.fireballDecal2.Meshes[0].MeshParts[0];
			hole.stainBuffer.SetData<cauldron.hitStream>(hole.stainTrans, 0, stainMax, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["premultiply"];
			effect.Parameters["World1"].SetValue(world);
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(hole.stainBuffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, stainMax);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x0013C060 File Offset: 0x0013A260
		public void resetExplody(bool death)
		{
			int num = this.bloody.maxCapacity - 1;
			this.explodePart = new Matrix[num];
			Random random = new Random(this.mainSeed);
			for (int i = 0; i < num; i++)
			{
				this.explodePart[i] = Matrix.CreateScale(new Vector3((float)random.Next(100, 400) / 10f, (float)random.Next(300, 1200) / 10f, (float)random.Next(100, 400) / 10f)) * Matrix.CreateTranslation((float)random.Next(-200, 200) / 10f, (float)random.Next(0, 280) / 12f, (float)random.Next(-200, 200) / 10f);
			}
			int num2 = this.mainSeed;
			for (int j = 0; j < num; j++)
			{
				this.bloody.dupe[j] = new explodeDupe2(num2 + j);
				num2++;
			}
			this.bloody.max = num;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0013C188 File Offset: 0x0013A388
		public void initExplode()
		{
			Random random = new Random();
			float num = (float)random.Next(-4500, -2200) / 100f;
			float num2 = (float)random.Next(90, 195) / 100f;
			num2 = 1.1f;
			Vector3 vector = new Vector3((float)random.Next(-40, 40) / 200f, (float)random.Next(-20, 20) / 100f, (float)random.Next(-40, 40) / 200f);
			for (int i = 0; i < this.explodePart.Length; i++)
			{
				float num3 = (float)random.Next(410, 510) / 100f;
				this.explodePart[i] = this.explodePart[i] * Matrix.CreateScale(this.mainScale.X) * Matrix.CreateRotationY(0f) * Matrix.CreateTranslation(new Vector3(3000f, 0f, 3000f));
				Vector3 vector2 = Vector3.Transform(Vector3.Zero, this.explodePart[i]) - new Vector3(3000f, num, 3000f);
				this.bloody.dupe[i].createState(this.explodePart[i], (vector + Vector3.Normalize(vector2)) * num3, this.mainScale.X, num2);
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0013C330 File Offset: 0x0013A530
		private void updateExplode(ref float[,] heights)
		{
			this.bloody.tempindex = 0;
			for (int i = 0; i < this.bloody.max; i++)
			{
				if (this.bloody.dupe[i].move == 1)
				{
					this.bloody.dupe[i].Update(ref heights);
					this.bloody.stream[i].Trans = this.bloody.dupe[i].transform;
					this.bloody.displayList[this.bloody.tempindex] = this.bloody.stream[i];
					this.bloody.tempindex = this.bloody.tempindex + 1;
					if (this.sc.myTimer % 4f == 0f && this.bloody.dupe[i].age > 1500)
					{
						if (Math.Abs(this.bloody.dupe[i].mypos.X) > 7000f || Math.Abs(this.bloody.dupe[i].mypos.Z) > 7000f)
						{
							this.bloody.dupe[i].move = 0;
						}
						this.bloody.dupe[i].scalePart *= 0.97f;
						this.bloody.dupe[i].myscale *= 0.97f;
						if (this.bloody.dupe[i].age > 2100)
						{
							this.bloody.dupe[i].move = 0;
							this.explodeTimer = 200;
							this.explode = false;
							this.level = 0;
						}
					}
				}
			}
			if (this.bloody.tempindex < 4 || !this.explode)
			{
				this.bloody.index = 0;
				this.bloody.max = 0;
			}
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0013C544 File Offset: 0x0013A744
		private static void GetHeightFast(ref float[,] heights, ref Vector3 pos, out float height, out Vector3 normal)
		{
			int num = (int)MathHelper.Clamp(pos.X / cauldron.unit, 0f, (float)(cauldron.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(pos.Z / cauldron.unit, 0f, (float)(cauldron.bitmap - 2));
			float num3 = pos.X % cauldron.unit / cauldron.unit;
			float num4 = pos.Z % cauldron.unit / cauldron.unit;
			float num5 = (1f - num3) * heights[num, num2] + num3 * heights[num + 1, num2];
			float num6 = (1f - num3) * heights[num, num2 + 1] + num3 * heights[num + 1, num2 + 1];
			height = (1f - num4) * num5 + num4 * num6;
			normal = Vector3.Normalize(new Vector3(-heights[num + 1, num2] + heights[num, num2], 30f, -heights[num, num2 + 1] + heights[num, num2]));
		}

		// Token: 0x04001632 RID: 5682
		private ScreenManager sc;

		// Token: 0x04001633 RID: 5683
		private ContentManager content;

		// Token: 0x04001634 RID: 5684
		private Matrix discPos = Matrix.Identity;

		// Token: 0x04001635 RID: 5685
		public static int bitmap;

		// Token: 0x04001636 RID: 5686
		public static float unit;

		// Token: 0x04001637 RID: 5687
		public int caldkinIndex = -1;

		// Token: 0x04001638 RID: 5688
		public int level;

		// Token: 0x04001639 RID: 5689
		public int oldlevel;

		// Token: 0x0400163A RID: 5690
		private int soundcount;

		// Token: 0x0400163B RID: 5691
		private float lightFade = 1f;

		// Token: 0x0400163C RID: 5692
		private Vector3 glowcolor = new Vector3(0f, 0f, 0f);

		// Token: 0x0400163D RID: 5693
		private int glowOnce;

		// Token: 0x0400163E RID: 5694
		private float glowRamp;

		// Token: 0x0400163F RID: 5695
		private float glowInc = 0.1f;

		// Token: 0x04001640 RID: 5696
		private Vector3 flashlightDir = Vector3.One;

		// Token: 0x04001641 RID: 5697
		public cauldron.finale bloody;

		// Token: 0x04001642 RID: 5698
		public Matrix[] explodePart;

		// Token: 0x04001643 RID: 5699
		public int explodeTimer = 100;

		// Token: 0x04001644 RID: 5700
		public bool explode;

		// Token: 0x04001645 RID: 5701
		private cauldron.hitStream hitstreamTemp = default(cauldron.hitStream);

		// Token: 0x04001646 RID: 5702
		private static VertexDeclaration vd2 = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
			new VertexElement(68, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4)
		});

		// Token: 0x04001647 RID: 5703
		public int fireRamp = 130;

		// Token: 0x04001648 RID: 5704
		public int fireTimer = 500;

		// Token: 0x04001649 RID: 5705
		public bool isFull;

		// Token: 0x0400164A RID: 5706
		private cauldron.hole fireball;

		// Token: 0x0400164B RID: 5707
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x0400164C RID: 5708
		private static VertexDeclaration instanceDec = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
		});

		// Token: 0x0400164D RID: 5709
		private cauldron.instancedObject[] tempInstance = new cauldron.instancedObject[1];

		// Token: 0x0400164E RID: 5710
		public cauldron.shell cald;

		// Token: 0x0400164F RID: 5711
		public cauldron.shell liquid;

		// Token: 0x04001650 RID: 5712
		public cauldron.shell c_leg;

		// Token: 0x04001651 RID: 5713
		public cauldron.shell c_big;

		// Token: 0x04001652 RID: 5714
		public cauldron.shell c_base;

		// Token: 0x04001653 RID: 5715
		public cauldron.shell c_top;

		// Token: 0x04001654 RID: 5716
		public cauldron.shell c_ring;

		// Token: 0x04001655 RID: 5717
		public SoundEffect sound1;

		// Token: 0x04001656 RID: 5718
		public SoundEffect boil;

		// Token: 0x04001657 RID: 5719
		public SoundEffect pop2;

		// Token: 0x04001658 RID: 5720
		public SoundEffect explodeSound;

		// Token: 0x04001659 RID: 5721
		public SoundEffect cackle;

		// Token: 0x0400165A RID: 5722
		public SoundEffect cackle2;

		// Token: 0x0400165B RID: 5723
		public SoundEffect cackle3;

		// Token: 0x0400165C RID: 5724
		public SoundEffect shortboil;

		// Token: 0x0400165D RID: 5725
		public SoundEffect sizzle;

		// Token: 0x0400165E RID: 5726
		public SoundEffect sizzle2;

		// Token: 0x0400165F RID: 5727
		public int alive;

		// Token: 0x04001660 RID: 5728
		private Matrix view;

		// Token: 0x04001661 RID: 5729
		private Matrix proj;

		// Token: 0x04001662 RID: 5730
		private Random rr;

		// Token: 0x04001663 RID: 5731
		private Random rx;

		// Token: 0x04001664 RID: 5732
		public bool exists;

		// Token: 0x04001665 RID: 5733
		public int mainSeed = 1;

		// Token: 0x04001666 RID: 5734
		public Vector3 mainScale = Vector3.One;

		// Token: 0x0200009B RID: 155
		public struct finale
		{
			// Token: 0x04001667 RID: 5735
			public int max;

			// Token: 0x04001668 RID: 5736
			public int tempindex;

			// Token: 0x04001669 RID: 5737
			public int index;

			// Token: 0x0400166A RID: 5738
			public int maxCapacity;

			// Token: 0x0400166B RID: 5739
			public cauldron.instancedObject[] stream;

			// Token: 0x0400166C RID: 5740
			public DynamicVertexBuffer buffer;

			// Token: 0x0400166D RID: 5741
			public cauldron.instancedObject[] displayList;

			// Token: 0x0400166E RID: 5742
			public explodeDupe2[] dupe;

			// Token: 0x0400166F RID: 5743
			public Model model;
		}

		// Token: 0x0200009C RID: 156
		public struct hitStream : IVertexType
		{
			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060005CA RID: 1482 RVA: 0x0013C773 File Offset: 0x0013A973
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return cauldron.hitStream.VertexDeclaration;
				}
			}

			// Token: 0x04001670 RID: 5744
			public Matrix Trans;

			// Token: 0x04001671 RID: 5745
			public float Fade;

			// Token: 0x04001672 RID: 5746
			public Vector4 Coord;

			// Token: 0x04001673 RID: 5747
			private static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0),
				new VertexElement(68, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 4)
			});
		}

		// Token: 0x0200009D RID: 157
		public struct hole
		{
			// Token: 0x04001674 RID: 5748
			public float[] fade;

			// Token: 0x04001675 RID: 5749
			public Vector3[] location;

			// Token: 0x04001676 RID: 5750
			public int[] bone;

			// Token: 0x04001677 RID: 5751
			public cauldron.hitStream[] stainTrans;

			// Token: 0x04001678 RID: 5752
			public int[] frame;

			// Token: 0x04001679 RID: 5753
			public Matrix[] scale;

			// Token: 0x0400167A RID: 5754
			public int stainIndex;

			// Token: 0x0400167B RID: 5755
			public int stainMax;

			// Token: 0x0400167C RID: 5756
			public int stainCapacity;

			// Token: 0x0400167D RID: 5757
			public DynamicVertexBuffer stainBuffer;

			// Token: 0x0400167E RID: 5758
			public Vector4[] stainR;
		}

		// Token: 0x0200009E RID: 158
		public struct instancedObject : IVertexType
		{
			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060005CC RID: 1484 RVA: 0x0013C81F File Offset: 0x0013AA1F
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return cauldron.instancedObject.InstanceVertexDeclaration;
				}
			}

			// Token: 0x0400167F RID: 5759
			public Matrix Trans;

			// Token: 0x04001680 RID: 5760
			public float tint;

			// Token: 0x04001681 RID: 5761
			private static readonly VertexDeclaration InstanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Single, VertexElementUsage.Fog, 0)
			});
		}

		// Token: 0x0200009F RID: 159
		public struct shell
		{
			// Token: 0x04001682 RID: 5762
			public int type;

			// Token: 0x04001683 RID: 5763
			public int drop;

			// Token: 0x04001684 RID: 5764
			public int max;

			// Token: 0x04001685 RID: 5765
			public int tempindex;

			// Token: 0x04001686 RID: 5766
			public int index;

			// Token: 0x04001687 RID: 5767
			public int maxCapacity;

			// Token: 0x04001688 RID: 5768
			public cauldron.instancedObject[] stream;

			// Token: 0x04001689 RID: 5769
			public DynamicVertexBuffer buffer;

			// Token: 0x0400168A RID: 5770
			public cauldron.instancedObject[] displayList;

			// Token: 0x0400168B RID: 5771
			public caldDupe[] dupe;

			// Token: 0x0400168C RID: 5772
			public Model model1;
		}
	}
}
