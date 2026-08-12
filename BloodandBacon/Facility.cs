using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Blood
{
	// Token: 0x02000065 RID: 101
	public class Facility : GameScreen
	{
		// Token: 0x06000394 RID: 916 RVA: 0x000D8A8C File Offset: 0x000D6C8C
		public void LoadContent(ContentManager contentx, ScreenManager screenManager)
		{
			this.sc = screenManager;
			if (this.content == null)
			{
				this.content = new ContentManager(this.sc.Game.Services, "Content");
			}
			this.gridwidth = 90;
			Facility.heightData = new int[2 * this.gridwidth, 2 * this.gridwidth];
			this.heightsEntry = new int[2 * this.gridwidth, 2 * this.gridwidth];
			Facility.clickable = new int[2 * this.gridwidth, 2 * this.gridwidth];
			this.clickableEntry = new int[2 * this.gridwidth, 2 * this.gridwidth];
			this.fogEffect = this.content.Load<Effect>("astro\\shaders\\fogShader");
			this.facShader = this.content.Load<Effect>("astro\\shaders\\facilityShader");
			this.rooms = this.sc.rooms;
			this.dirtrgb = new Texture2D(this.sc.GraphicsDevice, 500, 500);
			this.entryTexture = this.sc.entryShad;
			this.mixrgb = this.sc.entryrgb2;
			this.entryWay = this.content.Load<Model>("astro\\models\\castleEntry");
			this.facShader.Parameters["rgbTexture"].SetValue(this.rooms);
			this.blurEffect = this.content.Load<Effect>("astro\\shaders\\postShader");
			this.vaultChest = this.content.Load<Model>("astro\\models\\vaultChest");
			this.vaultChest3 = this.content.Load<Model>("astro\\models\\vaultChest3");
			this.vaultChest2 = this.content.Load<Model>("astro\\models\\vaultChest2");
			this.switchModel = this.content.Load<Model>("astro\\models\\switch2");
			this.trapModel = this.content.Load<Model>("astro\\models\\trap");
			this.triHall = this.content.Load<Model>("astro\\models\\castleThree");
			this.holyHall = this.content.Load<Model>("astro\\models\\castleHoly");
			this.powerHall = this.content.Load<Model>("astro\\models\\castlePower");
			this.salvageHall = this.content.Load<Model>("astro\\models\\castleSalvage");
			this.oxygenHall = this.content.Load<Model>("astro\\models\\castleOxygen");
			this.hallWay = this.content.Load<Model>("astro\\models\\castleHall");
			this.corner = this.content.Load<Model>("astro\\models\\castleCorner");
			this.deadend = this.content.Load<Model>("astro\\models\\castleDeadend");
			this.crossHall = this.content.Load<Model>("astro\\models\\castleCross");
			this.longHall = this.content.Load<Model>("astro\\models\\castleLongHall");
			this.mainDoor = this.content.Load<Model>("astro\\models\\castleGateNew2");
			this.cube = this.content.Load<Model>("astro\\models\\cube");
			this.rgbRect = new Vector4[] { this.rgb1catacombs, this.rgb1RectPrison, this.rgb1RectServant, this.rgb1RectNobles, this.rgb1RectRoyal, this.rgb1RectRoof };
			CultureInfo cultureInfo = CultureInfo.InvariantCulture;
			StreamReader streamReader = new StreamReader("content/astro/collide/entry.txt");
			int num = 0;
			this.entryHit = new int[597];
			while (!streamReader.EndOfStream)
			{
				int num2 = Convert.ToInt32(streamReader.ReadLine(), cultureInfo);
				this.entryHit[num] = num2;
				num++;
			}
			streamReader.Close();
			streamReader.Dispose();
			cultureInfo = CultureInfo.InvariantCulture;
			streamReader = new StreamReader("content/astro/collide/entry0.txt");
			num = 0;
			this.entryHitEnter = new int[3795];
			while (!streamReader.EndOfStream)
			{
				int num3 = Convert.ToInt32(streamReader.ReadLine(), cultureInfo);
				this.entryHitEnter[num] = num3;
				num++;
			}
			streamReader.Close();
			streamReader.Dispose();
			this.pp = this.sc.GraphicsDevice.PresentationParameters;
			this.resolveTargetX = new RenderTarget2D(this.sc.GraphicsDevice, 1280, 720, false, this.pp.BackBufferFormat, this.pp.DepthStencilFormat, 0, RenderTargetUsage.DiscardContents);
			this.loadOffset();
			this.pointer = new int[2000];
			this.doorTag = new int[2000];
			this.createPointers();
			int num4 = this.doorTag.Length;
			this.switchRot = new float[num4];
			this.chestRot = new float[num4];
			this.chestTrans = new float[num4];
			this.gateTrans = new float[num4];
			this.clickLocation = new Matrix[num4];
			for (int i = 0; i < this.switchRot.Length; i++)
			{
				this.switchRot[i] = 0f;
			}
			for (int j = 0; j < this.gateTrans.Length; j++)
			{
				this.gateTrans[j] = 0f;
			}
			for (int k = 0; k < this.chestTrans.Length; k++)
			{
				this.chestTrans[k] = 0f;
			}
			for (int l = 0; l < this.chestRot.Length; l++)
			{
				this.chestRot[l] = 0f;
			}
			this.loadClickables();
			this.floorFlag = 3;
			this.lastTrigger = 3;
			this.buildFloor(3);
			this.updateGateCollision();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000D9020 File Offset: 0x000D7220
		public new void UnloadContent()
		{
			this.content.Unload();
			this.content.Dispose();
			this.content = null;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000D9040 File Offset: 0x000D7240
		public void resetClickables()
		{
			this.createPointers();
			int num = this.doorTag.Length;
			this.switchRot = new float[num];
			this.chestRot = new float[num];
			this.chestTrans = new float[num];
			this.gateTrans = new float[num];
			this.clickLocation = new Matrix[num];
			for (int i = 0; i < this.switchRot.Length; i++)
			{
				this.switchRot[i] = 0f;
			}
			for (int j = 0; j < this.gateTrans.Length; j++)
			{
				this.gateTrans[j] = 0f;
			}
			for (int k = 0; k < this.chestTrans.Length; k++)
			{
				this.chestTrans[k] = 0f;
			}
			for (int l = 0; l < this.chestRot.Length; l++)
			{
				this.chestRot[l] = 0f;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000D9120 File Offset: 0x000D7320
		public bool level2Floor(int floor, float sum)
		{
			return (floor == 1 && sum != -920f) || (floor == 2 && sum != -460f) || (floor == 3 && sum != 0f) || (floor == 4 && sum != 460f) || (floor == 5 && sum != 920f && sum != 1120f) || (floor == 6 && sum != 1380f);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000D918C File Offset: 0x000D738C
		public void loadOffset()
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StreamReader streamReader = new StreamReader("content/astro/parts/trihall.txt");
			int num = 0;
			this.trihallPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.trihallPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.trihallPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/hall.txt");
			num = 0;
			this.hallwayPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.hallwayPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.hallwayPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/corner.txt");
			num = 0;
			this.cornerPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.cornerPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.cornerPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/power.txt");
			num = 0;
			this.powerPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.powerPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.powerPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/oxygen.txt");
			num = 0;
			this.oxygenPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.oxygenPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.oxygenPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/salvage.txt");
			num = 0;
			this.salvagePos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.salvagePos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.salvagePos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/dead.txt");
			num = 0;
			this.deadPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.deadPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.deadPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/entry.txt");
			num = 0;
			this.entryPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.entryPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.entryPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/gate.txt");
			num = 0;
			this.gatePos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.gatePos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.gatePos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/chest.txt");
			num = 0;
			this.chestPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.chestPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.chestPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/switch.txt");
			num = 0;
			this.switchPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.switchPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.switchPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/trap.txt");
			num = 0;
			this.trapPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.trapPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.trapPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/long.txt");
			num = 0;
			this.longPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.longPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.longPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/cross.txt");
			num = 0;
			this.crossPos = new float[2000];
			while (!streamReader.EndOfStream)
			{
				this.crossPos[num] = Convert.ToSingle(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<float>(ref this.crossPos, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/gatePP.txt");
			num = 0;
			this.gatePP = new int[2000];
			while (!streamReader.EndOfStream)
			{
				this.gatePP[num] = Convert.ToInt32(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<int>(ref this.gatePP, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/chestPP.txt");
			num = 0;
			this.chestPP = new int[2000];
			while (!streamReader.EndOfStream)
			{
				this.chestPP[num] = Convert.ToInt32(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<int>(ref this.chestPP, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/switchPP.txt");
			num = 0;
			this.switchPP = new int[2000];
			while (!streamReader.EndOfStream)
			{
				this.switchPP[num] = Convert.ToInt32(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<int>(ref this.switchPP, num);
			streamReader.Close();
			streamReader.Dispose();
			streamReader = new StreamReader("content/astro/parts/trapPP.txt");
			num = 0;
			this.trapPP = new int[2000];
			while (!streamReader.EndOfStream)
			{
				this.trapPP[num] = Convert.ToInt32(streamReader.ReadLine(), invariantCulture);
				num++;
			}
			Array.Resize<int>(ref this.trapPP, num);
			streamReader.Close();
			streamReader.Dispose();
			this.offsetbyRef(ref this.trihallPos);
			this.offsetbyRef(ref this.hallwayPos);
			this.offsetbyRef(ref this.cornerPos);
			this.offsetbyRef(ref this.deadPos);
			this.offsetbyRef(ref this.longPos);
			this.offsetbyRef(ref this.crossPos);
			this.offsetbyRef(ref this.powerPos);
			this.offsetbyRef(ref this.salvagePos);
			this.offsetbyRef(ref this.oxygenPos);
			this.offsetbyRef(ref this.entryPos);
			this.offsetbyRef(ref this.gatePos);
			this.offsetbyRef(ref this.chestPos);
			this.offsetbyRef(ref this.trapPos);
			this.offsetbyRef(ref this.switchPos);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000D9868 File Offset: 0x000D7A68
		public void offsetbyRef(ref float[] part)
		{
			for (int i = 0; i < part.Length; i += 4)
			{
				part[i] += this.gridset.X;
				part[i + 2] = part[i + 2] + this.gridset.Z;
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000D98B0 File Offset: 0x000D7AB0
		public void createPointers()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < this.chestPos.Length; i += 4)
			{
				this.pointer[num5] = this.chestPP[num2];
				this.doorTag[num5] = this.chestPP[num2 + 1];
				num2 += 4;
				num5++;
			}
			for (int j = 0; j < this.trapPos.Length; j += 4)
			{
				this.pointer[num5] = this.trapPP[num4];
				this.doorTag[num5] = this.trapPP[num4 + 1];
				num4 += 4;
				num5++;
			}
			for (int k = 0; k < this.switchPos.Length; k += 4)
			{
				this.pointer[num5] = this.switchPP[num3];
				this.doorTag[num5] = this.switchPP[num3 + 1];
				num3 += 4;
				num5++;
			}
			for (int l = 0; l < this.gatePos.Length; l += 4)
			{
				this.pointer[num5] = this.gatePP[num];
				this.doorTag[num5] = this.gatePP[num + 1];
				if (this.gatePP[num] == 101)
				{
					this.sc.frontDoor = num5;
				}
				num += 4;
				num5++;
			}
			Array.Resize<int>(ref this.pointer, num5);
			Array.Resize<int>(ref this.doorTag, num5);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000D9A0F File Offset: 0x000D7C0F
		public void loadClickables()
		{
			this.loadClickRef();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000D9A18 File Offset: 0x000D7C18
		public void loadClickRef()
		{
			this.clickCount = 0;
			for (int i = 0; i < this.chestPos.Length; i += 4)
			{
				Vector4 vector = new Vector4(this.chestPos[i], this.chestPos[i + 1], this.chestPos[i + 2], this.chestPos[i + 3]);
				Matrix matrix = Matrix.CreateRotationY(vector.W) * Matrix.CreateTranslation(new Vector3(vector.X, vector.Y, vector.Z));
				this.clickLocation[this.clickCount] = matrix;
				this.clickCount++;
			}
			for (int j = 0; j < this.trapPos.Length; j += 4)
			{
				Vector4 vector2 = new Vector4(this.trapPos[j], this.trapPos[j + 1], this.trapPos[j + 2], this.trapPos[j + 3]);
				Matrix matrix2 = Matrix.CreateRotationY(vector2.W) * Matrix.CreateTranslation(new Vector3(vector2.X, vector2.Y, vector2.Z));
				this.clickLocation[this.clickCount] = matrix2;
				this.clickCount++;
			}
			for (int k = 0; k < this.switchPos.Length; k += 4)
			{
				Vector4 vector3 = new Vector4(this.switchPos[k], this.switchPos[k + 1], this.switchPos[k + 2], this.switchPos[k + 3]);
				Matrix matrix3 = Matrix.CreateRotationY(vector3.W) * Matrix.CreateTranslation(new Vector3(vector3.X, vector3.Y, vector3.Z));
				this.clickLocation[this.clickCount] = matrix3;
				this.clickCount++;
			}
			for (int l = 0; l < this.gatePos.Length; l += 4)
			{
				Vector4 vector4 = new Vector4(this.gatePos[l], this.gatePos[l + 1], this.gatePos[l + 2], this.gatePos[l + 3]);
				Matrix matrix4 = Matrix.CreateRotationY(vector4.W) * Matrix.CreateTranslation(new Vector3(vector4.X, vector4.Y, vector4.Z));
				this.clickLocation[this.clickCount] = matrix4;
				this.clickCount++;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000D9CB4 File Offset: 0x000D7EB4
		public void buildFloor(int floor)
		{
			this.lastFloorBuilt = floor;
			this.clickList.Clear();
			this.clickListBool.Clear();
			this.triList.Clear();
			this.hallList.Clear();
			this.deadList.Clear();
			this.longList.Clear();
			this.cornerList.Clear();
			this.crossList.Clear();
			this.powerList.Clear();
			this.oxygenList.Clear();
			this.salvageList.Clear();
			Facility.facilityPlot = new List<float>();
			Facility.facilityPlot.Clear();
			Facility.facilityPlot.Add(this.entryPos[0]);
			Facility.facilityPlot.Add(this.entryPos[2] + 200f);
			Facility.facilityPlot.Add(this.entryPos[0]);
			Facility.facilityPlot.Add(this.entryPos[2] + 200f + 400f);
			Facility.facilityPlot.Add(this.entryPos[0]);
			Facility.facilityPlot.Add(this.entryPos[2] + 200f + 800f);
			Facility.facilityPlot.Add(this.entryPos[0]);
			Facility.facilityPlot.Add(this.entryPos[2] + 200f + 1200f);
			Facility.facilityPlot.Add(this.entryPos[0]);
			Facility.facilityPlot.Add(this.entryPos[2] + 200f + 1600f);
			Facility.facilityPlot.Add(this.entryPos[0]);
			Facility.facilityPlot.Add(this.entryPos[2] + 200f + 2000f);
			for (int i = 0; i < this.gridwidth * 2; i++)
			{
				for (int j = 0; j < this.gridwidth * 2; j++)
				{
					Facility.heightData[i, j] = this.bottomless;
					Facility.clickable[i, j] = -1;
					this.heightsEntry[i, j] = this.bottomless;
					this.clickableEntry[i, j] = -1;
				}
			}
			Matrix matrix = Matrix.CreateRotationY(this.entryPos[3]) * Matrix.CreateTranslation(new Vector3(this.entryPos[0] / 100f, 0f, this.entryPos[2] / 100f));
			for (int k = 0; k < this.entryHit.Length; k += 3)
			{
				int num = this.entryHit[k] / 100;
				int num2 = (int)((float)this.entryHit[k + 1] / this.scaler);
				int num3 = this.entryHit[k + 2] / 100;
				Vector3 vector = Vector3.Transform(new Vector3((float)num, 0f, (float)num3), matrix);
				this.entryRampHite = 1511f / this.scaler + (float)((int)this.entryPos[1]) + Facility.offset.Y;
				Facility.heightData[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = (int)((float)(num2 + (int)this.entryPos[1]) + Facility.offset.Y);
				Facility.clickable[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = -13;
				if (num >= -2 && num <= 2 && num3 >= -35 && num3 < -31)
				{
					Facility.clickable[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = -17;
				}
				if ((num == -3 || num == 3) && num3 >= -35 && num3 < -26)
				{
					Facility.clickable[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = -17;
				}
				if (num >= -2 && num <= 2 && num3 >= -10 && num3 <= 0)
				{
					Facility.clickable[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = -9;
				}
			}
			for (int l = 0; l < this.entryHitEnter.Length; l += 3)
			{
				int num4 = this.entryHitEnter[l] / 100;
				int num5 = (int)((float)this.entryHitEnter[l + 1] / this.scaler);
				int num6 = this.entryHitEnter[l + 2] / 100;
				Vector3 vector2 = Vector3.Transform(new Vector3((float)num4, 0f, (float)num6), matrix);
				this.heightsEntry[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = (int)((float)(num5 + (int)this.entryPos[1]) + Facility.offset.Y);
				this.clickableEntry[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = -33;
				if (num4 >= -2 && num4 <= 1 && num6 >= -31 && num6 <= -26)
				{
					this.clickableEntry[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = -30;
				}
				if (num6 <= -42)
				{
					this.heightsEntry[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = this.bottomless;
					this.clickableEntry[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = -1;
				}
			}
			this.buildFloorRef(floor);
			this.assignClickables(floor, 0);
			Vector2 vector3 = new Vector2(Facility.facilityPlot[0], Facility.facilityPlot[1]);
			Facility.reachPlot.Add(vector3.X);
			Facility.reachPlot.Add(vector3.Y);
			for (int m = 2; m < Facility.facilityPlot.Count; m += 2)
			{
				Vector2 vector4 = new Vector2(Facility.facilityPlot[m], Facility.facilityPlot[m + 1]);
				string text = this.botPath(ref Facility.facilityPlot, ref Facility.dummyPlot, vector3, vector4);
				if (text == "good")
				{
					Facility.reachPlot.Add(vector4.X);
					Facility.reachPlot.Add(vector4.Y);
				}
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000DA308 File Offset: 0x000D8508
		public void buildFloorRef(int floor)
		{
			if (this.powerPos != null)
			{
				for (int i = 0; i < this.powerPos.Length; i += 4)
				{
					this.powerList.Add(this.powerPos[i]);
					this.powerList.Add(this.powerPos[i + 1]);
					this.powerList.Add(this.powerPos[i + 2]);
					this.powerList.Add(this.powerPos[i + 3]);
					this.powerList.Add(0f);
					Matrix matrix = Matrix.CreateRotationY(this.powerPos[i + 3]) * Matrix.CreateTranslation(new Vector3(this.powerPos[i] / 100f, 0f, this.powerPos[i + 2] / 100f));
					for (int j = -4; j <= 5; j++)
					{
						for (int k = -5; k <= 5; k++)
						{
							Vector3 vector = Vector3.Transform(new Vector3((float)j, 0f, (float)k), matrix);
							Facility.heightData[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = (int)(this.powerPos[i + 1] + Facility.offset.Y);
						}
					}
					for (int l = -1; l <= 1; l++)
					{
						Vector3 vector2 = Vector3.Transform(new Vector3(-5f, 0f, (float)l), matrix);
						Facility.heightData[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = (int)(this.powerPos[i + 1] + Facility.offset.Y);
						vector2 = Vector3.Transform(new Vector3(-6f, 0f, (float)l), matrix);
						Facility.heightData[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = (int)(this.powerPos[i + 1] + Facility.offset.Y);
						vector2 = Vector3.Transform(new Vector3(5f, 0f, (float)l), matrix);
						Facility.heightData[(int)Math.Round((double)vector2.X), (int)Math.Round((double)vector2.Z)] = (int)(this.powerPos[i + 1] + Facility.offset.Y);
					}
					matrix = Matrix.CreateRotationY(this.powerPos[i + 3]);
					for (int m = 0; m <= 1; m++)
					{
						for (int n = -1; n <= 1; n++)
						{
							Vector3 vector3 = Vector3.Transform(new Vector3((float)m, 0f, (float)n), matrix);
							Facility.facilityPlot.Add(this.powerPos[i] + 400f * vector3.X);
							Facility.facilityPlot.Add(this.powerPos[i + 2] + 400f * vector3.Z);
						}
					}
					Vector3 vector4 = Vector3.Transform(new Vector3(-1f, 0f, 0f), matrix);
					Facility.facilityPlot.Add(this.powerPos[i] + 400f * vector4.X);
					Facility.facilityPlot.Add(this.powerPos[i + 2] + 400f * vector4.Z);
				}
			}
			if (this.oxygenPos != null)
			{
				for (int num = 0; num < this.oxygenPos.Length; num += 4)
				{
					this.oxygenList.Add(this.oxygenPos[num]);
					this.oxygenList.Add(this.oxygenPos[num + 1]);
					this.oxygenList.Add(this.oxygenPos[num + 2]);
					this.oxygenList.Add(this.oxygenPos[num + 3]);
					this.oxygenList.Add(0f);
					Matrix matrix2 = Matrix.CreateRotationY(this.oxygenPos[num + 3]) * Matrix.CreateTranslation(new Vector3(this.oxygenPos[num] / 100f, 0f, this.oxygenPos[num + 2] / 100f));
					for (int num2 = -4; num2 <= 5; num2++)
					{
						for (int num3 = -5; num3 <= 5; num3++)
						{
							Vector3 vector5 = Vector3.Transform(new Vector3((float)num2, 0f, (float)num3), matrix2);
							Facility.heightData[(int)Math.Round((double)vector5.X), (int)Math.Round((double)vector5.Z)] = (int)((float)((int)this.oxygenPos[num + 1]) + Facility.offset.Y);
						}
					}
					for (int num4 = -1; num4 <= 1; num4++)
					{
						Vector3 vector6 = Vector3.Transform(new Vector3(-5f, 0f, (float)num4), matrix2);
						Facility.heightData[(int)Math.Round((double)vector6.X), (int)Math.Round((double)vector6.Z)] = (int)((float)((int)this.oxygenPos[num + 1]) + Facility.offset.Y);
						vector6 = Vector3.Transform(new Vector3(-6f, 0f, (float)num4), matrix2);
						Facility.heightData[(int)Math.Round((double)vector6.X), (int)Math.Round((double)vector6.Z)] = (int)((float)((int)this.oxygenPos[num + 1]) + Facility.offset.Y);
						vector6 = Vector3.Transform(new Vector3(5f, 0f, (float)num4), matrix2);
						Facility.heightData[(int)Math.Round((double)vector6.X), (int)Math.Round((double)vector6.Z)] = (int)((float)((int)this.oxygenPos[num + 1]) + Facility.offset.Y);
					}
					matrix2 = Matrix.CreateRotationY(this.oxygenPos[num + 3]);
					for (int num5 = 0; num5 <= 1; num5++)
					{
						for (int num6 = -1; num6 <= 1; num6++)
						{
							Vector3 vector7 = Vector3.Transform(new Vector3((float)num5, 0f, (float)num6), matrix2);
							Facility.facilityPlot.Add(this.oxygenPos[num] + 400f * vector7.X);
							Facility.facilityPlot.Add(this.oxygenPos[num + 2] + 400f * vector7.Z);
						}
					}
					Vector3 vector8 = Vector3.Transform(new Vector3(-1f, 0f, 0f), matrix2);
					Facility.facilityPlot.Add(this.oxygenPos[num] + 400f * vector8.X);
					Facility.facilityPlot.Add(this.oxygenPos[num + 2] + 400f * vector8.Z);
				}
			}
			if (this.salvagePos != null)
			{
				for (int num7 = 0; num7 < this.salvagePos.Length; num7 += 4)
				{
					this.salvageList.Add(this.salvagePos[num7]);
					this.salvageList.Add(this.salvagePos[num7 + 1]);
					this.salvageList.Add(this.salvagePos[num7 + 2]);
					this.salvageList.Add(this.salvagePos[num7 + 3]);
					this.salvageList.Add(0f);
					Matrix matrix3 = Matrix.CreateRotationY(this.salvagePos[num7 + 3]) * Matrix.CreateTranslation(new Vector3(this.salvagePos[num7] / 100f, 0f, this.salvagePos[num7 + 2] / 100f));
					for (int num8 = -4; num8 <= 5; num8++)
					{
						for (int num9 = -5; num9 <= 5; num9++)
						{
							Vector3 vector9 = Vector3.Transform(new Vector3((float)num8, 0f, (float)num9), matrix3);
							Facility.heightData[(int)Math.Round((double)vector9.X), (int)Math.Round((double)vector9.Z)] = (int)(this.salvagePos[num7 + 1] + Facility.offset.Y);
						}
					}
					for (int num10 = -1; num10 <= 1; num10++)
					{
						Vector3 vector10 = Vector3.Transform(new Vector3(-5f, 0f, (float)num10), matrix3);
						Facility.heightData[(int)Math.Round((double)vector10.X), (int)Math.Round((double)vector10.Z)] = (int)(this.salvagePos[num7 + 1] + Facility.offset.Y);
						vector10 = Vector3.Transform(new Vector3(-6f, 0f, (float)num10), matrix3);
						Facility.heightData[(int)Math.Round((double)vector10.X), (int)Math.Round((double)vector10.Z)] = (int)(this.salvagePos[num7 + 1] + Facility.offset.Y);
						vector10 = Vector3.Transform(new Vector3(5f, 0f, (float)num10), matrix3);
						Facility.heightData[(int)Math.Round((double)vector10.X), (int)Math.Round((double)vector10.Z)] = (int)(this.salvagePos[num7 + 1] + Facility.offset.Y);
					}
					matrix3 = Matrix.CreateRotationY(this.salvagePos[num7 + 3]);
					for (int num11 = 0; num11 <= 1; num11++)
					{
						for (int num12 = -1; num12 <= 1; num12++)
						{
							Vector3 vector11 = Vector3.Transform(new Vector3((float)num11, 0f, (float)num12), matrix3);
							Facility.facilityPlot.Add(this.salvagePos[num7] + 400f * vector11.X);
							Facility.facilityPlot.Add(this.salvagePos[num7 + 2] + 400f * vector11.Z);
						}
					}
					Vector3 vector12 = Vector3.Transform(new Vector3(-1f, 0f, 0f), matrix3);
					Facility.facilityPlot.Add(this.salvagePos[num7] + 400f * vector12.X);
					Facility.facilityPlot.Add(this.salvagePos[num7 + 2] + 400f * vector12.Z);
				}
			}
			for (int num13 = 0; num13 < this.trihallPos.Length; num13 += 4)
			{
				this.triList.Add(this.trihallPos[num13]);
				this.triList.Add(this.trihallPos[num13 + 1]);
				this.triList.Add(this.trihallPos[num13 + 2]);
				this.triList.Add(this.trihallPos[num13 + 3]);
				this.triList.Add(0f);
				Matrix matrix4 = Matrix.CreateRotationY(this.trihallPos[num13 + 3]) * Matrix.CreateTranslation(new Vector3(this.trihallPos[num13] / 100f, 0f, this.trihallPos[num13 + 2] / 100f));
				for (int num14 = -6; num14 < 2; num14++)
				{
					for (int num15 = -1; num15 < 2; num15++)
					{
						Vector3 vector13 = Vector3.Transform(new Vector3((float)num14, 0f, (float)num15), matrix4);
						Facility.heightData[(int)Math.Round((double)vector13.X), (int)Math.Round((double)vector13.Z)] = (int)(this.trihallPos[num13 + 1] + Facility.offset.Y);
					}
				}
				for (int num16 = -1; num16 < 2; num16++)
				{
					for (int num17 = -6; num17 < 7; num17++)
					{
						Vector3 vector14 = Vector3.Transform(new Vector3((float)num16, 0f, (float)num17), matrix4);
						Facility.heightData[(int)Math.Round((double)vector14.X), (int)Math.Round((double)vector14.Z)] = (int)(this.trihallPos[num13 + 1] + Facility.offset.Y);
					}
				}
				matrix4 = Matrix.CreateRotationY(this.trihallPos[num13 + 3]);
				for (int num18 = -1; num18 <= 1; num18++)
				{
					Vector3 vector15 = Vector3.Transform(new Vector3(0f, 0f, (float)num18), matrix4);
					Facility.facilityPlot.Add(this.trihallPos[num13] + 400f * vector15.X);
					Facility.facilityPlot.Add(this.trihallPos[num13 + 2] + 400f * vector15.Z);
				}
				Vector3 vector16 = Vector3.Transform(new Vector3(-1f, 0f, 0f), matrix4);
				Facility.facilityPlot.Add(this.trihallPos[num13] + 400f * vector16.X);
				Facility.facilityPlot.Add(this.trihallPos[num13 + 2] + 400f * vector16.Z);
			}
			for (int num19 = 0; num19 < this.hallwayPos.Length; num19 += 4)
			{
				this.hallList.Add(this.hallwayPos[num19]);
				this.hallList.Add(this.hallwayPos[num19 + 1]);
				this.hallList.Add(this.hallwayPos[num19 + 2]);
				this.hallList.Add(this.hallwayPos[num19 + 3]);
				this.hallList.Add(0f);
				Matrix matrix5 = Matrix.CreateRotationY(this.hallwayPos[num19 + 3]) * Matrix.CreateTranslation(new Vector3(this.hallwayPos[num19] / 100f, 0f, this.hallwayPos[num19 + 2] / 100f));
				for (int num20 = -1; num20 < 2; num20++)
				{
					for (int num21 = -6; num21 < 3; num21++)
					{
						Vector3 vector17 = Vector3.Transform(new Vector3((float)num20, 0f, (float)num21), matrix5);
						Facility.heightData[(int)Math.Round((double)vector17.X), (int)Math.Round((double)vector17.Z)] = (int)(this.hallwayPos[num19 + 1] + Facility.offset.Y);
					}
				}
				matrix5 = Matrix.CreateRotationY(this.hallwayPos[num19 + 3]);
				for (int num22 = -1; num22 <= 0; num22++)
				{
					Vector3 vector18 = Vector3.Transform(new Vector3(0f, 0f, (float)num22), matrix5);
					Facility.facilityPlot.Add(this.hallwayPos[num19] + 400f * vector18.X);
					Facility.facilityPlot.Add(this.hallwayPos[num19 + 2] + 400f * vector18.Z);
				}
			}
			for (int num23 = 0; num23 < this.longPos.Length; num23 += 4)
			{
				this.longList.Add(this.longPos[num23]);
				this.longList.Add(this.longPos[num23 + 1]);
				this.longList.Add(this.longPos[num23 + 2]);
				this.longList.Add(this.longPos[num23 + 3]);
				this.longList.Add(1f);
				Matrix matrix6 = Matrix.CreateRotationY(this.longPos[num23 + 3]) * Matrix.CreateTranslation(new Vector3(this.longPos[num23] / 100f, 0f, this.longPos[num23 + 2] / 100f));
				for (int num24 = -1; num24 < 2; num24++)
				{
					for (int num25 = -6; num25 < 7; num25++)
					{
						Vector3 vector19 = Vector3.Transform(new Vector3((float)num24, 0f, (float)num25), matrix6);
						Facility.heightData[(int)Math.Round((double)vector19.X), (int)Math.Round((double)vector19.Z)] = (int)(this.longPos[num23 + 1] + Facility.offset.Y);
					}
				}
				matrix6 = Matrix.CreateRotationY(this.longPos[num23 + 3]);
				for (int num26 = -1; num26 <= 1; num26++)
				{
					Vector3 vector20 = Vector3.Transform(new Vector3(0f, 0f, (float)num26), matrix6);
					Facility.facilityPlot.Add(this.longPos[num23] + 400f * vector20.X);
					Facility.facilityPlot.Add(this.longPos[num23 + 2] + 400f * vector20.Z);
				}
			}
			for (int num27 = 0; num27 < this.cornerPos.Length; num27 += 4)
			{
				this.cornerList.Add(this.cornerPos[num27]);
				this.cornerList.Add(this.cornerPos[num27 + 1]);
				this.cornerList.Add(this.cornerPos[num27 + 2]);
				this.cornerList.Add(this.cornerPos[num27 + 3]);
				this.cornerList.Add(0f);
				Matrix matrix7 = Matrix.CreateRotationY(this.cornerPos[num27 + 3]) * Matrix.CreateTranslation(new Vector3(this.cornerPos[num27] / 100f, 0f, this.cornerPos[num27 + 2] / 100f));
				for (int num28 = -1; num28 < 2; num28++)
				{
					for (int num29 = -6; num29 < 2; num29++)
					{
						Vector3 vector21 = Vector3.Transform(new Vector3((float)num28, 0f, (float)num29), matrix7);
						Facility.heightData[(int)Math.Round((double)vector21.X), (int)Math.Round((double)vector21.Z)] = (int)(this.cornerPos[num27 + 1] + Facility.offset.Y);
					}
				}
				for (int num30 = -6; num30 < 2; num30++)
				{
					for (int num31 = -1; num31 < 2; num31++)
					{
						Vector3 vector22 = Vector3.Transform(new Vector3((float)num30, 0f, (float)num31), matrix7);
						Facility.heightData[(int)Math.Round((double)vector22.X), (int)Math.Round((double)vector22.Z)] = (int)(this.cornerPos[num27 + 1] + Facility.offset.Y);
					}
				}
				matrix7 = Matrix.CreateRotationY(this.cornerPos[num27 + 3]);
				for (int num32 = -1; num32 <= 0; num32++)
				{
					Vector3 vector23 = Vector3.Transform(new Vector3(0f, 0f, (float)num32), matrix7);
					Facility.facilityPlot.Add(this.cornerPos[num27] + 400f * vector23.X);
					Facility.facilityPlot.Add(this.cornerPos[num27 + 2] + 400f * vector23.Z);
				}
				Vector3 vector24 = Vector3.Transform(new Vector3(-1f, 0f, 0f), matrix7);
				Facility.facilityPlot.Add(this.cornerPos[num27] + 400f * vector24.X);
				Facility.facilityPlot.Add(this.cornerPos[num27 + 2] + 400f * vector24.Z);
			}
			for (int num33 = 0; num33 < this.deadPos.Length; num33 += 4)
			{
				this.deadList.Add(this.deadPos[num33]);
				this.deadList.Add(this.deadPos[num33 + 1]);
				this.deadList.Add(this.deadPos[num33 + 2]);
				this.deadList.Add(this.deadPos[num33 + 3]);
				this.deadList.Add(0f);
				Matrix matrix8 = Matrix.CreateRotationY(this.deadPos[num33 + 3]) * Matrix.CreateTranslation(new Vector3(this.deadPos[num33] / 100f, 0f, this.deadPos[num33 + 2] / 100f));
				for (int num34 = -1; num34 < 2; num34++)
				{
					for (int num35 = -6; num35 < 2; num35++)
					{
						Vector3 vector25 = Vector3.Transform(new Vector3((float)num34, 0f, (float)num35), matrix8);
						Facility.heightData[(int)Math.Round((double)vector25.X), (int)Math.Round((double)vector25.Z)] = (int)(this.deadPos[num33 + 1] + Facility.offset.Y);
					}
				}
				matrix8 = Matrix.CreateRotationY(this.deadPos[num33 + 3]);
				for (int num36 = -1; num36 <= 0; num36++)
				{
					Vector3 vector26 = Vector3.Transform(new Vector3(0f, 0f, (float)num36), matrix8);
					Facility.facilityPlot.Add(this.deadPos[num33] + 400f * vector26.X);
					Facility.facilityPlot.Add(this.deadPos[num33 + 2] + 400f * vector26.Z);
				}
			}
			for (int num37 = 0; num37 < this.crossPos.Length; num37 += 4)
			{
				this.crossList.Add(this.crossPos[num37]);
				this.crossList.Add(this.crossPos[num37 + 1]);
				this.crossList.Add(this.crossPos[num37 + 2]);
				this.crossList.Add(this.crossPos[num37 + 3]);
				this.crossList.Add(0f);
				Matrix matrix9 = Matrix.CreateRotationY(this.crossPos[num37 + 3]) * Matrix.CreateTranslation(new Vector3(this.crossPos[num37] / 100f, 0f, this.crossPos[num37 + 2] / 100f));
				for (int num38 = -6; num38 < 7; num38++)
				{
					for (int num39 = -1; num39 < 2; num39++)
					{
						Vector3 vector27 = Vector3.Transform(new Vector3((float)num38, 0f, (float)num39), matrix9);
						Facility.heightData[(int)Math.Round((double)vector27.X), (int)Math.Round((double)vector27.Z)] = (int)(this.crossPos[num37 + 1] + Facility.offset.Y);
					}
				}
				for (int num40 = -1; num40 < 2; num40++)
				{
					for (int num41 = -6; num41 < 7; num41++)
					{
						Vector3 vector28 = Vector3.Transform(new Vector3((float)num40, 0f, (float)num41), matrix9);
						Facility.heightData[(int)Math.Round((double)vector28.X), (int)Math.Round((double)vector28.Z)] = (int)(this.crossPos[num37 + 1] + Facility.offset.Y);
					}
				}
				matrix9 = Matrix.CreateRotationY(this.crossPos[num37 + 3]);
				for (int num42 = -1; num42 <= 1; num42++)
				{
					Vector3 vector29 = Vector3.Transform(new Vector3(0f, 0f, (float)num42), matrix9);
					Facility.facilityPlot.Add(this.crossPos[num37] + 400f * vector29.X);
					Facility.facilityPlot.Add(this.crossPos[num37 + 2] + 400f * vector29.Z);
				}
				Vector3 vector30 = Vector3.Transform(new Vector3(-1f, 0f, 0f), matrix9);
				Facility.facilityPlot.Add(this.crossPos[num37] + 400f * vector30.X);
				Facility.facilityPlot.Add(this.crossPos[num37 + 2] + 400f * vector30.Z);
				vector30 = Vector3.Transform(new Vector3(1f, 0f, 0f), matrix9);
				Facility.facilityPlot.Add(this.crossPos[num37] + 400f * vector30.X);
				Facility.facilityPlot.Add(this.crossPos[num37 + 2] + 400f * vector30.Z);
			}
			for (int num43 = 0; num43 < this.chestPos.Length; num43 += 4)
			{
				Matrix matrix10 = Matrix.CreateRotationY(this.chestPos[num43 + 3]) * Matrix.CreateTranslation(new Vector3(this.chestPos[num43] / 100f, 0f, this.chestPos[num43 + 2] / 100f));
				Vector3 vector31 = Vector3.Transform(new Vector3(0f, 0f, 0f), matrix10);
				Facility.heightData[(int)Math.Round((double)vector31.X), (int)Math.Round((double)vector31.Z)] = this.bottomless;
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000DBB38 File Offset: 0x000D9D38
		private string botPath(ref List<float> floorplot, ref List<Vector2> botPath, Vector2 startpos, Vector2 destiny)
		{
			List<Vector2> list = new List<Vector2>();
			float num = 800f;
			int num2 = -1;
			for (int i = 0; i < floorplot.Count; i += 2)
			{
				list.Add(new Vector2(floorplot[i] + 0f, floorplot[i + 1] + 0f));
				float num3 = Vector2.Distance(new Vector2(floorplot[i] + 0f, floorplot[i + 1] + 0f), startpos);
				if (num3 <= num)
				{
					num = num3;
					num2 = i;
				}
			}
			if (num2 == -1)
			{
				return "lost early";
			}
			Vector2 vector = new Vector2(floorplot[num2] + 0f, floorplot[num2 + 1] + 0f);
			List<Vector2> search = new List<Vector2>();
			List<Vector2> list2 = new List<Vector2>();
			list.ForEach(delegate(Vector2 item)
			{
				search.Add(item);
			});
			bool flag = this.buildbotPath(ref search, ref list2, vector, destiny);
			botPath.Clear();
			for (int j = 0; j < list2.Count; j++)
			{
				botPath.Add(list2[j]);
			}
			botPath.Add(destiny);
			if (!flag)
			{
				return "lost here ";
			}
			return "good";
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000DBC7C File Offset: 0x000D9E7C
		private bool buildbotPath(ref List<Vector2> source, ref List<Vector2> result, Vector2 start, Vector2 end)
		{
			bool flag = true;
			Random random = new Random();
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < source.Count; i++)
			{
				float num = Vector2.Distance(start, source[i]);
				if (num > 50f && num <= 400f)
				{
					list.Add(source[i]);
				}
			}
			if (list.Count == 0)
			{
				return false;
			}
			List<Vector2> list2 = new List<Vector2>();
			while (list.Count > 0)
			{
				int num2 = random.Next(0, list.Count);
				list2.Add(list[num2]);
				list.RemoveAt(num2);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (Vector2.Distance(end, list2[j]) <= 500f)
				{
					result.Add(list2[j]);
					return true;
				}
				start = list2[j];
				source.Remove(start);
				result.Add(start);
				flag = this.buildbotPath(ref source, ref result, start, end);
				if (flag)
				{
					return true;
				}
				if (!flag)
				{
					result.Remove(start);
				}
			}
			return flag;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000DBD94 File Offset: 0x000D9F94
		public void assignClickables(int floor, int dist)
		{
			for (int i = 0; i < this.clickCount; i++)
			{
				bool flag = this.pointer[i] >= 290 && this.pointer[i] <= 291;
				if (this.pointer[i] > 299)
				{
					int num = this.pointer[i];
				}
				Vector3 vector = Vector3.Transform(Vector3.Zero, this.clickLocation[i]);
				if ((floor != 1 || vector.Y < -460f) && (floor != 2 || (vector.Y >= -460f && vector.Y < 0f)) && (floor != 3 || (vector.Y >= 0f && vector.Y < 460f)) && (floor != 4 || (vector.Y >= 460f && vector.Y < 920f)) && (floor != 5 || (vector.Y >= 920f && vector.Y < 1380f)) && (floor != 6 || (vector.Y >= 1380f && vector.Y < 1840f)))
				{
					if (!flag)
					{
						this.addClickabletoGrid(vector, i);
					}
					this.clickList.Add(i);
					this.clickListBool.Add(true);
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000DBEF8 File Offset: 0x000DA0F8
		public void addClickabletoGrid(Vector3 mov, int num)
		{
			if (this.pointer[num] < 1)
			{
				return;
			}
			mov.X = (float)((int)Math.Round((double)(mov.X / 100f)));
			mov.Z = (float)((int)Math.Round((double)(mov.Z / 100f)));
			Facility.clickable[(int)mov.X - 1, (int)mov.Z - 1] = num;
			Facility.clickable[(int)mov.X, (int)mov.Z - 1] = num;
			Facility.clickable[(int)mov.X + 1, (int)mov.Z - 1] = num;
			Facility.clickable[(int)mov.X - 1, (int)mov.Z] = num;
			Facility.clickable[(int)mov.X, (int)mov.Z] = num;
			Facility.clickable[(int)mov.X + 1, (int)mov.Z] = num;
			Facility.clickable[(int)mov.X - 1, (int)mov.Z + 1] = num;
			Facility.clickable[(int)mov.X, (int)mov.Z + 1] = num;
			Facility.clickable[(int)mov.X + 1, (int)mov.Z + 1] = num;
			if (this.pointer[num] >= 100 && this.pointer[num] <= 150)
			{
				Vector3 vector = Vector3.Transform(Vector3.Zero, this.clickLocation[num]);
				Vector3 vector2 = Vector3.Transform(new Vector3(0f, 0f, -1f), this.clickLocation[num]) - vector;
				Vector3 vector3 = Vector3.Transform(new Vector3(-1f, 0f, 0f), this.clickLocation[num]) - vector;
				int num2 = (int)vector2.X;
				int num3 = (int)vector2.Z;
				int num4 = (int)vector3.X;
				int num5 = (int)vector3.Z;
				Facility.clickable[(int)mov.X + num4 * -1 + num2 * 2, (int)mov.Z + num5 * -1 + num3 * 2] = num;
				Facility.clickable[(int)mov.X + num2 * 2, (int)mov.Z + num3 * 2] = num;
				Facility.clickable[(int)mov.X + num4 + num2 * 2, (int)mov.Z + num5 + num3 * 2] = num;
				Facility.clickable[(int)mov.X + num4 * -1 + num2 * -2, (int)mov.Z + num5 * -1 + num3 * -2] = num;
				Facility.clickable[(int)mov.X + num2 * -2, (int)mov.Z + num3 * -2] = num;
				Facility.clickable[(int)mov.X + num4 + num2 * -2, (int)mov.Z + num5 + num3 * -2] = num;
				Facility.clickable[(int)mov.X + num4 * -1 + num2 * 3, (int)mov.Z + num5 * -1 + num3 * 3] = num;
				Facility.clickable[(int)mov.X + num2 * 3, (int)mov.Z + num3 * 3] = num;
				Facility.clickable[(int)mov.X + num4 + num2 * 3, (int)mov.Z + num5 + num3 * 3] = num;
				Facility.clickable[(int)mov.X + num4 * -1 + num2 * -3, (int)mov.Z + num5 * -1 + num3 * -3] = num;
				Facility.clickable[(int)mov.X + num2 * -3, (int)mov.Z + num3 * -3] = num;
				Facility.clickable[(int)mov.X + num4 + num2 * -3, (int)mov.Z + num5 + num3 * -3] = num;
				Facility.clickable[(int)mov.X + num4 * -1 + num2 * 4, (int)mov.Z + num5 * -1 + num3 * 4] = num;
				Facility.clickable[(int)mov.X + num2 * 4, (int)mov.Z + num3 * 4] = num;
				Facility.clickable[(int)mov.X + num4 + num2 * 4, (int)mov.Z + num5 + num3 * 4] = num;
				Facility.clickable[(int)mov.X + num4 * -1 + num2 * -4, (int)mov.Z + num5 * -1 + num3 * -4] = num;
				Facility.clickable[(int)mov.X + num2 * -4, (int)mov.Z + num3 * -4] = num;
				Facility.clickable[(int)mov.X + num4 + num2 * -4, (int)mov.Z + num5 + num3 * -4] = num;
			}
			if ((this.pointer[num] > 0 && this.pointer[num] <= 44) || (this.pointer[num] >= 500 && this.pointer[num] < 700))
			{
				Vector3 vector4 = Vector3.Transform(Vector3.Zero, this.clickLocation[num]);
				Vector3 vector5 = Vector3.Transform(new Vector3(-1f, 0f, 0f), this.clickLocation[num]) - vector4;
				Vector3 vector6 = Vector3.Transform(new Vector3(0f, 0f, -1f), this.clickLocation[num]) - vector4;
				int num6 = (int)vector5.X;
				int num7 = (int)vector5.Z;
				int num8 = (int)vector6.X;
				int num9 = (int)vector6.Z;
				int num10 = Facility.clickable[(int)mov.X + num8 * -1 + num6 * 2, (int)mov.Z + num9 * -1 + num7 * 2];
				int num11 = Facility.clickable[(int)mov.X + num6 * 2, (int)mov.Z + num7 * 2];
				int num12 = Facility.clickable[(int)mov.X + num8 + num6 * 2, (int)mov.Z + num9 + num7 * 2];
				if (num10 < 0)
				{
					Facility.clickable[(int)mov.X + num8 * -1 + num6 * 2, (int)mov.Z + num9 * -1 + num7 * 2] = num;
				}
				if (num11 < 0)
				{
					Facility.clickable[(int)mov.X + num6 * 2, (int)mov.Z + num7 * 2] = num;
				}
				if (num12 < 0)
				{
					Facility.clickable[(int)mov.X + num8 + num6 * 2, (int)mov.Z + num9 + num7 * 2] = num;
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000DC60C File Offset: 0x000DA80C
		public void updateGateCollision()
		{
			for (int i = 0; i < this.gatePos.Length; i += 4)
			{
				Matrix matrix = Matrix.CreateRotationY(this.gatePos[i + 3]) * Matrix.CreateTranslation(new Vector3(this.gatePos[i] / 100f, 0f, this.gatePos[i + 2] / 100f));
				Vector3 vector = Vector3.Transform(new Vector3(0f, 0f, 0f), matrix);
				int num = this.bottomless;
				int num2 = Facility.clickable[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)];
				if (num2 > -1)
				{
					if (this.pointer[num2] >= 100 && this.pointer[num2] <= 109)
					{
						num = this.bottomless;
					}
					if (this.pointer[num2] >= 110 && this.pointer[num2] <= 119)
					{
						num = (int)(this.gatePos[i + 1] / this.scaler + Facility.offset.Y);
					}
					for (int j = -2; j < 3; j++)
					{
						vector = Vector3.Transform(new Vector3((float)j, 0f, 0f), matrix);
						Facility.heightData[(int)Math.Round((double)vector.X), (int)Math.Round((double)vector.Z)] = num;
					}
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x000DC770 File Offset: 0x000DA970
		public void closeGates()
		{
			for (int i = 0; i < this.closeGate.Count; i++)
			{
				int num = this.closeGate[i];
				if (this.pointer[num] < 110 || this.pointer[num] > 119)
				{
					this.closeGate.Remove(num);
					return;
				}
				float num2 = 400f;
				if (this.gateTrans[num] == num2 && !this.sc.astronaut.doorList.Contains(num))
				{
					if (this.clickList.Contains(num))
					{
						Vector3 vector = Vector3.Transform(Vector3.Zero, this.clickLocation[num] * this.faciltyMatrix);
						if (Vector3.Distance(vector, this.campos) > 100f)
						{
							this.sc.door.Play(this.sc.ev * 0.2f, -0.2f, 0f);
							this.pointer[num] -= 10;
							this.closeGate.Remove(num);
							return;
						}
					}
					else if (num != this.sc.frontDoor)
					{
						this.pointer[num] -= 10;
						this.gateTrans[num] = 0f;
						this.closeGate.Remove(num);
						return;
					}
				}
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000DC8E8 File Offset: 0x000DAAE8
		public void opengateSLOW(int tempGate)
		{
			if (tempGate < 0)
			{
				return;
			}
			if (this.pointer[tempGate] >= 115)
			{
				this.pointer[tempGate] -= 5;
			}
			if (this.pointer[tempGate] >= 100 && this.pointer[tempGate] < 110)
			{
				if (this.pointer[tempGate] > 104)
				{
					this.pointer[tempGate] -= 5;
				}
				this.pointer[tempGate] += 10;
			}
			if (this.pointer[tempGate] != 101 && this.pointer[tempGate] != 111 && tempGate > 0)
			{
				int num = this.doorTag[tempGate];
				int num2 = this.doorTag[tempGate];
			}
			if ((this.pointer[tempGate] == 110 || this.pointer[tempGate] == 111) && !this.closeGate.Contains(tempGate))
			{
				this.closeGate.Add(tempGate);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000DC9D8 File Offset: 0x000DABD8
		public void opengateFAST(int tempGate)
		{
			if (this.pointer[tempGate] >= 115)
			{
				this.pointer[tempGate] -= 5;
			}
			if (this.pointer[tempGate] >= 100 && this.pointer[tempGate] < 110)
			{
				if (this.pointer[tempGate] > 104)
				{
					this.pointer[tempGate] -= 5;
				}
				this.pointer[tempGate] += 10;
			}
			float num = 240f;
			if (tempGate == this.sc.frontDoor)
			{
				num = 360f;
			}
			else if (this.doorTag[tempGate] > 0)
			{
				num = 360f;
			}
			this.gateTrans[tempGate] = num;
			if ((this.pointer[tempGate] == 110 || this.pointer[tempGate] == 111) && !this.closeGate.Contains(tempGate))
			{
				this.closeGate.Add(tempGate);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000DCACC File Offset: 0x000DACCC
		public float animGate(int p)
		{
			if (this.pointer[p] >= 100 && this.pointer[p] < 110)
			{
				float num = -20f;
				if (this.gateTrans[p] > 0f)
				{
					this.gateTrans[p] += num;
					if (this.gateTrans[p] == 300f)
					{
						this.updateGateCollision();
					}
					if (this.gateTrans[p] <= 0f)
					{
						this.gateTrans[p] = 0f;
						this.updateGateCollision();
						int num2 = this.pointer[p];
					}
				}
				return this.gateTrans[p];
			}
			if (this.pointer[p] >= 110 && this.pointer[p] <= 120)
			{
				float num3 = 400f;
				float num = 20f;
				if (this.gateTrans[p] < num3)
				{
					this.gateTrans[p] += num;
					if (this.gateTrans[p] == 100f)
					{
						this.updateGateCollision();
					}
					if (this.gateTrans[p] >= num3)
					{
						this.gateTrans[p] = num3;
						this.updateGateCollision();
					}
				}
				return this.gateTrans[p];
			}
			return this.gateTrans[p];
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000DCC00 File Offset: 0x000DAE00
		public bool Ktoggle(Keys k)
		{
			return this.keyState.IsKeyDown(k) && this.prevkeyState.IsKeyUp(k);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000DCC2C File Offset: 0x000DAE2C
		public void HandleInput(InputState input, GamePadState gamePadState, GamePadState prevstate, Vector3 camposX, Vector3 camlookpos)
		{
			int num = this.gridVal;
			this.campos = camposX;
			this.camlookpos = camlookpos;
			this.prevkeyState = input.lastKeyState;
			this.keyState = input.currentKeyState;
			if (this.sc.astronaut.doorList.Count > 0)
			{
				for (int i = 0; i < this.sc.astronaut.doorList.Count; i++)
				{
					int num2 = this.sc.astronaut.doorList[i];
					if (this.pointer[num2] >= 100 && this.pointer[num2] <= 104)
					{
						this.sc.door.Play(this.sc.ev * 0.1f, 0f, 0f);
						this.opengateSLOW(num2);
					}
				}
			}
			if (this.myframe % 20 == 0)
			{
				Facility.atSwitch = false;
				Facility.atSwitch2 = false;
				Facility.atMain = false;
				if (num > -1)
				{
					this.pos = Vector3.Transform(Vector3.Zero, this.clickLocation[num] * this.faciltyMatrix);
					if (this.pointer[num] > 249 && this.pointer[num] < 280)
					{
						this.pos = Vector3.Transform(new Vector3(-278f, 210f, 0f), this.clickLocation[num] * this.faciltyMatrix);
					}
					this.norm1 = Vector2.Normalize(new Vector2(this.pos.X, this.pos.Z) - new Vector2(this.campos.X, this.campos.Z));
					this.norm2 = Vector2.Normalize(new Vector2(camlookpos.X, camlookpos.Z) - new Vector2(this.campos.X, this.campos.Z));
					this.dot = Vector2.Dot(this.norm1, this.norm2);
					this.facingIt = this.dot > 0.7f;
					if (this.facingIt)
					{
						if (this.pointer[num] >= 250 && this.pointer[num] <= 280)
						{
							if (this.doorTag[num] == 49)
							{
								Facility.atSwitch = true;
							}
							if (this.doorTag[num] == 40)
							{
								Facility.atSwitch2 = true;
							}
						}
						if (this.pointer[num] == 101)
						{
							Facility.atMain = true;
						}
					}
				}
			}
			if ((this.Ktoggle(this.sc.x_key) || (gamePadState.Buttons.A == ButtonState.Pressed && prevstate.Buttons.A == ButtonState.Released)) && num > -1)
			{
				this.pos = Vector3.Transform(Vector3.Zero, this.clickLocation[num] * this.faciltyMatrix);
				if (this.pointer[num] > 249 && this.pointer[num] < 280)
				{
					this.pos = Vector3.Transform(new Vector3(-278f, 210f, 0f), this.clickLocation[num] * this.faciltyMatrix);
				}
				this.norm1 = Vector2.Normalize(new Vector2(this.pos.X, this.pos.Z) - new Vector2(this.campos.X, this.campos.Z));
				this.norm2 = Vector2.Normalize(new Vector2(camlookpos.X, camlookpos.Z) - new Vector2(this.campos.X, this.campos.Z));
				this.dot = Vector2.Dot(this.norm1, this.norm2);
				this.facingIt = this.dot > 0.7f;
				if (this.facingIt)
				{
					if (this.pointer[num] >= 250 && this.pointer[num] <= 280)
					{
						if (this.pointer[num] == 250)
						{
							this.switchRot[num] = 0f;
							this.pointer[num] = 251;
						}
						else if (this.pointer[num] == 251)
						{
							this.switchRot[num] = -1.57f;
							this.pointer[num] = 250;
						}
						if (this.doorTag[num] == 49)
						{
							this.sc.lever.Play(this.sc.ev, 0.2f, 0f);
							Facility.openConstruction = true;
						}
						if (this.doorTag[num] == 40)
						{
							Facility.createWorkerLoc = new Vector4(this.salvageList[0] / this.scaler + Facility.offset.X, this.salvageList[1] / this.scaler + Facility.offset.Y, this.salvageList[2] / this.scaler + Facility.offset.Z, this.salvageList[3]);
						}
					}
					if (this.pointer[num] >= 100 && this.pointer[num] <= 104)
					{
						if (this.pointer[num] == 101)
						{
							this.sc.door.Play(this.sc.ev * 0.4f, 0f, 0f);
							this.opengateSLOW(num);
							return;
						}
						this.sc.door.Play(this.sc.ev * 0.4f, 0f, 0f);
						this.opengateSLOW(num);
						return;
					}
					else if (this.pointer[num] >= 105 && this.pointer[num] <= 109 && this.gateShake == 0)
					{
						if (this.doorTag[num] == 0)
						{
							this.gateShake = 61;
							this.clangID = num;
							this.gateShakeMatrix = this.clickLocation[num];
							if (Math.Abs(this.campos.X - camlookpos.X) > Math.Abs(this.campos.Z - camlookpos.Z))
							{
								this.gateRot = 0f;
							}
							else
							{
								this.gateRot = 1.57f;
							}
						}
						if (this.doorTag[num] == 64)
						{
							this.pointer[num] = 100;
							this.sc.click.Play(this.sc.ev, 0f, 0f);
						}
					}
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000DD2D8 File Offset: 0x000DB4D8
		public void Update()
		{
			this.myframe++;
			if (this.gateShake > 0)
			{
				this.gateShake--;
				float num = (float)Math.Sin((double)((float)this.gateShake)) * (float)(this.gateShake / 12);
				Vector3 vector = Vector3.Transform(new Vector3(num, 0f, 0f), Matrix.CreateRotationY(this.gateRot));
				this.clickLocation[this.clangID] *= Matrix.CreateTranslation(vector);
				if (this.gateShake == 0)
				{
					this.clickLocation[this.clangID] = this.gateShakeMatrix;
				}
			}
			if (this.myframe % 10 == 0)
			{
				Vector2 vector2 = new Vector2(this.campos.X, this.campos.Z);
				for (int i = 0; i < this.clickList.Count; i++)
				{
					int num2 = this.clickList[i];
					Vector3 vector3 = Vector3.Transform(Vector3.Zero, this.clickLocation[num2] * this.faciltyMatrix);
					float num3 = Vector2.DistanceSquared(vector2, new Vector2(vector3.X, vector3.Z));
					if (this.pointer[num2] > 249 && this.pointer[num2] < 280)
					{
						vector3 = Vector3.Transform(new Vector3(-200f, 170f, 0f), this.clickLocation[num2] * this.faciltyMatrix);
					}
					this.norm1 = Vector2.Normalize(new Vector2(vector3.X, vector3.Z) - new Vector2(this.campos.X, this.campos.Z));
					this.norm2 = Vector2.Normalize(new Vector2(this.camlookpos.X, this.camlookpos.Z) - new Vector2(this.campos.X, this.campos.Z));
					this.dot = Vector2.Dot(this.norm1, this.norm2);
					bool flag = this.dot > 0.2f;
					this.clickListBool[i] = false;
					if ((num3 < this.sightLimit && flag) || num3 < 90000f)
					{
						this.clickListBool[i] = true;
					}
				}
				this.checkFacing(ref this.triList);
				this.checkFacing(ref this.longList);
				this.checkFacing(ref this.hallList);
				this.checkFacing(ref this.cornerList);
				this.checkFacing(ref this.deadList);
				this.checkFacing(ref this.crossList);
				this.checkFacing(ref this.powerList);
				this.checkFacing(ref this.oxygenList);
				this.checkFacing(ref this.salvageList);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000DD5CC File Offset: 0x000DB7CC
		public void checkFacing(ref List<float> parts)
		{
			Vector2 vector = new Vector2(this.campos.X, this.campos.Z);
			for (int i = 0; i < parts.Count; i += 5)
			{
				Vector3 vector2 = new Vector3(parts[i], parts[i + 1], parts[i + 2]);
				vector2.X /= this.scaler;
				vector2.Z /= this.scaler;
				vector2 += Facility.offset;
				float num = Vector2.DistanceSquared(vector, new Vector2(vector2.X, vector2.Z));
				this.norm1 = Vector2.Normalize(new Vector2(vector2.X, vector2.Z) - new Vector2(this.campos.X, this.campos.Z));
				this.norm2 = Vector2.Normalize(new Vector2(this.camlookpos.X, this.camlookpos.Z) - new Vector2(this.campos.X, this.campos.Z));
				this.dot = Vector2.Dot(this.norm1, this.norm2);
				bool flag = this.dot > 0.1f;
				parts[i + 4] = 0f;
				if ((num < this.sightLimit && flag) || num < 160000f)
				{
					float num2 = 1f - (float)(Math.Sqrt((double)num) - 50.0) / 400f;
					float num3 = MathHelper.Clamp(num2, 0.02f, 1f);
					parts[i + 4] = num3 * this.sc.ev;
				}
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000DD798 File Offset: 0x000DB998
		public void DrawBlack(Matrix viewMatrix, Matrix projectionMatrix, Vector3 campos)
		{
			this.faciltyMatrix = Matrix.CreateScale(1f / this.scaler) * Matrix.CreateTranslation(Facility.offset);
			this.fogEffect.Parameters["view"].SetValue(viewMatrix);
			this.fogEffect.Parameters["projection"].SetValue(projectionMatrix);
			this.fogEffect.Parameters["lightColor"].SetValue(new Vector4(1f, 1f, 1f, 1f));
			new Vector2(campos.X, campos.Z);
			string text = "black";
			this.facShader.Parameters["view"].SetValue(viewMatrix);
			this.facShader.Parameters["projection"].SetValue(projectionMatrix);
			this.facShader.Parameters["campos"].SetValue(campos);
			this.drawingEntry = Facility.outsideCastle || this.gridVal < -4 || this.gateTrans[this.sc.frontDoor] != 0f || this.gridVal == this.sc.frontDoor;
			if (this.drawingEntry)
			{
				this.DrawEntryWay(this.entryWay, new Vector3(this.entryPos[0], this.entryPos[1], this.entryPos[2]), this.entryPos[3], this.entryTexture, this.dirtrgb, "green");
			}
			for (int i = 0; i < this.powerList.Count; i += 5)
			{
				int num = (int)((this.powerList[i + 1] + 920f) / 460f);
				if (this.powerList[i + 4] > 0f)
				{
					this.castlePartGreen(this.powerHall, new Vector3(this.powerList[i], this.powerList[i + 1], this.powerList[i + 2]), this.powerList[i + 3], this.whiteRect, this.rgbRect[num], text);
				}
			}
			for (int j = 0; j < this.oxygenList.Count; j += 5)
			{
				int num2 = (int)((this.oxygenList[j + 1] + 920f) / 460f);
				if (this.oxygenList[j + 4] > 0f)
				{
					this.castlePartGreen(this.oxygenHall, new Vector3(this.oxygenList[j], this.oxygenList[j + 1], this.oxygenList[j + 2]), this.oxygenList[j + 3], this.whiteRect, this.rgbRect[num2], text);
				}
			}
			for (int k = 0; k < this.salvageList.Count; k += 5)
			{
				int num3 = (int)((this.salvageList[k + 1] + 920f) / 460f);
				if (this.salvageList[k + 4] > 0f)
				{
					this.castlePartGreen(this.salvageHall, new Vector3(this.salvageList[k], this.salvageList[k + 1], this.salvageList[k + 2]), this.salvageList[k + 3], this.whiteRect, this.rgbRect[num3], text);
				}
			}
			for (int l = 0; l < this.triList.Count; l += 5)
			{
				int num4 = (int)((this.triList[l + 1] + 920f) / 460f);
				if (this.triList[l + 4] > 0f)
				{
					this.castlePartGreen(this.triHall, new Vector3(this.triList[l], this.triList[l + 1], this.triList[l + 2]), this.triList[l + 3], this.whiteRect, this.rgbRect[num4], text);
				}
			}
			for (int m = 0; m < this.hallList.Count; m += 5)
			{
				int num5 = (int)((this.hallList[m + 1] + 920f) / 460f);
				if (this.hallList[m + 4] > 0f)
				{
					this.castlePartGreen(this.hallWay, new Vector3(this.hallList[m], this.hallList[m + 1], this.hallList[m + 2]), this.hallList[m + 3], this.whiteRect, this.rgbRect[num5], text);
				}
			}
			for (int n = 0; n < this.crossList.Count; n += 5)
			{
				int num6 = (int)((this.crossList[n + 1] + 920f) / 460f);
				if (this.crossList[n + 4] > 0f)
				{
					this.castlePartGreen(this.crossHall, new Vector3(this.crossList[n], this.crossList[n + 1], this.crossList[n + 2]), this.crossList[n + 3], this.whiteRect, this.rgbRect[num6], text);
				}
			}
			for (int num7 = 0; num7 < this.longList.Count; num7 += 5)
			{
				int num8 = (int)((this.longList[num7 + 1] + 920f) / 460f);
				if (this.longList[num7 + 4] > 0f)
				{
					this.castlePartGreen(this.longHall, new Vector3(this.longList[num7], this.longList[num7 + 1], this.longList[num7 + 2]), this.longList[num7 + 3], this.whiteRect, this.rgbRect[num8], text);
				}
			}
			for (int num9 = 0; num9 < this.cornerList.Count; num9 += 5)
			{
				int num10 = (int)((this.cornerList[num9 + 1] + 920f) / 460f);
				if (this.cornerList[num9 + 4] > 0f)
				{
					this.castlePartGreen(this.corner, new Vector3(this.cornerList[num9], this.cornerList[num9 + 1], this.cornerList[num9 + 2]), this.cornerList[num9 + 3], this.whiteRect, this.rgbRect[num10], text);
				}
			}
			for (int num11 = 0; num11 < this.deadList.Count; num11 += 5)
			{
				int num12 = (int)((this.deadList[num11 + 1] + 920f) / 460f);
				if (this.deadList[num11 + 4] > 0f)
				{
					this.castlePartGreen(this.deadend, new Vector3(this.deadList[num11], this.deadList[num11 + 1], this.deadList[num11 + 2]), this.deadList[num11 + 3], this.whiteRect, this.rgbRect[num12], text);
				}
			}
			for (int num13 = 0; num13 < this.clickList.Count; num13++)
			{
				int num14 = this.clickList[num13];
				if (this.pointer[num14] >= 290)
				{
					int num15 = this.pointer[num14];
				}
				if (this.pointer[num14] > 0 && (this.clickListBool[num13] || (!Facility.inFacility && num14 == this.sc.frontDoor)))
				{
					if (this.pointer[num14] >= 100 && this.pointer[num14] <= 150)
					{
						if (num14 != this.sc.frontDoor)
						{
							this.DrawSpaceDoor(this.mainDoor, this.clickLocation[num14], this.gateTrans[num14], this.gateshadRect, this.gatergbRect2, text);
						}
						if (num14 == this.sc.frontDoor)
						{
							this.DrawSpaceDoor(this.mainDoor, this.clickLocation[num14], this.gateTrans[num14], this.gateshadRect, this.gateOrangeRect, text);
						}
					}
					if (this.pointer[num14] >= 300 && this.pointer[num14] <= 390)
					{
						if (this.pointer[num14] != 351)
						{
							int num16 = this.pointer[num14];
						}
						int num17 = this.doorTag[num14] % 1000;
						if (num17 % 10 == 4)
						{
							this.drawChest(this.vaultChest2, this.clickLocation[num14], this.spiderchestRect, text, num14, this.pointer[num14]);
						}
						else if (num17 % 10 == 2)
						{
							this.drawChest(this.vaultChest2, this.clickLocation[num14], this.kingchestRect, text, num14, this.pointer[num14]);
						}
						else if (num17 % 10 == 5)
						{
							this.drawChest(this.vaultChest3, this.clickLocation[num14], this.chestRect3, text, num14, this.pointer[num14]);
						}
						else
						{
							this.drawChest(this.vaultChest, this.clickLocation[num14], this.chestRect, text, num14, this.pointer[num14]);
						}
					}
				}
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000DE1F8 File Offset: 0x000DC3F8
		public void Draw(Matrix viewMatrix, Matrix projectionMatrix, Vector3 campos, Vector3 dir, Vector3 amb, Vector3 diff, Vector3 planet, RenderTarget2D t1, Texture2D target2, SpriteBatch sp, float distance, bool set)
		{
			this.drawcount = 0;
			float num = 1f - MathHelper.Clamp((distance - 10000f) / 20000f, 0f, 1f);
			if (num <= 0f)
			{
				return;
			}
			this.faciltyMatrix = Matrix.CreateScale(1f / this.scaler) * Matrix.CreateTranslation(Facility.offset);
			this.fogEffect.Parameters["LightDirection"].SetValue(dir);
			this.fogEffect.Parameters["fader"].SetValue(num);
			this.fogEffect.Parameters["amb"].SetValue(amb);
			this.fogEffect.Parameters["diff"].SetValue(diff);
			this.fogEffect.Parameters["view"].SetValue(viewMatrix);
			this.fogEffect.Parameters["projection"].SetValue(projectionMatrix);
			this.fogEffect.Parameters["planet"].SetValue(planet);
			new Vector2(campos.X, campos.Z);
			string text = "insideDark";
			this.facShader.Parameters["view"].SetValue(viewMatrix);
			this.facShader.Parameters["projection"].SetValue(projectionMatrix);
			this.facShader.Parameters["campos"].SetValue(campos);
			if (Facility.inFacility || this.gateTrans[this.sc.frontDoor] > 0f)
			{
				if (Facility.inFacility && set)
				{
					this.sc.GraphicsDevice.SetRenderTarget(this.resolveTargetX);
					this.sc.GraphicsDevice.Clear(Color.Transparent);
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
					this.sc.GraphicsDevice.BlendState = BlendState.Opaque;
					this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;
					this.sc.astronaut.Draw(viewMatrix, projectionMatrix, new Vector3(0f, 1f, 0f), Vector3.Zero, new Vector3(1f, 1f, 1f), -1f, 0f);
					for (int i = 0; i < this.powerList.Count; i += 5)
					{
						int num2 = (int)((this.powerList[i + 1] + 920f) / 460f);
						if (this.powerList[i + 4] > 0f)
						{
							this.castlePart2(this.powerHall, new Vector3(this.powerList[i], this.powerList[i + 1], this.powerList[i + 2]), this.powerList[i + 3], this.whiteRect, this.rgbRect[num2], text);
							Matrix matrix = Matrix.CreateRotationY(-(float)this.myframe / 10f) * Matrix.CreateTranslation(-42f, 200f, 325f);
							Vector3 vector = new Vector3(1f, -0.7f, 1f);
							string text2 = "rotor";
							this.specialPart(this.powerHall, new Vector3(this.powerList[i], this.powerList[i + 1], this.powerList[i + 2]), this.powerList[i + 3], this.whiteRect, this.rgbRect[num2], matrix, vector, text2, "invert");
						}
					}
					for (int j = 0; j < this.oxygenList.Count; j += 5)
					{
						int num3 = (int)((this.oxygenList[j + 1] + 920f) / 460f);
						if (this.oxygenList[j + 4] > 0f)
						{
							this.castlePart2(this.oxygenHall, new Vector3(this.oxygenList[j], this.oxygenList[j + 1], this.oxygenList[j + 2]), this.oxygenList[j + 3], this.whiteRect, this.rgbRect[num3], text);
						}
					}
					for (int k = 0; k < this.salvageList.Count; k += 5)
					{
						int num4 = (int)((this.salvageList[k + 1] + 920f) / 460f);
						if (this.salvageList[k + 4] > 0f)
						{
							this.castlePart2(this.salvageHall, new Vector3(this.salvageList[k], this.salvageList[k + 1], this.salvageList[k + 2]), this.salvageList[k + 3], this.whiteRect, this.rgbRect[num4], text);
						}
					}
					for (int l = 0; l < this.triList.Count; l += 5)
					{
						int num5 = (int)((this.triList[l + 1] + 920f) / 460f);
						if (this.triList[l + 4] > 0f)
						{
							this.castlePart2(this.triHall, new Vector3(this.triList[l], this.triList[l + 1], this.triList[l + 2]), this.triList[l + 3], this.whiteRect, this.rgbRect[num5], text);
						}
					}
					for (int m = 0; m < this.hallList.Count; m += 5)
					{
						int num6 = (int)((this.hallList[m + 1] + 920f) / 460f);
						if (this.hallList[m + 4] > 0f)
						{
							this.castlePart2(this.hallWay, new Vector3(this.hallList[m], this.hallList[m + 1], this.hallList[m + 2]), this.hallList[m + 3], this.whiteRect, this.rgbRect[num6], text);
						}
					}
					for (int n = 0; n < this.crossList.Count; n += 5)
					{
						int num7 = (int)((this.crossList[n + 1] + 920f) / 460f);
						if (this.crossList[n + 4] > 0f)
						{
							this.castlePart2(this.crossHall, new Vector3(this.crossList[n], this.crossList[n + 1], this.crossList[n + 2]), this.crossList[n + 3], this.whiteRect, this.rgbRect[num7], text);
						}
					}
					for (int num8 = 0; num8 < this.longList.Count; num8 += 5)
					{
						int num9 = (int)((this.longList[num8 + 1] + 920f) / 460f);
						if (this.longList[num8 + 4] > 0f)
						{
							this.castlePart2(this.longHall, new Vector3(this.longList[num8], this.longList[num8 + 1], this.longList[num8 + 2]), this.longList[num8 + 3], this.whiteRect, this.rgbRect[num9], text);
						}
					}
					for (int num10 = 0; num10 < this.cornerList.Count; num10 += 5)
					{
						int num11 = (int)((this.cornerList[num10 + 1] + 920f) / 460f);
						if (this.cornerList[num10 + 4] > 0f)
						{
							this.castlePart2(this.corner, new Vector3(this.cornerList[num10], this.cornerList[num10 + 1], this.cornerList[num10 + 2]), this.cornerList[num10 + 3], this.whiteRect, this.rgbRect[num11], text);
						}
					}
					for (int num12 = 0; num12 < this.deadList.Count; num12 += 5)
					{
						int num13 = (int)((this.deadList[num12 + 1] + 920f) / 460f);
						if (this.deadList[num12 + 4] > 0f)
						{
							this.castlePart2(this.deadend, new Vector3(this.deadList[num12], this.deadList[num12 + 1], this.deadList[num12 + 2]), this.deadList[num12 + 3], this.whiteRect, this.rgbRect[num13], text);
						}
					}
					for (int num14 = 0; num14 < this.clickList.Count; num14++)
					{
						int num15 = this.clickList[num14];
						if (this.pointer[num15] >= 290)
						{
							int num16 = this.pointer[num15];
						}
						if (this.pointer[num15] > 0 && (this.clickListBool[num14] || (!Facility.inFacility && num15 == this.sc.frontDoor)))
						{
							if (this.pointer[num15] >= 100 && this.pointer[num15] <= 150)
							{
								if (num15 != this.sc.frontDoor)
								{
									this.DrawSpaceDoor2(this.mainDoor, this.clickLocation[num15], this.gateTrans[num15], this.gateshadRect, this.gatergbRect2, text);
								}
								if (num15 == this.sc.frontDoor)
								{
									this.DrawSpaceDoor2(this.mainDoor, this.clickLocation[num15], this.gateTrans[num15], this.gateshadRect, this.gateOrangeRect, text);
								}
							}
							this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
							if (this.pointer[num15] >= 300 && this.pointer[num15] <= 390)
							{
								if (this.pointer[num15] != 351)
								{
									int num17 = this.pointer[num15];
								}
								int num18 = this.doorTag[num15] % 1000;
								if (num18 % 10 == 4)
								{
									this.drawChest2(this.vaultChest2, this.clickLocation[num15], this.spiderchestRect, text, num15, this.pointer[num15]);
								}
								else if (num18 % 10 == 2)
								{
									this.drawChest2(this.vaultChest2, this.clickLocation[num15], this.kingchestRect, text, num15, this.pointer[num15]);
								}
								else if (num18 % 10 == 5)
								{
									this.drawChest2(this.vaultChest3, this.clickLocation[num15], this.chestRect3, text, num15, this.pointer[num15]);
								}
								else
								{
									this.drawChest2(this.vaultChest, this.clickLocation[num15], this.chestRect, text, num15, this.pointer[num15]);
								}
							}
							if (this.pointer[num15] >= 250 && this.pointer[num15] <= 280)
							{
								this.drawSwitch2(this.switchModel, this.clickLocation[num15], text, num15, this.pointer[num15]);
							}
							if (this.pointer[num15] >= 290 && this.pointer[num15] <= 295)
							{
								this.drawTrap2(this.trapModel, this.clickLocation[num15], text, num15, this.pointer[num15]);
							}
						}
					}
					this.sc.GraphicsDevice.SetRenderTarget(t1);
				}
				this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
				this.sc.GraphicsDevice.BlendState = BlendState.Opaque;
				this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				for (int num19 = 0; num19 < this.powerList.Count; num19 += 5)
				{
					int num20 = (int)((this.powerList[num19 + 1] + 920f) / 460f);
					if (this.powerList[num19 + 4] > 0f)
					{
						this.castlePart(this.powerHall, new Vector3(this.powerList[num19], this.powerList[num19 + 1], this.powerList[num19 + 2]), this.powerList[num19 + 3], this.whiteRect, this.rgbRect[num20], text);
						Matrix matrix2 = Matrix.CreateRotationY(-(float)this.myframe / 10f) * Matrix.CreateTranslation(-42f, 200f, 325f);
						Vector3 vector2 = new Vector3(1f, 1f, 1f);
						string text3 = "rotor";
						this.specialPart(this.powerHall, new Vector3(this.powerList[num19], this.powerList[num19 + 1], this.powerList[num19 + 2]), this.powerList[num19 + 3], this.whiteRect, this.rgbRect[num20], matrix2, vector2, text3, text);
					}
				}
				for (int num21 = 0; num21 < this.oxygenList.Count; num21 += 5)
				{
					int num22 = (int)((this.oxygenList[num21 + 1] + 920f) / 460f);
					if (this.oxygenList[num21 + 4] > 0f)
					{
						this.castlePart(this.oxygenHall, new Vector3(this.oxygenList[num21], this.oxygenList[num21 + 1], this.oxygenList[num21 + 2]), this.oxygenList[num21 + 3], this.whiteRect, this.rgbRect[num22], text);
					}
				}
				for (int num23 = 0; num23 < this.salvageList.Count; num23 += 5)
				{
					int num24 = (int)((this.salvageList[num23 + 1] + 920f) / 460f);
					if (this.salvageList[num23 + 4] > 0f)
					{
						this.castlePart(this.salvageHall, new Vector3(this.salvageList[num23], this.salvageList[num23 + 1], this.salvageList[num23 + 2]), this.salvageList[num23 + 3], this.whiteRect, this.rgbRect[num24], text);
					}
				}
				for (int num25 = 0; num25 < this.triList.Count; num25 += 5)
				{
					int num26 = (int)((this.triList[num25 + 1] + 920f) / 460f);
					if (this.triList[num25 + 4] > 0f)
					{
						this.castlePart(this.triHall, new Vector3(this.triList[num25], this.triList[num25 + 1], this.triList[num25 + 2]), this.triList[num25 + 3], this.whiteRect, this.rgbRect[num26], text);
					}
				}
				for (int num27 = 0; num27 < this.hallList.Count; num27 += 5)
				{
					int num28 = (int)((this.hallList[num27 + 1] + 920f) / 460f);
					if (this.hallList[num27 + 4] > 0f)
					{
						this.castlePart(this.hallWay, new Vector3(this.hallList[num27], this.hallList[num27 + 1], this.hallList[num27 + 2]), this.hallList[num27 + 3], this.whiteRect, this.rgbRect[num28], text);
					}
				}
				for (int num29 = 0; num29 < this.crossList.Count; num29 += 5)
				{
					int num30 = (int)((this.crossList[num29 + 1] + 920f) / 460f);
					if (this.crossList[num29 + 4] > 0f)
					{
						this.castlePart(this.crossHall, new Vector3(this.crossList[num29], this.crossList[num29 + 1], this.crossList[num29 + 2]), this.crossList[num29 + 3], this.whiteRect, this.rgbRect[num30], text);
					}
				}
				for (int num31 = 0; num31 < this.longList.Count; num31 += 5)
				{
					int num32 = (int)((this.longList[num31 + 1] + 920f) / 460f);
					if (this.longList[num31 + 4] > 0f)
					{
						this.castlePart(this.longHall, new Vector3(this.longList[num31], this.longList[num31 + 1], this.longList[num31 + 2]), this.longList[num31 + 3], this.whiteRect, this.rgbRect[num32], text);
					}
				}
				for (int num33 = 0; num33 < this.cornerList.Count; num33 += 5)
				{
					int num34 = (int)((this.cornerList[num33 + 1] + 920f) / 460f);
					if (this.cornerList[num33 + 4] > 0f)
					{
						this.castlePart(this.corner, new Vector3(this.cornerList[num33], this.cornerList[num33 + 1], this.cornerList[num33 + 2]), this.cornerList[num33 + 3], this.whiteRect, this.rgbRect[num34], text);
					}
				}
				for (int num35 = 0; num35 < this.deadList.Count; num35 += 5)
				{
					int num36 = (int)((this.deadList[num35 + 1] + 920f) / 460f);
					if (this.deadList[num35 + 4] > 0f)
					{
						this.castlePart(this.deadend, new Vector3(this.deadList[num35], this.deadList[num35 + 1], this.deadList[num35 + 2]), this.deadList[num35 + 3], this.whiteRect, this.rgbRect[num36], text);
					}
				}
				for (int num37 = 0; num37 < this.clickList.Count; num37++)
				{
					int num38 = this.clickList[num37];
					if (this.pointer[num38] >= 290)
					{
						int num39 = this.pointer[num38];
					}
					if (this.pointer[num38] > 0 && (this.clickListBool[num37] || (!Facility.inFacility && num38 == this.sc.frontDoor)))
					{
						if (this.pointer[num38] >= 100 && this.pointer[num38] <= 150 && num38 != this.sc.frontDoor)
						{
							this.DrawSpaceDoor(this.mainDoor, this.clickLocation[num38], this.animGate(num38), this.gateshadRect, this.gatergbRect2, text);
						}
						if (this.pointer[num38] >= 300 && this.pointer[num38] <= 390)
						{
							if (this.pointer[num38] != 351)
							{
								int num40 = this.pointer[num38];
							}
							int num41 = this.doorTag[num38] % 1000;
							if (num41 % 10 == 4)
							{
								this.drawChest(this.vaultChest2, this.clickLocation[num38], this.spiderchestRect, text, num38, this.pointer[num38]);
							}
							else if (num41 % 10 == 2)
							{
								this.drawChest(this.vaultChest2, this.clickLocation[num38], this.kingchestRect, text, num38, this.pointer[num38]);
							}
							else if (num41 % 10 == 5)
							{
								this.drawChest(this.vaultChest3, this.clickLocation[num38], this.chestRect3, text, num38, this.pointer[num38]);
							}
							else
							{
								this.drawChest(this.vaultChest, this.clickLocation[num38], this.chestRect, text, num38, this.pointer[num38]);
							}
						}
						if (this.pointer[num38] >= 250 && this.pointer[num38] <= 280)
						{
							this.drawSwitch(this.switchModel, this.clickLocation[num38], text, num38, this.pointer[num38]);
						}
						if (this.pointer[num38] >= 290 && this.pointer[num38] <= 295)
						{
							this.drawTrap(this.trapModel, this.clickLocation[num38], text, num38, this.pointer[num38]);
						}
					}
				}
				if (set)
				{
					this.blurEffect.Parameters["flatTexture"].SetValue(target2);
					this.blurEffect.Parameters["delta"].SetValue(this.specRot2);
					this.blurEffect.CurrentTechnique = this.blurEffect.Techniques["ripple"];
					sp.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, this.blurEffect);
					sp.Draw(this.resolveTargetX, new Rectangle(0, 0, this.sc.GraphicsDevice.Viewport.Width, this.sc.GraphicsDevice.Viewport.Height), Color.White);
					sp.End();
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
					this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
					this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
				}
			}
			this.closeGates();
			this.DrawSpaceDoor(this.mainDoor, this.clickLocation[this.sc.frontDoor], this.animGate(this.sc.frontDoor), this.gateshadRect, this.gateOrangeRect, text);
			this.drawingEntry = Facility.outsideCastle || this.gridVal < -4 || this.gateTrans[this.sc.frontDoor] != 0f || this.gridVal == this.sc.frontDoor;
			if (this.drawingEntry)
			{
				this.DrawEntryWay(this.entryWay, new Vector3(this.entryPos[0], this.entryPos[1], this.entryPos[2]), this.entryPos[3], this.entryTexture, this.dirtrgb, "outdoors");
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000DFAB8 File Offset: 0x000DDCB8
		private void DrawEntryWay(Model model, Vector3 pos, float rot, Texture2D tex, Texture2D tex2, string tech)
		{
			pos.X /= this.scaler;
			pos.Z /= this.scaler;
			pos += Facility.offset;
			Matrix matrix = Matrix.CreateScale(1f / this.scaler) * Matrix.CreateRotationY(rot) * Matrix.CreateTranslation(pos);
			model.Meshes[0].MeshParts[0].Effect = this.fogEffect;
			this.fogEffect.Parameters["shadowTexture"].SetValue(tex);
			this.fogEffect.Parameters["rgbTexture"].SetValue(tex2);
			this.fogEffect.Parameters["rgbTexture2"].SetValue(this.mixrgb);
			this.fogEffect.Parameters["world"].SetValue(matrix);
			this.fogEffect.CurrentTechnique = this.fogEffect.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000DFBE8 File Offset: 0x000DDDE8
		private void DrawCube(Model model, Vector3 pos, float rot, Texture2D tex, Texture2D tex2, string tech)
		{
			pos.X /= this.scaler;
			pos.Z /= this.scaler;
			pos += Facility.offset;
			Matrix matrix = Matrix.CreateScale(1f / this.scaler) * Matrix.CreateRotationY(rot) * Matrix.CreateTranslation(pos);
			model.Meshes[0].MeshParts[0].Effect = this.fogEffect;
			this.fogEffect.Parameters["shadowTexture"].SetValue(tex);
			this.fogEffect.Parameters["rgbTexture"].SetValue(tex2);
			this.fogEffect.Parameters["rgbTexture2"].SetValue(this.mixrgb);
			this.fogEffect.Parameters["world"].SetValue(matrix);
			this.fogEffect.CurrentTechnique = this.fogEffect.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000DFD18 File Offset: 0x000DDF18
		private void castlePartGreen(Model model, Vector3 pos, float rot, Vector4 uvA, Vector4 uvB, string tech)
		{
			pos.X /= this.scaler;
			pos.Z /= this.scaler;
			pos += Facility.offset;
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			Matrix matrix = Matrix.CreateScale(1f / this.scaler) * Matrix.CreateRotationY(rot) * Matrix.CreateTranslation(pos);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["floor"].SetValue(pos.Y + 1f);
			this.facShader.Parameters["world"].SetValue(matrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000DFEB0 File Offset: 0x000DE0B0
		private void castlePart(Model model, Vector3 pos, float rot, Vector4 uvA, Vector4 uvB, string tech)
		{
			this.drawcount++;
			pos.X /= this.scaler;
			pos.Z /= this.scaler;
			pos += Facility.offset;
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			Matrix matrix = Matrix.CreateScale(1f / this.scaler) * Matrix.CreateRotationY(rot) * Matrix.CreateTranslation(pos);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(matrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000E0030 File Offset: 0x000DE230
		private void castlePart2(Model model, Vector3 pos, float rot, Vector4 uvA, Vector4 uvB, string tech)
		{
			this.drawcount++;
			pos.X /= this.scaler;
			pos.Z /= this.scaler;
			pos += Facility.offset;
			tech = "invert";
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			Matrix matrix = Matrix.CreateScale(1f / this.scaler, 1f / this.scaler * -0.7f, 1f / this.scaler) * Matrix.CreateRotationY(rot) * Matrix.CreateTranslation(pos);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(matrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000E01D4 File Offset: 0x000DE3D4
		private void specialPart(Model model, Vector3 pos, float rot, Vector4 uvA, Vector4 uvB, Matrix offcenter, Vector3 scale, string partname, string tech)
		{
			pos.X /= this.scaler;
			pos.Z /= this.scaler;
			pos += Facility.offset;
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			Matrix matrix = Matrix.CreateScale(scale / this.scaler) * Matrix.CreateRotationY(rot) * Matrix.CreateTranslation(pos);
			model.Meshes[partname].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(offcenter * matrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[partname].Draw();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000E0350 File Offset: 0x000DE550
		private void DrawDoor(Model model, Matrix world, Vector4 uvA, Vector4 uvB, string tech)
		{
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000E046C File Offset: 0x000DE66C
		private void DrawSpaceDoor(Model model, Matrix world, float amt, Vector4 uvA, Vector4 uvB, string tech)
		{
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			model.Meshes[2].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateTranslation(amt, 0f, 0f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[2].Draw();
			model.Meshes[1].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["world"].SetValue(Matrix.CreateTranslation(-amt, 0f, 0f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[1].Draw();
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["world"].SetValue(world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000E06A0 File Offset: 0x000DE8A0
		private void drawChest(Model model, Matrix world, Vector4 color, string tech, int pointer, int val)
		{
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = this.vaultchestshadRect;
			Vector4 vector2 = color;
			Vector4 vector3 = new Vector4(num / vector.Z, vector.X / num, num2 / vector.W, vector.Y / num2);
			Vector4 vector4 = new Vector4(num / vector2.Z, vector2.X / num, num2 / vector2.W, vector2.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
			if (val % 10 == 1 && this.chestRot[pointer] > -2f)
			{
				this.chestRot[pointer] -= 0.016f;
				if (this.chestRot[pointer] < -2f)
				{
					this.chestRot[pointer] = -2f;
				}
			}
			if (val % 10 == 0 && this.chestRot[pointer] < 0f)
			{
				this.chestRot[pointer] += 0.016f;
				if (this.chestRot[pointer] > 0f)
				{
					this.chestRot[pointer] = 0f;
				}
			}
			float num3 = this.chestRot[pointer];
			Matrix matrix = Matrix.CreateRotationX(num3) * Matrix.CreateTranslation(0f, 71f, -52f);
			model.Meshes[1].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(matrix * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[1].Draw();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000E0958 File Offset: 0x000DEB58
		private void drawSwitch(Model model, Matrix world, string tech, int pointer, int val)
		{
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = this.switchShadRect;
			Vector4 vector2 = this.switchRect;
			Vector4 vector3 = new Vector4(num / vector.Z, vector.X / num, num2 / vector.W, vector.Y / num2);
			Vector4 vector4 = new Vector4(num / vector2.Z, vector2.X / num, num2 / vector2.W, vector2.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateTranslation(-278f, 250f, 0f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
			if (val == 250 && this.switchRot[pointer] < 0f)
			{
				this.switchRot[pointer] += 0.05f;
				if (this.switchRot[pointer] > 0f)
				{
					this.switchRot[pointer] = 0f;
				}
			}
			if (val == 251 && this.switchRot[pointer] > -1.57f)
			{
				this.switchRot[pointer] -= 0.05f;
				if (this.switchRot[pointer] < -1.57f)
				{
					this.switchRot[pointer] = -1.57f;
				}
			}
			float num3 = this.switchRot[pointer];
			Matrix matrix = Matrix.CreateRotationX(num3) * Matrix.CreateTranslation(-278f, 250f, 0f);
			model.Meshes[1].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(matrix * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[1].Draw();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000E0C30 File Offset: 0x000DEE30
		private void drawTrap(Model model, Matrix world, string tech, int pointer, int val)
		{
			float num = 2000f;
			float num2 = 1700f;
			Vector4 vector = this.trapShadRect;
			Vector4 vector2 = this.trapRect;
			Vector4 vector3 = new Vector4(num / vector.Z, vector.X / num, num2 / vector.W, vector.Y / num2);
			Vector4 vector4 = new Vector4(num / vector2.Z, vector2.X / num, num2 / vector2.W, vector2.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000E0D5C File Offset: 0x000DEF5C
		private void DrawDoor2(Model model, Matrix world, Vector4 uvA, Vector4 uvB, string tech)
		{
			float num = 2000f;
			float num2 = 1700f;
			tech = "invert";
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateScale(1f, -1f, 1f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000E0E98 File Offset: 0x000DF098
		private void DrawSpaceDoor2(Model model, Matrix world, float amt, Vector4 uvA, Vector4 uvB, string tech)
		{
			float num = 2000f;
			float num2 = 1700f;
			tech = "invert";
			Vector4 vector = new Vector4(num / uvA.Z, uvA.X / num, num2 / uvA.W, uvA.Y / num2);
			Vector4 vector2 = new Vector4(num / uvB.Z, uvB.X / num, num2 / uvB.W, uvB.Y / num2);
			model.Meshes[2].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector);
			this.facShader.Parameters["uvB"].SetValue(vector2);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateScale(1f, -0.7f, 1f) * Matrix.CreateTranslation(amt, 0f, 0f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[2].Draw();
			model.Meshes[1].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["world"].SetValue(Matrix.CreateScale(1f, -0.7f, 1f) * Matrix.CreateTranslation(-amt, 0f, 0f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[1].Draw();
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["world"].SetValue(Matrix.CreateScale(1f, -0.7f, 1f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000E111C File Offset: 0x000DF31C
		private void drawChest2(Model model, Matrix world, Vector4 color, string tech, int pointer, int val)
		{
			float num = 2000f;
			float num2 = 1700f;
			tech = "invert";
			Vector4 vector = this.vaultchestshadRect;
			Vector4 vector2 = color;
			Vector4 vector3 = new Vector4(num / vector.Z, vector.X / num, num2 / vector.W, vector.Y / num2);
			Vector4 vector4 = new Vector4(num / vector2.Z, vector2.X / num, num2 / vector2.W, vector2.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateScale(1f, -1.2f, 1f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000E1264 File Offset: 0x000DF464
		private void drawSwitch2(Model model, Matrix world, string tech, int pointer, int val)
		{
			float num = 2000f;
			float num2 = 1700f;
			tech = "invert";
			Vector4 vector = this.switchShadRect;
			Vector4 vector2 = this.switchRect;
			Vector4 vector3 = new Vector4(num / vector.Z, vector.X / num, num2 / vector.W, vector.Y / num2);
			Vector4 vector4 = new Vector4(num / vector2.Z, vector2.X / num, num2 / vector2.W, vector2.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateTranslation(-278f, 250f, 0f) * Matrix.CreateScale(1f, -0.7f, 1f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
			float num3 = this.switchRot[pointer];
			Matrix matrix = Matrix.CreateRotationX(num3) * Matrix.CreateTranslation(-278f, 250f, 0f);
			model.Meshes[1].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(matrix * Matrix.CreateScale(1f, -0.7f, 1f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[1].Draw();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000E14C8 File Offset: 0x000DF6C8
		private void drawTrap2(Model model, Matrix world, string tech, int pointer, int val)
		{
			float num = 2000f;
			float num2 = 1700f;
			tech = "invert";
			Vector4 vector = this.trapShadRect;
			Vector4 vector2 = this.trapRect;
			Vector4 vector3 = new Vector4(num / vector.Z, vector.X / num, num2 / vector.W, vector.Y / num2);
			Vector4 vector4 = new Vector4(num / vector2.Z, vector2.X / num, num2 / vector2.W, vector2.Y / num2);
			model.Meshes[0].MeshParts[0].Effect = this.facShader;
			this.facShader.Parameters["uvA"].SetValue(vector3);
			this.facShader.Parameters["uvB"].SetValue(vector4);
			this.facShader.Parameters["world"].SetValue(Matrix.CreateScale(1f, -1f, 1f) * world * this.faciltyMatrix);
			this.facShader.CurrentTechnique = this.facShader.Techniques[tech];
			model.Meshes[0].Draw();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000E1614 File Offset: 0x000DF814
		public void GetHeightEntry(Vector3 position, out float height, float h)
		{
			position -= Facility.offset;
			position *= this.scaler;
			float num = 100f;
			int num2 = (int)MathHelper.Clamp(position.X / num, 0f, 178f);
			int num3 = (int)MathHelper.Clamp(position.Z / num, 0f, 178f);
			float num4 = position.X % num / num;
			float num5 = position.Z % num / num;
			int num6 = num2 + 1;
			int num7 = num3 + 1;
			float num8 = MathHelper.Lerp((float)this.heightsEntry[num2, num3], (float)this.heightsEntry[num6, num3], num4);
			float num9 = MathHelper.Lerp((float)this.heightsEntry[num2, num7], (float)this.heightsEntry[num6, num7], num4);
			height = MathHelper.Lerp(num8, num9, num5);
			this.gridVal = this.clickableEntry[(int)MathHelper.Clamp(position.X / num, 0f, 178f), (int)MathHelper.Clamp(position.Z / num, 0f, 178f)];
			Facility.inFacility = false;
			Facility.outsideCastle = true;
			this.steep = true;
			if (this.gridVal == -30 && !this.jumping)
			{
				Facility.outsideCastle = false;
			}
			if (height < this.entryRampHite - 1000f)
			{
				height = MathHelper.Lerp(h, this.entryRampHite, 0.05f);
				this.steep = false;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000E178C File Offset: 0x000DF98C
		public void GetHeight(Vector3 position, out float height, float h)
		{
			position -= Facility.offset;
			position *= this.scaler;
			float num = 100f;
			int num2 = (int)MathHelper.Clamp(position.X / num, 0f, 175f);
			int num3 = (int)MathHelper.Clamp(position.Z / num, 0f, 175f);
			float num4 = position.X % num / num;
			float num5 = position.Z % num / num;
			int num6 = num2 + 1;
			int num7 = num3 + 1;
			float num8 = MathHelper.Lerp((float)Facility.heightData[num2, num3], (float)Facility.heightData[num6, num3], num4);
			float num9 = MathHelper.Lerp((float)Facility.heightData[num2, num7], (float)Facility.heightData[num6, num7], num4);
			height = MathHelper.Lerp(num8, num9, num5);
			this.gridVal = Facility.clickable[(int)MathHelper.Clamp((float)Math.Round((double)(position.X / num)), 0f, 175f), (int)MathHelper.Clamp((float)Math.Round((double)(position.Z / num)), 0f, 175f)];
			Facility.inFacility = false;
			if (this.gridVal >= -4 || this.gridVal == this.sc.frontDoor || this.gridVal == -9)
			{
				Facility.inFacility = true;
			}
			Facility.outsideCastle = false;
			if (this.gridVal == -17)
			{
				Facility.outsideCastle = true;
			}
		}

		// Token: 0x04000EF0 RID: 3824
		private KeyboardState keyState;

		// Token: 0x04000EF1 RID: 3825
		private KeyboardState prevkeyState;

		// Token: 0x04000EF2 RID: 3826
		private MouseState mouseState;

		// Token: 0x04000EF3 RID: 3827
		private MouseState prevMouse;

		// Token: 0x04000EF4 RID: 3828
		private GamePadState gamePadState;

		// Token: 0x04000EF5 RID: 3829
		private GamePadState prevstate;

		// Token: 0x04000EF6 RID: 3830
		public static Vector3 offset = new Vector3(0f, 0f, 0f);

		// Token: 0x04000EF7 RID: 3831
		public static bool outsideCastle = true;

		// Token: 0x04000EF8 RID: 3832
		public static bool inFacility = false;

		// Token: 0x04000EF9 RID: 3833
		public static bool atSwitch = false;

		// Token: 0x04000EFA RID: 3834
		public static bool atSwitch2 = false;

		// Token: 0x04000EFB RID: 3835
		public static bool atMain = false;

		// Token: 0x04000EFC RID: 3836
		public static Vector4 createWorkerLoc = Vector4.Zero;

		// Token: 0x04000EFD RID: 3837
		public static bool openConstruction = false;

		// Token: 0x04000EFE RID: 3838
		public static List<Vector2> mypath = new List<Vector2>();

		// Token: 0x04000EFF RID: 3839
		public static List<float> facilityPlot = new List<float>();

		// Token: 0x04000F00 RID: 3840
		public static List<float> reachPlot = new List<float>();

		// Token: 0x04000F01 RID: 3841
		public static List<Vector2> dummyPlot = new List<Vector2>();

		// Token: 0x04000F02 RID: 3842
		private float sightLimit = 2250000f;

		// Token: 0x04000F03 RID: 3843
		private int myframe;

		// Token: 0x04000F04 RID: 3844
		private int playerIndex;

		// Token: 0x04000F05 RID: 3845
		public float specRot = 4.7f;

		// Token: 0x04000F06 RID: 3846
		public float specRot2 = 0.023f;

		// Token: 0x04000F07 RID: 3847
		private float camtilt;

		// Token: 0x04000F08 RID: 3848
		private float camrot;

		// Token: 0x04000F09 RID: 3849
		public bool rebuild;

		// Token: 0x04000F0A RID: 3850
		private int gridwidth;

		// Token: 0x04000F0B RID: 3851
		public static int[,] heightData;

		// Token: 0x04000F0C RID: 3852
		private int[,] heightsEntry;

		// Token: 0x04000F0D RID: 3853
		public static int[,] clickable;

		// Token: 0x04000F0E RID: 3854
		public int[,] clickableEntry;

		// Token: 0x04000F0F RID: 3855
		public int gridVal;

		// Token: 0x04000F10 RID: 3856
		public bool jumping;

		// Token: 0x04000F11 RID: 3857
		public bool steep;

		// Token: 0x04000F12 RID: 3858
		private int gateShake;

		// Token: 0x04000F13 RID: 3859
		private int clangID;

		// Token: 0x04000F14 RID: 3860
		private Matrix gateShakeMatrix;

		// Token: 0x04000F15 RID: 3861
		private float gateRot;

		// Token: 0x04000F16 RID: 3862
		private Effect blurEffect;

		// Token: 0x04000F17 RID: 3863
		private Matrix[] clickLocation = new Matrix[1800];

		// Token: 0x04000F18 RID: 3864
		private List<int> clickList = new List<int>();

		// Token: 0x04000F19 RID: 3865
		private List<bool> clickListBool = new List<bool>();

		// Token: 0x04000F1A RID: 3866
		private List<float> triList = new List<float>();

		// Token: 0x04000F1B RID: 3867
		private List<float> deadList = new List<float>();

		// Token: 0x04000F1C RID: 3868
		private List<float> hallList = new List<float>();

		// Token: 0x04000F1D RID: 3869
		private List<float> crossList = new List<float>();

		// Token: 0x04000F1E RID: 3870
		private List<float> longList = new List<float>();

		// Token: 0x04000F1F RID: 3871
		private List<float> cornerList = new List<float>();

		// Token: 0x04000F20 RID: 3872
		private List<float> oxygenList = new List<float>();

		// Token: 0x04000F21 RID: 3873
		private List<float> powerList = new List<float>();

		// Token: 0x04000F22 RID: 3874
		private List<float> salvageList = new List<float>();

		// Token: 0x04000F23 RID: 3875
		private List<float> stairList = new List<float>();

		// Token: 0x04000F24 RID: 3876
		private List<float> holyList = new List<float>();

		// Token: 0x04000F25 RID: 3877
		private List<float> brokeList = new List<float>();

		// Token: 0x04000F26 RID: 3878
		public bool drawingEntry = true;

		// Token: 0x04000F27 RID: 3879
		public int drawcount;

		// Token: 0x04000F28 RID: 3880
		private int clickCount;

		// Token: 0x04000F29 RID: 3881
		private int floorFlag = 3;

		// Token: 0x04000F2A RID: 3882
		private int gateID = -1;

		// Token: 0x04000F2B RID: 3883
		private int gateAnim;

		// Token: 0x04000F2C RID: 3884
		private int lastFloorBuilt = -1;

		// Token: 0x04000F2D RID: 3885
		private int pitIndex;

		// Token: 0x04000F2E RID: 3886
		private int lastpitBuilt = -1;

		// Token: 0x04000F2F RID: 3887
		private Model triHall;

		// Token: 0x04000F30 RID: 3888
		private Model hallWay;

		// Token: 0x04000F31 RID: 3889
		private Model stairWell;

		// Token: 0x04000F32 RID: 3890
		private Model paperCard;

		// Token: 0x04000F33 RID: 3891
		private Model corner;

		// Token: 0x04000F34 RID: 3892
		private Model deadend;

		// Token: 0x04000F35 RID: 3893
		private Model deadend2;

		// Token: 0x04000F36 RID: 3894
		private Model brokeHall;

		// Token: 0x04000F37 RID: 3895
		private Model crossHall;

		// Token: 0x04000F38 RID: 3896
		private Model holyHall;

		// Token: 0x04000F39 RID: 3897
		private Model powerHall;

		// Token: 0x04000F3A RID: 3898
		private Model salvageHall;

		// Token: 0x04000F3B RID: 3899
		private Model oxygenHall;

		// Token: 0x04000F3C RID: 3900
		private Model rustedgate;

		// Token: 0x04000F3D RID: 3901
		private Model rustedgate2;

		// Token: 0x04000F3E RID: 3902
		private Model mainDoor;

		// Token: 0x04000F3F RID: 3903
		private Model onyxDoor;

		// Token: 0x04000F40 RID: 3904
		private Model prisonDoor;

		// Token: 0x04000F41 RID: 3905
		private Model spiderskullDoor;

		// Token: 0x04000F42 RID: 3906
		private Model pit1;

		// Token: 0x04000F43 RID: 3907
		private Model newFrame;

		// Token: 0x04000F44 RID: 3908
		private Model oldFrame;

		// Token: 0x04000F45 RID: 3909
		private Model invPack;

		// Token: 0x04000F46 RID: 3910
		private Model tunnelWater;

		// Token: 0x04000F47 RID: 3911
		private Model ceiling;

		// Token: 0x04000F48 RID: 3912
		private Model ceiling2;

		// Token: 0x04000F49 RID: 3913
		private Model entryWay;

		// Token: 0x04000F4A RID: 3914
		private Model altar;

		// Token: 0x04000F4B RID: 3915
		private Model longHall;

		// Token: 0x04000F4C RID: 3916
		private Model gauntlet;

		// Token: 0x04000F4D RID: 3917
		private Model flame;

		// Token: 0x04000F4E RID: 3918
		private Model ghostpath1;

		// Token: 0x04000F4F RID: 3919
		private Model ghostpath2;

		// Token: 0x04000F50 RID: 3920
		private Model ghostpath3;

		// Token: 0x04000F51 RID: 3921
		private Model ghostpath4;

		// Token: 0x04000F52 RID: 3922
		private Model ghostpath5;

		// Token: 0x04000F53 RID: 3923
		private Model ghostpath6;

		// Token: 0x04000F54 RID: 3924
		private Model ghostpath7;

		// Token: 0x04000F55 RID: 3925
		private Model torch;

		// Token: 0x04000F56 RID: 3926
		private Model vaultChest;

		// Token: 0x04000F57 RID: 3927
		private Model vaultChest3;

		// Token: 0x04000F58 RID: 3928
		private Model switchModel;

		// Token: 0x04000F59 RID: 3929
		private Model trapModel;

		// Token: 0x04000F5A RID: 3930
		private Model vaultChest2;

		// Token: 0x04000F5B RID: 3931
		private Model plaque;

		// Token: 0x04000F5C RID: 3932
		private Model lootball;

		// Token: 0x04000F5D RID: 3933
		private Model cube;

		// Token: 0x04000F5E RID: 3934
		private Effect fogEffect;

		// Token: 0x04000F5F RID: 3935
		private Effect facShader;

		// Token: 0x04000F60 RID: 3936
		private Vector4 shadRect = new Vector4(1250f, 1050f, 100f, 100f);

		// Token: 0x04000F61 RID: 3937
		private Vector4 whiteRect = new Vector4(1250f, 850f, 80f, 80f);

		// Token: 0x04000F62 RID: 3938
		private Vector4 rgb1Rect = new Vector4(300f, 700f, 500f, 500f);

		// Token: 0x04000F63 RID: 3939
		private Vector4 rgb1catacombs = new Vector4(1400f, 500f, 500f, 500f);

		// Token: 0x04000F64 RID: 3940
		private Vector4 rgb1RectServant = new Vector4(0f, 0f, 1000f, 800f);

		// Token: 0x04000F65 RID: 3941
		private Vector4 rgb1RectPrison = new Vector4(500f, 1200f, 500f, 500f);

		// Token: 0x04000F66 RID: 3942
		private Vector4 rgb1RectNobles = new Vector4(1000f, 1200f, 500f, 500f);

		// Token: 0x04000F67 RID: 3943
		private Vector4 rgb1RectRoyal = new Vector4(1500f, 1200f, 500f, 500f);

		// Token: 0x04000F68 RID: 3944
		private Vector4 rgb1RectRoof = new Vector4(1400f, 0f, 500f, 500f);

		// Token: 0x04000F69 RID: 3945
		private Vector4[] rgbRect;

		// Token: 0x04000F6A RID: 3946
		private Vector4 gateshadRect = new Vector4(1200f, 0f, 200f, 200f);

		// Token: 0x04000F6B RID: 3947
		private Vector4 prisonrgbRect = new Vector4(1000f, 0f, 200f, 200f);

		// Token: 0x04000F6C RID: 3948
		private Vector4 tombrgbRect = new Vector4(800f, 0f, 200f, 200f);

		// Token: 0x04000F6D RID: 3949
		private Vector4 woodenrgbRect = new Vector4(1000f, 200f, 200f, 200f);

		// Token: 0x04000F6E RID: 3950
		private Vector4 skullrgbRect = new Vector4(800f, 200f, 200f, 200f);

		// Token: 0x04000F6F RID: 3951
		private Vector4 spiderrgbRect = new Vector4(800f, 400f, 200f, 200f);

		// Token: 0x04000F70 RID: 3952
		private Vector4 trapShadRect = new Vector4(600f, 1000f, 200f, 200f);

		// Token: 0x04000F71 RID: 3953
		private Vector4 trapRect = new Vector4(400f, 1000f, 200f, 200f);

		// Token: 0x04000F72 RID: 3954
		private Vector4 switchShadRect = new Vector4(1600f, 1000f, 200f, 200f);

		// Token: 0x04000F73 RID: 3955
		private Vector4 switchRect = new Vector4(600f, 800f, 200f, 200f);

		// Token: 0x04000F74 RID: 3956
		private Vector4 chestRect = new Vector4(0f, 1000f, 200f, 200f);

		// Token: 0x04000F75 RID: 3957
		private Vector4 chestRect3 = new Vector4(1800f, 1000f, 200f, 200f);

		// Token: 0x04000F76 RID: 3958
		private Vector4 gateOrangeRect = new Vector4(824f, 1200f, 313f, 313f);

		// Token: 0x04000F77 RID: 3959
		private Vector4 gatergbRect2 = new Vector4(502f, 1200f, 313f, 313f);

		// Token: 0x04000F78 RID: 3960
		private Vector4 vaultchestshadRect = new Vector4(1200f, 800f, 200f, 200f);

		// Token: 0x04000F79 RID: 3961
		private Vector4 kingchestRect = new Vector4(1000f, 600f, 200f, 200f);

		// Token: 0x04000F7A RID: 3962
		private Vector4 spiderchestRect = new Vector4(1000f, 400f, 200f, 200f);

		// Token: 0x04000F7B RID: 3963
		private Vector4 powerShad = new Vector4(0f, 1300f, 400f, 400f);

		// Token: 0x04000F7C RID: 3964
		private PresentationParameters pp;

		// Token: 0x04000F7D RID: 3965
		private RenderTarget2D resolveTargetX;

		// Token: 0x04000F7E RID: 3966
		public Texture2D rooms;

		// Token: 0x04000F7F RID: 3967
		public Texture2D reflec;

		// Token: 0x04000F80 RID: 3968
		public Texture2D entryTexture;

		// Token: 0x04000F81 RID: 3969
		public Texture2D dirtrgb;

		// Token: 0x04000F82 RID: 3970
		public Texture2D mixrgb;

		// Token: 0x04000F83 RID: 3971
		private float[] brokePos;

		// Token: 0x04000F84 RID: 3972
		private float[] stairwellPos;

		// Token: 0x04000F85 RID: 3973
		private float[] holyPos;

		// Token: 0x04000F86 RID: 3974
		private float[] puzzleDoorPos;

		// Token: 0x04000F87 RID: 3975
		private float[] victoryPos;

		// Token: 0x04000F88 RID: 3976
		public float[] trihallPos;

		// Token: 0x04000F89 RID: 3977
		public float[] crossPos;

		// Token: 0x04000F8A RID: 3978
		public float[] hallwayPos;

		// Token: 0x04000F8B RID: 3979
		public float[] longPos;

		// Token: 0x04000F8C RID: 3980
		public float[] deadPos;

		// Token: 0x04000F8D RID: 3981
		public float[] cornerPos;

		// Token: 0x04000F8E RID: 3982
		public float[] entryPos;

		// Token: 0x04000F8F RID: 3983
		public float[] chestPos;

		// Token: 0x04000F90 RID: 3984
		public float[] trapPos;

		// Token: 0x04000F91 RID: 3985
		public float[] switchPos;

		// Token: 0x04000F92 RID: 3986
		public float[] gatePos;

		// Token: 0x04000F93 RID: 3987
		public float[] powerPos;

		// Token: 0x04000F94 RID: 3988
		public float[] salvagePos;

		// Token: 0x04000F95 RID: 3989
		public float[] oxygenPos;

		// Token: 0x04000F96 RID: 3990
		private int[] gatePP;

		// Token: 0x04000F97 RID: 3991
		private int[] chestPP;

		// Token: 0x04000F98 RID: 3992
		private int[] switchPP;

		// Token: 0x04000F99 RID: 3993
		private int[] trapPP;

		// Token: 0x04000F9A RID: 3994
		private int[] defaultDoorTag;

		// Token: 0x04000F9B RID: 3995
		public int[] pointer;

		// Token: 0x04000F9C RID: 3996
		private int[] doorTag;

		// Token: 0x04000F9D RID: 3997
		private int[] skullPortrait = new int[9];

		// Token: 0x04000F9E RID: 3998
		private float[] switchRot;

		// Token: 0x04000F9F RID: 3999
		private float[] chestRot;

		// Token: 0x04000FA0 RID: 4000
		private float[] gateTrans;

		// Token: 0x04000FA1 RID: 4001
		private float[] chestTrans;

		// Token: 0x04000FA2 RID: 4002
		private List<int> closeGate = new List<int>();

		// Token: 0x04000FA3 RID: 4003
		private int[] gateSpeed = new int[]
		{
			120, 180, 120, 120, 120, 1, 1, 1, 1, 1,
			-100, -150, -100, -100, -100, 1, 1, 1, 1, 1
		};

		// Token: 0x04000FA4 RID: 4004
		private int lastTrigger;

		// Token: 0x04000FA5 RID: 4005
		private int bottomless = -2000000000;

		// Token: 0x04000FA6 RID: 4006
		private ScreenManager sc;

		// Token: 0x04000FA7 RID: 4007
		private Vector3 campos;

		// Token: 0x04000FA8 RID: 4008
		private Vector3 camlookpos;

		// Token: 0x04000FA9 RID: 4009
		private Vector3 pos;

		// Token: 0x04000FAA RID: 4010
		private Vector2 norm1;

		// Token: 0x04000FAB RID: 4011
		private Vector2 norm2;

		// Token: 0x04000FAC RID: 4012
		private Vector3 norm3;

		// Token: 0x04000FAD RID: 4013
		private float dot;

		// Token: 0x04000FAE RID: 4014
		private bool facingIt;

		// Token: 0x04000FAF RID: 4015
		private Matrix faciltyMatrix = Matrix.Identity;

		// Token: 0x04000FB0 RID: 4016
		public Vector2 facilityLocate = new Vector2(0f, 0f);

		// Token: 0x04000FB1 RID: 4017
		public Vector3 gridset = new Vector3(9000f, 0f, 9000f);

		// Token: 0x04000FB2 RID: 4018
		public float scaler = 4f;

		// Token: 0x04000FB3 RID: 4019
		private multiply multi = new multiply();

		// Token: 0x04000FB4 RID: 4020
		private int[] entryHit;

		// Token: 0x04000FB5 RID: 4021
		private int[] entryHitEnter;

		// Token: 0x04000FB6 RID: 4022
		private float entryRampHite;

		// Token: 0x04000FB7 RID: 4023
		public bool onRamp;

		// Token: 0x04000FB8 RID: 4024
		private ContentManager content;
	}
}
