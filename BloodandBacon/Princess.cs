using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;
using SkinnedModel;

namespace Blood
{
	// Token: 0x02000079 RID: 121
	internal class Princess
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x000E6C60 File Offset: 0x000E4E60
		public Princess(int index, string name, bool thisTrial)
		{
			this.isTrialMode = thisTrial;
			Princess.bit = 0;
			Princess.uvIndex = 0;
			Princess.xcoord = 0;
			Princess.ycoord = 0;
			Princess.speeches = 0;
			Princess.cuttyCount = 0;
			Princess.allplayersReady = false;
			Princess.cuttyDoneSpeech = false;
			Princess.someoneTalking = false;
			Princess.shortestDistance = 20000f;
			Princess.whichPigTalks = 0;
			Princess.seed = 5;
			Princess.nameBuild.Length = 0;
			Princess.nameBuild.Append(name);
			Random random = new Random();
			if (index == 3)
			{
				this.pigIndex = index;
				this.myIndex = 0;
				this.health = 1600;
				this.startHealth = 1600;
				int num = random.Next(3, 6);
				this.speechList = new List<int> { num };
				Princess.speeches = 0;
				this.cuttyPos = new Vector3(2988f, -54f, 2975f);
				this.cuttyScale = 2.6f;
				this.speechInclude = new List<int> { 0 };
				this.cuttyRot = 1.7f;
				this.targetRot = 1.7f;
			}
			if (index == 4)
			{
				this.pigIndex = index;
				this.myIndex = 0;
				this.health = 1600;
				this.startHealth = 1600;
				int num2 = random.Next(3, 6);
				this.speechList = new List<int> { num2 };
				Princess.speeches = 0;
				this.cuttyPos = new Vector3(2988f, -54f, 2975f);
				this.cuttyScale = 3.2f;
				this.speechInclude = new List<int> { 0 };
				this.cuttyRot = 4.84f;
				this.targetRot = 4.84f;
			}
			if (index == 5)
			{
				this.pigIndex = index;
				this.myIndex = 0;
				this.health = 1600;
				this.startHealth = 1600;
				int num3 = random.Next(3, 6);
				this.speechList = new List<int> { num3 };
				Princess.speeches = 0;
				this.cuttyPos = new Vector3(2988f, -54f, 2975f);
				this.cuttyScale = 2.6f;
				this.speechInclude = new List<int> { 0 };
				this.cuttyRot = 1.7f;
				this.targetRot = 1.7f;
			}
			if (index == 6)
			{
				this.pigIndex = index;
				this.myIndex = 0;
				this.health = 1600;
				this.startHealth = 1600;
				int num4 = random.Next(3, 6);
				this.speechList = new List<int> { num4 };
				Princess.speeches = 0;
				this.cuttyPos = new Vector3(2988f, -54f, 2975f);
				this.cuttyScale = 3.6f;
				this.speechInclude = new List<int> { 0 };
				this.cuttyRot = 5.04f;
				this.targetRot = 5.04f;
			}
			if (index == 7)
			{
				this.pigIndex = index;
				this.myIndex = 0;
				this.health = 1600;
				this.startHealth = 1600;
				this.speechList = new List<int> { 8 };
				Princess.speeches = 0;
				this.cuttyPos = new Vector3(2988f, -54f, 2975f);
				this.cuttyScale = 3.8f;
				this.speechInclude = new List<int> { 0 };
				this.cuttyRot = 4.1400003f;
				this.targetRot = 4.1400003f;
			}
			if (this.isTrialMode)
			{
				this.health = 1600;
				this.startHealth = 1600;
			}
			this.healthPerc = (float)this.health / (float)this.startHealth;
			this.oldcuttyPos = this.cuttyPos;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000E7408 File Offset: 0x000E5608
		public void LoadContent(ScreenManager sc, ContentManager cc)
		{
			this.sc = sc;
			this.content = cc;
			this.canvasPaint.index = new List<int>();
			this.canvasPaint.x = new List<float>();
			this.canvasPaint.z = new List<float>();
			this.bodyPaint.index = new List<int>();
			this.bodyPaint.x = new List<float>();
			this.bodyPaint.z = new List<float>();
			this.spriteBatch = new SpriteBatch(sc.GraphicsDevice);
			this.rr = new Random();
			this.bossTheme = this.content.Load<SoundEffect>("audio\\newBoss");
			this.burps = new SoundEffects(1);
			this.burps.sound[0] = this.content.Load<SoundEffect>("audio\\gallop2").CreateInstance();
			this.burps.sound[0].IsLooped = true;
			this.burps.sound[0].Play();
			this.burps.sound[0].Volume = 0f;
			this.heartbeat = new SoundEffects(1);
			this.heartbeat.sound[0] = this.content.Load<SoundEffect>("audio//heartbeat").CreateInstance();
			this.bucking = new SoundEffects(1);
			this.bucking.sound[0] = this.content.Load<SoundEffect>("audio//buckClomp2").CreateInstance();
			this.fart = new SoundEffects(1);
			this.fart.sound[0] = this.content.Load<SoundEffect>("audio//fart4").CreateInstance();
			this.explode = new SoundEffects(1);
			this.explode.sound[0] = this.content.Load<SoundEffect>("audio\\explode2").CreateInstance();
			this.windy = new SoundEffects(1);
			this.windy.sound[0] = this.content.Load<SoundEffect>("audio\\windy").CreateInstance();
			this.dogroll = new SoundEffects(2);
			this.dogroll.sound[0] = this.content.Load<SoundEffect>("audio\\dogroll").CreateInstance();
			this.dogroll.sound[1] = this.content.Load<SoundEffect>("audio\\dogroll").CreateInstance();
			this.splats = new SoundEffects(3);
			this.splats.sound[0] = this.content.Load<SoundEffect>("audio\\splats").CreateInstance();
			this.splats.sound[1] = this.content.Load<SoundEffect>("audio\\splats").CreateInstance();
			this.splats.sound[2] = this.content.Load<SoundEffect>("audio\\splats").CreateInstance();
			this.solidSkin = this.content.Load<Effect>("effects\\SolidSkinEffect");
			this.solidSkin.Parameters["World"].SetValue(Matrix.CreateTranslation(0f, 0f, 0f));
			this.glowEffect = this.content.Load<Effect>("effects\\glowEffect2");
			this.glowEffect.CurrentTechnique = this.glowEffect.Techniques["EdgeDetect"];
			this.simple = this.content.Load<Effect>("effects//simpleEffect");
			this.simple2 = this.content.Load<Effect>("effects//simpleEffect2");
			this.tunnelfx = this.content.Load<Effect>("effects//tunnelEffect");
			this.canvas = this.content.Load<Model>("Models//farmCanvas");
			this.tunnel = this.content.Load<Model>("Models//windtunnel");
			Model model;
			if (this.pigIndex == 3)
			{
				model = this.content.Load<Model>("Models\\chunk4");
				this.pigTexture = this.content.Load<Texture2D>("npc\\pigBoss");
			}
			else if (this.pigIndex == 4)
			{
				model = this.content.Load<Model>("Models\\chunkBlk");
				this.pigTexture = this.content.Load<Texture2D>("npc\\pigBossBlack");
			}
			else if (this.pigIndex == 5)
			{
				model = this.content.Load<Model>("Models\\chunkGreen");
				this.pigTexture = this.content.Load<Texture2D>("npc\\pigBossGreen");
			}
			else if (this.pigIndex == 6)
			{
				model = this.content.Load<Model>("Models\\chunkUgly");
				this.pigTexture = this.content.Load<Texture2D>("npc\\pigBossUgly");
			}
			else if (this.pigIndex == 7)
			{
				model = this.content.Load<Model>("Models\\chunkUgly3");
				this.pigTexture = this.content.Load<Texture2D>("npc\\pigBossUgly3");
			}
			else
			{
				model = this.content.Load<Model>("Models\\chunk4");
				this.pigTexture = this.content.Load<Texture2D>("npc\\pigBoss");
			}
			this.heartTexture = this.content.Load<Texture2D>("npc\\heart");
			this.tunneldust = this.content.Load<Texture2D>("texture\\dusty");
			this.tunnelfx.Parameters["Texture"].SetValue(this.tunneldust);
			this.burst = this.content.Load<Effect>("effects\\burstEffect");
			this.burstMatrix = Matrix.CreateScale(1500f);
			this.glow = this.content.Load<Texture2D>("texture\\glow");
			this.grid = this.content.Load<Model>("models\\outlineGrid");
			this.burst.Parameters["Texture"].SetValue(this.glow);
			this.gutsEffect = this.content.Load<Effect>("effects\\gutsEffect");
			this.guts = this.content.Load<Texture2D>("texture\\guts");
			this.gutsModel = this.content.Load<Model>("models\\guts");
			this.gutsEffect.Parameters["Texture"].SetValue(this.guts);
			sc.bossIndex = 0;
			this.loadModels();
			this.pigJaw = new float[2200];
			this.pigSkin = this.content.Load<Effect>("effects\\bossSkin2");
			this.pigSkin.Parameters["World"].SetValue(Matrix.CreateTranslation(0f, 0f, 0f));
			this.pigSkin.Parameters["Texture"].SetValue(this.pigTexture);
			this.pigSkin.Parameters["Texture2"].SetValue(this.heartTexture);
			this.col_Bone = new int[] { 7, 7, 5, 3, 2, 17 };
			this.col_Scale[0] = 11f;
			this.col_Pos[0] = new Vector3(0f, 66f, 127f);
			this.col_Scale[1] = 27f;
			this.col_Pos[1] = new Vector3(0f, 72.46032f, 98.99477f);
			this.col_Scale[2] = 52f;
			this.col_Pos[2] = new Vector3(0f, 66.70475f, 46.14088f);
			this.col_Scale[3] = 58f;
			this.col_Pos[3] = new Vector3(0f, 64.01877f, -9.665999f);
			this.col_Scale[4] = 47f;
			this.col_Pos[4] = new Vector3(0f, 64.01877f, -62.562748f);
			this.col_Scale[5] = 25f;
			this.col_Pos[5] = new Vector3(0f, 54.4f, 22.6f);
			this.puke1 = default(Princess.vomit);
			this.puke1.max = 0;
			this.puke1.maxCapacity = 1200;
			this.puke1.index = 0;
			this.puke1.model = this.content.Load<Model>("Models//pukelet");
			this.puke1.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Princess.instanceDec, this.puke1.maxCapacity, BufferUsage.WriteOnly);
			this.puke1.displayList = new Princess.instancedObject[this.puke1.maxCapacity];
			this.puke1.dupe = new vomitDupe[this.puke1.maxCapacity];
			for (int i = 0; i < this.puke1.maxCapacity; i++)
			{
				this.puke1.dupe[i] = new vomitDupe(i);
			}
			this.puke1.stream = new Princess.instancedObject[this.puke1.maxCapacity];
			this.bloody = default(Princess.finale);
			this.bloody.max = 0;
			this.bloody.maxCapacity = 399;
			this.bloody.index = 0;
			this.bloody.model = this.content.Load<Model>("Models//bloodything");
			this.bloody.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Princess.instanceDec, this.bloody.maxCapacity, BufferUsage.WriteOnly);
			this.bloody.displayList = new Princess.instancedObject[this.bloody.maxCapacity];
			this.bloody.dupe = new explodeDupe[this.bloody.maxCapacity];
			for (int j = 0; j < this.bloody.maxCapacity; j++)
			{
				this.bloody.dupe[j] = new explodeDupe(j);
			}
			this.bloody.stream = new Princess.instancedObject[this.bloody.maxCapacity];
			this.chunk = default(Princess.shell);
			this.chunk.max = 0;
			this.chunk.maxCapacity = 150;
			this.chunk.index = 0;
			this.chunk.offset = new Matrix[this.chunk.maxCapacity];
			this.chunk.bone = new int[this.chunk.maxCapacity];
			this.chunk.model = model;
			this.chunk.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Princess.instanceDec, this.chunk.maxCapacity, BufferUsage.WriteOnly);
			this.chunk.displayList = new Princess.instancedObject[this.chunk.maxCapacity];
			this.chunk.dupe = new chunkDupe[this.chunk.maxCapacity];
			for (int k = 0; k < this.chunk.maxCapacity; k++)
			{
				this.chunk.dupe[k] = new chunkDupe(k);
			}
			this.chunk.stream = new Princess.instancedObject[this.chunk.maxCapacity];
			this.faceChunk = default(Princess.shell);
			this.faceChunk.max = 0;
			this.faceChunk.maxCapacity = 60;
			this.faceChunk.index = 0;
			this.faceChunk.offset = new Matrix[this.faceChunk.maxCapacity];
			this.faceChunk.bone = new int[this.faceChunk.maxCapacity];
			this.faceChunk.model = model;
			this.faceChunk.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Princess.instanceDec, this.faceChunk.maxCapacity, BufferUsage.WriteOnly);
			this.faceChunk.displayList = new Princess.instancedObject[this.faceChunk.maxCapacity];
			this.faceChunk.dupe = new chunkDupe[this.faceChunk.maxCapacity];
			for (int l = 0; l < this.faceChunk.maxCapacity; l++)
			{
				this.faceChunk.dupe[l] = new chunkDupe(l);
			}
			this.faceChunk.stream = new Princess.instancedObject[this.faceChunk.maxCapacity];
			this.assChunk = default(Princess.shell);
			this.assChunk.max = 0;
			this.assChunk.maxCapacity = 79;
			this.assChunk.index = 0;
			this.assChunk.offset = new Matrix[this.assChunk.maxCapacity];
			this.assChunk.bone = new int[this.assChunk.maxCapacity];
			this.assChunk.model = model;
			this.assChunk.buffer = new DynamicVertexBuffer(sc.GraphicsDevice, Princess.instanceDec, this.assChunk.maxCapacity, BufferUsage.WriteOnly);
			this.assChunk.displayList = new Princess.instancedObject[this.assChunk.maxCapacity];
			this.assChunk.dupe = new chunkDupe[this.assChunk.maxCapacity];
			for (int m = 0; m < this.assChunk.maxCapacity; m++)
			{
				this.assChunk.dupe[m] = new chunkDupe(m);
			}
			this.assChunk.stream = new Princess.instancedObject[this.assChunk.maxCapacity];
			this.resetExplody(false);
			Matrix[] array = new Matrix[]
			{
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-1.4893694f) * Matrix.CreateRotationY(1.4391826f) * Matrix.CreateRotationZ(3.98103f) * Matrix.CreateTranslation(26.601454f, 105.04799f, -29.761477f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.1021366f) * Matrix.CreateRotationY(-1.4194796f) * Matrix.CreateRotationZ(0.31777713f) * Matrix.CreateTranslation(28.973555f, 99.132576f, -68.302086f),
				Matrix.CreateScale(0.75693464f, 0.6127788f, 0.6127788f) * Matrix.CreateRotationX(2.9046712f) * Matrix.CreateRotationY(0.5454444f) * Matrix.CreateRotationZ(8.517765f) * Matrix.CreateTranslation(25.075022f, 106.966934f, 33.082f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.8705144f) * Matrix.CreateRotationY(0.77327985f) * Matrix.CreateRotationZ(8.430361f) * Matrix.CreateTranslation(22.822615f, 102.60141f, 51.00435f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.7391796f) * Matrix.CreateRotationY(-0.7497437f) * Matrix.CreateRotationZ(8.731192f) * Matrix.CreateTranslation(35.940727f, 89.76424f, 46.57292f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.856856f) * Matrix.CreateRotationY(0.7751548f) * Matrix.CreateRotationZ(8.440346f) * Matrix.CreateTranslation(29.348396f, 96.68955f, 42.941235f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.48163447f) * Matrix.CreateRotationY(-1.2217877f) * Matrix.CreateRotationZ(10.856129f) * Matrix.CreateTranslation(47.858826f, 74.694176f, 20.022469f),
				Matrix.CreateScale(0.63172895f, 0.5492081f, 0.6019117f) * Matrix.CreateRotationX(-0.074346125f) * Matrix.CreateRotationY(-1.0266804f) * Matrix.CreateRotationZ(-1.1601237f) * Matrix.CreateTranslation(48.365707f, 71.780334f, -16.264496f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.7668495f) * Matrix.CreateRotationY(-1.4676846f) * Matrix.CreateRotationZ(8.693154f) * Matrix.CreateTranslation(41.905216f, 83.9779f, -48.790844f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.2856886f) * Matrix.CreateRotationY(0.7930756f) * Matrix.CreateRotationZ(8.599927f) * Matrix.CreateTranslation(38.394627f, 89.42756f, -53.723396f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.177453f) * Matrix.CreateRotationY(-0.6140531f) * Matrix.CreateRotationZ(8.58768f) * Matrix.CreateTranslation(29.980164f, 105.9818f, 0.7926182f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.1278527f) * Matrix.CreateRotationY(0.44408563f) * Matrix.CreateRotationZ(8.514753f) * Matrix.CreateTranslation(36.714855f, 97.02453f, 18.064993f),
				Matrix.CreateScale(0.64229f, 0.4090254f, 0.64229f) * Matrix.CreateRotationX(3.1220567f) * Matrix.CreateRotationY(2.6045527f) * Matrix.CreateRotationZ(8.436325f) * Matrix.CreateTranslation(36.321476f, 98.700584f, 8.202137f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.198539f) * Matrix.CreateRotationY(0.7041228f) * Matrix.CreateRotationZ(8.763815f) * Matrix.CreateTranslation(29.616478f, 106.56437f, 12.399319f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.2375805f) * Matrix.CreateRotationY(-0.5807444f) * Matrix.CreateRotationZ(8.146708f) * Matrix.CreateTranslation(48.876415f, 73.201385f, 7.0009856f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.2375805f) * Matrix.CreateRotationY(-0.5807444f) * Matrix.CreateRotationZ(8.146708f) * Matrix.CreateTranslation(46.511894f, 70.45753f, -30.596186f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.9516215f) * Matrix.CreateRotationY(0.5767148f) * Matrix.CreateRotationZ(7.8946295f) * Matrix.CreateTranslation(44.62853f, 73.07472f, 40.850574f),
				Matrix.CreateScale(0.91852087f, 1.0195514f, 0.91852087f) * Matrix.CreateRotationX(-1.4243152f) * Matrix.CreateRotationY(1.4011669f) * Matrix.CreateRotationZ(-1.4899749f) * Matrix.CreateTranslation(5.167546f, 117.757164f, -28.114382f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-2.857909f) * Matrix.CreateRotationY(0.3168916f) * Matrix.CreateRotationZ(-3.1381488f) * Matrix.CreateTranslation(4.1356807f, 113.545135f, -52.18575f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-1.4190837f) * Matrix.CreateRotationY(1.2751633f) * Matrix.CreateRotationZ(-1.5232395f) * Matrix.CreateTranslation(5.6728544f, 109.98587f, -67.45575f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-1.183979f) * Matrix.CreateRotationY(1.5054332f) * Matrix.CreateRotationZ(-1.2483118f) * Matrix.CreateTranslation(4.7227435f, 119.35774f, -5.977935f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-0.33295974f) * Matrix.CreateRotationY(1.4979795f) * Matrix.CreateRotationZ(-0.34992173f) * Matrix.CreateTranslation(3.9753363f, 120.05341f, 14.698188f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-1.7603773f) * Matrix.CreateRotationY(1.7019247f) * Matrix.CreateRotationZ(-1.8223745f) * Matrix.CreateTranslation(3.6125603f, 118.07541f, 35.307117f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(1.4848529f) * Matrix.CreateRotationY(1.2796446f) * Matrix.CreateRotationZ(1.4248667f) * Matrix.CreateTranslation(2.7820716f, 112.83604f, 55.786175f),
				Matrix.CreateScale(0.58616364f, 0.5492081f, 0.51937884f) * Matrix.CreateRotationX(0.5534142f) * Matrix.CreateRotationY(1.1179872f) * Matrix.CreateRotationZ(5.935321f) * Matrix.CreateTranslation(30.431097f, 98.32836f, 44.184765f),
				Matrix.CreateScale(0.6396528f, 0.6396528f, 0.6396528f) * Matrix.CreateRotationX(3.2282624f) * Matrix.CreateRotationY(1.0458726f) * Matrix.CreateRotationZ(8.801974f) * Matrix.CreateTranslation(23.565924f, 110.71371f, 22.65279f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.1446145f) * Matrix.CreateRotationY(0.44317666f) * Matrix.CreateRotationZ(8.622264f) * Matrix.CreateTranslation(31.724424f, 103.45403f, 23.342377f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(2.6710258f) * Matrix.CreateRotationY(-0.7143527f) * Matrix.CreateRotationZ(8.833094f) * Matrix.CreateTranslation(30.030521f, 96.033615f, 51.189873f),
				Matrix.CreateScale(0.6941537f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.8499496f) * Matrix.CreateRotationY(0.99365586f) * Matrix.CreateRotationZ(8.437673f) * Matrix.CreateTranslation(20.925108f, 108.70247f, 41.065956f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(2.9789674f) * Matrix.CreateRotationY(0.49997333f) * Matrix.CreateRotationZ(8.476306f) * Matrix.CreateTranslation(34.522575f, 99.08463f, 30.814508f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.0255241f) * Matrix.CreateRotationY(0.41990322f) * Matrix.CreateRotationZ(8.493059f) * Matrix.CreateTranslation(39.041393f, 93.84401f, 24.51765f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.533742f) * Matrix.CreateRotationY(-2.4315264f) * Matrix.CreateRotationZ(8.212889f) * Matrix.CreateTranslation(36.74667f, 93.65792f, 38.62052f),
				Matrix.CreateScale(0.6784089f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.024586972f) * Matrix.CreateRotationY(2.3995197f) * Matrix.CreateRotationZ(1.1720934f) * Matrix.CreateTranslation(-42.301956f, 83.67706f, -35.196625f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.26915348f) * Matrix.CreateRotationY(2.1643934f) * Matrix.CreateRotationZ(1.5072769f) * Matrix.CreateTranslation(-44.94263f, 80.847885f, -17.52322f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.272598f) * Matrix.CreateRotationY(-0.5911699f) * Matrix.CreateRotationZ(-2.2221394f) * Matrix.CreateTranslation(-36.442333f, 97.77751f, -19.14101f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.3452033f) * Matrix.CreateRotationY(2.270528f) * Matrix.CreateRotationZ(1.3382814f) * Matrix.CreateTranslation(-40.287647f, 89.78955f, -23.884356f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.260376f) * Matrix.CreateRotationY(3.546283f) * Matrix.CreateRotationZ(0.973279f) * Matrix.CreateTranslation(-38.04717f, 91.24086f, -66.84701f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.09478507f) * Matrix.CreateRotationY(2.4567804f) * Matrix.CreateRotationZ(1.0092003f) * Matrix.CreateTranslation(-34.356186f, 97.12025f, -47.25208f),
				Matrix.CreateScale(0.578982f, 0.46110308f, 0.85718226f) * Matrix.CreateRotationX(-0.023165703f) * Matrix.CreateRotationY(-0.21391098f) * Matrix.CreateRotationZ(0.76715505f) * Matrix.CreateTranslation(-34.45692f, 97.82798f, -57.826355f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.10596075f) * Matrix.CreateRotationY(2.2968152f) * Matrix.CreateRotationZ(1.2274684f) * Matrix.CreateTranslation(-40.298897f, 89.099945f, -54.90931f),
				Matrix.CreateScale(0.88370997f, 0.8279952f, 0.7830241f) * Matrix.CreateRotationX(0.012940885f) * Matrix.CreateRotationY(1.017281f) * Matrix.CreateRotationZ(0.9692761f) * Matrix.CreateTranslation(-38.778194f, 96.443954f, -5.145422f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.21269071f) * Matrix.CreateRotationY(2.0659883f) * Matrix.CreateRotationZ(1.3537576f) * Matrix.CreateTranslation(-43.77305f, 80.556244f, -46.508724f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(0.0757202f) * Matrix.CreateRotationY(2.6029825f) * Matrix.CreateRotationZ(1.098416f) * Matrix.CreateTranslation(-39.798645f, 90.27742f, -43.680634f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(3.254875f) * Matrix.CreateRotationY(-0.55905384f) * Matrix.CreateRotationZ(-2.0453165f) * Matrix.CreateTranslation(-42.75852f, 89.67516f, -15.294105f),
				Matrix.CreateScale(0.6941537f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.18377335f) * Matrix.CreateRotationY(1.959056f) * Matrix.CreateRotationZ(1.358342f) * Matrix.CreateTranslation(-45.145027f, 76.98852f, -28.596762f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(0.07876453f) * Matrix.CreateRotationY(2.4346287f) * Matrix.CreateRotationZ(1.1105181f) * Matrix.CreateTranslation(-37.749214f, 93.027435f, -36.030464f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(0.114633724f) * Matrix.CreateRotationY(2.5159886f) * Matrix.CreateRotationZ(1.1052729f) * Matrix.CreateTranslation(-32.8451f, 99.344215f, -40.420853f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(0.012498404f) * Matrix.CreateRotationY(-0.9433144f) * Matrix.CreateRotationZ(0.9398156f) * Matrix.CreateTranslation(-35.6935f, 96.38744f, -28.341637f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.060085f) * Matrix.CreateRotationY(1.04354f) * Matrix.CreateRotationZ(-2.0599744f) * Matrix.CreateTranslation(-45.672043f, 75.31387f, -57.09627f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.10596075f) * Matrix.CreateRotationY(2.2968152f) * Matrix.CreateRotationZ(1.2274684f) * Matrix.CreateTranslation(-43.434834f, 82.93181f, -62.781208f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.33972377f) * Matrix.CreateRotationY(2.1147f) * Matrix.CreateRotationZ(1.5054169f) * Matrix.CreateTranslation(-47.18694f, 71.302345f, -14.404327f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(0.03809634f) * Matrix.CreateRotationY(1.8118261f) * Matrix.CreateRotationZ(1.3055999f) * Matrix.CreateTranslation(-47.017536f, 71.64015f, -38.118782f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.27608076f) * Matrix.CreateRotationY(2.0219f) * Matrix.CreateRotationZ(1.5119294f) * Matrix.CreateTranslation(-46.291462f, 71.87676f, -25.726515f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(0.03809634f) * Matrix.CreateRotationY(1.8118261f) * Matrix.CreateRotationZ(1.3055999f) * Matrix.CreateTranslation(-46.56113f, 72.85199f, -46.375103f),
				Matrix.CreateScale(0.5793862f, 0.47013438f, 0.5793862f) * Matrix.CreateRotationX(3.0626905f) * Matrix.CreateRotationY(0.59309727f) * Matrix.CreateRotationZ(2.7343767f) * Matrix.CreateTranslation(12.125879f, 117.64237f, 25.390263f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(3.1735346f) * Matrix.CreateRotationY(0.56342447f) * Matrix.CreateRotationZ(2.79592f) * Matrix.CreateTranslation(13.84377f, 118.50698f, 5.4462337f),
				Matrix.CreateScale(0.5877558f, 0.4769258f, 0.5877558f) * Matrix.CreateRotationX(-3.0432675f) * Matrix.CreateRotationY(0.6191079f) * Matrix.CreateRotationZ(2.8309457f) * Matrix.CreateTranslation(13.76582f, 118.116684f, -15.709471f),
				Matrix.CreateScale(0.5521139f, 0.44800466f, 0.5521139f) * Matrix.CreateRotationX(-2.895909f) * Matrix.CreateRotationY(0.6625589f) * Matrix.CreateRotationZ(2.9234185f) * Matrix.CreateTranslation(13.334257f, 115.250786f, -39.664593f),
				Matrix.CreateScale(0.54861265f, 0.44516364f, 0.54861265f) * Matrix.CreateRotationX(3.4741619f) * Matrix.CreateRotationY(0.5807329f) * Matrix.CreateRotationZ(3.0649614f) * Matrix.CreateTranslation(14.694037f, 111.05985f, -57.784054f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(2.9427772f) * Matrix.CreateRotationY(0.60225713f) * Matrix.CreateRotationZ(2.7433112f) * Matrix.CreateTranslation(11.075772f, 114.226006f, 45.798096f),
				Matrix.CreateScale(0.57615334f, 0.46751112f, 0.57615334f) * Matrix.CreateRotationX(2.836445f) * Matrix.CreateRotationY(0.57145303f) * Matrix.CreateRotationZ(2.492016f) * Matrix.CreateTranslation(13.357086f, 110.51045f, 53.780468f),
				Matrix.CreateScale(0.56409657f, 0.45772785f, 0.56409657f) * Matrix.CreateRotationX(2.8962195f) * Matrix.CreateRotationY(0.5944767f) * Matrix.CreateRotationZ(2.5679412f) * Matrix.CreateTranslation(14.340964f, 116.055374f, 36.030422f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(3.1735346f) * Matrix.CreateRotationY(0.56342447f) * Matrix.CreateRotationZ(2.79592f) * Matrix.CreateTranslation(13.600037f, 117.64966f, 17.069946f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(3.088904f) * Matrix.CreateRotationY(0.59648275f) * Matrix.CreateRotationZ(2.583408f) * Matrix.CreateTranslation(20.242727f, 114.12172f, 11.997535f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(0.022594044f) * Matrix.CreateRotationY(-0.5566799f) * Matrix.CreateRotationZ(5.822581f) * Matrix.CreateTranslation(14.245571f, 118.02889f, -2.70445f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(3.465098f) * Matrix.CreateRotationY(0.5890991f) * Matrix.CreateRotationZ(2.9724963f) * Matrix.CreateTranslation(16.63063f, 108.23989f, -65.59853f),
				Matrix.CreateScale(0.5401568f, 0.43830225f, 0.5401568f) * Matrix.CreateRotationX(-2.895909f) * Matrix.CreateRotationY(0.6625589f) * Matrix.CreateRotationZ(2.9234185f) * Matrix.CreateTranslation(17.224548f, 112.03938f, -47.758823f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(-2.8890154f) * Matrix.CreateRotationY(0.6743725f) * Matrix.CreateRotationZ(2.8790624f) * Matrix.CreateTranslation(16.056366f, 114.95357f, -30.533741f),
				Matrix.CreateScale(0.29412758f, 0.23866549f, 0.29412758f) * Matrix.CreateRotationX(-2.895909f) * Matrix.CreateRotationY(0.6625589f) * Matrix.CreateRotationZ(2.9234185f) * Matrix.CreateTranslation(14.079507f, 117.761925f, -23.103033f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.48163447f) * Matrix.CreateRotationY(-1.2217877f) * Matrix.CreateRotationZ(10.856129f) * Matrix.CreateTranslation(46.064194f, 74.32302f, 31.372202f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.2375805f) * Matrix.CreateRotationY(-0.5807444f) * Matrix.CreateRotationZ(8.146708f) * Matrix.CreateTranslation(48.950165f, 72.65733f, -5.4769893f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.99375f) * Matrix.CreateRotationY(0.6162873f) * Matrix.CreateRotationZ(1.761059f) * Matrix.CreateTranslation(42.96185f, 83.41237f, 33.57761f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.3648104f) * Matrix.CreateRotationY(-1.2129749f) * Matrix.CreateRotationZ(-1.552406f) * Matrix.CreateTranslation(44.290043f, 84.41099f, 23.551777f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.31524593f) * Matrix.CreateRotationY(-1.2319382f) * Matrix.CreateRotationZ(-1.4699188f) * Matrix.CreateTranslation(44.68848f, 84.60289f, 12.038694f),
				Matrix.CreateScale(0.63172895f, 0.5492081f, 0.6019117f) * Matrix.CreateRotationX(-0.024177898f) * Matrix.CreateRotationY(-1.0405471f) * Matrix.CreateRotationZ(-1.1248354f) * Matrix.CreateTranslation(45.819656f, 84.02553f, 1.8935523f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-3.0039136f) * Matrix.CreateRotationY(-0.6034175f) * Matrix.CreateRotationZ(1.8884672f) * Matrix.CreateTranslation(45.05016f, 82.919365f, -12.619938f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.9289367f) * Matrix.CreateRotationY(-0.625863f) * Matrix.CreateRotationZ(1.8139372f) * Matrix.CreateTranslation(43.785103f, 81.59443f, -25.331417f),
				Matrix.CreateScale(0.6845234f, 0.6845234f, 0.6845234f) * Matrix.CreateRotationX(-3.0733778f) * Matrix.CreateRotationY(-0.6034175f) * Matrix.CreateRotationZ(1.8884672f) * Matrix.CreateTranslation(42.03446f, 82.19244f, -36.820465f),
				Matrix.CreateScale(0.46064842f, 0.27673215f, 0.28763586f) * Matrix.CreateRotationX(2.7231576f) * Matrix.CreateRotationY(0.9055733f) * Matrix.CreateRotationZ(1.5645994f) * Matrix.CreateTranslation(41.055378f, 81.84674f, 45.17037f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-1.3362103f) * Matrix.CreateRotationY(1.4607451f) * Matrix.CreateRotationZ(4.2820363f) * Matrix.CreateTranslation(24.133646f, 109.80681f, -17.716839f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-2.0971658f) * Matrix.CreateRotationY(1.4910691f) * Matrix.CreateRotationZ(3.3057952f) * Matrix.CreateTranslation(33.435867f, 101.233795f, -9.457364f),
				Matrix.CreateScale(0.3885317f, 0.31526834f, 0.3885317f) * Matrix.CreateRotationX(3.096028f) * Matrix.CreateRotationY(-0.29583392f) * Matrix.CreateRotationZ(2.6263158f) * Matrix.CreateTranslation(21.178265f, 114.628716f, -8.859396f),
				Matrix.CreateScale(0.58232343f, 0.5218734f, 0.58232343f) * Matrix.CreateRotationX(3.1140215f) * Matrix.CreateRotationY(-0.32917207f) * Matrix.CreateRotationZ(2.5795608f) * Matrix.CreateTranslation(21.881813f, 112.93406f, 0.6908919f),
				Matrix.CreateScale(0.6784089f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.8183746f) * Matrix.CreateRotationY(-0.5714998f) * Matrix.CreateRotationZ(-2.112944f) * Matrix.CreateTranslation(-31.715384f, 100.81515f, 35.92108f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.949118f) * Matrix.CreateRotationY(-0.246607f) * Matrix.CreateRotationZ(-2.283821f) * Matrix.CreateTranslation(-18.459146f, 111.294945f, 42.88511f),
				Matrix.CreateScale(0.7685885f, 0.7685885f, 0.7685885f) * Matrix.CreateRotationX(0.99403036f) * Matrix.CreateRotationY(-1.3992392f) * Matrix.CreateRotationZ(-0.40916508f) * Matrix.CreateTranslation(-17.675207f, 114.617744f, 21.666431f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.0144184f) * Matrix.CreateRotationY(-0.3796233f) * Matrix.CreateRotationZ(-2.4302666f) * Matrix.CreateTranslation(-23.314445f, 109.165276f, 32.488f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.571567f) * Matrix.CreateRotationY(-1.3889934f) * Matrix.CreateRotationZ(0.5293887f) * Matrix.CreateTranslation(-47.05853f, 77.58652f, 19.908728f),
				Matrix.CreateScale(0.631282f, 0.631282f, 0.631282f) * Matrix.CreateRotationX(3.0388262f) * Matrix.CreateRotationY(-0.6493934f) * Matrix.CreateRotationZ(-2.1356833f) * Matrix.CreateTranslation(-38.62055f, 95.844635f, 18.07455f),
				Matrix.CreateScale(0.5492081f, 0.34974864f, 0.5492081f) * Matrix.CreateRotationX(-0.3269507f) * Matrix.CreateRotationY(1.8169777f) * Matrix.CreateRotationZ(0.75073373f) * Matrix.CreateTranslation(-44.21431f, 88.83822f, 12.026179f),
				Matrix.CreateScale(0.5752881f, 0.5752881f, 0.5752881f) * Matrix.CreateRotationX(3.1210628f) * Matrix.CreateRotationY(-0.5292624f) * Matrix.CreateRotationZ(-1.9298366f) * Matrix.CreateTranslation(-42.857903f, 87.22709f, 23.192413f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.9526737f) * Matrix.CreateRotationY(-0.18967915f) * Matrix.CreateRotationZ(-2.0625076f) * Matrix.CreateTranslation(-38.31622f, 91.21098f, 36.39579f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(2.9230194f) * Matrix.CreateRotationY(-0.7858745f) * Matrix.CreateRotationZ(-2.0719798f) * Matrix.CreateTranslation(-36.93455f, 96.641495f, 27.188274f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(0.5746365f) * Matrix.CreateRotationY(-0.98349833f) * Matrix.CreateRotationZ(0.112267375f) * Matrix.CreateTranslation(-15.664769f, 115.16176f, 34.990795f),
				Matrix.CreateScale(0.6941537f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.8983095f) * Matrix.CreateRotationY(-0.13687114f) * Matrix.CreateRotationZ(-2.226478f) * Matrix.CreateTranslation(-26.548971f, 102.56368f, 44.113907f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(2.9950087f) * Matrix.CreateRotationY(-0.6311514f) * Matrix.CreateRotationZ(-2.2128832f) * Matrix.CreateTranslation(-32.468334f, 103.55354f, 25.78184f),
				Matrix.CreateScale(0.631282f, 0.40523103f, 0.631282f) * Matrix.CreateRotationX(3.0226066f) * Matrix.CreateRotationY(-0.71131897f) * Matrix.CreateRotationZ(-2.1943827f) * Matrix.CreateTranslation(-32.009197f, 104.646706f, 13.76978f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(0.028566897f) * Matrix.CreateRotationY(0.41065037f) * Matrix.CreateRotationZ(0.763514f) * Matrix.CreateTranslation(-26.55525f, 109.60999f, 23.534729f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.9267273f) * Matrix.CreateRotationY(-0.017405048f) * Matrix.CreateRotationZ(-2.0390394f) * Matrix.CreateTranslation(-43.739704f, 79.630684f, 40.711716f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.1341736f) * Matrix.CreateRotationY(-0.2534797f) * Matrix.CreateRotationZ(-2.0926132f) * Matrix.CreateTranslation(-45.4441f, 78.40346f, 30.288828f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.83875f) * Matrix.CreateRotationY(-0.16264097f) * Matrix.CreateRotationZ(-2.2829986f) * Matrix.CreateTranslation(-15.601932f, 110.01565f, 54.846703f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(2.8501997f) * Matrix.CreateRotationY(0.049227986f) * Matrix.CreateRotationZ(-2.2404156f) * Matrix.CreateTranslation(-32.387863f, 93.54818f, 49.37319f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(2.8374522f) * Matrix.CreateRotationY(-0.16159062f) * Matrix.CreateRotationZ(-2.1405036f) * Matrix.CreateTranslation(-23.557959f, 102.49102f, 51.579956f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(2.9177535f) * Matrix.CreateRotationY(0.123767324f) * Matrix.CreateRotationZ(-2.1801937f) * Matrix.CreateTranslation(-38.58807f, 87.418335f, 46.356518f),
				Matrix.CreateScale(0.60340136f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.07980949f) * Matrix.CreateRotationY(-1.1351779f) * Matrix.CreateRotationZ(-0.6748722f) * Matrix.CreateTranslation(36.843533f, 91.67015f, -64.666985f),
				Matrix.CreateScale(0.3719859f, 0.3719859f, 0.3719859f) * Matrix.CreateRotationX(3.0634537f) * Matrix.CreateRotationY(-0.4657729f) * Matrix.CreateRotationZ(-1.8082863f) * Matrix.CreateTranslation(-50.030273f, 71.13418f, 22.296547f),
				Matrix.CreateScale(0.3719859f, 0.3719859f, 0.3719859f) * Matrix.CreateRotationX(3.0017214f) * Matrix.CreateRotationY(-0.5292588f) * Matrix.CreateRotationZ(-1.8227212f) * Matrix.CreateTranslation(-48.65056f, 71.23681f, 30.537598f),
				Matrix.CreateScale(0.3719859f, 0.3719859f, 0.3719859f) * Matrix.CreateRotationX(2.9745467f) * Matrix.CreateRotationY(-0.5230291f) * Matrix.CreateRotationZ(-1.7606051f) * Matrix.CreateTranslation(-46.34685f, 71.58137f, 38.797543f),
				Matrix.CreateScale(0.60340136f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.19620855f) * Matrix.CreateRotationY(-1.618147f) * Matrix.CreateRotationZ(-0.9977801f) * Matrix.CreateTranslation(42.667965f, 82.972534f, -60.70823f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(3.0757961f) * Matrix.CreateRotationY(1.259747f) * Matrix.CreateRotationZ(8.194264f) * Matrix.CreateTranslation(46.360863f, 73.90459f, -56.553f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.062424272f) * Matrix.CreateRotationY(1.125631f) * Matrix.CreateRotationZ(5.199453f) * Matrix.CreateTranslation(46.53683f, 70.44083f, -42.246243f),
				Matrix.CreateScale(0.9503381f, 0.6749803f, 0.8673502f) * Matrix.CreateRotationX(-2.8961036f) * Matrix.CreateRotationY(-1.4105315f) * Matrix.CreateRotationZ(-3.0920403f) * Matrix.CreateTranslation(-13.347501f, 117.80767f, -17.47968f),
				Matrix.CreateScale(0.82678324f, 0.5872251f, 0.7545847f) * Matrix.CreateRotationX(-3.3621786f) * Matrix.CreateRotationY(-1.4114841f) * Matrix.CreateRotationZ(-2.6201708f) * Matrix.CreateTranslation(-13.807469f, 117.8573f, 3.927928f),
				Matrix.CreateScale(0.65177715f, 0.36413002f, 0.46790737f) * Matrix.CreateRotationX(-3.5862126f) * Matrix.CreateRotationY(-1.4052868f) * Matrix.CreateRotationZ(-2.410742f) * Matrix.CreateTranslation(-9.44646f, 118.132675f, 25.141537f),
				Matrix.CreateScale(0.65177715f, 0.36413002f, 0.46790737f) * Matrix.CreateRotationX(-4.834741f) * Matrix.CreateRotationY(-1.3280842f) * Matrix.CreateRotationZ(-1.2132983f) * Matrix.CreateTranslation(-10.327638f, 115.15794f, 44.81575f),
				Matrix.CreateScale(0.9503381f, 0.6749803f, 0.8673502f) * Matrix.CreateRotationX(-2.5902612f) * Matrix.CreateRotationY(-1.3313416f) * Matrix.CreateRotationZ(-3.4029002f) * Matrix.CreateTranslation(-13.066761f, 114.46297f, -38.310303f),
				Matrix.CreateScale(0.69323176f, 0.4923698f, 0.6326956f) * Matrix.CreateRotationX(-1.9461199f) * Matrix.CreateRotationY(-1.3403001f) * Matrix.CreateRotationZ(-4.053498f) * Matrix.CreateTranslation(-12.103757f, 111.96011f, -57.2832f),
				Matrix.CreateScale(0.63916355f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.1071842f) * Matrix.CreateRotationY(-0.24922463f) * Matrix.CreateRotationZ(-2.3952706f) * Matrix.CreateTranslation(-24.018898f, 113.888084f, 0.62771386f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.1627698f) * Matrix.CreateRotationY(-0.83293545f) * Matrix.CreateRotationZ(-2.5373893f) * Matrix.CreateTranslation(-23.7381f, 113.193375f, -10.097442f),
				Matrix.CreateScale(0.631282f, 0.40523103f, 0.631282f) * Matrix.CreateRotationX(3.0226066f) * Matrix.CreateRotationY(-0.71131897f) * Matrix.CreateRotationZ(-2.1943827f) * Matrix.CreateTranslation(-31.592089f, 105.75908f, -7.283146f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.0226066f) * Matrix.CreateRotationY(-0.71131897f) * Matrix.CreateRotationZ(-2.1943827f) * Matrix.CreateTranslation(-33.045033f, 103.71973f, 3.152794f),
				Matrix.CreateScale(0.5492081f, 0.35254633f, 0.5492081f) * Matrix.CreateRotationX(3.046901f) * Matrix.CreateRotationY(-0.8331882f) * Matrix.CreateRotationZ(-2.433287f) * Matrix.CreateTranslation(-24.511145f, 112.25528f, 12.332244f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.26915348f) * Matrix.CreateRotationY(2.1643934f) * Matrix.CreateRotationZ(1.5072769f) * Matrix.CreateTranslation(-45.938446f, 82.584785f, -5.234365f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.33972377f) * Matrix.CreateRotationY(2.1147f) * Matrix.CreateRotationZ(1.5054169f) * Matrix.CreateTranslation(-48.960464f, 72.718666f, -4.006808f),
				Matrix.CreateScale(0.68472135f, 0.68472135f, 0.68472135f) * Matrix.CreateRotationX(-2.9650643f) * Matrix.CreateRotationY(-0.6245846f) * Matrix.CreateRotationZ(1.9953859f) * Matrix.CreateTranslation(39.996655f, 91.09647f, -22.254923f),
				Matrix.CreateScale(0.5965378f, 0.5965378f, 0.5965378f) * Matrix.CreateRotationX(-3.0286539f) * Matrix.CreateRotationY(-0.6447598f) * Matrix.CreateRotationZ(2.0748138f) * Matrix.CreateTranslation(42.1131f, 94.73886f, -7.857341f),
				Matrix.CreateScale(0.5492081f, 0.34974864f, 0.5492081f) * Matrix.CreateRotationX(3.1220567f) * Matrix.CreateRotationY(2.6045527f) * Matrix.CreateRotationZ(8.436325f) * Matrix.CreateTranslation(41.468212f, 91.99784f, 6.761472f),
				Matrix.CreateScale(0.5492081f, 0.34974864f, 0.5492081f) * Matrix.CreateRotationX(3.1220567f) * Matrix.CreateRotationY(2.6045527f) * Matrix.CreateRotationZ(8.436325f) * Matrix.CreateTranslation(42.27822f, 90.608116f, 17.14855f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-1.4893694f) * Matrix.CreateRotationY(1.4391826f) * Matrix.CreateRotationZ(3.98103f) * Matrix.CreateTranslation(28.375973f, 101.370735f, -45.400314f),
				Matrix.CreateScale(0.48401043f, 0.3927431f, 0.48401043f) * Matrix.CreateRotationX(3.465098f) * Matrix.CreateRotationY(0.5890991f) * Matrix.CreateRotationZ(2.9724963f) * Matrix.CreateTranslation(17.543701f, 106.62763f, -72.68181f),
				Matrix.CreateScale(0.59780675f, 0.59780675f, 0.59780675f) * Matrix.CreateRotationX(-1.1021366f) * Matrix.CreateRotationY(-1.4194796f) * Matrix.CreateRotationZ(0.31777713f) * Matrix.CreateTranslation(23.579853f, 105.94708f, -60.53807f),
				Matrix.CreateScale(0.60340136f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.07980949f) * Matrix.CreateRotationY(-1.1351779f) * Matrix.CreateRotationZ(-0.6748722f) * Matrix.CreateTranslation(30.878492f, 97.630684f, -55.208458f),
				Matrix.CreateScale(0.67004806f, 0.4267024f, 0.67004806f) * Matrix.CreateRotationX(-0.09178543f) * Matrix.CreateRotationY(0.32912064f) * Matrix.CreateRotationZ(0.5163088f) * Matrix.CreateTranslation(-20.032274f, 109.846985f, -54.9661f),
				Matrix.CreateScale(0.69553673f, 0.69553673f, 0.69553673f) * Matrix.CreateRotationX(0.15637583f) * Matrix.CreateRotationY(2.466111f) * Matrix.CreateRotationZ(0.7724612f) * Matrix.CreateTranslation(-28.664843f, 103.80397f, -53.495365f),
				Matrix.CreateScale(0.7340461f, 0.47119713f, 0.7340461f) * Matrix.CreateRotationX(0.17209189f) * Matrix.CreateRotationY(2.5297437f) * Matrix.CreateRotationZ(0.8631235f) * Matrix.CreateTranslation(-22.228613f, 110.26114f, -41.57115f),
				Matrix.CreateScale(0.7464699f, 0.4791722f, 0.7464699f) * Matrix.CreateRotationX(-0.06945549f) * Matrix.CreateRotationY(-0.9366604f) * Matrix.CreateRotationZ(0.7305289f) * Matrix.CreateTranslation(-28.260693f, 106.73951f, -27.362675f),
				Matrix.CreateScale(0.93094337f, 0.93094337f, 0.93094337f) * Matrix.CreateRotationX(0.18969549f) * Matrix.CreateRotationY(3.7360227f) * Matrix.CreateRotationZ(0.61117554f) * Matrix.CreateTranslation(-25.137514f, 108.87087f, -19.950605f),
				Matrix.CreateScale(0.61509734f, 0.3917085f, 0.61509734f) * Matrix.CreateRotationX(-0.28304192f) * Matrix.CreateRotationY(0.7407013f) * Matrix.CreateRotationZ(0.30823246f) * Matrix.CreateTranslation(-25.406887f, 104.12406f, -67.50924f),
				Matrix.CreateScale(0.5492081f, 0.34974864f, 0.5492081f) * Matrix.CreateRotationX(-0.33470553f) * Matrix.CreateRotationY(0.710084f) * Matrix.CreateRotationZ(0.12451228f) * Matrix.CreateTranslation(-13.594674f, 109.20684f, -68.963844f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.571567f) * Matrix.CreateRotationY(-1.3889934f) * Matrix.CreateRotationZ(0.5293887f) * Matrix.CreateTranslation(-47.625107f, 77.88855f, 8.258694f),
				Matrix.CreateScale(0.3719859f, 0.3719859f, 0.3719859f) * Matrix.CreateRotationX(3.0634537f) * Matrix.CreateRotationY(-0.4657729f) * Matrix.CreateRotationZ(-1.8082863f) * Matrix.CreateTranslation(-50.592834f, 71.034f, 13.52861f),
				Matrix.CreateScale(0.3719859f, 0.3719859f, 0.3719859f) * Matrix.CreateRotationX(3.0634537f) * Matrix.CreateRotationY(-0.4657729f) * Matrix.CreateRotationZ(-1.8082863f) * Matrix.CreateTranslation(-51.084633f, 70.933815f, 5.951716f),
				Matrix.CreateScale(0.631282f, 0.40201524f, 0.631282f) * Matrix.CreateRotationX(-0.3269507f) * Matrix.CreateRotationY(1.8169777f) * Matrix.CreateRotationZ(0.75073373f) * Matrix.CreateTranslation(-39.042713f, 96.93261f, 5.9430146f),
				Matrix.CreateScale(0.5492081f, 0.34974864f, 0.5492081f) * Matrix.CreateRotationX(-0.3269507f) * Matrix.CreateRotationY(1.8169777f) * Matrix.CreateRotationZ(0.75073373f) * Matrix.CreateTranslation(-43.763844f, 89.63663f, 2.8048666f),
				Matrix.CreateScale(0.6668183f, 0.6668183f, 0.6668183f) * Matrix.CreateRotationX(-2.998355f) * Matrix.CreateRotationY(0.6572863f) * Matrix.CreateRotationZ(2.0548205f) * Matrix.CreateTranslation(36.883926f, 96.088425f, -34.18301f),
				Matrix.CreateScale(0.412663f, 0.35254633f, 0.3732656f) * Matrix.CreateRotationX(2.9177535f) * Matrix.CreateRotationY(0.123767324f) * Matrix.CreateRotationZ(-2.1801937f) * Matrix.CreateTranslation(-42.52037f, 86.58178f, 33.837627f),
				Matrix.CreateScale(0.5942891f, 0.5492081f, 0.7608631f) * Matrix.CreateRotationX(3.455039f) * Matrix.CreateRotationY(1.2379096f) * Matrix.CreateRotationZ(8.794167f) * Matrix.CreateTranslation(38.697006f, 91.67688f, -43.382774f),
				Matrix.CreateScale(0.8014864f, 0.88964397f, 0.8014864f) * Matrix.CreateRotationX(-2.0971658f) * Matrix.CreateRotationY(1.4910691f) * Matrix.CreateRotationZ(3.3057952f) * Matrix.CreateTranslation(34.571896f, 97.83498f, -22.11409f)
			};
			for (int n = 0; n < 148; n++)
			{
				this.dropChunk(ref this.chunk, array[n], 3);
			}
			array = new Matrix[]
			{
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(2.1746864f) * Matrix.CreateRotationY(0.8770141f) * Matrix.CreateRotationZ(8.594833f) * Matrix.CreateTranslation(-5.556709f, 95.53957f, 113.717514f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(1.0803245f) * Matrix.CreateRotationY(0.88169813f) * Matrix.CreateRotationZ(7.1429486f) * Matrix.CreateTranslation(0.75332713f, 94.15164f, 115.56339f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(2.1925786f) * Matrix.CreateRotationY(0.80926996f) * Matrix.CreateRotationZ(8.528606f) * Matrix.CreateTranslation(8.087221f, 94.327934f, 115.59202f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(1.8514681f) * Matrix.CreateRotationY(0.24493179f) * Matrix.CreateRotationZ(8.385155f) * Matrix.CreateTranslation(6.296268f, 88.39294f, 119.941216f),
				Matrix.CreateScale(0.30302027f, 0.2437828f, 0.2813648f) * Matrix.CreateRotationX(1.3229165f) * Matrix.CreateRotationY(0.2780876f) * Matrix.CreateRotationZ(6.7688985f) * Matrix.CreateTranslation(4.963311f, 81.00862f, 122.748f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(3.1278183f) * Matrix.CreateRotationY(1.1404976f) * Matrix.CreateRotationZ(3.3042448f) * Matrix.CreateTranslation(-8.444035f, 98.070816f, 106.50215f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(0.4566107f) * Matrix.CreateRotationY(1.1504093f) * Matrix.CreateRotationZ(0.38427266f) * Matrix.CreateTranslation(-1.8399755f, 98.51286f, 108.11159f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(2.8086393f) * Matrix.CreateRotationY(1.0538026f) * Matrix.CreateRotationZ(2.6191573f) * Matrix.CreateTranslation(5.449545f, 97.328224f, 108.06936f),
				Matrix.CreateScale(0.3891732f, 0.3130937f, 0.3613608f) * Matrix.CreateRotationX(3.1004205f) * Matrix.CreateRotationY(-0.16276997f) * Matrix.CreateRotationZ(2.5253348f) * Matrix.CreateTranslation(10.831279f, 94.28727f, 108.138664f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(3.2813237f) * Matrix.CreateRotationY(1.0319936f) * Matrix.CreateRotationZ(3.2404447f) * Matrix.CreateTranslation(-5.1726103f, 98.36752f, 101.40253f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(3.2813237f) * Matrix.CreateRotationY(1.0319936f) * Matrix.CreateRotationZ(3.2404447f) * Matrix.CreateTranslation(0.28798407f, 99.15402f, 101.45956f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(3.2813237f) * Matrix.CreateRotationY(1.0319936f) * Matrix.CreateRotationZ(3.2404447f) * Matrix.CreateTranslation(5.5944076f, 98.32128f, 102.308f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(3.0835788f) * Matrix.CreateRotationY(0.9019291f) * Matrix.CreateRotationZ(2.5192397f) * Matrix.CreateTranslation(9.48187f, 95.96213f, 103.35636f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(3.253598f) * Matrix.CreateRotationY(1.2091832f) * Matrix.CreateRotationZ(4.215438f) * Matrix.CreateTranslation(-14.623247f, 94.260605f, 105.85333f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(3.049706f) * Matrix.CreateRotationY(4.2820697f) * Matrix.CreateRotationZ(3.8176272f) * Matrix.CreateTranslation(-11.032044f, 97.20025f, 101.99082f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(3.049706f) * Matrix.CreateRotationY(4.2820697f) * Matrix.CreateRotationZ(3.8176272f) * Matrix.CreateTranslation(-14.948451f, 94.52942f, 101.47423f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.6672786f) * Matrix.CreateRotationY(3.5845025f) * Matrix.CreateRotationZ(4.80943f) * Matrix.CreateTranslation(-31.633392f, 103.89071f, 110.79135f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.019545f) * Matrix.CreateRotationY(4.10219f) * Matrix.CreateRotationZ(5.1391516f) * Matrix.CreateTranslation(-26.268997f, 104.27949f, 111.54248f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(0.80888605f) * Matrix.CreateRotationY(4.245341f) * Matrix.CreateRotationZ(5.307986f) * Matrix.CreateTranslation(-21.183895f, 103.28259f, 111.69881f),
				Matrix.CreateScale(0.3296346f, 0.2651943f, 0.30607715f) * Matrix.CreateRotationX(1.6672786f) * Matrix.CreateRotationY(3.5845025f) * Matrix.CreateRotationZ(4.80943f) * Matrix.CreateTranslation(-29.352356f, 98.80729f, 108.14307f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.4344348f) * Matrix.CreateRotationY(3.6287203f) * Matrix.CreateRotationZ(5.218542f) * Matrix.CreateTranslation(-23.532885f, 98.0299f, 106.60842f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.0107301f) * Matrix.CreateRotationY(3.8478553f) * Matrix.CreateRotationZ(5.178656f) * Matrix.CreateTranslation(-19.98247f, 100.421646f, 108.01033f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(0.80888605f) * Matrix.CreateRotationY(4.245341f) * Matrix.CreateRotationZ(5.307986f) * Matrix.CreateTranslation(-18.408722f, 99.487206f, 110.583885f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.4837326f) * Matrix.CreateRotationY(3.4580224f) * Matrix.CreateRotationZ(5.0309386f) * Matrix.CreateTranslation(-26.037312f, 93.49069f, 106.19683f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.4447907f) * Matrix.CreateRotationY(3.4782767f) * Matrix.CreateRotationZ(5.0047097f) * Matrix.CreateTranslation(-19.706734f, 94.475296f, 105.28838f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.1072714f) * Matrix.CreateRotationY(3.7975168f) * Matrix.CreateRotationZ(5.0333447f) * Matrix.CreateTranslation(-16.181517f, 97.4408f, 106.88894f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(2.6417265f) * Matrix.CreateRotationY(2.9703288f) * Matrix.CreateRotationZ(2.5654485f) * Matrix.CreateTranslation(-11.834446f, 99.04786f, 111.80526f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(0.6066495f) * Matrix.CreateRotationY(3.908579f) * Matrix.CreateRotationZ(5.411663f) * Matrix.CreateTranslation(-16.664295f, 101.5979f, 111.148254f),
				Matrix.CreateScale(0.24190491f, 0.1331595f, 0.2121244f) * Matrix.CreateRotationX(2.2545562f) * Matrix.CreateRotationY(3.5349376f) * Matrix.CreateRotationZ(3.5916617f) * Matrix.CreateTranslation(-24.363554f, 102.140724f, 109.10907f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(1.0056846f) * Matrix.CreateRotationY(0.99867195f) * Matrix.CreateRotationZ(8.306467f) * Matrix.CreateTranslation(-13.453114f, 92.526505f, 111.616875f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-1.8076816f) * Matrix.CreateRotationY(-0.027260397f) * Matrix.CreateRotationZ(-2.7729237f) * Matrix.CreateTranslation(-21.451288f, 89.81291f, 104.78996f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(0.20949696f) * Matrix.CreateRotationY(1.8587636f) * Matrix.CreateRotationZ(1.4587867f) * Matrix.CreateTranslation(-16.879189f, 88.8569f, 106.76356f),
				Matrix.CreateScale(0.38712147f, 0.31144306f, 0.3594557f) * Matrix.CreateRotationX(2.5712478f) * Matrix.CreateRotationY(-0.0025405968f) * Matrix.CreateRotationZ(1.758418f) * Matrix.CreateTranslation(14.789168f, 81.7561f, 115.70111f),
				Matrix.CreateScale(0.1684977f, 0.13555807f, 0.15645595f) * Matrix.CreateRotationX(-0.5022078f) * Matrix.CreateRotationY(0.39912f) * Matrix.CreateRotationZ(0.28263077f) * Matrix.CreateTranslation(23.64873f, 104.045044f, 116.22992f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-0.98196733f) * Matrix.CreateRotationY(-0.07829085f) * Matrix.CreateRotationZ(-0.039024133f) * Matrix.CreateTranslation(23.608864f, 100.777985f, 112.62826f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-1.0321302f) * Matrix.CreateRotationY(-0.20873947f) * Matrix.CreateRotationZ(-0.14431144f) * Matrix.CreateTranslation(22.27088f, 97.46503f, 109.99588f),
				Matrix.CreateScale(0.23286094f, 0.18733893f, 0.21621944f) * Matrix.CreateRotationX(-0.13535571f) * Matrix.CreateRotationY(0.6865736f) * Matrix.CreateRotationZ(0.38400498f) * Matrix.CreateTranslation(19.728506f, 102.17037f, 115.84464f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-0.518204f) * Matrix.CreateRotationY(0.055907734f) * Matrix.CreateRotationZ(0.41903734f) * Matrix.CreateTranslation(16.291985f, 100.123116f, 112.41372f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-0.97066826f) * Matrix.CreateRotationY(0.0016372857f) * Matrix.CreateRotationZ(0.20408918f) * Matrix.CreateTranslation(18.405169f, 97.38823f, 109.038185f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(0.3299982f) * Matrix.CreateRotationY(0.4894939f) * Matrix.CreateRotationZ(0.8413306f) * Matrix.CreateTranslation(16.095556f, 99.37985f, 115.85256f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-0.5838058f) * Matrix.CreateRotationY(0.29608816f) * Matrix.CreateRotationZ(0.47668618f) * Matrix.CreateTranslation(12.420965f, 96.83835f, 111.21925f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-0.9660442f) * Matrix.CreateRotationY(0.12508559f) * Matrix.CreateRotationZ(0.19941287f) * Matrix.CreateTranslation(15.346796f, 94.09566f, 108.08215f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-1.217677f) * Matrix.CreateRotationY(-0.18107499f) * Matrix.CreateRotationZ(0.2032162f) * Matrix.CreateTranslation(21.150663f, 93.88156f, 107.578094f),
				Matrix.CreateScale(0.24190491f, 0.1331595f, 0.2121244f) * Matrix.CreateRotationX(-1.2991965f) * Matrix.CreateRotationY(0.81374866f) * Matrix.CreateRotationZ(-1.0156515f) * Matrix.CreateTranslation(21.429167f, 100.9801f, 111.34757f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(-1.7983912f) * Matrix.CreateRotationY(0.28956425f) * Matrix.CreateRotationZ(-2.2798836f) * Matrix.CreateTranslation(19.139101f, 89.45162f, 105.60781f),
				Matrix.CreateScale(0.2431268f, 0.19559792f, 0.22575165f) * Matrix.CreateRotationX(1.4344832f) * Matrix.CreateRotationY(-1.4309354f) * Matrix.CreateRotationZ(-0.57658786f) * Matrix.CreateTranslation(12.234722f, 96.13136f, 115.39971f),
				Matrix.CreateScale(0.30302027f, 0.2437828f, 0.2813648f) * Matrix.CreateRotationX(1.4987752f) * Matrix.CreateRotationY(0.7135183f) * Matrix.CreateRotationZ(6.644081f) * Matrix.CreateTranslation(10.027553f, 81.39528f, 120.18646f),
				Matrix.CreateScale(0.32025385f, 0.2576474f, 0.2973668f) * Matrix.CreateRotationX(2.7592385f) * Matrix.CreateRotationY(-0.39296126f) * Matrix.CreateRotationZ(1.8487116f) * Matrix.CreateTranslation(17.965237f, 79.736206f, 110.20524f),
				Matrix.CreateScale(0.31720233f, 0.16534735f, 0.29453334f) * Matrix.CreateRotationX(0.8415748f) * Matrix.CreateRotationY(0.20422222f) * Matrix.CreateRotationZ(5.193258f) * Matrix.CreateTranslation(11.528593f, 87.96191f, 117.91866f),
				Matrix.CreateScale(0.32025385f, 0.18283471f, 0.2973668f) * Matrix.CreateRotationX(2.7592385f) * Matrix.CreateRotationY(-0.39296126f) * Matrix.CreateRotationZ(1.8487116f) * Matrix.CreateTranslation(16.070314f, 89.964584f, 111.84926f),
				Matrix.CreateScale(0.17614454f, 0.1005619f, 0.16355631f) * Matrix.CreateRotationX(2.7592385f) * Matrix.CreateRotationY(-0.39296126f) * Matrix.CreateRotationZ(1.8487116f) * Matrix.CreateTranslation(16.125658f, 85.37537f, 112.92349f),
				Matrix.CreateScale(0.17614454f, 0.1005619f, 0.16355631f) * Matrix.CreateRotationX(2.7592385f) * Matrix.CreateRotationY(-0.39296126f) * Matrix.CreateRotationZ(1.8487116f) * Matrix.CreateTranslation(17.525887f, 84.8205f, 109.628716f),
				Matrix.CreateScale(0.17614454f, 0.1005619f, 0.16355631f) * Matrix.CreateRotationX(2.7592385f) * Matrix.CreateRotationY(-0.39296126f) * Matrix.CreateRotationZ(1.8487116f) * Matrix.CreateTranslation(18.376661f, 87.039955f, 107.308624f),
				Matrix.CreateScale(0.17614454f, 0.1005619f, 0.16355631f) * Matrix.CreateRotationX(3.1815517f) * Matrix.CreateRotationY(-0.4181991f) * Matrix.CreateRotationZ(1.6567189f) * Matrix.CreateTranslation(18.090372f, 84.09554f, 105.95903f),
				Matrix.CreateScale(0.17614454f, 0.1005619f, 0.16355631f) * Matrix.CreateRotationX(3.3778288f) * Matrix.CreateRotationY(-0.36525625f) * Matrix.CreateRotationZ(1.5936564f) * Matrix.CreateTranslation(18.447073f, 80.50692f, 105.51458f)
			};
			for (int num = 0; num < 55; num++)
			{
				this.dropChunk(ref this.faceChunk, array[num], 7);
			}
			array = new Matrix[]
			{
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.5846392f) * Matrix.CreateRotationY(-0.014835481f) * Matrix.CreateRotationZ(0.7115125f) * Matrix.CreateTranslation(3.1078758f, 67.61283f, -106.55259f),
				Matrix.CreateScale(0.5492081f, 0.7507169f, 0.8029641f) * Matrix.CreateRotationX(-1.3701718f) * Matrix.CreateRotationY(-0.012169369f) * Matrix.CreateRotationZ(-0.040402282f) * Matrix.CreateTranslation(2.9305055f, 80.62694f, -104.24733f),
				Matrix.CreateScale(0.70592993f, 0.5492081f, 0.47559366f) * Matrix.CreateRotationX(-1.5158356f) * Matrix.CreateRotationY(-0.117167786f) * Matrix.CreateRotationZ(-0.03497848f) * Matrix.CreateTranslation(10.195426f, 74.731514f, -105.112785f),
				Matrix.CreateScale(0.57177246f, 0.4458087f, 0.31561494f) * Matrix.CreateRotationX(-1.6042562f) * Matrix.CreateRotationY(0.047173783f) * Matrix.CreateRotationZ(-1.8796893f) * Matrix.CreateTranslation(-3.4566817f, 72.73312f, -105.938484f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.7092129f) * Matrix.CreateRotationY(0.26111522f) * Matrix.CreateRotationZ(-0.064709336f) * Matrix.CreateTranslation(-18.380386f, 62.86674f, -103.597916f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.7045047f) * Matrix.CreateRotationY(0.013593543f) * Matrix.CreateRotationZ(-0.030589852f) * Matrix.CreateTranslation(-4.1560817f, 62.118717f, -106.11586f),
				Matrix.CreateScale(0.32492113f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.6453074f) * Matrix.CreateRotationY(0.016161628f) * Matrix.CreateRotationZ(0.497547f) * Matrix.CreateTranslation(2.0058608f, 56.66722f, -105.30627f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.7054935f) * Matrix.CreateRotationY(-0.121635325f) * Matrix.CreateRotationZ(-0.012319749f) * Matrix.CreateTranslation(10.195426f, 61.806583f, -105.36563f),
				Matrix.CreateScale(0.94858f, 0.5492081f, 0.45682305f) * Matrix.CreateRotationX(-1.5954841f) * Matrix.CreateRotationY(-0.32521608f) * Matrix.CreateRotationZ(0.1891407f) * Matrix.CreateTranslation(17.378033f, 67.949196f, -103.997215f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.6973703f) * Matrix.CreateRotationY(-0.27637738f) * Matrix.CreateRotationZ(1.3097699f) * Matrix.CreateTranslation(17.272568f, 80.21303f, -101.93212f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.4638656f) * Matrix.CreateRotationY(-0.49483693f) * Matrix.CreateRotationZ(-0.1271584f) * Matrix.CreateTranslation(23.988367f, 73.67303f, -100.823654f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.5900644f) * Matrix.CreateRotationY(0.22163798f) * Matrix.CreateRotationZ(-0.15261841f) * Matrix.CreateTranslation(-11.219935f, 68.364586f, -105.26003f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.3865564f) * Matrix.CreateRotationY(0.13629584f) * Matrix.CreateRotationZ(1.1992105f) * Matrix.CreateTranslation(-11.058108f, 56.293888f, -104.53733f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.3853917f) * Matrix.CreateRotationY(0.2518054f) * Matrix.CreateRotationZ(-0.12442406f) * Matrix.CreateTranslation(-11.286323f, 80.298775f, -103.03417f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.27597f) * Matrix.CreateRotationY(0.025076263f) * Matrix.CreateRotationZ(-0.022175437f) * Matrix.CreateTranslation(-4.1560817f, 86.63884f, -102.13447f),
				Matrix.CreateScale(0.63652015f, 0.63652015f, 0.63652015f) * Matrix.CreateRotationX(-0.9661885f) * Matrix.CreateRotationY(-0.28353474f) * Matrix.CreateRotationZ(0.11555218f) * Matrix.CreateTranslation(3.8893895f, 89.29078f, -98.13169f),
				Matrix.CreateScale(0.63652015f, 0.63652015f, 0.63652015f) * Matrix.CreateRotationX(-0.9661885f) * Matrix.CreateRotationY(-0.28353474f) * Matrix.CreateRotationZ(0.11555218f) * Matrix.CreateTranslation(8.940514f, 89.7711f, -94.69171f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.6053509f) * Matrix.CreateRotationY(-0.19312143f) * Matrix.CreateRotationZ(0.011296679f) * Matrix.CreateTranslation(17.452402f, 56.29916f, -102.87777f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.5743136f) * Matrix.CreateRotationY(0.3214582f) * Matrix.CreateRotationZ(1.9443505f) * Matrix.CreateTranslation(10.195426f, 50.26846f, -102.59111f),
				Matrix.CreateScale(0.4914728f, 0.5492081f, 0.5178722f) * Matrix.CreateRotationX(-1.9465988f) * Matrix.CreateRotationY(-0.04572508f) * Matrix.CreateRotationZ(-0.018464899f) * Matrix.CreateTranslation(3.0732512f, 45.97189f, -101.279106f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.8666884f) * Matrix.CreateRotationY(0.008474327f) * Matrix.CreateRotationZ(-0.032383278f) * Matrix.CreateTranslation(-4.1560817f, 50.4553f, -103.381905f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.67821f) * Matrix.CreateRotationY(-0.5090938f) * Matrix.CreateRotationZ(-0.02371972f) * Matrix.CreateTranslation(23.988367f, 61.56994f, -100.95417f),
				Matrix.CreateScale(0.2705493f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.2899157f) * Matrix.CreateRotationY(-0.72987324f) * Matrix.CreateRotationZ(-0.22599353f) * Matrix.CreateTranslation(30.160667f, 65.47449f, -97.69554f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.3365968f) * Matrix.CreateRotationY(-0.5502652f) * Matrix.CreateRotationZ(-0.09253939f) * Matrix.CreateTranslation(28.562046f, 78.60998f, -96.64705f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.1986895f) * Matrix.CreateRotationY(-0.4496977f) * Matrix.CreateRotationZ(-0.2483829f) * Matrix.CreateTranslation(23.988367f, 83.99449f, -97.66775f),
				Matrix.CreateScale(0.63652015f, 0.63652015f, 0.63652015f) * Matrix.CreateRotationX(-0.9661885f) * Matrix.CreateRotationY(-0.28353474f) * Matrix.CreateRotationZ(0.11555218f) * Matrix.CreateTranslation(2.3776069f, 94.06802f, -94.868286f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.8822971f) * Matrix.CreateRotationY(0.14015731f) * Matrix.CreateRotationZ(-0.009255556f) * Matrix.CreateTranslation(-3.8378632f, 92.799706f, -96.80923f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.2651675f) * Matrix.CreateRotationY(0.26382017f) * Matrix.CreateRotationZ(0.05230235f) * Matrix.CreateTranslation(-18.380386f, 86.272736f, -99.5334f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.3169764f) * Matrix.CreateRotationX(-1.2966465f) * Matrix.CreateRotationY(0.008123664f) * Matrix.CreateRotationZ(1.3179368f) * Matrix.CreateTranslation(-19.964022f, 74.182724f, -102.82208f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.4452698f) * Matrix.CreateRotationY(0.5223918f) * Matrix.CreateRotationZ(-0.0029149558f) * Matrix.CreateTranslation(-24.889975f, 68.94341f, -100.74631f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.8762473f) * Matrix.CreateRotationY(0.24718492f) * Matrix.CreateRotationZ(-0.106798336f) * Matrix.CreateTranslation(-18.380386f, 51.600254f, -101.01787f),
				Matrix.CreateScale(0.42794695f, 0.5492081f, 0.35001442f) * Matrix.CreateRotationX(-1.9608252f) * Matrix.CreateRotationY(0.13064536f) * Matrix.CreateRotationZ(-0.23222719f) * Matrix.CreateTranslation(-11.027479f, 46.062172f, -100.22634f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.9148448f) * Matrix.CreateRotationY(-0.20298538f) * Matrix.CreateRotationZ(0.05230667f) * Matrix.CreateTranslation(17.334446f, 45.8704f, -98.46881f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.2094212f) * Matrix.CreateRotationY(-0.116487764f) * Matrix.CreateRotationZ(0.03718929f) * Matrix.CreateTranslation(9.938953f, 39.926693f, -96.38662f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.8638681f) * Matrix.CreateRotationY(-0.5055974f) * Matrix.CreateRotationZ(0.06668243f) * Matrix.CreateTranslation(23.988367f, 50.747566f, -98.199356f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.5800563f) * Matrix.CreateRotationY(-0.6725001f) * Matrix.CreateRotationZ(-0.044211153f) * Matrix.CreateTranslation(29.986889f, 56.207153f, -95.98568f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.1815281f) * Matrix.CreateRotationY(-1.0725075f) * Matrix.CreateRotationZ(-0.37330717f) * Matrix.CreateTranslation(34.453873f, 71.669266f, -91.52195f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.4889047f) * Matrix.CreateRotationY(-1.0186902f) * Matrix.CreateRotationZ(0.056712914f) * Matrix.CreateTranslation(33.064293f, 77.952515f, -91.80834f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.8755695f) * Matrix.CreateRotationY(-0.17781247f) * Matrix.CreateRotationZ(-0.31678164f) * Matrix.CreateTranslation(20.400799f, 92.71499f, -91.93767f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.8822971f) * Matrix.CreateRotationY(0.14015731f) * Matrix.CreateRotationZ(-0.009255556f) * Matrix.CreateTranslation(-9.879172f, 89.55384f, -98.70473f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.8278552f) * Matrix.CreateRotationY(0.29837137f) * Matrix.CreateRotationZ(0.0013276901f) * Matrix.CreateTranslation(-17.662302f, 92.46165f, -93.845215f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.2579253f) * Matrix.CreateRotationY(0.68983287f) * Matrix.CreateRotationZ(0.0069400733f) * Matrix.CreateTranslation(-24.450983f, 81.02595f, -97.46898f),
				Matrix.CreateScale(0.7280517f, 0.5492081f, 0.5133965f) * Matrix.CreateRotationX(-1.6284887f) * Matrix.CreateRotationY(0.51974475f) * Matrix.CreateRotationZ(-0.10933615f) * Matrix.CreateTranslation(-24.834806f, 57.22952f, -100.374886f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.8823295f) * Matrix.CreateRotationY(0.4796757f) * Matrix.CreateRotationZ(-0.21550027f) * Matrix.CreateTranslation(-24.889975f, 52.826237f, -98.31365f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.205311f) * Matrix.CreateRotationY(0.00034005797f) * Matrix.CreateRotationZ(-0.033471633f) * Matrix.CreateTranslation(-4.3281846f, 39.915607f, -97.19421f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.3231473f) * Matrix.CreateRotationY(-0.116487764f) * Matrix.CreateRotationZ(0.03718929f) * Matrix.CreateTranslation(15.315189f, 36.140957f, -92.88421f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.2330668f) * Matrix.CreateRotationY(-0.47302473f) * Matrix.CreateRotationZ(0.1972035f) * Matrix.CreateTranslation(23.202915f, 41.308872f, -92.53364f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.9405462f) * Matrix.CreateRotationY(-0.563806f) * Matrix.CreateRotationZ(0.13085556f) * Matrix.CreateTranslation(30.094227f, 48.04029f, -92.291626f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.714557f) * Matrix.CreateRotationY(-1.1070327f) * Matrix.CreateRotationZ(0.10168494f) * Matrix.CreateTranslation(34.453873f, 56.59346f, -90.71127f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.1815281f) * Matrix.CreateRotationY(-1.0725075f) * Matrix.CreateRotationZ(-0.37330717f) * Matrix.CreateTranslation(36.442837f, 68.27344f, -87.93319f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(1.0773473f) * Matrix.CreateRotationY(3.867327f) * Matrix.CreateRotationZ(0.17169067f) * Matrix.CreateTranslation(-28.98379f, 80.55913f, -93.53776f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.4425325f) * Matrix.CreateRotationY(0.5203474f) * Matrix.CreateRotationZ(0.034381848f) * Matrix.CreateTranslation(-34.07364f, 74.91253f, -94.70304f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.6594555f) * Matrix.CreateRotationY(0.516653f) * Matrix.CreateRotationZ(-0.073439576f) * Matrix.CreateTranslation(-31.044107f, 63.710682f, -96.89971f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.4425325f) * Matrix.CreateRotationY(0.5203474f) * Matrix.CreateRotationZ(0.034381848f) * Matrix.CreateTranslation(-31.044107f, 75.01673f, -96.44006f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.168756f) * Matrix.CreateRotationY(0.49587926f) * Matrix.CreateRotationZ(0.16823038f) * Matrix.CreateTranslation(-31.044107f, 84.256546f, -93.090324f),
				Matrix.CreateScale(0.5492081f, 0.8177453f, 0.5492081f) * Matrix.CreateRotationX(0.59339905f) * Matrix.CreateRotationY(3.2670996f) * Matrix.CreateRotationZ(0.82572746f) * Matrix.CreateTranslation(-27.472307f, 88.52301f, -91.77288f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(0.7353731f) * Matrix.CreateRotationY(3.5824244f) * Matrix.CreateRotationZ(0.42705923f) * Matrix.CreateTranslation(-22.710556f, 92.36538f, -91.93782f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.403014f) * Matrix.CreateRotationY(0.51883024f) * Matrix.CreateRotationZ(0.05400566f) * Matrix.CreateTranslation(-34.145824f, 67.9987f, -95.2201f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.069983f) * Matrix.CreateRotationY(0.5540223f) * Matrix.CreateRotationZ(-0.42915565f) * Matrix.CreateTranslation(-24.52844f, 48.140343f, -96.82264f),
				Matrix.CreateScale(0.4601228f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.324843f) * Matrix.CreateRotationY(-0.054546572f) * Matrix.CreateRotationZ(-0.0028381015f) * Matrix.CreateTranslation(0.30835664f, 35.464058f, -94.76395f),
				Matrix.CreateScale(0.19484207f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.116205f) * Matrix.CreateRotationY(-1.05907f) * Matrix.CreateRotationZ(-0.4304821f) * Matrix.CreateTranslation(33.62607f, 65.03218f, -93.81542f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.0335214f) * Matrix.CreateRotationY(-0.6668906f) * Matrix.CreateRotationZ(-0.3914625f) * Matrix.CreateTranslation(30.160667f, 81.76501f, -94.32763f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.84196424f) * Matrix.CreateRotationY(-0.07450044f) * Matrix.CreateRotationZ(-0.48466796f) * Matrix.CreateTranslation(25.68081f, 91.01244f, -92.24491f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.9074931f) * Matrix.CreateRotationY(-0.1044257f) * Matrix.CreateRotationZ(-0.23673004f) * Matrix.CreateTranslation(13.733306f, 89.46902f, -96.978485f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.95538026f) * Matrix.CreateRotationY(-0.1550788f) * Matrix.CreateRotationZ(-0.23464693f) * Matrix.CreateTranslation(14.272582f, 94.20745f, -92.822174f),
				Matrix.CreateScale(0.3244631f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.4194545f) * Matrix.CreateRotationY(-0.2763122f) * Matrix.CreateRotationZ(0.667177f) * Matrix.CreateTranslation(8.037078f, 84.34308f, -102.36092f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.8822971f) * Matrix.CreateRotationY(0.14015731f) * Matrix.CreateRotationZ(-0.009255556f) * Matrix.CreateTranslation(-3.2385256f, 89.880455f, -99.31867f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.8822971f) * Matrix.CreateRotationY(0.14015731f) * Matrix.CreateRotationZ(-0.009255556f) * Matrix.CreateTranslation(-10.641102f, 92.86268f, -95.849365f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.7260845f) * Matrix.CreateRotationY(0.5114255f) * Matrix.CreateRotationZ(-0.10621024f) * Matrix.CreateTranslation(-31.044107f, 58.63907f, -96.29777f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.1127684f) * Matrix.CreateRotationY(0.44237784f) * Matrix.CreateRotationZ(-0.2855793f) * Matrix.CreateTranslation(-31.044107f, 50.213203f, -93.20737f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.6594555f) * Matrix.CreateRotationY(0.516653f) * Matrix.CreateRotationZ(-0.073439576f) * Matrix.CreateTranslation(-33.85883f, 63.895256f, -95.58768f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(1.0773473f) * Matrix.CreateRotationY(3.867327f) * Matrix.CreateRotationZ(0.17169067f) * Matrix.CreateTranslation(-32.184517f, 80.00413f, -90.65548f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.6849225f) * Matrix.CreateRotationY(-0.24239352f) * Matrix.CreateRotationZ(-0.7679393f) * Matrix.CreateTranslation(31.212025f, 86.87792f, -90.33482f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.7924579f) * Matrix.CreateRotationY(-0.94705695f) * Matrix.CreateRotationZ(-0.7045537f) * Matrix.CreateTranslation(34.453873f, 79.83499f, -89.10998f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-2.3615553f) * Matrix.CreateRotationY(-0.9695611f) * Matrix.CreateRotationZ(0.6653488f) * Matrix.CreateTranslation(34.453873f, 50.206257f, -87.25814f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.8438241f) * Matrix.CreateRotationY(0.49744162f) * Matrix.CreateRotationZ(-0.16316704f) * Matrix.CreateTranslation(-32.472736f, 53.75036f, -93.757805f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-1.8438241f) * Matrix.CreateRotationY(0.49744162f) * Matrix.CreateRotationZ(-0.16316704f) * Matrix.CreateTranslation(-31.044107f, 53.515163f, -94.54397f),
				Matrix.CreateScale(0.5492081f, 0.5492081f, 0.5492081f) * Matrix.CreateRotationX(-0.9135846f) * Matrix.CreateRotationY(-0.68297535f) * Matrix.CreateRotationZ(-0.27600548f) * Matrix.CreateTranslation(29.88582f, 84.34374f, -93.59616f)
			};
			for (int num2 = 0; num2 < 78; num2++)
			{
				this.dropChunk(ref this.assChunk, array[num2], 2);
			}
			this.rocks = new rockSystem(sc.Game, this.content);
			this.rocks.Initialize();
			this.rocks.LoadContent(sc.GraphicsDevice);
			this.dots = new dot2System(sc.Game, this.content);
			this.dots.Initialize();
			this.dots.LoadContent(sc.GraphicsDevice);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000EF464 File Offset: 0x000ED664
		public void setPrincessTargets()
		{
			this.screenTarget1 = new RenderTarget2D(this.sc.GraphicsDevice, 1024, 1024, true, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.screenTarget2 = new RenderTarget2D(this.sc.GraphicsDevice, 1024, 1024, true, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.canvasTarget1 = new RenderTarget2D(this.sc.GraphicsDevice, 2000, 2000, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.canvasTarget2 = new RenderTarget2D(this.sc.GraphicsDevice, 2000, 2000, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);
			this.sc.isLoading = false;
			this.initTarget();
			this.initCanvas();
			this.sc.isLoading = true;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000EF52C File Offset: 0x000ED72C
		public void disposePrincessTargets()
		{
			if (!this.screenTarget1.IsDisposed)
			{
				this.screenTarget1.Dispose();
			}
			if (!this.screenTarget2.IsDisposed)
			{
				this.screenTarget2.Dispose();
			}
			if (!this.canvasTarget1.IsDisposed)
			{
				this.canvasTarget1.Dispose();
			}
			if (!this.canvasTarget2.IsDisposed)
			{
				this.canvasTarget2.Dispose();
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000EF59C File Offset: 0x000ED79C
		public void loadModels()
		{
			if (this.sc.localLoad)
			{
				this.pigModel = this.content.Load<Model>("boss\\bossBase");
			}
			else
			{
				this.pigModel = this.sc.pigModelPrincess;
			}
			SkinningData skinningData = this.pigModel.Tag as SkinningData;
			this.pig1 = new AnimationPlayer[7];
			this.pig1[0] = new AnimationPlayer(skinningData);
			AnimationClip animationClip = skinningData.AnimationClips["bosswait"];
			this.pig1[0].StartClip(animationClip);
			this.pig1[1] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["bossbuck"];
			this.pig1[1].StartClip(animationClip);
			this.pig1[2] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["bossjump"];
			this.pig1[2].StartClip(animationClip);
			this.pig1[3] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["bossroll"];
			this.pig1[3].StartClip(animationClip);
			this.pig1[4] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["bossdie"];
			this.pig1[4].StartClip(animationClip);
			this.pig1[5] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["bossturnleft"];
			this.pig1[5].StartClip(animationClip);
			this.pig1[6] = new AnimationPlayer(skinningData);
			animationClip = skinningData.AnimationClips["bossturnright"];
			this.pig1[6].StartClip(animationClip);
			this.clipIndexA = 0;
			this.a = this.pig1[this.clipIndexA];
			this.b = this.pig1[this.clipIndexA];
			this.princessBone = new Matrix[this.a.skinTransforms.Length];
			this.pigModelLoaded = true;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000EF783 File Offset: 0x000ED983
		public void UnloadContent()
		{
			this.rocks.unloadContent();
			this.dots.unloadContent();
			this.content.Unload();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000EF7A8 File Offset: 0x000ED9A8
		public void SetTangents(ref Curve x, ref Curve y)
		{
			for (int i = 0; i < x.Keys.Count; i++)
			{
				int num = i - 1;
				if (num < 0)
				{
					num = i;
				}
				int num2 = i + 1;
				if (num2 == x.Keys.Count)
				{
					num2 = i;
				}
				CurveKey curveKey = x.Keys[num];
				CurveKey curveKey2 = x.Keys[num2];
				CurveKey curveKey3 = x.Keys[i];
				Princess.SetCurveKeyTangent(ref curveKey, ref curveKey3, ref curveKey2);
				x.Keys[i] = curveKey3;
				curveKey = y.Keys[num];
				curveKey2 = y.Keys[num2];
				curveKey3 = y.Keys[i];
				Princess.SetCurveKeyTangent(ref curveKey, ref curveKey3, ref curveKey2);
				y.Keys[i] = curveKey3;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000EF888 File Offset: 0x000EDA88
		private static void SetCurveKeyTangent(ref CurveKey prev, ref CurveKey cur, ref CurveKey next)
		{
			float num = next.Position - prev.Position;
			float num2 = next.Value - prev.Value;
			if (Math.Abs(num2) < 1E-45f)
			{
				cur.TangentIn = 0f;
				cur.TangentOut = 0f;
				return;
			}
			cur.TangentIn = num2 * (cur.Position - prev.Position) / num;
			cur.TangentOut = num2 * (next.Position - cur.Position) / num;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000EF910 File Offset: 0x000EDB10
		public void damHealth(ushort amt)
		{
			if (!Princess.cuttyDoneSpeech)
			{
				return;
			}
			if (amt > 0)
			{
				this.hurtBoss++;
			}
			this.health -= amt;
			if (this.health < 0 || this.health >= 15000)
			{
				this.health = 0;
			}
			if (this.bodyState > 0 && this.health < 25)
			{
				this.heartExposed = true;
			}
			if (this.heartExposed)
			{
				this.health = 0;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000EF98C File Offset: 0x000EDB8C
		public void startHeartAttack()
		{
			this.volumeFade = true;
			this.heartVol = true;
			this.heartattack = 370f;
			this.heartbeat.Play(this.sc.ev, 0f, 0f);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000EF9C8 File Offset: 0x000EDBC8
		public void checkTargets(Vector3 gunpos, Vector3 gunlook, ref Cursor genCursor, float wall)
		{
			if (!Princess.cuttyDoneSpeech)
			{
				this.health = this.startHealth;
				return;
			}
			this.cuttyDistance = 20000f;
			float num = 20000f;
			this.boneHit = -1;
			Matrix matrix = Matrix.Identity;
			this.cuttyisHit = false;
			if (this.explodeTimer <= 0)
			{
				return;
			}
			for (int i = 0; i < this.col_Bone.Length; i++)
			{
				matrix = Matrix.CreateTranslation(this.col_Pos[i]) * this.princessBone[this.col_Bone[i]];
				Vector3 vector = Vector3.Transform(Vector3.Zero, matrix);
				float? num2 = genCursor.hitSphere2(gunpos, gunlook, vector, this.col_Scale[i] * this.cuttyScale);
				bool flag = this.heartExposed && this.heartattack <= 0f && this.cuttyPos.Y > 100f && (this.boneHit == 2 || this.boneHit == 3) && this.col_Bone[i] == 17;
				if (num2 != null && (flag || (num2.Value < num && num2.Value < wall)))
				{
					this.cuttyisHit = true;
					this.hitCenter = Vector3.Transform(Vector3.Zero, matrix);
					this.hitEdge = num2.Value * genCursor.rayDir + genCursor.rayPos;
					num = num2.Value;
					this.cuttyDistance = num;
					this.boneHit = i;
				}
			}
			if (this.cuttyisHit && this.boneHit != -1)
			{
				Matrix matrix2 = Matrix.Invert(Matrix.CreateTranslation(this.col_Pos[this.boneHit]) * this.princessBone[this.col_Bone[this.boneHit]]);
				Vector3 vector2 = Vector3.Transform(this.hitEdge, matrix2) - Vector3.Transform(this.hitCenter, matrix2);
				if (this.boneHit == 5 && this.heartExposed && Vector2.DistanceSquared(new Vector2(this.cuttyPos.X, this.cuttyPos.Z), new Vector2(this.playerpos.X, this.playerpos.Z)) < this.minDistance[this.df])
				{
					this.heartHit += 1;
					this.heartIndex = 0;
					this.damHealth(10);
					this.startHeartAttack();
				}
				else if (this.boneHit == 0)
				{
					if (vector2.X < 0f)
					{
						Vector2 vector3 = new Vector2(460f, 14f);
						Vector2 vector4 = new Vector2(512f, 15f);
						Vector2 vector5 = new Vector2(415f, 98f);
						Vector2 vector6 = new Vector2(512f, 114f);
						this.addBulletHole(vector3, vector4, vector5, vector6, vector2.Z, vector2.Y, this.boneHit);
					}
					else
					{
						Vector2 vector7 = new Vector2(564f, 14f);
						Vector2 vector8 = new Vector2(512f, 15f);
						Vector2 vector9 = new Vector2(589f, 98f);
						Vector2 vector10 = new Vector2(512f, 114f);
						this.addBulletHole(vector7, vector8, vector9, vector10, vector2.Z, vector2.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 1)
				{
					if (vector2.X < 0f)
					{
						Vector2 vector11 = new Vector2(409f, 62f);
						Vector2 vector12 = new Vector2(512f, 82f);
						Vector2 vector13 = new Vector2(295f, 244f);
						Vector2 vector14 = new Vector2(512f, 311f);
						this.addBulletHole(vector11, vector12, vector13, vector14, vector2.Z, vector2.Y, this.boneHit);
					}
					else
					{
						Vector2 vector15 = new Vector2(615f, 62f);
						Vector2 vector16 = new Vector2(512f, 82f);
						Vector2 vector17 = new Vector2(729f, 244f);
						Vector2 vector18 = new Vector2(512f, 311f);
						this.addBulletHole(vector15, vector16, vector17, vector18, vector2.Z, vector2.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 2)
				{
					if (vector2.X < 0f)
					{
						Vector2 vector19 = new Vector2(262f, 165f);
						Vector2 vector20 = new Vector2(512f, 225f);
						Vector2 vector21 = new Vector2(224f, 428f);
						Vector2 vector22 = new Vector2(512f, 473f);
						this.addBulletHole(vector19, vector20, vector21, vector22, vector2.Z, vector2.Y, this.boneHit);
					}
					else
					{
						Vector2 vector23 = new Vector2(762f, 165f);
						Vector2 vector24 = new Vector2(512f, 225f);
						Vector2 vector25 = new Vector2(810f, 428f);
						Vector2 vector26 = new Vector2(512f, 473f);
						this.addBulletHole(vector23, vector24, vector25, vector26, vector2.Z, vector2.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 3)
				{
					if (vector2.X < 0f)
					{
						Vector2 vector27 = new Vector2(266f, 351f);
						Vector2 vector28 = new Vector2(512f, 367f);
						Vector2 vector29 = new Vector2(252f, 474f);
						Vector2 vector30 = new Vector2(512f, 499f);
						this.addBulletHole(vector27, vector28, vector29, vector30, vector2.Z, vector2.Y, this.boneHit);
					}
					else
					{
						Vector2 vector31 = new Vector2(758f, 351f);
						Vector2 vector32 = new Vector2(512f, 367f);
						Vector2 vector33 = new Vector2(772f, 474f);
						Vector2 vector34 = new Vector2(512f, 499f);
						this.addBulletHole(vector31, vector32, vector33, vector34, vector2.Z, vector2.Y, -this.boneHit);
					}
				}
				else if (this.boneHit == 4)
				{
					if (vector2.X < 0f)
					{
						Vector2 vector35 = new Vector2(256f, 416f);
						Vector2 vector36 = new Vector2(512f, 448f);
						Vector2 vector37 = new Vector2(284f, 670f);
						Vector2 vector38 = new Vector2(512f, 568f);
						this.addBulletHole(vector35, vector36, vector37, vector38, vector2.Z, vector2.Y, this.boneHit);
					}
					else
					{
						Vector2 vector39 = new Vector2(768f, 416f);
						Vector2 vector40 = new Vector2(512f, 448f);
						Vector2 vector41 = new Vector2(740f, 670f);
						Vector2 vector42 = new Vector2(512f, 568f);
						this.addBulletHole(vector39, vector40, vector41, vector42, vector2.Z, vector2.Y, -this.boneHit);
					}
				}
				ushort num3 = 0;
				if (this.distanceCutty < this.minDistance[this.df])
				{
					if (this.df == 2)
					{
						num3 = 1;
					}
					else if (this.df == 1)
					{
						num3 = 2;
					}
					else if (this.df == 0)
					{
						num3 = 3;
					}
					else if (this.df == 3)
					{
						num3 = 1;
					}
					else if (this.df == 4 && this.rr.Next(1, 1000) < 800)
					{
						num3 = 1;
					}
					else if (this.df >= 5 && this.rr.Next(1, 1000) < 300)
					{
						num3 = 1;
					}
				}
				this.damHealth(num3);
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000F01A4 File Offset: 0x000EE3A4
		private void addBulletHole(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float sendY, float sendX, int bonehit)
		{
			float num = this.col_Scale[this.boneHit];
			float num2 = (sendY + num) / (num * 2f);
			float num3 = (sendX + num) / (num * 2f);
			float num4 = MathHelper.Lerp(p3.X, p1.X, num2);
			float num5 = MathHelper.Lerp(p4.X, p2.X, num2);
			float num6 = MathHelper.Lerp(num4, num5, num3);
			float num7 = MathHelper.Lerp(p3.Y, p4.Y, num3);
			float num8 = MathHelper.Lerp(p1.Y, p2.Y, num3);
			float num9 = MathHelper.Lerp(num7, num8, num2);
			Princess.uvIndex = (byte)this.myIndex;
			Princess.xcoord = (int)num6;
			Princess.ycoord = (int)num9;
			int num10 = 6;
			if (this.distanceCutty < this.minDistance[this.df])
			{
				num10 = this.sc.paintColor;
			}
			this.addTargetBlood(num10, (int)num6 - 15, (int)num6 + 15, (int)num9 - 15, (int)num9 + 15);
			this.addTargetBlood(num10, (int)num6 - 15, (int)num6 + 15, (int)num9 - 15, (int)num9 + 15);
			if (this.boneHit == 1 && num3 > 0.4f)
			{
				if (bonehit > 0)
				{
					this.addTargetBlood(num10, 14, 209, 725, 845);
					this.addTargetBlood(num10, 14, 209, 725, 845);
				}
				else
				{
					this.addTargetBlood(num10, 248, 443, 737, 857);
					this.addTargetBlood(num10, 248, 443, 737, 857);
				}
			}
			if ((this.boneHit == 3 || this.boneHit == 2) && num3 > 0.52f && (this.boneHit != 2 || num2 < 0.48f) && this.distanceCutty < this.minDistance[this.df])
			{
				this.spineDamage += 1;
				if (this.spineDamage > 300)
				{
					this.spineDamage = 300;
				}
				if ((float)this.spineDamage > this.spineBreach[this.df])
				{
					if (!this.spineDestroyed)
					{
						this.spineSwitch();
					}
					if ((float)this.spineDamage < this.spineBreach[this.df] + (float)(620 / this.spineDam[this.df]))
					{
						this.damHealth(this.spineDam[this.df]);
						this.buzzSkeleton(18);
						this.boneStrike = true;
					}
				}
			}
			if (this.boneHit == 1 && this.distanceCutty < this.minDistance[this.df])
			{
				this.faceDamage += 1;
				if (this.faceDamage > 300)
				{
					this.faceDamage = 300;
				}
				if ((float)this.faceDamage > this.faceBreach[this.df])
				{
					if (!this.faceDestroyed)
					{
						this.faceSwitch();
					}
					if ((float)this.faceDamage < this.faceBreach[this.df] + (float)(600 / this.faceDam[this.df]))
					{
						this.damHealth(this.faceDam[this.df]);
						this.buzzSkeleton(8);
						this.boneStrike = true;
					}
				}
			}
			if (this.boneHit == 4 && num2 < 0.2f && this.distanceCutty < this.minDistance[this.df])
			{
				this.assDamage += 1;
				if (this.assDamage > 300)
				{
					this.assDamage = 300;
				}
				if ((float)this.assDamage > this.assBreach[this.df])
				{
					if (!this.assDestroyed)
					{
						this.assSwitch();
					}
					if ((float)this.assDamage < this.assBreach[this.df] + (float)(610 / this.assDam[this.df]))
					{
						this.damHealth(this.assDam[this.df]);
						this.buzzSkeleton(8);
						this.boneStrike = true;
						this.seizureTimer = 40;
					}
				}
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000F05B7 File Offset: 0x000EE7B7
		public void fixModel()
		{
			this.pigModel = this.content.Load<Model>("boss\\bossBase");
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000F05D0 File Offset: 0x000EE7D0
		public void initTarget()
		{
			this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget1);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget2);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.pigSkin.Parameters["BloodTexture"].SetValue(this.screenTarget1);
			this.targetChoice = 1;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000F067C File Offset: 0x000EE87C
		public void addTargetBlood(int index, int xMin, int xMax, int yMin, int yMax)
		{
			this.bodyPaint.index.Add(index);
			this.bodyPaint.x.Add((float)this.rr.Next(xMin * 100, xMax * 100) / 100f);
			this.bodyPaint.z.Add((float)this.rr.Next(yMin * 100, yMax * 100) / 100f);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000F06F0 File Offset: 0x000EE8F0
		public void executeTargetBlood()
		{
			if (this.targetChoice == 2)
			{
				this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget1);
			}
			else
			{
				this.sc.GraphicsDevice.SetRenderTarget(this.screenTarget2);
			}
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.spriteBatch.Begin();
			if (this.targetChoice == 2)
			{
				this.spriteBatch.Draw(this.screenTarget2, Vector2.Zero, Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.screenTarget1, Vector2.Zero, Color.White);
			}
			for (int i = 0; i < this.bodyPaint.index.Count; i++)
			{
				Color color = Color.White * ((float)this.rr.Next(40, 100) / 100f);
				color.A = (byte)this.rr.Next(120, 255);
				this.spriteBatch.Draw(this.sc.wound, new Vector2(this.bodyPaint.x[i], this.bodyPaint.z[i]), new Rectangle?(this.sc.woundRect[this.bodyPaint.index[i]]), color, (float)this.rr.Next(-1800, 1800) / 100f, new Vector2(32f, 32f), (float)this.rr.Next(10, 50) / 100f, SpriteEffects.None, 0f);
			}
			this.spriteBatch.End();
			this.sc.GraphicsDevice.SetRenderTarget(null);
			if (this.targetChoice == 2)
			{
				this.pigSkin.Parameters["BloodTexture"].SetValue(this.screenTarget1);
				this.simple2.Parameters["BloodTexture"].SetValue(this.screenTarget1);
				this.targetChoice = 1;
			}
			else
			{
				this.pigSkin.Parameters["BloodTexture"].SetValue(this.screenTarget2);
				this.simple2.Parameters["BloodTexture"].SetValue(this.screenTarget2);
				this.targetChoice = 2;
			}
			this.bodyPaint.index.Clear();
			this.bodyPaint.x.Clear();
			this.bodyPaint.z.Clear();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000F0988 File Offset: 0x000EEB88
		public void addCanvasCrap(int index, float X, float Z)
		{
			float num = X - 3000f + 1085f;
			float num2 = Z - 3000f + 1085f;
			if (num > 0f && num < 2170f && num2 > 0f && num2 < 2170f)
			{
				this.addCanvasBlood(index, num * 0.92166f, num2 * 0.92166f);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000F09E8 File Offset: 0x000EEBE8
		private void initCanvas()
		{
			this.sc.GraphicsDevice.SetRenderTarget(this.canvasTarget1);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.sc.GraphicsDevice.SetRenderTarget(this.canvasTarget2);
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.sc.GraphicsDevice.SetRenderTarget(null);
			this.simple.Parameters["Texture"].SetValue(this.canvasTarget1);
			this.canvasChoice = 1;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000F0A94 File Offset: 0x000EEC94
		public void addCanvasBlood(int index, float x, float z)
		{
			this.canvasPaint.index.Add(index);
			this.canvasPaint.x.Add(x);
			this.canvasPaint.z.Add(z);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000F0ACC File Offset: 0x000EECCC
		public void executeCanvasBlood()
		{
			if (this.canvasChoice == 2)
			{
				this.sc.GraphicsDevice.SetRenderTarget(this.canvasTarget1);
			}
			else
			{
				this.sc.GraphicsDevice.SetRenderTarget(this.canvasTarget2);
			}
			this.sc.GraphicsDevice.Clear(Color.Transparent);
			this.spriteBatch.Begin();
			if (this.canvasChoice == 2)
			{
				this.spriteBatch.Draw(this.canvasTarget2, Vector2.Zero, Color.White);
			}
			else
			{
				this.spriteBatch.Draw(this.canvasTarget1, Vector2.Zero, Color.White);
			}
			for (int i = 0; i < this.canvasPaint.index.Count; i++)
			{
				float num = (float)this.rr.Next(10, 70) / 100f;
				Color color = Color.White * num;
				color.A = (byte)(255f * ((float)this.rr.Next((int)(num * 100f) + 10, 85) / 100f));
				this.spriteBatch.Draw(this.sc.wound, new Vector2(this.canvasPaint.x[i], this.canvasPaint.z[i]), new Rectangle?(this.sc.woundRect[this.canvasPaint.index[i]]), color, (float)this.rr.Next(-1800, 1800) / 100f, new Vector2(32f, 32f), (float)this.rr.Next(60, 210) / 100f, SpriteEffects.None, 0f);
			}
			this.spriteBatch.End();
			this.sc.GraphicsDevice.SetRenderTarget(null);
			if (this.canvasChoice == 2)
			{
				this.simple.Parameters["Texture"].SetValue(this.canvasTarget1);
				this.canvasChoice = 1;
			}
			else
			{
				this.simple.Parameters["Texture"].SetValue(this.canvasTarget2);
				this.canvasChoice = 2;
			}
			this.canvasPaint.index.Clear();
			this.canvasPaint.x.Clear();
			this.canvasPaint.z.Clear();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000F0D3C File Offset: 0x000EEF3C
		public void runDestructionChecks(int type)
		{
			if (type < 1)
			{
				return;
			}
			if ((float)this.spineDamage > this.spineBreach[this.df] && !this.spineDestroyed)
			{
				this.spineSwitch();
			}
			if ((float)this.assDamage > this.assBreach[this.df] && !this.assDestroyed)
			{
				this.assSwitch();
			}
			if ((float)this.faceDamage > this.faceBreach[this.df] && !this.faceDestroyed)
			{
				this.faceSwitch();
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000F0DBC File Offset: 0x000EEFBC
		public void buzzSkeleton(int amt)
		{
			this.showSkelTimer = 30;
			this.sc.buzz.Play(this.sc.ev, (float)this.rr.Next(-50, 20) / 100f, 0f);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000F0E08 File Offset: 0x000EF008
		private void spineSwitch()
		{
			if (!this.sc.bossLoaded || !this.pigModelLoaded)
			{
				return;
			}
			if (this.pigJawIndex == -1)
			{
				this.talkIndex = 0;
				Princess.whichPigTalks = this.myIndex;
			}
			if (this.df != 2 && this.df != 5 && !this.death1)
			{
				this.shootWoundMessage = true;
			}
			this.sc.cuttygouge.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, (float)this.rr.Next(-20, 30) / 100f);
			this.chunk.startDrop = true;
			this.spineDestroyed = true;
			if ((float)this.spineDamage < this.spineBreach[this.df])
			{
				this.spineDamage = (ushort)(this.spineBreach[this.df] + 5f);
			}
			if (this.bodyState == 0)
			{
				this.bodyState = 1;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 1;
				return;
			}
			if (this.bodyState == 2)
			{
				this.bodyState = 4;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 6;
				return;
			}
			if (this.bodyState == 3)
			{
				this.bodyState = 5;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 5;
				return;
			}
			if (this.bodyState == 6)
			{
				this.bodyState = 7;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 7;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000F0FA8 File Offset: 0x000EF1A8
		private void assSwitch()
		{
			if (!this.sc.bossLoaded || !this.pigModelLoaded)
			{
				return;
			}
			if (this.pigJawIndex == -1)
			{
				this.talkIndex = 0;
				Princess.whichPigTalks = this.myIndex;
			}
			if (this.df != 2 && this.df != 5 && !this.death1)
			{
				this.shootWoundMessage = true;
			}
			this.assChunk.startDrop = true;
			this.sc.cuttygouge.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, (float)this.rr.Next(-20, 30) / 100f);
			this.assDestroyed = true;
			if ((float)this.assDamage < this.assBreach[this.df])
			{
				this.assDamage = (ushort)(this.assBreach[this.df] + 5f);
			}
			if (this.bodyState == 0)
			{
				this.bodyState = 2;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 2;
				return;
			}
			if (this.bodyState == 1)
			{
				this.bodyState = 4;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 6;
				return;
			}
			if (this.bodyState == 3)
			{
				this.bodyState = 6;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 4;
				return;
			}
			if (this.bodyState == 5)
			{
				this.bodyState = 7;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 7;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000F1148 File Offset: 0x000EF348
		private void faceSwitch()
		{
			if (!this.sc.bossLoaded || !this.pigModelLoaded)
			{
				return;
			}
			if (this.pigJawIndex == -1)
			{
				this.talkIndex = 0;
				Princess.whichPigTalks = this.myIndex;
			}
			if (this.df != 2 && this.df != 5 && !this.death1)
			{
				this.shootWoundMessage = true;
			}
			this.faceChunk.startDrop = true;
			this.sc.cuttygouge.Play(this.sc.ev, (float)this.rr.Next(-20, 0) / 100f, (float)this.rr.Next(-20, 30) / 100f);
			this.faceDestroyed = true;
			if ((float)this.faceDamage < this.faceBreach[this.df])
			{
				this.faceDamage = (ushort)(this.faceBreach[this.df] + 5f);
			}
			if (this.bodyState == 0)
			{
				this.bodyState = 3;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 3;
				return;
			}
			if (this.bodyState == 2)
			{
				this.bodyState = 6;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 4;
				return;
			}
			if (this.bodyState == 1)
			{
				this.bodyState = 5;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 5;
				return;
			}
			if (this.bodyState == 4)
			{
				this.bodyState = 7;
				this.pigModel = this.sc.bossAll;
				this.sc.bossIndex = 7;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000F12E8 File Offset: 0x000EF4E8
		public void Update(GameTime gm, NetworkSession net, Vector3 campos, int timeframe, Vector3 lookVec, Vector3 playerPos, Vector3 remotePos, float playerHealth, float remoteHealth, bool isSpectating, ref float[,] heights)
		{
			if (!this.pigModelLoaded)
			{
				return;
			}
			this.networkSession = net;
			this.playerpos = playerPos;
			this.remotepos = remotePos;
			this.timeframe = timeframe;
			this.spectator = isSpectating;
			this.df = this.sc.df;
			if (!this.spectator)
			{
				this.df += 3;
			}
			if (this.isTrialMode)
			{
				this.df = 6;
			}
			this.playerHealth = playerHealth;
			this.remoteHealth = remoteHealth;
			this.healthPerc = (float)this.health / (float)this.startHealth;
			if (this.sc.host && !this.deathsent)
			{
				this.pigLogic();
			}
			this.pigBones(lookVec, ref heights);
			if (this.chunk.startDrop)
			{
				this.updateChunk(ref this.chunk, ref heights);
			}
			if (this.faceChunk.startDrop)
			{
				this.updateChunk(ref this.faceChunk, ref heights);
			}
			if (this.assChunk.startDrop)
			{
				this.updateChunk(ref this.assChunk, ref heights);
			}
			if (this.puke1.max > 1)
			{
				if (this.cuttyShit)
				{
					this.updateShit(ref this.puke1, ref heights);
				}
				else if (this.death1)
				{
					this.updatePuke2(ref this.puke1, ref heights);
				}
				else
				{
					this.updatePuke(ref this.puke1, ref heights);
				}
			}
			this.runScheduler(ref heights);
			if (this.shockTimer > 0f)
			{
				this.shockWave(ref heights);
			}
			this.rocks.Update(gm);
			this.dots.Update(gm);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000F1478 File Offset: 0x000EF678
		private void dropChunk(ref Princess.shell sh, Matrix mm, int bone)
		{
			sh.offset[sh.index] = mm;
			sh.bone[sh.index] = bone;
			sh.dupe[sh.index].init((float)this.rr.Next(20, 60) / 100f, 2f, 1f, mm, new Vector3(2f, 1f, 0f), -(float)this.rr.Next(80, 160) / 1000f, 80, 80f, 220f);
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.stream[sh.index].color = Vector3.One;
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

		// Token: 0x06000432 RID: 1074 RVA: 0x000F15AC File Offset: 0x000EF7AC
		private void updateChunk(ref Princess.shell sh, ref float[,] heights)
		{
			sh.tempindex = 0;
			bool flag = true;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move == 1 || sh.dupe[i].move == 2)
				{
					sh.dupe[i].Update(ref heights);
				}
				if (sh.dupe[i].move == 0)
				{
					sh.dupe[i].transform = sh.offset[i] * this.princessBone[sh.bone[i]];
				}
				if (sh.startDrop)
				{
					sh.dropTimer += 0.015f;
					if ((float)i < sh.dropTimer && sh.dupe[i].move == 0)
					{
						sh.dupe[i].move = 1;
						sh.dupe[i].createState(sh.dupe[i].transform);
						Vector3 vector = (this.cuttyPos - this.oldcuttyPos) * (float)this.rr.Next(75, 100) / 100f;
						float num = MathHelper.Lerp(3f, 0.5f, (float)i / (float)sh.maxCapacity);
						sh.dupe[i].velocity = vector + (sh.dupe[i].transform.Up + new Vector3(0f, 0.6f, 0f)) * num;
						sh.dupe[i].Update(ref heights);
						this.cuttyChunkRelease = true;
						this.hitCenter2 = sh.dupe[i].mypos;
						this.hitEdge2 = this.hitCenter2 + sh.dupe[i].transform.Up;
					}
					if (sh.dupe[i].move != 3)
					{
						flag = false;
					}
				}
				sh.stream[i].Trans = sh.dupe[i].transform;
				sh.displayList[sh.tempindex] = sh.stream[i];
				sh.tempindex++;
			}
			if (sh.startDrop && flag)
			{
				sh.startDrop = false;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000F1808 File Offset: 0x000EFA08
		private void dropPuke(ref Princess.vomit sh, int thick, int width, float speed, Matrix bonelock)
		{
			Vector3 vector = new Vector3((float)this.rr.Next(-width, width) / 100f, (float)this.rr.Next(thick, -20) / 100f, 0f);
			vector.Z += (float)this.rr.Next(0, 120) / 10f;
			Matrix.CreateTranslation(vector.X, vector.Y, vector.Z, out this.m1);
			Matrix.Multiply(ref this.m1, ref bonelock, out this.m5);
			sh.dupe[sh.index].init((float)this.rr.Next(20, 48) / 100f, this.m5, Vector3.Zero, -0.42f, this.rr.Next(20, 120), 80f, 220f);
			Vector3 vector2 = new Vector3((float)this.rr.Next(-100, 100) / 1200f, (float)this.rr.Next(-100, 100) / 1200f, (float)this.rr.Next(-100, 100) / 1200f);
			sh.dupe[sh.index].velocity = (sh.dupe[sh.index].transform.Forward + vector2) * speed;
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.dupe[sh.index].mycolor = new Vector3(122f, 116f, 24f) / 275f;
			if (vector.X >= 0.2f)
			{
				sh.dupe[sh.index].mycolor = new Vector3(142f, 146f, 47f) / 425f;
			}
			sh.stream[sh.index].color = sh.dupe[sh.index].mycolor;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000F1A7C File Offset: 0x000EFC7C
		private void updatePuke(ref Princess.vomit sh, ref float[,] heights)
		{
			sh.tempindex = 0;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move == 1 || sh.dupe[i].move == 2)
				{
					sh.dupe[i].Update(ref heights, this.vomitveloc);
					sh.stream[i].Trans = sh.dupe[i].transform;
					if (sh.dupe[i].age >= 18f && sh.dupe[i].age <= 26f)
					{
						sh.dupe[i].mycolor = sh.dupe[i].mycolor * 0.95f;
						sh.stream[i].color = sh.dupe[i].mycolor;
					}
					else if (sh.dupe[i].age >= 0f && sh.dupe[i].age <= 4f)
					{
						sh.stream[i].color = new Vector3(122f, 116f, 34f) / 255f * (0.1f + sh.dupe[i].age * 0.22f);
					}
					else if (sh.dupe[i].age == 8f)
					{
						sh.stream[i].color = sh.dupe[i].mycolor * 1.7f;
					}
					else
					{
						sh.stream[i].color = sh.dupe[i].mycolor;
					}
					sh.displayList[sh.tempindex] = sh.stream[i];
					sh.tempindex++;
				}
			}
			if (sh.tempindex < 4)
			{
				sh.index = 0;
				sh.max = 0;
				sh.tempindex = 0;
			}
			if (sh.max > 50)
			{
				for (int j = 10; j < sh.max; j += 60)
				{
					if (!this.cuttyVomitHit && sh.dupe[j].move == 1 && Vector3.DistanceSquared(sh.dupe[j].mypos, new Vector3(this.playerpos.X, this.playerpos.Y + 50f, this.playerpos.Z)) < 8100f)
					{
						this.cuttyVomitHit = true;
						this.splats.Play(this.sc.ev, (float)this.rr.Next(-50, 20) / 100f, (float)this.rr.Next(-90, 90) / 100f);
					}
					if (!this.cuttyVomitHit2 && sh.dupe[j].move == 1 && !this.spectator && Vector3.DistanceSquared(sh.dupe[j].mypos, new Vector3(this.remotepos.X, this.remotepos.Y + 50f, this.remotepos.Z)) < 8100f)
					{
						this.cuttyVomitHit2 = true;
					}
					if (sh.dupe[j].splat)
					{
						sh.dupe[j].splat = false;
						this.addCanvasCrap(this.rr.Next(1, 4), sh.dupe[j].mypos.X, sh.dupe[j].mypos.Z);
					}
				}
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000F1E24 File Offset: 0x000F0024
		private void dropPuke2(ref Princess.vomit sh, int thick, int width, float speed, Matrix bonelock)
		{
			Vector3 vector = new Vector3((float)this.rr.Next(-width, width) / 100f, (float)this.rr.Next(thick, -20) / 100f, 0f);
			vector.Z += (float)this.rr.Next(0, 120) / 10f;
			Matrix.CreateTranslation(vector.X, vector.Y, vector.Z, out this.m1);
			Matrix.Multiply(ref this.m1, ref bonelock, out this.m5);
			sh.dupe[sh.index].init((float)this.rr.Next(20, 48) / 100f, this.m5, Vector3.Zero, -0.42f, this.rr.Next(20, 120), 80f, 220f);
			Vector3 vector2 = new Vector3((float)this.rr.Next(-100, 100) / 1200f, (float)this.rr.Next(-100, 100) / 1200f, (float)this.rr.Next(-100, 100) / 1200f);
			sh.dupe[sh.index].velocity = (sh.dupe[sh.index].transform.Forward + vector2) * speed;
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.dupe[sh.index].mycolor = new Vector3(190f, 30f, 30f) / 275f;
			sh.stream[sh.index].color = sh.dupe[sh.index].mycolor;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000F205C File Offset: 0x000F025C
		private void updatePuke2(ref Princess.vomit sh, ref float[,] heights)
		{
			sh.tempindex = 0;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move == 1 || sh.dupe[i].move == 2)
				{
					sh.dupe[i].Update(ref heights, this.vomitveloc);
					sh.stream[i].Trans = sh.dupe[i].transform;
					if (sh.dupe[i].age >= 16f)
					{
						sh.dupe[i].mycolor = sh.dupe[i].mycolor * 0.95f;
						sh.stream[i].color = sh.dupe[i].mycolor;
					}
					else if (sh.dupe[i].age >= 0f && sh.dupe[i].age <= 4f)
					{
						sh.stream[i].color = new Vector3(140f, 10f, 10f) / 255f * (0.1f + sh.dupe[i].age * 0.22f);
					}
					else
					{
						sh.stream[i].color = sh.dupe[i].mycolor;
					}
					sh.displayList[sh.tempindex] = sh.stream[i];
					sh.tempindex++;
				}
			}
			if (sh.tempindex < 4)
			{
				sh.index = 0;
				sh.max = 0;
				sh.tempindex = 0;
			}
			if (sh.max > 50)
			{
				for (int j = 10; j < sh.max; j += 60)
				{
					if (sh.dupe[j].splat)
					{
						sh.dupe[j].splat = false;
						this.addCanvasCrap(8, sh.dupe[j].mypos.X, sh.dupe[j].mypos.Z);
					}
				}
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000F2284 File Offset: 0x000F0484
		private void dropShit(ref Princess.vomit sh, float speed, Matrix bonelock, float myTimer)
		{
			Vector3 vector = new Vector3((float)this.rr.Next(-500, 500) / 100f, (float)this.rr.Next(-400, 400) / 100f, 0f);
			vector.Z += (float)this.rr.Next(-50, 10) / 10f;
			vector *= myTimer;
			Matrix.CreateTranslation(vector.X, vector.Y, vector.Z, out this.m1);
			Matrix.Multiply(ref this.m1, ref bonelock, out this.m5);
			sh.dupe[sh.index].init((float)this.rr.Next(20, 48) / 100f, this.m5, Vector3.Zero, -0.42f, this.rr.Next(20, 120), 80f, 220f);
			Vector3 vector2 = new Vector3((float)this.rr.Next(-100, 100) / 900f, (float)this.rr.Next(-50, 150) / 900f, (float)this.rr.Next(-100, 100) / 900f);
			vector2.Y += Math.Abs((float)Math.Sin((double)(this.sc.myTimer / 12f))) * 0.3f;
			sh.dupe[sh.index].velocity = (sh.dupe[sh.index].transform.Forward + vector2) * speed;
			sh.stream[sh.index].Trans = sh.dupe[sh.index].transform;
			sh.dupe[sh.index].mycolor = Vector3.Lerp(new Vector3(37f, 18f, 0f) / 255f, new Vector3(66f, 37f, 0f) / 255f, Math.Abs((float)Math.Sin((double)(this.sc.myTimer / 14f))));
			sh.stream[sh.index].color = sh.dupe[sh.index].mycolor;
			sh.index++;
			if (sh.index > sh.maxCapacity - 1)
			{
				sh.index = 0;
			}
			sh.max++;
			if (sh.max > sh.maxCapacity)
			{
				sh.max = sh.maxCapacity;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000F2548 File Offset: 0x000F0748
		private void updateShit(ref Princess.vomit sh, ref float[,] heights)
		{
			sh.tempindex = 0;
			for (int i = 0; i < sh.max; i++)
			{
				if (sh.dupe[i].move == 1 || sh.dupe[i].move == 2)
				{
					sh.dupe[i].Update(ref heights, this.vomitveloc);
					sh.stream[i].Trans = sh.dupe[i].transform;
					if (sh.dupe[i].age >= 18f)
					{
						sh.dupe[i].mycolor = sh.dupe[i].mycolor * 0.95f;
						sh.stream[i].color = sh.dupe[i].mycolor;
					}
					else if (sh.dupe[i].age >= 0f && sh.dupe[i].age <= 4f)
					{
						sh.stream[i].color = sh.dupe[i].mycolor * (0.1f + sh.dupe[i].age * 0.22f);
					}
					else if (sh.dupe[i].age == 7f)
					{
						sh.stream[i].color = sh.dupe[i].mycolor * 1.4f;
					}
					else
					{
						sh.stream[i].color = sh.dupe[i].mycolor;
					}
					sh.displayList[sh.tempindex] = sh.stream[i];
					sh.tempindex++;
				}
			}
			if (sh.tempindex < 4)
			{
				sh.index = 0;
				sh.max = 0;
				sh.tempindex = 0;
			}
			if (sh.max > 50)
			{
				for (int j = 10; j < sh.max; j += 60)
				{
					if (!this.cuttyShitHit && sh.dupe[j].move == 1 && Vector3.DistanceSquared(sh.dupe[j].mypos, new Vector3(this.playerpos.X, this.playerpos.Y + 60f, this.playerpos.Z)) < 14400f)
					{
						this.cuttyShitHit = true;
						this.splats.Play(this.sc.ev, (float)this.rr.Next(-99, -30) / 100f, (float)this.rr.Next(-90, 90) / 100f);
					}
					if (!this.cuttyShitHit2 && sh.dupe[j].move == 1 && !this.spectator && Vector3.DistanceSquared(sh.dupe[j].mypos, new Vector3(this.remotepos.X, this.remotepos.Y + 60f, this.remotepos.Z)) < 14400f)
					{
						this.cuttyShitHit2 = true;
					}
					if (sh.dupe[j].splat)
					{
						sh.dupe[j].splat = false;
						this.addCanvasCrap(this.rr.Next(9, 13), sh.dupe[j].mypos.X, sh.dupe[j].mypos.Z);
					}
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000F28CC File Offset: 0x000F0ACC
		public void makePigTalk(int line)
		{
			if (this.death1)
			{
				return;
			}
			this.cuttyStruct.homing = 0;
			this.cuttyStruct.rot = (float)this.rr.Next(90, 130) / 100f * 8f;
			this.cuttyStruct.animType = 0;
			this.cuttyStruct.talkindex = line;
			this.scheduleAction(146);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000F293C File Offset: 0x000F0B3C
		private void volumeBurps()
		{
			if (!this.burps.sound[0].IsDisposed)
			{
				this.burps.sound[0].Volume = this.sc.ev - MathHelper.Clamp(this.distanceCutty / 2250000f, 0f, this.sc.ev);
				if (this.explodeTimer < 5)
				{
					this.burps.sound[0].Volume = 0f;
				}
			}
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000F29BC File Offset: 0x000F0BBC
		private void pigVolume()
		{
			if (!this.pigDialog1.sound[0].IsDisposed)
			{
				this.pigDialog1.sound[0].Volume = this.sc.vv;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000F29F0 File Offset: 0x000F0BF0
		private void pigLogic()
		{
			if (!this.cuttyDoneLocalSpeech)
			{
				if (Princess.allplayersReady && !Princess.someoneTalking)
				{
					if (Princess.speeches < this.speechList.Count && this.speechInclude.Contains(Princess.speeches))
					{
						Princess.someoneTalking = true;
						this.cuttyStruct.talkindex = this.speechList[Princess.speeches];
						this.scheduleAction(146);
						Princess.speeches++;
						return;
					}
					if (Princess.speeches >= this.speechList.Count)
					{
						this.cuttyStruct.talkindex = -1;
						this.scheduleAction(146);
						this.cuttyDoneLocalSpeech = true;
						Princess.cuttyDoneSpeech = true;
					}
				}
				return;
			}
			if (!this.newAction && this.health <= 0 && this.heartHit >= 5 && !this.isTrialMode)
			{
				this.deathsent = true;
				this.cuttyStruct.dur = 40;
				this.cuttyStruct.rot = this.cuttyRot;
				this.scheduleAction(142);
				this.newAction = true;
				return;
			}
			this.nextAttack--;
			if (!this.isTrialMode && !this.cuttyShit && !this.newAction && (this.healthPerc < 0.3f || (this.healthPerc > 0.4f && this.healthPerc < 0.7f && this.rr.Next(1, 1000) < 30)) && this.nextAttack < 0)
			{
				this.cuttyStruct.dur = (ushort)this.rr.Next(500, 1400);
				if (this.heartExposed && this.rr.Next(1, 1000) < this.jumpOdds[this.df])
				{
					this.cuttyStruct.dur = 2500;
				}
				if (this.rr.Next(1, 1000) < 100)
				{
					this.cuttyStruct.dur = 2500;
				}
				this.scheduleAction(141);
				this.newAction = true;
				return;
			}
			this.buckDelay -= 1f;
			bool flag = (this.healthPerc >= 0.8f && this.rr.Next(1, 1000) < 60) || (this.healthPerc > 0.3f && this.healthPerc < 0.8f && this.rr.Next(1, 1000) < 20);
			if (!this.newAction && this.buckDelay <= 0f && this.healthPerc > 0.3f && flag)
			{
				this.cuttyStruct.dur = 0;
				this.cuttyStruct.rot = this.cuttyRot;
				this.scheduleAction(144);
				this.newAction = true;
				return;
			}
			this.shitDelay -= 1f;
			if (this.gonnaShit || (!this.gonnaVomit && this.homingTimer <= 0 && this.shitDelay <= 0f && !this.cuttyShit && !this.attack1))
			{
				this.gonnaShit = false;
				this.cuttyStruct.dur = (ushort)this.rr.Next(180, 320);
				this.cuttyStruct.rot = this.cuttyRot;
				this.scheduleAction(145);
				this.shitDelay = 5000f;
				return;
			}
			this.gonnaRollDelay -= 1f;
			if (!this.newAction && this.gonnaRollDelay <= 0f && this.healthPerc < 0.8f)
			{
				this.cuttyStruct.dur = (ushort)this.rr.Next(3, 9);
				this.cuttyStruct.rot = this.cuttyRot;
				this.scheduleAction(143);
				this.newAction = true;
				return;
			}
			this.turningWait--;
			this.damcheck = this.damTrigger[this.df];
			if (this.heartExposed)
			{
				this.damcheck = 2;
			}
			if ((this.turningWait <= 0 || this.hurtBoss > this.damcheck) && !this.newAction && !this.cuttyShit)
			{
				int num = 0;
				float num2 = 1440000f;
				bool flag2 = false;
				bool flag3 = false;
				int num3 = 300;
				if (this.heartExposed)
				{
					num3 = 500;
				}
				if (this.hurtBoss > this.damcheck)
				{
					flag2 = true;
					if (this.rr.Next(1, 1000) < num3 && !this.isTrialMode)
					{
						flag2 = false;
						flag3 = true;
					}
				}
				else
				{
					int num4 = 450;
					int num5 = 600;
					if (this.heartExposed)
					{
						num4 = 500;
						num5 = 850;
					}
					if (!this.isTrialMode)
					{
						num4 = 700;
						num5 = 850;
					}
					int num6 = this.rr.Next(1, 1000);
					if (num6 < num4)
					{
						flag2 = true;
					}
					if (num6 >= num4 && num6 < num5)
					{
						flag3 = true;
					}
				}
				this.hurtBoss = 0;
				if (this.spectator)
				{
					if (this.distanceCutty < num2)
					{
						num = 1;
					}
					else if (this.rr.Next(1, 1000) < 300)
					{
						num = 1;
					}
				}
				if (!this.spectator)
				{
					if (this.playerHealth > 0f && this.remoteHealth > 0f)
					{
						if (this.distanceCutty < num2 && this.distanceCutty2 < num2)
						{
							num = this.rr.Next(1, 3);
						}
						else if (this.distanceCutty < num2)
						{
							num = 1;
							if (this.rr.Next(1, 1000) < 200)
							{
								num = 2;
							}
						}
						else if (this.distanceCutty2 < num2)
						{
							num = 2;
							if (this.rr.Next(1, 1000) < 200)
							{
								num = 1;
							}
						}
						else if (this.distanceCutty >= num2 && this.distanceCutty2 >= num2)
						{
							num = this.rr.Next(1, 3);
						}
					}
					else if (this.playerHealth > 0f && this.remoteHealth <= 0f)
					{
						num = 1;
						if (this.distanceCutty >= num2)
						{
							num = this.rr.Next(1, 3);
						}
					}
					else if (this.playerHealth <= 0f && this.remoteHealth > 0f)
					{
						num = 2;
						if (this.distanceCutty2 >= num2)
						{
							num = this.rr.Next(1, 3);
						}
					}
					else if (this.playerHealth <= 0f && this.remoteHealth <= 0f)
					{
						num = this.rr.Next(1, 3);
					}
				}
				if (num > 0)
				{
					if (flag2)
					{
						this.homing = 0;
						this.gonnaVomit = true;
						float num7 = 0f;
						if (num == 1)
						{
							num7 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.playerpos.Z), (double)(this.cuttyPos.X - this.playerpos.X)) - 1.57f;
						}
						if (num == 2)
						{
							num7 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.remotepos.Z), (double)(this.cuttyPos.X - this.remotepos.X)) - 1.57f;
						}
						float num8 = Math.Abs(Princess.WrapAngle(num7) - Princess.WrapAngle(this.cuttyRot));
						this.cuttyStruct.animType = 1;
						float num9 = 0.5f;
						if (Princess.WrapAngle(num7 - this.cuttyRot) < 0f)
						{
							this.cuttyStruct.animType = 2;
							num9 = -0.5f;
						}
						this.cuttyStruct.homing = num;
						this.cuttyStruct.rot = num7;
						if (num8 < 2.5f)
						{
							this.cuttyStruct.rot = num7 + num9;
						}
						this.cuttyStruct.talkindex = -1;
						this.scheduleAction(140);
						this.newAction = true;
						return;
					}
					if (flag3)
					{
						this.homing = 0;
						float num10 = 0f;
						bool flag4 = false;
						if (num == 1)
						{
							num10 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.playerpos.Z), (double)(this.cuttyPos.X - this.playerpos.X)) - 4.712f;
							if (this.playerHealth < 100f)
							{
								flag4 = true;
							}
						}
						if (num == 2)
						{
							num10 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.remotepos.Z), (double)(this.cuttyPos.X - this.remotepos.X)) - 4.712f;
							if (this.remoteHealth < 100f)
							{
								flag4 = true;
							}
						}
						float num11 = Math.Abs(Princess.WrapAngle(num10) - Princess.WrapAngle(this.cuttyRot));
						float num12 = 0.3f;
						this.cuttyStruct.animType = 1;
						if (Princess.WrapAngle(num10 - this.cuttyRot) < 0f)
						{
							num12 = -0.3f;
							this.cuttyStruct.animType = 2;
						}
						this.cuttyStruct.homing = 5;
						this.cuttyStruct.rot = num10;
						if (num11 < 2.5f && !flag4)
						{
							this.cuttyStruct.rot = num10 + num12;
						}
						this.cuttyStruct.talkindex = -1;
						this.scheduleAction(140);
						this.newAction = true;
						return;
					}
					this.homing = 0;
					float num13 = 0f;
					if (num == 1)
					{
						num13 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.playerpos.Z), (double)(this.cuttyPos.X - this.playerpos.X)) - 1.57f;
					}
					if (num == 2)
					{
						num13 = -(float)Math.Atan2((double)(this.cuttyPos.Z - this.remotepos.Z), (double)(this.cuttyPos.X - this.remotepos.X)) - 1.57f;
					}
					float num14 = Math.Abs(Princess.WrapAngle(num13) - Princess.WrapAngle(this.cuttyRot));
					float num15 = 0.5f;
					this.cuttyStruct.animType = 1;
					if (Princess.WrapAngle(num13 - this.cuttyRot) < 0f)
					{
						num15 = -0.5f;
						this.cuttyStruct.animType = 2;
					}
					this.cuttyStruct.homing = 0;
					this.cuttyStruct.rot = num13;
					if (num14 < 2.5f)
					{
						this.cuttyStruct.rot = num13 + num15;
					}
					this.cuttyStruct.talkindex = -1;
					this.scheduleAction(140);
					this.newAction = true;
					return;
				}
				else
				{
					this.turningWait = this.rr.Next(300, 700);
					if (this.heartExposed)
					{
						this.turningWait = this.rr.Next(200, 360);
					}
				}
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x000F34CC File Offset: 0x000F16CC
		private void pigBones(Vector3 lookVec, ref float[,] heights)
		{
			this.talkSmooth = 0f;
			this.lookingatPig = false;
			this.distanceCutty = Vector2.DistanceSquared(new Vector2(this.playerpos.X, this.playerpos.Z), new Vector2(this.cuttyPos.X, this.cuttyPos.Z));
			this.distanceCutty2 = 100000f;
			if (!this.spectator)
			{
				this.distanceCutty2 = Vector2.DistanceSquared(new Vector2(this.remotepos.X, this.remotepos.Z), new Vector2(this.cuttyPos.X, this.cuttyPos.Z));
			}
			if (this.attack1)
			{
				this.pigAttack(ref heights);
			}
			else if (this.cuttyRoll)
			{
				this.pigRoll();
			}
			else if (this.death1)
			{
				this.pigDying();
			}
			else if (this.cuttyBuck)
			{
				this.pigBuck();
			}
			else
			{
				this.pigWaiting();
			}
			if (this.myClip != this.clipIndexA)
			{
				this.tween = 0f;
				this.clipIndexB = this.clipIndexA;
				this.clipIndexA = this.myClip;
			}
			this.a = this.pig1[this.clipIndexA];
			this.b = this.pig1[this.clipIndexB];
			if (this.explodeTimer > 0)
			{
				this.volumeBurps();
			}
			this.isSpeaking = false;
			if (this.pigJawIndex >= 0)
			{
				this.isSpeaking = true;
				this.talkSmooth = this.pigJaw[this.pigJawIndex] * 1f;
				this.pigJawIndex++;
				if (this.talkSmooth > 0f && !this.attack1 && (this.homingTimer > 0 || this.death1) && !this.cuttyShit)
				{
					float num = (this.talkSmooth - 10f) / 30f;
					num = MathHelper.Clamp(num, 0f, 1f);
					int num2 = (int)MathHelper.Lerp(50f, 350f, num);
					int num3 = (int)MathHelper.Lerp(-100f, -60f, num);
					float num4 = 9f;
					int num5 = (int)MathHelper.Lerp(0f, 17f, num);
					Matrix.CreateFromYawPitchRoll(-3.0789733f, -0.5f, 0.009578228f, out this.m1);
					Matrix.CreateTranslation(-1.5515343f, 67.19339f, 113.69844f, out this.m2);
					Matrix.Multiply(ref this.m1, ref this.m2, out this.m3);
					Matrix.Multiply(ref this.m3, ref this.princessBone[7], out this.m4);
					Vector3 zero = Vector3.Zero;
					Vector3.Transform(ref zero, ref this.m4, out this.vomitpos);
					if (this.oldvomitpos == Vector3.Zero)
					{
						this.oldvomitpos = this.vomitpos;
					}
					this.vomitveloc = this.vomitpos - this.oldvomitpos;
					this.oldvomitpos = this.vomitpos;
					for (int i = 0; i < num5; i++)
					{
						if (this.death1)
						{
							num4 = 5f;
							this.dropPuke2(ref this.puke1, num3, num2, num4, this.m4);
						}
						else
						{
							this.dropPuke(ref this.puke1, num3, num2, num4, this.m4);
						}
					}
				}
				else
				{
					this.oldvomitpos = Vector3.Zero;
					this.vomitveloc = Vector3.Zero;
				}
				if (this.pigJaw[this.pigJawIndex] == -1f)
				{
					this.pigJawIndex = -1;
					Princess.someoneTalking = false;
					this.pigLine = -1;
					this.talkSmooth = 0f;
					this.oldvomitpos = Vector3.Zero;
					this.vomitveloc = Vector3.Zero;
					if (this.homingTimer > 0)
					{
						this.homingTimer = 2;
					}
				}
			}
			double num6 = (double)(this.pigFrame1 * 0.0417f);
			this.currentTimeValue = TimeSpan.FromSeconds(num6 % this.a.currentClipValue.Duration.TotalSeconds);
			this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 22;
			this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
			this.a.UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
			if (this.tween < 1f)
			{
				double num7 = (double)(this.bFrame1 * 0.0417f);
				this.tween += 0.05f;
				this.currentTimeValue = TimeSpan.FromSeconds(num7 % this.b.currentClipValue.Duration.TotalSeconds);
				this.currentKeyframe = (int)(this.currentTimeValue.TotalSeconds * 60.0) * 22;
				this.currentTimeValue += TimeSpan.FromSeconds(0.041999999433755875);
				this.b.UpdateBoneTransforms2(this.currentKeyframe, this.currentTimeValue);
				for (int j = 0; j < this.b.boneTransforms.Length; j++)
				{
					this.a.boneTransforms[j] = Matrix.Lerp(this.b.boneTransforms[j], this.a.boneTransforms[j], this.tween);
				}
			}
			float num8 = 0f;
			if (this.homingTimer > 0)
			{
				float num9 = this.cuttyRot;
				if (this.homingTimer > this.homingStart - 50)
				{
					this.bonePos = Vector3.Transform(new Vector3(0f, 86f, 17f), this.princessBone[4]);
				}
				else
				{
					this.bonePos = Vector3.Transform(new Vector3(0f, 77f, 106f), this.princessBone[7]);
				}
				float num10;
				float num11;
				if ((this.homing == 1 && this.sc.host) || (this.homing == 2 && !this.sc.host))
				{
					num10 = this.bonePos.Z - this.playerpos.Z;
					num11 = this.bonePos.X - this.playerpos.X;
				}
				else
				{
					num10 = this.bonePos.Z - this.remotepos.Z;
					num11 = this.bonePos.X - this.remotepos.X;
				}
				num8 = -(float)Math.Atan2((double)num10, (double)num11) - (1.57f + num9);
			}
			if (this.homingTimer > 0 || Math.Abs(this.tiltOffset) > 0.01f || Math.Abs(this.piglook - num8) > 0.01f)
			{
				this.hurtBoss = 0;
				float num12 = Princess.WrapAngle(num8 - this.piglook);
				if (num8 == 0f)
				{
					num12 = MathHelper.Clamp(num12, -0.06f, 0.06f);
				}
				else
				{
					num12 = MathHelper.Clamp(num12, -0.06f, 0.06f);
				}
				this.piglook = Princess.WrapAngle(this.piglook + num12);
				float num13 = this.angleView[this.df];
				if (this.healthPerc > 0.4f)
				{
					num13 = this.angleView[this.df] * 0.8f;
				}
				if (this.healthPerc > 0.8f)
				{
					num13 = this.angleView[this.df] * 0.6f;
				}
				this.piglook = MathHelper.Clamp(this.piglook, -num13, num13);
				if (this.homingTimer > 0)
				{
					this.tiltOffset = -MathHelper.Clamp((Vector2.Distance(new Vector2(this.playerpos.X, this.playerpos.Z), new Vector2(this.bonePos.X, this.bonePos.Z)) - 10f) / 1100f, 0f, 0.6f);
					this.tiltOffset = (float)Math.Sin((double)(this.sc.myTimer / 16f)) * 0.15f + 0.1f + this.tiltOffset;
					this.tiltOffset *= MathHelper.Clamp((float)(this.homingStart - this.homingTimer) / 70f, 0f, 1f);
				}
				else
				{
					this.tiltOffset *= 0.97f;
				}
				this.a.boneTransforms[7] = Matrix.CreateRotationZ(this.tiltOffset + Math.Abs(this.piglook) * 0.2f * 0.7f) * Matrix.CreateRotationY(-this.piglook * 0.3f * 0.7f) * Matrix.CreateRotationX(this.piglook * 0.7f) * this.a.boneTransforms[7];
				this.a.boneTransforms[5] = Matrix.CreateRotationZ(0f + Math.Abs(this.piglook) * 0.45f * 0.3f) * Matrix.CreateRotationX(-this.piglook * 0.6f * 0.3f) * Matrix.CreateRotationY(-this.piglook * 0.3f) * this.a.boneTransforms[5];
			}
			this.myTimer1 = (float)Math.Sin((double)(this.sc.myTimer / 32f)) / 2f + 0.5f;
			this.assScale = Vector3.Hermite(new Vector3(1f, 1f, 1f), Vector3.Zero, new Vector3(0.3f, 0f, 0f), Vector3.Zero, this.myTimer1);
			if (this.cuttyShit)
			{
				this.assRamp += 0.02f;
			}
			else
			{
				this.assRamp -= 0.02f;
			}
			this.assRamp = MathHelper.Clamp(this.assRamp, 0f, 1f);
			if (this.assRamp > 0f)
			{
				this.myTimer2 = (float)Math.Sin((double)(this.sc.myTimer / 16f)) / 2f + 0.5f;
				this.assScale2 = Vector3.Hermite(new Vector3(0.2f, -1f, -1f), Vector3.Zero, new Vector3(0f, -4f, -4f), Vector3.Zero, this.myTimer2);
				this.assScale = Vector3.Lerp(this.assScale, this.assScale2, this.assRamp);
			}
			if (this.cuttyShit)
			{
				this.shitTimer -= 1f;
				if (this.shitTimer < 60f)
				{
					this.shitVol = false;
				}
				if (this.shitTimer <= 0f)
				{
					this.cuttyShit = false;
					this.shitTimer = 0f;
					this.shitDelay = (float)this.rr.Next(950, 1880);
					if (this.heartExposed)
					{
						this.shitDelay = (float)this.rr.Next(1250, 2450);
					}
				}
				if (this.shitTimer > 60f)
				{
					Matrix.CreateTranslation(-1f, 64f, -82f, out this.m2);
					Matrix.Multiply(ref this.m2, ref this.princessBone[2], out this.m4);
					Vector3 zero2 = Vector3.Zero;
					Vector3.Transform(ref zero2, ref this.m4, out this.vomitpos);
					if (this.oldvomitpos == Vector3.Zero)
					{
						this.oldvomitpos = this.vomitpos;
					}
					this.vomitveloc = this.vomitpos - this.oldvomitpos;
					this.oldvomitpos = this.vomitpos;
					for (int k = 0; k < 15; k++)
					{
						this.dropShit(ref this.puke1, 5.2f + this.myTimer2, this.m4, this.myTimer2);
					}
				}
			}
			if (!this.jumpVol && !this.rollVol && !this.vomitVol && !this.shitVol && !this.heartVol)
			{
				this.volumeFade = false;
			}
			this.a.boneTransforms[21] = Matrix.CreateScale(this.assScale) * this.a.boneTransforms[21];
			this.heartMatrix = this.a.boneTransforms[17];
			if (this.explodeTimer > 0)
			{
				float num14 = 19f;
				float num15 = 1.4f;
				if (this.death1 || this.heartattack > 0f)
				{
					num14 = 8f;
				}
				float num16 = MathHelper.Lerp(0.25f, num15, (float)Math.Sin((double)(this.sc.myTimer / num14)) / 2f + 0.5f);
				this.a.boneTransforms[17] = Matrix.CreateScale(num16) * this.a.boneTransforms[17];
			}
			if (this.isSpeaking)
			{
				float num17 = 0f;
				this.a.boneTransforms[9] = Matrix.CreateRotationZ(MathHelper.ToRadians(num17 - this.talkSmooth)) * this.a.boneTransforms[9];
				Matrix matrix = Matrix.CreateRotationX(MathHelper.ToRadians(0f - this.talkSmooth / 8f * (float)Math.Sin((double)(this.sc.myTimer / 14f))));
				this.a.boneTransforms[6] = matrix * this.a.boneTransforms[6];
			}
			if (!this.cuttyisDead)
			{
				this.cuttyVeloc = this.cuttyPos - this.oldcuttyPos;
				this.oldcuttyPos = this.cuttyPos;
				for (int l = 0; l < this.col_Bone.Length - 1; l++)
				{
					Vector3 vector = Vector3.Transform(this.col_Pos[l], this.princessBone[this.col_Bone[l]]);
					float num18 = this.col_Scale[l] * this.cuttyScale * this.hitScale[this.df];
					float num19 = Vector3.Distance(new Vector3(this.playerpos.X, this.playerpos.Y + 90f, this.playerpos.Z), vector);
					float num20 = Vector3.Distance(new Vector3(this.playerpos.X, this.playerpos.Y, this.playerpos.Z), vector);
					if (num19 < num18 || num20 < num18)
					{
						this.cuttyCollide = true;
						break;
					}
				}
				float num21 = this.cuttyRot;
				float num22 = 0.005f;
				if (this.homingDirection > 0)
				{
					num21 = this.targetRot;
					num22 = 0.02f;
				}
				float num23 = Princess.WrapAngle(num21 - this.cuttyRot);
				num23 = MathHelper.Clamp(num23, -num22, num22);
				this.cuttyRot = Princess.WrapAngle(this.cuttyRot + num23);
				if (this.homingDirection > 0 && Math.Abs(Princess.WrapAngle(this.targetRot - this.cuttyRot)) <= 0.02f)
				{
					this.homingDirection = 0;
					if (this.homing == 0)
					{
						this.newAction = false;
						this.turningWait = this.rr.Next(400, 950);
						if (this.heartExposed)
						{
							this.turningWait = this.rr.Next(200, 460);
						}
						if (this.buckDelay < 50f)
						{
							this.buckDelay = 70f;
						}
						if (this.nextAttack < 50)
						{
							this.nextAttack = 70;
						}
						if (this.gonnaRollDelay < 50f)
						{
							this.gonnaRollDelay = 70f;
						}
						if (this.shitDelay <= 50f)
						{
							this.shitDelay = 70f;
						}
					}
				}
				if (this.homingTimer > 0)
				{
					this.homingTimer--;
					if (this.homingTimer == this.homingStart - 10)
					{
						this.talkIndex = this.rr.Next(1, 3);
						Princess.whichPigTalks = this.myIndex;
					}
					if (this.homingTimer <= 0)
					{
						this.newAction = false;
						this.vomitVol = false;
						this.gonnaVomit = false;
						this.homing = 0;
						this.homingDirection = 0;
						this.turningWait = this.rr.Next(300, 900);
						if (this.heartExposed)
						{
							this.turningWait = this.rr.Next(150, 360);
						}
						if (this.shitDelay <= 50f)
						{
							this.shitDelay = 70f;
						}
						if (this.buckDelay <= 50f)
						{
							this.buckDelay = 70f;
						}
						if (this.gonnaRollDelay <= 50f)
						{
							this.gonnaRollDelay = 70f;
						}
						if (this.nextAttack <= 50)
						{
							this.nextAttack = 70;
						}
					}
				}
			}
			else
			{
				this.explodeTimer--;
				if (this.explodeTimer <= 0)
				{
					this.updateExplode(ref heights);
				}
				else if (this.explodeTimer == 5)
				{
					this.explode.Play(this.sc.ev, 0f, 0f);
					this.initExplode();
				}
			}
			if (this.faceseizureTimer > 0)
			{
				this.faceseizureTimer--;
				if (this.faceseizureTimer % 2 == 0)
				{
					Math.Cos((double)(this.sc.myTimer / 26f));
					float num24 = 380f + (float)Math.Sin((double)(this.sc.myTimer / 34f)) * 220f;
					float num25 = 480f + (float)Math.Cos((double)(this.sc.myTimer / 24f)) * 200f;
					int num26 = 13;
					int num27 = 15;
					this.a.boneTransforms[7] = Matrix.CreateRotationY((float)this.rr.Next(-num27, num27) / num25) * this.a.boneTransforms[7];
					this.a.boneTransforms[7] = Matrix.CreateRotationZ((float)this.rr.Next(-num26, num26) / num24) * this.a.boneTransforms[7];
				}
			}
			if (this.seizureTimer > 0)
			{
				this.seizureTimer--;
				float num28 = 350f + (float)Math.Cos((double)(this.sc.myTimer / 26f)) * 300f;
				float num29 = 350f + (float)Math.Sin((double)(this.sc.myTimer / 34f)) * 300f;
				float num30 = 480f + (float)Math.Cos((double)(this.sc.myTimer / 24f)) * 200f;
				int num31 = 10;
				int num32 = 20;
				this.a.boneTransforms[5] = Matrix.CreateRotationZ((float)this.rr.Next(-num31, num31) / num30) * this.a.boneTransforms[5];
				this.a.boneTransforms[5] = Matrix.CreateRotationX((float)this.rr.Next(-num31, num31) / num30) * this.a.boneTransforms[5];
				this.a.boneTransforms[12] = Matrix.CreateRotationX((float)this.rr.Next(-num32, num32) / num30) * this.a.boneTransforms[12];
				this.a.boneTransforms[12] = Matrix.CreateRotationY((float)this.rr.Next(-num32, num32) / num28) * this.a.boneTransforms[12];
				this.a.boneTransforms[13] = Matrix.CreateRotationX((float)this.rr.Next(-num31, num31) / num29) * this.a.boneTransforms[13];
				this.a.boneTransforms[13] = Matrix.CreateRotationY((float)this.rr.Next(-num32, num32) / num28) * this.a.boneTransforms[13];
				this.a.boneTransforms[16] = Matrix.CreateRotationX((float)this.rr.Next(-num31, num31) / num28) * this.a.boneTransforms[16];
				this.a.boneTransforms[16] = Matrix.CreateRotationY((float)this.rr.Next(-num31, num31) / num28) * this.a.boneTransforms[16];
				this.a.boneTransforms[17] = Matrix.CreateRotationX((float)this.rr.Next(-num32, num32) / num29) * this.a.boneTransforms[17];
				this.a.boneTransforms[17] = Matrix.CreateRotationY((float)this.rr.Next(-num32, num32) / num28) * this.a.boneTransforms[17];
				this.a.boneTransforms[7] = Matrix.CreateRotationY((float)this.rr.Next(-num32, num32) / num30) * this.a.boneTransforms[7];
				this.a.boneTransforms[20] = Matrix.CreateRotationX((float)this.rr.Next(-num32, num32) / num28) * this.a.boneTransforms[20];
				this.a.boneTransforms[20] = Matrix.CreateRotationY((float)this.rr.Next(-num31, num31) / num29) * this.a.boneTransforms[20];
			}
			this.cuttyTrans = Matrix.CreateScale(this.cuttyScale) * Matrix.CreateRotationY(this.cuttyRot) * Matrix.CreateTranslation(this.cuttyPos);
			this.a.UpdateWorldTransforms(this.cuttyTrans, this.a.boneTransforms);
			this.a.skinTransforms.CopyTo(this.princessBone, 0);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000F4CA4 File Offset: 0x000F2EA4
		private void pigWaiting()
		{
			this.myClip = 0;
			this.fps = 0.4f;
			if (this.homingDirection == 1)
			{
				this.myClip = 5;
				this.fps = 0.8f;
			}
			if (this.homingDirection == 2)
			{
				this.myClip = 6;
				this.fps = 0.8f;
			}
			this.pigFrame1 += this.fps;
			this.bFrame1 += this.fps;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000F4D20 File Offset: 0x000F2F20
		private void pigDying()
		{
			this.attack1 = false;
			this.myClip = 4;
			if (this.clipIndexA != 4)
			{
				this.sc.dieyell.Play(this.sc.ev, 0f, 0f);
				this.myClip = 4;
				this.bFrame1 = this.pigFrame1;
				this.pigFrame1 = 0f;
			}
			if (!this.cuttyisDead && this.pigFrame1 >= 30f && this.pigFrame1 < 30.5f)
			{
				this.talkIndex = 0;
				Princess.whichPigTalks = this.myIndex;
				if (!this.spineDestroyed)
				{
					this.spineSwitch();
				}
				else if (!this.assDestroyed)
				{
					this.assSwitch();
				}
				else if (!this.faceDestroyed)
				{
					this.faceSwitch();
				}
			}
			if (!this.cuttyisDead && this.pigFrame1 >= 89f && this.pigFrame1 < 89.5f)
			{
				if (!this.spineDestroyed)
				{
					this.spineSwitch();
				}
				else if (!this.assDestroyed)
				{
					this.assSwitch();
				}
				else if (!this.faceDestroyed)
				{
					this.faceSwitch();
				}
			}
			if (!this.cuttyisDead)
			{
				this.fps = 0.4f;
				this.pigFrame1 += this.fps;
				this.bFrame1 += this.fps;
				if (this.pigFrame1 >= 119f)
				{
					this.cuttyisDead = true;
					this.bFrame1 = this.pigFrame1;
					this.pigFrame1 = 119f;
				}
			}
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000F4EA0 File Offset: 0x000F30A0
		private void pigRoll()
		{
			this.myClip = 3;
			this.fps = 0.6f;
			if (this.cuttyRollCount < 1)
			{
				this.cuttyRollFade -= 0.012f;
				if (this.cuttyRollFade <= 0f)
				{
					this.cuttyRollFade = 0f;
				}
			}
			else
			{
				this.energize();
				this.energize();
				this.energize();
				this.energize();
			}
			if (this.pigFrame1 > 61f && this.cuttyRollCount > 0)
			{
				this.pigFrame1 = 34.2f;
				this.cuttyRollCount--;
				this.dogroll.Play(this.sc.ev, (float)this.rr.Next(-10, 10) / 100f, 0f);
			}
			if (this.pigFrame1 >= 65f)
			{
				this.rollVol = false;
			}
			if (this.pigFrame1 >= 100f)
			{
				this.cuttyRoll = false;
				this.newAction = false;
				this.cuttyRollFade = 1f;
				this.gonnaRollDelay = (float)this.rr.Next(500, 1150);
				if (this.healthPerc < 0.6f && this.healthPerc > 0.2f)
				{
					this.gonnaRollDelay = (float)this.rr.Next(720, 1350);
				}
				if (this.heartExposed)
				{
					this.gonnaRollDelay = (float)this.rr.Next(1200, 2200);
				}
				if (this.nextAttack <= 50)
				{
					this.nextAttack = 70;
				}
				if (this.turningWait <= 50)
				{
					this.turningWait = 70;
				}
				if (this.buckDelay <= 50f)
				{
					this.buckDelay = 70f;
				}
				if (this.shitDelay <= 50f)
				{
					this.shitDelay = 70f;
				}
			}
			this.bFrame1 = this.pigFrame1;
			this.pigFrame1 += this.fps;
			this.bFrame1 += this.fps;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000F50A8 File Offset: 0x000F32A8
		private void pigAttack(ref float[,] heights)
		{
			this.myClip = 2;
			this.fps = 0f;
			this.attackWait1 -= 1f;
			if (this.attackWait1 <= 0f)
			{
				this.attackWait1 = 0f;
				this.attacktimer += 1f;
				if (this.attacktimer > this.attackduration)
				{
					this.attacktimer = this.attackduration + 5f;
					this.attackWait2 -= 1f;
				}
			}
			if (this.attackWait1 > 0f)
			{
				this.fps = 0.4f;
				this.cuttyPos.Y = -54f;
				if (this.df != 2 && this.df != 5 && this.attackWait1 == 1f && !this.shootHeartMessage && this.heartExposed)
				{
					if (this.df != 0 && this.df != 3)
					{
						this.heartMessCount++;
						if (this.heartMessCount < 5 && this.sc.currentDay < 30)
						{
							this.shootHeartMessage = true;
						}
					}
					else
					{
						this.heartMessCount++;
						this.shootHeartMessage = true;
					}
				}
			}
			if (this.attackWait2 > 0f && this.attackWait1 <= 0f && this.attacktimer <= this.attackduration)
			{
				this.upForceAcc += 5f;
				if (this.upForceAcc >= this.upForceMax)
				{
					this.upForceAcc = this.upForceMax;
					this.upForce -= this.grav;
				}
				else
				{
					this.upForce = this.upForceAcc;
				}
				this.cuttyPos.Y = this.cuttyPos.Y + this.upForce;
				if (this.cuttyPos.Y < 0f && this.upForce < 0f)
				{
					this.volumeFade = true;
					this.jumpVol = true;
				}
				if (this.cuttyPos.Y < -54f)
				{
					this.cuttyPos.Y = -54f;
					this.attacktimer = this.attackduration + 1f;
					this.pigFrame1 = 54f;
				}
				else if (this.attacktimer >= this.attackduration - 1f)
				{
					this.attacktimer = this.attackduration - 3f;
				}
				this.fps = 0.4f;
				if (this.pigFrame1 > 54f)
				{
					this.pigFrame1 = 54f;
				}
			}
			if (this.attackWait2 > 0f && this.attacktimer > this.attackduration)
			{
				if (this.attackWait2 == 55f)
				{
					this.sc.cuttyWave.Play(this.sc.ev, 0f, 0f);
					this.shockTimer = 145f;
					this.shockRadius = 150f;
					this.shockHit = false;
					this.shockHasHit = false;
					this.groundhit = true;
				}
				if (this.pigFrame1 < 54f)
				{
					this.pigFrame1 = 54f;
				}
				this.cuttyPos.Y = -54f;
				this.fps = 0.4f;
			}
			if (this.attackWait2 <= 0f)
			{
				this.attack1 = false;
				this.nextAttack = this.rr.Next(500, 1200);
				if (this.heartExposed)
				{
					this.nextAttack = this.rr.Next(150, 560);
				}
				this.newAction = false;
				if (this.buckDelay <= 50f)
				{
					this.buckDelay = 70f;
				}
				if (this.gonnaRollDelay <= 50f)
				{
					this.gonnaRollDelay = 70f;
				}
				if (this.nextAttack <= 50)
				{
					this.nextAttack = 70;
				}
				if (this.shitDelay <= 50f)
				{
					this.shitDelay = 70f;
				}
			}
			this.bFrame1 = this.pigFrame1;
			this.pigFrame1 += this.fps;
			this.bFrame1 += this.fps;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000F54CC File Offset: 0x000F36CC
		private void pigBuck()
		{
			this.myClip = 1;
			this.fps = 0.4f;
			if (this.pigFrame1 >= 45f)
			{
				this.cuttyBuck = false;
				this.newAction = false;
				this.buckDelay = (float)this.rr.Next(800, 1450);
				if (this.healthPerc > 0.8f)
				{
					this.buckDelay = (float)this.rr.Next(500, 950);
				}
				if (this.heartExposed)
				{
					this.buckDelay = (float)this.rr.Next(2500, 5250);
				}
				if (this.nextAttack <= 50)
				{
					this.nextAttack = 70;
				}
				if (this.gonnaRollDelay <= 50f)
				{
					this.gonnaRollDelay = 70f;
				}
				if (this.turningWait <= 50)
				{
					this.turningWait = 70;
				}
				if (this.shitDelay <= 50f)
				{
					this.shitDelay = 70f;
				}
			}
			this.bFrame1 = this.pigFrame1;
			this.pigFrame1 += this.fps;
			this.bFrame1 += this.fps;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000F55F8 File Offset: 0x000F37F8
		public void host_SendData(ref packetSender packetWriter, int timeFrame)
		{
			if (this.cuttyStruct_send.flag == 140)
			{
				packetWriter.Write(140);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write((byte)this.cuttyStruct_send.homing);
				packetWriter.Write(this.cuttyStruct_send.rot);
				packetWriter.Write((byte)this.cuttyStruct_send.animType);
				if (this.cuttyStruct_send.talkindex > -1)
				{
					packetWriter.Write((byte)this.cuttyStruct_send.talkindex);
				}
				else
				{
					packetWriter.Write(150);
				}
			}
			if (this.cuttyStruct_send.flag == 141)
			{
				packetWriter.Write(141);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
			}
			if (this.cuttyStruct_send.flag == 142)
			{
				packetWriter.Write(142);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.rot);
			}
			if (this.cuttyStruct_send.flag == 143)
			{
				packetWriter.Write(143);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.rot);
				if (this.cuttyStruct_send.talkindex > -1)
				{
					packetWriter.Write((byte)this.cuttyStruct_send.talkindex);
				}
				else
				{
					packetWriter.Write(150);
				}
			}
			if (this.cuttyStruct_send.flag == 144)
			{
				packetWriter.Write(144);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.rot);
			}
			if (this.cuttyStruct_send.flag == 145)
			{
				packetWriter.Write(145);
				packetWriter.Write((byte)this.myIndex);
				packetWriter.Write(this.cuttyStruct_send.dur);
				packetWriter.Write(this.cuttyStruct_send.rot);
			}
			if (this.cuttyStruct_send.flag == 146)
			{
				packetWriter.Write(146);
				packetWriter.Write((byte)this.myIndex);
				if (this.cuttyStruct_send.talkindex > -1)
				{
					packetWriter.Write((byte)this.cuttyStruct_send.talkindex);
				}
				else
				{
					packetWriter.Write(150);
				}
			}
			this.actionScheduled = false;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000F58AC File Offset: 0x000F3AAC
		private void scheduleAction(int flag)
		{
			this.delay = 0f;
			if (this.networkSession != null && this.networkSession.RemoteGamers.Count > 0)
			{
				this.delay = (float)(this.networkSession.RemoteGamers[0].RoundtripTime.Milliseconds / 2);
			}
			this.delay = (float)Math.Round((double)(this.delay * 0.06f));
			this.timeDelay = this.timeframe + (int)this.delay;
			if (this.timeDelay <= this.lasttimeDelay && this.lasttimeDelay - this.timeDelay < 60)
			{
				this.timeDelay = this.lasttimeDelay + 1;
			}
			this.lasttimeDelay = this.timeDelay;
			if (flag == 140)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.homing = this.cuttyStruct.homing;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
				this.cuttyStruct_send.animType = this.cuttyStruct.animType;
				this.cuttyStruct_send.talkindex = this.cuttyStruct.talkindex;
			}
			if (flag == 141)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
			}
			if (flag == 142)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
			}
			if (flag == 143)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
				this.cuttyStruct_send.talkindex = this.cuttyStruct.talkindex;
			}
			if (flag == 144)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
			}
			if (flag == 145)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.dur = this.cuttyStruct.dur;
				this.cuttyStruct_send.rot = this.cuttyStruct.rot;
			}
			if (flag == 146)
			{
				this.cuttyStruct_send.flag = flag;
				this.cuttyStruct_send.time = this.timeDelay;
				this.cuttyStruct_send.talkindex = this.cuttyStruct.talkindex;
			}
			this.scheduleList.Add(this.cuttyStruct_send);
			this.actionScheduled = true;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000F5BD4 File Offset: 0x000F3DD4
		private void runScheduler(ref float[,] heights)
		{
			for (int i = 0; i < this.scheduleList.Count; i++)
			{
				this.tempConduct = this.scheduleList[i];
				if (this.timeframe >= this.tempConduct.time || Math.Abs(this.tempConduct.time - this.timeframe) > 120)
				{
					this.princessAction(ref this.tempConduct, true, ref heights);
					this.scheduleList.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000F5C54 File Offset: 0x000F3E54
		public void princessAction(ref Princess.conductor tempy, bool isLocal, ref float[,] heights)
		{
			if (tempy.flag == 140)
			{
				this.homing = tempy.homing;
				this.homingDirection = tempy.animType;
				if (this.homing == 5)
				{
					this.homing = 0;
					this.gonnaShit = true;
					this.volumeFade = true;
					this.shitVol = true;
				}
				this.targetRot = tempy.rot;
				if (this.homing > 0 && this.homing < 5)
				{
					this.homingTimer = this.homingStart;
					this.volumeFade = true;
					this.vomitVol = true;
				}
				if (tempy.talkindex > -1)
				{
					this.talkIndex = tempy.talkindex;
					Princess.whichPigTalks = this.myIndex;
				}
				else
				{
					Princess.cuttyDoneSpeech = true;
					this.cuttyDoneLocalSpeech = true;
				}
			}
			if (tempy.flag == 141)
			{
				this.attack1 = true;
				this.bFrame1 = this.pigFrame1;
				this.pigFrame1 = 0f;
				this.attacktimer = 0f;
				this.attackWait1 = 42f;
				this.attackWait2 = 62f;
				this.attackduration = (float)tempy.dur;
				if (this.attackduration == 2500f)
				{
					this.attackduration = (float)this.rr.Next(70, 85);
				}
				else
				{
					this.attackduration = MathHelper.Lerp(110f, 200f, (this.attackduration - 500f) / 1400f);
				}
				this.grav = 0.5f;
				this.upForceAcc = 0f;
				this.upForce = 0f;
				this.upForceMax = this.grav * (this.attackduration / 2f);
			}
			if (tempy.flag == 142)
			{
				this.death1 = true;
				this.cuttyisDead = false;
				this.health = 0;
			}
			if (tempy.flag == 143)
			{
				this.bFrame1 = this.pigFrame1;
				this.pigFrame1 = 0f;
				this.cuttyRoll = true;
				this.cuttyRollCount = (int)tempy.dur;
				if (this.cuttyRollCount > 3)
				{
					this.windy.Play(this.sc.ev, (float)this.rr.Next(-20, 10) / 100f, 0f);
				}
				else
				{
					this.windy.Play(this.sc.ev, (float)this.rr.Next(50, 70) / 100f, 0f);
				}
				this.volumeFade = true;
				this.rollVol = true;
				if (tempy.talkindex > -1)
				{
					this.talkIndex = tempy.talkindex;
					Princess.whichPigTalks = this.myIndex;
				}
			}
			if (tempy.flag == 144)
			{
				this.cuttyBuck = true;
				this.bFrame1 = this.pigFrame1;
				this.pigFrame1 = 0f;
				this.bucking.Play(this.sc.ev, 0f, 0f);
			}
			if (tempy.flag == 145)
			{
				this.cuttyShit = true;
				this.shitDelay = 5000f;
				this.shitTimer = (float)tempy.dur;
				this.fart.Play(this.sc.ev, (float)this.rr.Next(-50, 10) / 100f, 0f);
				this.volumeFade = true;
				this.shitVol = true;
			}
			if (tempy.flag == 146)
			{
				if (tempy.talkindex > -1)
				{
					this.talkIndex = tempy.talkindex;
					Princess.whichPigTalks = this.myIndex;
					return;
				}
				Princess.cuttyDoneSpeech = true;
				this.cuttyDoneLocalSpeech = true;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000F5FDF File Offset: 0x000F41DF
		private static float WrapAngle(float radians)
		{
			while (radians < -3.1415927f)
			{
				radians += 6.2831855f;
			}
			while (radians > 3.1415927f)
			{
				radians -= 6.2831855f;
			}
			return radians;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000F6008 File Offset: 0x000F4208
		public void Draw(Vector3 campos, Matrix view, Matrix proj, bool showSphere, ref Texture2D ttworld, Matrix viewWorld, int tech, bool walletopen)
		{
			this.campos = campos;
			this.view = view;
			this.proj = proj;
			if (this.chunk.startDrop)
			{
				this.DrawInstance(this.chunk, "fastShader4");
			}
			if (this.faceChunk.startDrop)
			{
				this.DrawInstance(this.faceChunk, "fastShader4");
			}
			if (this.assChunk.startDrop)
			{
				this.DrawInstance(this.assChunk, "fastShader4");
			}
			if (this.death1)
			{
				this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
			}
			if (this.explodeTimer > 0)
			{
				this.drawPrincess(ref ttworld, viewWorld, tech);
			}
			else
			{
				this.drawExplode();
			}
			if (this.explodeTimer < 5)
			{
				this.drawGuts();
			}
			if (this.death1)
			{
				this.sc.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
			}
			if (this.sc.bossLoaded && this.explodeTimer > 0 && this.heartExposed)
			{
				this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.None;
				float num = Vector2.Distance(new Vector2(campos.X, campos.Z), new Vector2(this.cuttyPos.X, this.cuttyPos.Z));
				if (!walletopen)
				{
					this.drawHeart(num);
				}
				this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
			}
			if (this.showSkelTimer > 0 && !this.cuttyisDead)
			{
				this.showSkelTimer--;
				if (this.showSkelTimer < 0)
				{
					this.showSkelTimer = 0;
				}
				if (this.showSkelTimer % 5 == 0)
				{
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.None;
					this.sc.GraphicsDevice.BlendState = BlendState.Additive;
					this.drawCuttySkel();
					this.sc.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
					this.sc.GraphicsDevice.BlendState = BlendState.NonPremultiplied;
				}
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000F620A File Offset: 0x000F440A
		public void DrawBoulders(Vector3 campos)
		{
			this.rocks.SetCamera(this.view, this.proj);
			this.rocks.Draw(0);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000F622F File Offset: 0x000F442F
		public void drawDots()
		{
			this.dots.SetCamera(this.view, this.proj);
			this.dots.Draw(0);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000F6254 File Offset: 0x000F4454
		public void resetExplody(bool death)
		{
			if (death)
			{
				this.explodeTimer = 50;
				this.death1 = false;
				this.pigFrame1 = 0f;
				this.cuttyStruct.dur = 40;
				this.cuttyStruct.rot = this.cuttyRot;
				this.scheduleAction(142);
			}
			int num = 390;
			this.explodePart = new Matrix[num];
			this.explodePart[0] = Matrix.CreateScale(14.4770775f, 11.212118f, 8.2677765f) * Matrix.CreateTranslation(-24.719204f, 28.82737f, 98.235565f);
			this.explodePart[1] = Matrix.CreateScale(15.845062f, 13.797251f, 9.090149f) * Matrix.CreateTranslation(-23.689943f, 19.77886f, 98.335754f);
			this.explodePart[2] = Matrix.CreateScale(17.413097f, 21.482819f, 33.953438f) * Matrix.CreateTranslation(1.6536639f, 67.832405f, -95.31728f);
			this.explodePart[3] = Matrix.CreateScale(22.439804f, 30.769562f, 41.824394f) * Matrix.CreateTranslation(10.01247f, 47.05307f, -93.637f);
			this.explodePart[4] = Matrix.CreateScale(19.059658f, 20.029388f, 42.988495f) * Matrix.CreateTranslation(0.8243537f, 81.26076f, -84.04066f);
			this.explodePart[5] = Matrix.CreateScale(19.2028f, 28.08191f, 62.05641f) * Matrix.CreateTranslation(7.2776937f, 63.878178f, -81.66424f);
			this.explodePart[6] = Matrix.CreateScale(15.100418f, 18.270126f, 30.98259f) * Matrix.CreateTranslation(0.67752814f, 72.45757f, -75.201126f);
			this.explodePart[7] = Matrix.CreateScale(19.972649f, 24.480133f, 36.26729f) * Matrix.CreateTranslation(8.476687f, 56.103996f, -68.76012f);
			this.explodePart[8] = Matrix.CreateScale(21.606892f, 19.509083f, 35.45726f) * Matrix.CreateTranslation(-24.060406f, 68.10201f, -87.5686f);
			this.explodePart[9] = Matrix.CreateScale(40.951904f, 17.907219f, 49.562515f) * Matrix.CreateTranslation(-16.16684f, 57.621506f, -81.8079f);
			this.explodePart[10] = Matrix.CreateScale(22.91507f, 20.475647f, 62.72592f) * Matrix.CreateTranslation(-13.84039f, 77.545334f, -72.96725f);
			this.explodePart[11] = Matrix.CreateScale(25.175545f, 26.525627f, 71.84491f) * Matrix.CreateTranslation(-6.7899027f, 62.560516f, -72.38807f);
			this.explodePart[12] = Matrix.CreateScale(16.229553f, 19.7223f, 27.715096f) * Matrix.CreateTranslation(15.373486f, 29.21568f, 77.74955f);
			this.explodePart[13] = Matrix.CreateScale(17.919415f, 29.104538f, 28.817627f) * Matrix.CreateTranslation(22.88195f, 9.356084f, 81.37706f);
			this.explodePart[14] = Matrix.CreateScale(17.448753f, 17.945179f, 51.227127f) * Matrix.CreateTranslation(14.35212f, 42.52254f, 82.97742f);
			this.explodePart[15] = Matrix.CreateScale(18.75489f, 24.671352f, 52.830048f) * Matrix.CreateTranslation(19.509216f, 27.934805f, 86.21612f);
			this.explodePart[16] = Matrix.CreateScale(15.785828f, 20.122063f, 32.417404f) * Matrix.CreateTranslation(15.518388f, 32.50083f, 96.382774f);
			this.explodePart[17] = Matrix.CreateScale(16.556454f, 29.689804f, 28.356018f) * Matrix.CreateTranslation(21.71776f, 12.926045f, 98.099365f);
			this.explodePart[18] = Matrix.CreateScale(30.015816f, 27.516861f, 35.14643f) * Matrix.CreateTranslation(-12.749041f, 73.25353f, -27.297926f);
			this.explodePart[19] = Matrix.CreateScale(31.034859f, 38.428413f, 36.640625f) * Matrix.CreateTranslation(-1.116889f, 47.157917f, -22.511927f);
			this.explodePart[20] = Matrix.CreateScale(13.092724f, 15.237968f, 52.02321f) * Matrix.CreateTranslation(2.4730308f, 79.73395f, -18.245047f);
			this.explodePart[21] = Matrix.CreateScale(20.942772f, 18.463966f, 41.723694f) * Matrix.CreateTranslation(10.816211f, 67.45801f, -3.322219f);
			this.explodePart[22] = Matrix.CreateScale(22.994125f, 14.697845f, 108.76511f) * Matrix.CreateTranslation(3.9330032f, 85.14862f, -6.235113f);
			this.explodePart[23] = Matrix.CreateScale(19.210232f, 19.785645f, 84.88803f) * Matrix.CreateTranslation(10.3272f, 75.57598f, 8.226435f);
			this.explodePart[24] = Matrix.CreateScale(15.626091f, 16.893593f, 51.94104f) * Matrix.CreateTranslation(5.584772f, 82.858284f, 27.410841f);
			this.explodePart[25] = Matrix.CreateScale(19.218018f, 22.043282f, 45.777023f) * Matrix.CreateTranslation(11.045926f, 69.445984f, 26.98805f);
			this.explodePart[26] = Matrix.CreateScale(37.494648f, 40.354237f, 32.710953f) * Matrix.CreateTranslation(-28.310461f, 48.492134f, 41.111824f);
			this.explodePart[27] = Matrix.CreateScale(60.714676f, 46.58931f, 44.916138f) * Matrix.CreateTranslation(-12.951456f, 16.399618f, 53.01792f);
			this.explodePart[28] = Matrix.CreateScale(39.028442f, 33.295372f, 37.515877f) * Matrix.CreateTranslation(-20.623013f, 66.488304f, 43.131474f);
			this.explodePart[29] = Matrix.CreateScale(38.756325f, 47.349815f, 42.236755f) * Matrix.CreateTranslation(-4.8913894f, 34.503952f, 51.115997f);
			this.explodePart[30] = Matrix.CreateScale(33.0288f, 28.264584f, 57.991272f) * Matrix.CreateTranslation(-26.958998f, 70.70919f, 13.862508f);
			this.explodePart[31] = Matrix.CreateScale(52.907135f, 26.618103f, 33.70124f) * Matrix.CreateTranslation(-16.9818f, 54.75593f, 4.402969f);
			this.explodePart[32] = Matrix.CreateScale(29.04358f, 19.231373f, 54.550034f) * Matrix.CreateTranslation(-29.495043f, 64.691444f, -17.108648f);
			this.explodePart[33] = Matrix.CreateScale(43.188866f, 17.281998f, 51.04892f) * Matrix.CreateTranslation(-22.366356f, 54.92744f, -16.26659f);
			this.explodePart[34] = Matrix.CreateScale(21.484652f, 18.373482f, 32.009346f) * Matrix.CreateTranslation(-34.830994f, 60.37344f, 46.739357f);
			this.explodePart[35] = Matrix.CreateScale(30.345642f, 20.570557f, 34.15812f) * Matrix.CreateTranslation(-23.67651f, 46.0856f, 56.83587f);
			this.explodePart[36] = Matrix.CreateScale(26.007965f, 16.771996f, 44.788788f) * Matrix.CreateTranslation(27.747835f, 58.45891f, 46.601517f);
			this.explodePart[37] = Matrix.CreateScale(26.286942f, 20.817154f, 51.86377f) * Matrix.CreateTranslation(29.972824f, 48.299053f, 67.147285f);
			this.explodePart[38] = Matrix.CreateScale(20.68274f, 19.325233f, 55.828613f) * Matrix.CreateTranslation(32.157646f, 35.80836f, 89.128716f);
			this.explodePart[39] = Matrix.CreateScale(22.741058f, 15.605682f, 39.507385f) * Matrix.CreateTranslation(31.96695f, 46.758312f, 77.579544f);
			this.explodePart[40] = Matrix.CreateScale(0.6192398f, 0.20168304f, 0.6689148f) * Matrix.CreateTranslation(23.044876f, 40.566563f, 104.06808f);
			this.explodePart[41] = Matrix.CreateScale(18.252808f, 11.090745f, 16.89746f) * Matrix.CreateTranslation(43.78763f, 16.61206f, -15.891022f);
			this.explodePart[42] = Matrix.CreateScale(15.808849f, 13.411287f, 12.412464f) * Matrix.CreateTranslation(40.646534f, 8.695201f, -17.656181f);
			this.explodePart[43] = Matrix.CreateScale(4.861637f, 5.441223f, 9.439075f) * Matrix.CreateTranslation(41.822433f, 23.510197f, 0.18810844f);
			this.explodePart[44] = Matrix.CreateScale(12.355938f, 10.226662f, 10.008926f) * Matrix.CreateTranslation(37.045536f, 16.935566f, -0.31110215f);
			this.explodePart[45] = Matrix.CreateScale(44.499725f, 27.41536f, 48.509186f) * Matrix.CreateTranslation(-11.931428f, 37.51112f, -57.840862f);
			this.explodePart[46] = Matrix.CreateScale(46.229553f, 38.30728f, 63.03299f) * Matrix.CreateTranslation(-12.709274f, 19.574709f, -62.490776f);
			this.explodePart[47] = Matrix.CreateScale(24.33699f, 16.452255f, 41.911865f) * Matrix.CreateTranslation(-25.992533f, 71.40927f, -63.72718f);
			this.explodePart[48] = Matrix.CreateScale(33.444397f, 19.196556f, 46.79094f) * Matrix.CreateTranslation(-24.388172f, 62.71343f, -64.12918f);
			this.explodePart[49] = Matrix.CreateScale(41.41916f, 20.604416f, 48.0802f) * Matrix.CreateTranslation(-21.260939f, 55.36613f, -62.5402f);
			this.explodePart[50] = Matrix.CreateScale(50.64865f, 19.706882f, 38.26271f) * Matrix.CreateTranslation(-16.015137f, 49.049942f, -53.27703f);
			this.explodePart[51] = Matrix.CreateScale(0.08560181f, 0.01689148f, 0.0660553f) * Matrix.CreateTranslation(-39.325798f, 44.99202f, -58.775272f);
			this.explodePart[52] = Matrix.CreateScale(12.2883f, 19.992775f, 37.30078f) * Matrix.CreateTranslation(-6.0450716f, 64.48841f, -92.82181f);
			this.explodePart[53] = Matrix.CreateScale(22.183586f, 26.177998f, 69.61978f) * Matrix.CreateTranslation(-3.4190834f, 46.679012f, -79.25354f);
			this.explodePart[54] = Matrix.CreateScale(29.625328f, 30.215925f, 82.78897f) * Matrix.CreateTranslation(-0.23747587f, 29.236568f, -72.28792f);
			this.explodePart[55] = Matrix.CreateScale(38.515762f, 38.951164f, 75.8085f) * Matrix.CreateTranslation(5.3973494f, 7.356596f, -68.28343f);
			this.explodePart[56] = Matrix.CreateScale(24.857552f, 17.046692f, 82.86948f) * Matrix.CreateTranslation(-11.432834f, 82.44336f, 8.98785f);
			this.explodePart[57] = Matrix.CreateScale(25.84169f, 19.381401f, 81.69614f) * Matrix.CreateTranslation(-9.275717f, 75.730194f, 11.506266f);
			this.explodePart[58] = Matrix.CreateScale(20.060822f, 17.095177f, 61.574585f) * Matrix.CreateTranslation(-4.1694293f, 66.6553f, 6.879777f);
			this.explodePart[59] = Matrix.CreateScale(18.443523f, 23.026157f, 32.279213f) * Matrix.CreateTranslation(2.7291853f, 52.00983f, -2.4799023f);
			this.explodePart[60] = Matrix.CreateScale(31.204643f, 19.327961f, 41.41841f) * Matrix.CreateTranslation(-30.992834f, 47.470192f, 51.072655f);
			this.explodePart[61] = Matrix.CreateScale(39.83934f, 18.262608f, 35.09835f) * Matrix.CreateTranslation(-19.063951f, 35.939713f, 60.94499f);
			this.explodePart[62] = Matrix.CreateScale(31.899246f, 26.879925f, 48.78044f) * Matrix.CreateTranslation(-12.06262f, 38.14187f, 10.80822f);
			this.explodePart[63] = Matrix.CreateScale(45.418945f, 40.132965f, 84.17444f) * Matrix.CreateTranslation(-13.620339f, 14.937023f, -6.189102f);
			this.explodePart[64] = Matrix.CreateScale(10.400272f, 7.857049f, 10.865067f) * Matrix.CreateTranslation(-26.159792f, -2.7782829f, 3.4086018f);
			this.explodePart[65] = Matrix.CreateScale(8.638336f, 9.4921f, 9.11322f) * Matrix.CreateTranslation(-27.973892f, -8.927f, 2.6436346f);
			this.explodePart[66] = Matrix.CreateScale(39.75426f, 30.111412f, 46.499638f) * Matrix.CreateTranslation(-19.011116f, 41.20406f, 10.074438f);
			this.explodePart[67] = Matrix.CreateScale(47.43918f, 44.177612f, 90.6597f) * Matrix.CreateTranslation(-18.105293f, 16.542292f, -10.80101f);
			this.explodePart[68] = Matrix.CreateScale(21.792313f, 16.428444f, 34.52677f) * Matrix.CreateTranslation(40.260094f, 36.11992f, 35.408737f);
			this.explodePart[69] = Matrix.CreateScale(2.1650352f, 0.57406616f, 2.1719055f) * Matrix.CreateTranslation(38.183f, 31.4127f, 49.45735f);
			this.explodePart[70] = Matrix.CreateScale(21.979546f, 24.310612f, 41.41684f) * Matrix.CreateTranslation(35.198536f, 21.231457f, 29.009186f);
			this.explodePart[71] = Matrix.CreateScale(12.849083f, 11.869043f, 26.552864f) * Matrix.CreateTranslation(39.980057f, 31.020039f, 49.40527f);
			this.explodePart[72] = Matrix.CreateScale(13.794891f, 18.505669f, 34.09932f) * Matrix.CreateTranslation(34.670208f, 18.567423f, 45.048573f);
			this.explodePart[73] = Matrix.CreateScale(28.837692f, 22.547367f, 53.934982f) * Matrix.CreateTranslation(39.254883f, 37.099926f, 16.478153f);
			this.explodePart[74] = Matrix.CreateScale(29.047905f, 31.93254f, 70.12552f) * Matrix.CreateTranslation(38.241497f, 17.06254f, 1.4323273f);
			this.explodePart[75] = Matrix.CreateScale(37.318283f, 26.6465f, 55.584473f) * Matrix.CreateTranslation(34.853947f, 38.7868f, 6.0868464f);
			this.explodePart[76] = Matrix.CreateScale(31.870499f, 35.952118f, 64.01532f) * Matrix.CreateTranslation(35.156162f, 18.83377f, -9.923008f);
			this.explodePart[77] = Matrix.CreateScale(3.3943596f, 1.3104935f, 3.0001984f) * Matrix.CreateTranslation(35.594307f, 33.30713f, 20.70087f);
			this.explodePart[78] = Matrix.CreateScale(2.2831612f, 1.2494698f, 1.7508698f) * Matrix.CreateTranslation(33.336647f, 39.71297f, 37.185028f);
			this.explodePart[79] = Matrix.CreateScale(2.4970932f, 1.4318542f, 0.7927513f) * Matrix.CreateTranslation(33.527145f, 39.127033f, 37.540634f);
			this.explodePart[80] = Matrix.CreateScale(19.76847f, 22.154182f, 51.675278f) * Matrix.CreateTranslation(27.993782f, 56.949726f, -71.59706f);
			this.explodePart[81] = Matrix.CreateScale(20.350243f, 33.914963f, 62.45746f) * Matrix.CreateTranslation(30.166916f, 34.7374f, -76.807526f);
			this.explodePart[82] = Matrix.CreateScale(19.619957f, 23.663116f, 45.339233f) * Matrix.CreateTranslation(31.799118f, 49.812088f, -66.02525f);
			this.explodePart[83] = Matrix.CreateScale(20.616768f, 38.396736f, 60.243683f) * Matrix.CreateTranslation(33.988297f, 25.133354f, -72.31908f);
			this.explodePart[84] = Matrix.CreateScale(39.316803f, 22.950235f, 49.520905f) * Matrix.CreateTranslation(-18.381208f, 43.054016f, -62.88692f);
			this.explodePart[85] = Matrix.CreateScale(37.9767f, 30.363197f, 61.934998f) * Matrix.CreateTranslation(-22.377886f, 26.912098f, -68.892296f);
			this.explodePart[86] = Matrix.CreateScale(1.4362907f, 0.815485f, 2.6392822f) * Matrix.CreateTranslation(-36.23982f, 10.043349f, -78.669785f);
			this.explodePart[87] = Matrix.CreateScale(2.7681427f, 1.3553772f, 4.1386337f) * Matrix.CreateTranslation(-36.80842f, 9.36445f, -78.71055f);
			this.explodePart[88] = Matrix.CreateScale(33.700237f, 17.681717f, 43.695145f) * Matrix.CreateTranslation(-30.504032f, 41.629528f, 51.712082f);
			this.explodePart[89] = Matrix.CreateScale(38.97284f, 16.924465f, 32.88659f) * Matrix.CreateTranslation(-11.8817005f, 29.367071f, 64.06844f);
			this.explodePart[90] = Matrix.CreateScale(15.265381f, 16.136456f, 12.99361f) * Matrix.CreateTranslation(-43.233334f, 29.135569f, 63.693424f);
			this.explodePart[91] = Matrix.CreateScale(10.663845f, 22.214474f, 11.587494f) * Matrix.CreateTranslation(-47.641068f, 12.582777f, 58.881706f);
			this.explodePart[92] = Matrix.CreateScale(44.558044f, 24.72963f, 71.373566f) * Matrix.CreateTranslation(-11.27385f, 18.15395f, 4.224779f);
			this.explodePart[93] = Matrix.CreateScale(46.907684f, 33.964867f, 84.84607f) * Matrix.CreateTranslation(-6.439537f, 2.1194487f, 0.72776794f);
			this.explodePart[94] = Matrix.CreateScale(7.6967926f, 9.989346f, 9.529007f) * Matrix.CreateTranslation(-0.5703161f, 45.313927f, 30.14122f);
			this.explodePart[95] = Matrix.CreateScale(28.524109f, 21.795464f, 39.56392f) * Matrix.CreateTranslation(-7.0997586f, 31.01323f, 17.217358f);
			this.explodePart[96] = Matrix.CreateScale(0.100842476f, 0.13121223f, 0.0503664f) * Matrix.CreateTranslation(4.371228f, 27.649824f, -14.355411f);
			this.explodePart[97] = Matrix.CreateScale(0.23032379f, 0.20167351f, 0.09950924f) * Matrix.CreateTranslation(4.350857f, 27.51936f, -14.359238f);
			this.explodePart[98] = Matrix.CreateScale(45.705f, 23.200596f, 45.200928f) * Matrix.CreateTranslation(-24.803352f, 47.28013f, -5.0934114f);
			this.explodePart[99] = Matrix.CreateScale(45.757904f, 26.533493f, 70.69827f) * Matrix.CreateTranslation(-23.390436f, 38.387924f, -5.1117764f);
			this.explodePart[100] = Matrix.CreateScale(41.587418f, 26.871029f, 82.33168f) * Matrix.CreateTranslation(-23.791243f, 26.871086f, -9.617586f);
			this.explodePart[101] = Matrix.CreateScale(38.087555f, 31.390274f, 72.279236f) * Matrix.CreateTranslation(-20.987774f, 10.463584f, -23.308533f);
			this.explodePart[102] = Matrix.CreateScale(24.128883f, 21.19019f, 54.514572f) * Matrix.CreateTranslation(19.9892f, 69.161896f, -93.04912f);
			this.explodePart[103] = Matrix.CreateScale(20.985924f, 30.605087f, 49.506058f) * Matrix.CreateTranslation(23.606882f, 51.185276f, -87.96055f);
			this.explodePart[104] = Matrix.CreateScale(18.319527f, 19.671494f, 45.586273f) * Matrix.CreateTranslation(25.523468f, 62.1968f, -77.150154f);
			this.explodePart[105] = Matrix.CreateScale(18.536629f, 29.366585f, 50.45575f) * Matrix.CreateTranslation(27.092978f, 43.77159f, -85.28785f);
			this.explodePart[106] = Matrix.CreateScale(17.648232f, 20.727835f, 37.783806f) * Matrix.CreateTranslation(36.08632f, 38.056858f, -59.885326f);
			this.explodePart[107] = Matrix.CreateScale(19.093384f, 33.862434f, 53.80287f) * Matrix.CreateTranslation(37.31041f, 15.800602f, -67.43016f);
			this.explodePart[108] = Matrix.CreateScale(18.1195f, 19.89926f, 34.731606f) * Matrix.CreateTranslation(39.01675f, 30.772535f, -56.127888f);
			this.explodePart[109] = Matrix.CreateScale(20.47631f, 30.660034f, 53.209946f) * Matrix.CreateTranslation(40.620537f, 11.781858f, -63.214348f);
			this.explodePart[110] = Matrix.CreateScale(23.257866f, 14.274567f, 28.495068f) * Matrix.CreateTranslation(-30.466799f, 40.799404f, -54.05661f);
			this.explodePart[111] = Matrix.CreateScale(20.486046f, 17.934147f, 28.743202f) * Matrix.CreateTranslation(-32.049564f, 30.690447f, -53.659855f);
			this.explodePart[112] = Matrix.CreateScale(2.9715729f, 1.6575718f, 1.8535538f) * Matrix.CreateTranslation(-41.915524f, 10.562537f, -77.46623f);
			this.explodePart[113] = Matrix.CreateScale(3.423195f, 1.7647219f, 1.9522858f) * Matrix.CreateTranslation(-41.58939f, 9.913489f, -77.2366f);
			this.explodePart[114] = Matrix.CreateScale(31.780884f, 19.328167f, 33.57852f) * Matrix.CreateTranslation(-7.498803f, 21.021463f, 67.512375f);
			this.explodePart[115] = Matrix.CreateScale(38.139435f, 19.170517f, 40.84024f) * Matrix.CreateTranslation(-4.1309614f, 12.922731f, 68.841255f);
			this.explodePart[116] = Matrix.CreateScale(34.275764f, 23.354996f, 42.60518f) * Matrix.CreateTranslation(-29.881142f, 34.91813f, 51.410225f);
			this.explodePart[117] = Matrix.CreateScale(33.623047f, 30.66101f, 39.687317f) * Matrix.CreateTranslation(-23.723627f, 18.167183f, 54.18229f);
			this.explodePart[118] = Matrix.CreateScale(2.4002342f, 1.1582661f, 2.4297333f) * Matrix.CreateTranslation(-15.629359f, 30.643862f, 73.71498f);
			this.explodePart[119] = Matrix.CreateScale(2.9368868f, 0.9970646f, 3.242424f) * Matrix.CreateTranslation(-15.523405f, 30.376686f, 74.08604f);
			this.explodePart[120] = Matrix.CreateScale(29.58239f, 14.512348f, 26.363869f) * Matrix.CreateTranslation(-27.866032f, 44.365765f, -68.80113f);
			this.explodePart[121] = Matrix.CreateScale(19.058392f, 21.744186f, 19.176102f) * Matrix.CreateTranslation(-24.78543f, 32.676807f, -68.129364f);
			this.explodePart[122] = Matrix.CreateScale(6.2164383f, 10.539967f, 5.5073166f) * Matrix.CreateTranslation(-40.301838f, 28.139194f, -83.23457f);
			this.explodePart[123] = Matrix.CreateScale(8.946846f, 15.99828f, 10.676025f) * Matrix.CreateTranslation(-40.008007f, 16.944515f, -81.650215f);
			this.explodePart[124] = Matrix.CreateScale(6.421974f, 7.0790863f, 5.7717056f) * Matrix.CreateTranslation(-39.819744f, 26.204788f, -84.757095f);
			this.explodePart[125] = Matrix.CreateScale(6.469208f, 10.796577f, 5.8637466f) * Matrix.CreateTranslation(-39.882294f, 19.67662f, -84.770096f);
			this.explodePart[126] = Matrix.CreateScale(26.566727f, 19.214363f, 46.182648f) * Matrix.CreateTranslation(-14.992972f, 47.891254f, -86.97806f);
			this.explodePart[127] = Matrix.CreateScale(25.005722f, 25.091812f, 42.25612f) * Matrix.CreateTranslation(-17.846258f, 33.91532f, -87.39686f);
			this.explodePart[128] = Matrix.CreateScale(27.560562f, 12.574024f, 30.293716f) * Matrix.CreateTranslation(-19.526176f, 57.139973f, -94.71356f);
			this.explodePart[129] = Matrix.CreateScale(22.858856f, 13.738449f, 47.53586f) * Matrix.CreateTranslation(-16.187464f, 51.757095f, -86.35681f);
			this.explodePart[130] = Matrix.CreateScale(26.706062f, 18.025383f, 40.900116f) * Matrix.CreateTranslation(-30.56873f, 47.633286f, -87.11176f);
			this.explodePart[131] = Matrix.CreateScale(27.901497f, 23.977554f, 41.54036f) * Matrix.CreateTranslation(-29.06662f, 35.898243f, -86.657646f);
			this.explodePart[132] = Matrix.CreateScale(17.664059f, 15.364987f, 21.150513f) * Matrix.CreateTranslation(-5.8724556f, 11.780821f, 116.307526f);
			this.explodePart[133] = Matrix.CreateScale(38.83422f, 22.525581f, 51.65512f) * Matrix.CreateTranslation(-28.290565f, 52.875996f, -18.364288f);
			this.explodePart[134] = Matrix.CreateScale(45.399704f, 21.26149f, 44.79889f) * Matrix.CreateTranslation(-24.015924f, 45.19824f, -19.409899f);
			this.explodePart[135] = Matrix.CreateScale(49.666443f, 22.955627f, 38.238464f) * Matrix.CreateTranslation(-19.279318f, 37.979084f, -21.436043f);
			this.explodePart[136] = Matrix.CreateScale(55.22206f, 21.852821f, 33.83525f) * Matrix.CreateTranslation(-10.828375f, 31.407667f, -21.711052f);
			this.explodePart[137] = Matrix.CreateScale(7.7280445f, 4.2341995f, 11.268887f) * Matrix.CreateTranslation(21.335548f, -4.921878f, -56.241207f);
			this.explodePart[138] = Matrix.CreateScale(8.126251f, 4.4103203f, 14.853149f) * Matrix.CreateTranslation(21.920397f, -6.2165275f, -57.941284f);
			this.explodePart[139] = Matrix.CreateScale(8.596619f, 5.097332f, 17.048313f) * Matrix.CreateTranslation(22.65899f, -8.068317f, -59.038868f);
			this.explodePart[140] = Matrix.CreateScale(8.751112f, 5.3384724f, 17.602478f) * Matrix.CreateTranslation(23.247078f, -9.96559f, -59.530174f);
			this.explodePart[141] = Matrix.CreateScale(17.732517f, 15.487602f, 42.942986f) * Matrix.CreateTranslation(-0.49205637f, 76.0922f, 45.215126f);
			this.explodePart[142] = Matrix.CreateScale(18.348724f, 19.856537f, 75.037415f) * Matrix.CreateTranslation(3.1561494f, 65.843445f, 40.991104f);
			this.explodePart[143] = Matrix.CreateScale(20.387718f, 21.487045f, 89.95314f) * Matrix.CreateTranslation(6.618634f, 54.009815f, 40.133495f);
			this.explodePart[144] = Matrix.CreateScale(18.078186f, 33.6147f, 119.35855f) * Matrix.CreateTranslation(8.895262f, 37.526615f, 27.092688f);
			this.explodePart[145] = Matrix.CreateScale(16.796936f, 13.626396f, 30.630096f) * Matrix.CreateTranslation(38.412094f, 68.859665f, 33.59689f);
			this.explodePart[146] = Matrix.CreateScale(23.845703f, 13.673912f, 44.082275f) * Matrix.CreateTranslation(37.967648f, 63.132057f, 31.72934f);
			this.explodePart[147] = Matrix.CreateScale(28.991364f, 17.507656f, 53.97577f) * Matrix.CreateTranslation(37.872345f, 56.962006f, 31.818901f);
			this.explodePart[148] = Matrix.CreateScale(24.311398f, 18.450813f, 46.265434f) * Matrix.CreateTranslation(39.479748f, 50.637665f, 40.66784f);
			this.explodePart[149] = Matrix.CreateScale(1.2020874f, 0.7558899f, 2.8245544f) * Matrix.CreateTranslation(26.45124f, 48.87663f, 14.390148f);
			this.explodePart[150] = Matrix.CreateScale(0.8041611f, 0.5078888f, 0.7917175f) * Matrix.CreateTranslation(23.628248f, 50.346558f, 4.9528885f);
			this.explodePart[151] = Matrix.CreateScale(26.532547f, 13.228119f, 29.640076f) * Matrix.CreateTranslation(40.028095f, 55.118973f, 21.525623f);
			this.explodePart[152] = Matrix.CreateScale(26.450905f, 13.933407f, 40.314255f) * Matrix.CreateTranslation(39.996544f, 51.59658f, 26.996542f);
			this.explodePart[153] = Matrix.CreateScale(22.771729f, 13.006132f, 46.850403f) * Matrix.CreateTranslation(41.086517f, 45.802235f, 34.719013f);
			this.explodePart[154] = Matrix.CreateScale(18.97654f, 15.910336f, 45.075188f) * Matrix.CreateTranslation(42.109035f, 39.363068f, 42.146614f);
			this.explodePart[155] = Matrix.CreateScale(27.203697f, 20.053356f, 57.007156f) * Matrix.CreateTranslation(22.006584f, 57.000877f, 30.079868f);
			this.explodePart[156] = Matrix.CreateScale(26.226723f, 20.103394f, 77.94928f) * Matrix.CreateTranslation(25.68183f, 49.250095f, 33.664696f);
			this.explodePart[157] = Matrix.CreateScale(26.37326f, 21.762264f, 99.3378f) * Matrix.CreateTranslation(28.400463f, 38.32013f, 42.628193f);
			this.explodePart[158] = Matrix.CreateScale(22.319344f, 27.86692f, 103.43497f) * Matrix.CreateTranslation(29.707071f, 23.968828f, 52.798588f);
			this.explodePart[159] = Matrix.CreateScale(16.413326f, 6.882597f, 13.29364f) * Matrix.CreateTranslation(29.543575f, 45.349934f, 111.46764f);
			this.explodePart[160] = Matrix.CreateScale(16.865288f, 6.652622f, 14.03598f) * Matrix.CreateTranslation(30.724401f, 44.312275f, 111.48081f);
			this.explodePart[161] = Matrix.CreateScale(16.398323f, 6.839443f, 14.299088f) * Matrix.CreateTranslation(30.885729f, 43.174065f, 111.38531f);
			this.explodePart[162] = Matrix.CreateScale(15.302731f, 7.7384453f, 12.862614f) * Matrix.CreateTranslation(31.438816f, 41.54636f, 112.1036f);
			this.explodePart[163] = Matrix.CreateScale(13.374107f, 7.956669f, 8.541534f) * Matrix.CreateTranslation(19.98711f, 61.609127f, 75.735985f);
			this.explodePart[164] = Matrix.CreateScale(15.550163f, 7.5718117f, 12.551262f) * Matrix.CreateTranslation(22.979473f, 58.190533f, 77.66637f);
			this.explodePart[165] = Matrix.CreateScale(16.701664f, 8.761715f, 15.625816f) * Matrix.CreateTranslation(24.67745f, 54.471863f, 79.31373f);
			this.explodePart[166] = Matrix.CreateScale(15.767635f, 10.892181f, 19.15535f) * Matrix.CreateTranslation(25.6757f, 49.662426f, 82.55539f);
			this.explodePart[167] = Matrix.CreateScale(30.543137f, 13.389458f, 35.527344f) * Matrix.CreateTranslation(25.912197f, 68.19394f, 56.647415f);
			this.explodePart[168] = Matrix.CreateScale(29.204048f, 13.292015f, 34.0642f) * Matrix.CreateTranslation(27.277353f, 65.77502f, 58.583f);
			this.explodePart[169] = Matrix.CreateScale(28.675354f, 14.001553f, 31.532661f) * Matrix.CreateTranslation(28.058397f, 60.923626f, 61.424995f);
			this.explodePart[170] = Matrix.CreateScale(27.248169f, 15.603378f, 30.356216f) * Matrix.CreateTranslation(29.227753f, 56.284813f, 64.6238f);
			this.explodePart[171] = Matrix.CreateScale(24.733818f, 11.746483f, 38.40564f) * Matrix.CreateTranslation(18.01219f, 75.731316f, 46.373882f);
			this.explodePart[172] = Matrix.CreateScale(25.351326f, 9.682056f, 40.698364f) * Matrix.CreateTranslation(20.542059f, 73.9257f, 46.95279f);
			this.explodePart[173] = Matrix.CreateScale(29.455803f, 10.674393f, 39.648773f) * Matrix.CreateTranslation(23.734167f, 72.271034f, 51.111965f);
			this.explodePart[174] = Matrix.CreateScale(29.693916f, 9.300003f, 36.35971f) * Matrix.CreateTranslation(25.48057f, 71.14641f, 53.9619f);
			this.explodePart[175] = Matrix.CreateScale(31.877777f, 12.282436f, 40.722137f) * Matrix.CreateTranslation(23.314026f, 68.6196f, 39.255875f);
			this.explodePart[176] = Matrix.CreateScale(29.334198f, 13.332703f, 40.646286f) * Matrix.CreateTranslation(24.93291f, 66.27664f, 41.731762f);
			this.explodePart[177] = Matrix.CreateScale(28.129364f, 12.913578f, 38.003235f) * Matrix.CreateTranslation(26.196228f, 62.650433f, 45.679344f);
			this.explodePart[178] = Matrix.CreateScale(27.405365f, 14.56274f, 35.19861f) * Matrix.CreateTranslation(27.402054f, 58.244617f, 52.157253f);
			this.explodePart[179] = Matrix.CreateScale(24.608704f, 20.153511f, 38.308365f) * Matrix.CreateTranslation(18.746414f, 83.06308f, -36.185535f);
			this.explodePart[180] = Matrix.CreateScale(24.913567f, 25.355392f, 43.303314f) * Matrix.CreateTranslation(25.036682f, 69.220276f, -29.766401f);
			this.explodePart[181] = Matrix.CreateScale(25.326141f, 26.834942f, 48.074112f) * Matrix.CreateTranslation(31.379807f, 50.6861f, -23.462214f);
			this.explodePart[182] = Matrix.CreateScale(26.961853f, 36.129234f, 42.38472f) * Matrix.CreateTranslation(38.173126f, 28.698286f, -21.909775f);
			this.explodePart[183] = Matrix.CreateScale(22.320343f, 15.139103f, 47.794617f) * Matrix.CreateTranslation(16.327667f, 84.65627f, -75.442085f);
			this.explodePart[184] = Matrix.CreateScale(22.369446f, 15.707275f, 51.384216f) * Matrix.CreateTranslation(18.636377f, 79.50743f, -75.75903f);
			this.explodePart[185] = Matrix.CreateScale(21.86863f, 15.481026f, 40.85614f) * Matrix.CreateTranslation(21.44018f, 72.819695f, -68.877686f);
			this.explodePart[186] = Matrix.CreateScale(23.407928f, 19.072754f, 31.71936f) * Matrix.CreateTranslation(24.772799f, 63.71541f, -61.710934f);
			this.explodePart[187] = Matrix.CreateScale(12.43129f, 9.600494f, 14.032719f) * Matrix.CreateTranslation(32.322395f, 84.949036f, -53.67837f);
			this.explodePart[188] = Matrix.CreateScale(14.581482f, 10.579132f, 20.693146f) * Matrix.CreateTranslation(36.719795f, 78.46515f, -50.35573f);
			this.explodePart[189] = Matrix.CreateScale(16.352829f, 12.117363f, 23.881443f) * Matrix.CreateTranslation(40.094093f, 71.48632f, -47.22887f);
			this.explodePart[190] = Matrix.CreateScale(17.741398f, 15.315109f, 26.928253f) * Matrix.CreateTranslation(43.86021f, 63.06702f, -43.834362f);
			this.explodePart[191] = Matrix.CreateScale(17.01392f, 9.551792f, 34.38224f) * Matrix.CreateTranslation(39.01611f, 68.98143f, -68.95471f);
			this.explodePart[192] = Matrix.CreateScale(16.420658f, 10.467411f, 33.897476f) * Matrix.CreateTranslation(41.08218f, 65.50758f, -67.25849f);
			this.explodePart[193] = Matrix.CreateScale(16.308083f, 11.028141f, 32.705055f) * Matrix.CreateTranslation(43.047745f, 60.3603f, -64.71068f);
			this.explodePart[194] = Matrix.CreateScale(18.076538f, 12.299503f, 29.978157f) * Matrix.CreateTranslation(45.82735f, 54.803337f, -63.04539f);
			this.explodePart[195] = Matrix.CreateScale(7.742874f, 9.1087f, 27.45758f) * Matrix.CreateTranslation(29.963203f, 84.95617f, -72.01425f);
			this.explodePart[196] = Matrix.CreateScale(11.364964f, 8.566036f, 34.850647f) * Matrix.CreateTranslation(32.66638f, 81.365295f, -74.3969f);
			this.explodePart[197] = Matrix.CreateScale(14.298721f, 10.434551f, 41.930237f) * Matrix.CreateTranslation(35.0379f, 77.13804f, -75.64045f);
			this.explodePart[198] = Matrix.CreateScale(17.892258f, 9.501492f, 44.1286f) * Matrix.CreateTranslation(37.621468f, 74.01816f, -75.211685f);
			this.explodePart[199] = Matrix.CreateScale(16.950806f, 11.778801f, 28.988014f) * Matrix.CreateTranslation(37.351242f, 67.72262f, -89.938324f);
			this.explodePart[200] = Matrix.CreateScale(17.775604f, 12.286957f, 34.366875f) * Matrix.CreateTranslation(40.64209f, 61.558136f, -88.02314f);
			this.explodePart[201] = Matrix.CreateScale(18.57172f, 15.126797f, 42.68152f) * Matrix.CreateTranslation(43.667767f, 53.960747f, -85.80877f);
			this.explodePart[202] = Matrix.CreateScale(18.828178f, 17.575432f, 41.70263f) * Matrix.CreateTranslation(45.780464f, 45.82873f, -85.154015f);
			this.explodePart[203] = Matrix.CreateScale(12.494274f, 13.0299f, 52.12349f) * Matrix.CreateTranslation(39.7224f, 77.80955f, 14.375791f);
			this.explodePart[204] = Matrix.CreateScale(14.735558f, 13.759758f, 65.85361f) * Matrix.CreateTranslation(43.01034f, 71.64808f, 7.872326f);
			this.explodePart[205] = Matrix.CreateScale(16.244305f, 15.957134f, 82.686386f) * Matrix.CreateTranslation(44.88639f, 63.352867f, -6.726181f);
			this.explodePart[206] = Matrix.CreateScale(15.854847f, 20.945831f, 65.11706f) * Matrix.CreateTranslation(46.14355f, 52.869816f, -44.597244f);
			this.explodePart[207] = Matrix.CreateScale(7.573597f, 6.1791344f, 17.556168f) * Matrix.CreateTranslation(43.47756f, 60.448208f, 1.7378135f);
			this.explodePart[208] = Matrix.CreateScale(14.932522f, 11.361305f, 66.398315f) * Matrix.CreateTranslation(36.9505f, 79.94822f, -18.310736f);
			this.explodePart[209] = Matrix.CreateScale(14.769039f, 10.57782f, 63.156433f) * Matrix.CreateTranslation(34.158688f, 84.6526f, -25.447023f);
			this.explodePart[210] = Matrix.CreateScale(1.8764629f, 1.4754791f, 5.6979675f) * Matrix.CreateTranslation(33.84909f, 84.123f, 13.684275f);
			this.explodePart[211] = Matrix.CreateScale(15.245197f, 12.790627f, 51.309814f) * Matrix.CreateTranslation(39.77407f, 74.24559f, -20.848427f);
			this.explodePart[212] = Matrix.CreateScale(16.1185f, 13.608887f, 34.80075f) * Matrix.CreateTranslation(42.127457f, 68.39919f, -25.505173f);
			this.explodePart[213] = Matrix.CreateScale(12.269737f, 13.005768f, 45.78473f) * Matrix.CreateTranslation(46.795197f, 62.554768f, 11.867269f);
			this.explodePart[214] = Matrix.CreateScale(12.8125f, 15.5860405f, 50.430878f) * Matrix.CreateTranslation(47.04791f, 55.54995f, 3.4460316f);
			this.explodePart[215] = Matrix.CreateScale(13.766247f, 15.400444f, 45.36403f) * Matrix.CreateTranslation(49.595913f, 44.342003f, -9.175696f);
			this.explodePart[216] = Matrix.CreateScale(10.7607765f, 23.155212f, 49.2191f) * Matrix.CreateTranslation(50.943462f, 30.249727f, -22.697586f);
			this.explodePart[217] = Matrix.CreateScale(10.319044f, 13.953129f, 30.944283f) * Matrix.CreateTranslation(17.095343f, 78.633385f, -5.764005f);
			this.explodePart[218] = Matrix.CreateScale(16.215809f, 15.065987f, 35.460556f) * Matrix.CreateTranslation(23.744223f, 69.73243f, -1.14709f);
			this.explodePart[219] = Matrix.CreateScale(21.422691f, 16.332703f, 36.204987f) * Matrix.CreateTranslation(30.189276f, 59.480255f, 2.3869762f);
			this.explodePart[220] = Matrix.CreateScale(24.259129f, 21.574287f, 29.895416f) * Matrix.CreateTranslation(35.670864f, 47.199833f, 1.7735596f);
			this.explodePart[221] = Matrix.CreateScale(22.707207f, 15.165401f, 77.41116f) * Matrix.CreateTranslation(19.84553f, 85.92539f, 3.2401562f);
			this.explodePart[222] = Matrix.CreateScale(20.559906f, 15.430046f, 79.448f) * Matrix.CreateTranslation(25.5477f, 80.55085f, 7.163082f);
			this.explodePart[223] = Matrix.CreateScale(17.896858f, 14.726204f, 72.80011f) * Matrix.CreateTranslation(29.87858f, 73.330986f, 9.01767f);
			this.explodePart[224] = Matrix.CreateScale(15.130314f, 21.75592f, 58.060654f) * Matrix.CreateTranslation(35.177956f, 61.32845f, 6.213442f);
			this.explodePart[225] = Matrix.CreateScale(9.317448f, 10.388329f, 44.164764f) * Matrix.CreateTranslation(17.279451f, 83.87123f, 24.817947f);
			this.explodePart[226] = Matrix.CreateScale(12.60273f, 11.61142f, 44.911148f) * Matrix.CreateTranslation(20.800854f, 78.21008f, 26.275013f);
			this.explodePart[227] = Matrix.CreateScale(14.85955f, 12.84087f, 36.144104f) * Matrix.CreateTranslation(24.595266f, 71.22392f, 23.751648f);
			this.explodePart[228] = Matrix.CreateScale(18.490997f, 12.916393f, 20.715271f) * Matrix.CreateTranslation(28.834534f, 63.725803f, 17.567667f);
			this.explodePart[229] = Matrix.CreateScale(14.297546f, 10.739288f, 34.46691f) * Matrix.CreateTranslation(47.761814f, 55.46646f, -14.542532f);
			this.explodePart[230] = Matrix.CreateScale(12.501087f, 9.310886f, 23.957394f) * Matrix.CreateTranslation(47.144016f, 60.173244f, -4.3209167f);
			this.explodePart[231] = Matrix.CreateScale(1.3402557f, 1.0970078f, 3.6485214f) * Matrix.CreateTranslation(52.63345f, 59.645325f, -19.45566f);
			this.explodePart[232] = Matrix.CreateScale(0.6336365f, 0.47517776f, 0.6898041f) * Matrix.CreateTranslation(43.285103f, 54.88142f, -22.40847f);
			this.explodePart[233] = Matrix.CreateScale(14.964256f, 11.024662f, 26.044182f) * Matrix.CreateTranslation(49.10553f, 49.94267f, -17.6106f);
			this.explodePart[234] = Matrix.CreateScale(13.880291f, 14.481522f, 21.251427f) * Matrix.CreateTranslation(50.87748f, 42.3252f, -18.022951f);
			this.explodePart[235] = Matrix.CreateScale(13.989635f, 9.112564f, 38.127876f) * Matrix.CreateTranslation(48.064438f, 42.799667f, -51.601086f);
			this.explodePart[236] = Matrix.CreateScale(13.112247f, 9.581547f, 43.44069f) * Matrix.CreateTranslation(49.67265f, 39.802807f, -44.949257f);
			this.explodePart[237] = Matrix.CreateScale(11.52277f, 10.477566f, 38.361908f) * Matrix.CreateTranslation(50.44282f, 34.933403f, -41.77121f);
			this.explodePart[238] = Matrix.CreateScale(9.278172f, 14.936214f, 34.650497f) * Matrix.CreateTranslation(51.05473f, 27.832378f, -39.88987f);
			this.explodePart[239] = Matrix.CreateScale(13.437435f, 7.8151894f, 31.489304f) * Matrix.CreateTranslation(47.285053f, 50.916756f, -41.199284f);
			this.explodePart[240] = Matrix.CreateScale(0.546402f, 0.16745377f, 0.9639435f) * Matrix.CreateTranslation(51.530617f, 52.99216f, -47.86547f);
			this.explodePart[241] = Matrix.CreateScale(12.538876f, 7.950783f, 17.96243f) * Matrix.CreateTranslation(46.835217f, 52.83833f, -34.910946f);
			this.explodePart[242] = Matrix.CreateScale(0.81292725f, 0.5229912f, 0.97180176f) * Matrix.CreateTranslation(42.795273f, 48.800446f, -48.812237f);
			this.explodePart[243] = Matrix.CreateScale(14.637314f, 11.247715f, 37.331726f) * Matrix.CreateTranslation(47.7406f, 42.272675f, -78.37468f);
			this.explodePart[244] = Matrix.CreateScale(14.551224f, 11.004181f, 45.00654f) * Matrix.CreateTranslation(47.68757f, 38.766865f, -78.23061f);
			this.explodePart[245] = Matrix.CreateScale(13.039524f, 11.284271f, 45.02868f) * Matrix.CreateTranslation(48.3209f, 33.33625f, -74.26215f);
			this.explodePart[246] = Matrix.CreateScale(10.505737f, 15.109894f, 44.14154f) * Matrix.CreateTranslation(49.479153f, 25.763039f, -66.11387f);
			this.explodePart[247] = Matrix.CreateScale(15.501465f, 18.076729f, 55.59584f) * Matrix.CreateTranslation(17.24342f, 47.213493f, -18.728651f);
			this.explodePart[248] = Matrix.CreateScale(19.07344f, 23.956207f, 111.906494f) * Matrix.CreateTranslation(18.885986f, 36.017246f, -53.785175f);
			this.explodePart[249] = Matrix.CreateScale(21.872597f, 23.643156f, 97.74243f) * Matrix.CreateTranslation(20.703297f, 22.345802f, -60.867188f);
			this.explodePart[250] = Matrix.CreateScale(21.357876f, 32.860146f, 75.089294f) * Matrix.CreateTranslation(24.401028f, 4.362201f, -66.28195f);
			this.explodePart[251] = Matrix.CreateScale(39.391464f, 21.147896f, 21.220957f) * Matrix.CreateTranslation(-26.055788f, 65.278496f, 18.181376f);
			this.explodePart[252] = Matrix.CreateScale(44.657623f, 24.156128f, 40.813293f) * Matrix.CreateTranslation(-26.05249f, 57.957508f, 10.055077f);
			this.explodePart[253] = Matrix.CreateScale(46.666214f, 24.81445f, 42.42134f) * Matrix.CreateTranslation(-24.583334f, 49.98239f, 10.817696f);
			this.explodePart[254] = Matrix.CreateScale(47.11537f, 29.04306f, 29.121216f) * Matrix.CreateTranslation(-22.836813f, 40.694653f, 19.14212f);
			this.explodePart[255] = Matrix.CreateScale(7.741117f, 17.169586f, 29.364677f) * Matrix.CreateTranslation(-5.892443f, 69.97655f, 50.818665f);
			this.explodePart[256] = Matrix.CreateScale(13.02623f, 21.462921f, 41.78871f) * Matrix.CreateTranslation(-1.9605258f, 55.109375f, 63.46292f);
			this.explodePart[257] = Matrix.CreateScale(18.837498f, 24.121225f, 57.344543f) * Matrix.CreateTranslation(2.459224f, 37.1668f, 83.61486f);
			this.explodePart[258] = Matrix.CreateScale(26.28875f, 32.921654f, 50.452896f) * Matrix.CreateTranslation(7.6025414f, 17.14613f, 88.26848f);
			this.explodePart[259] = Matrix.CreateScale(21.785652f, 12.76709f, 19.822853f) * Matrix.CreateTranslation(-18.675163f, 78.156784f, 46.27141f);
			this.explodePart[260] = Matrix.CreateScale(23.621552f, 13.5829315f, 28.694511f) * Matrix.CreateTranslation(-17.014654f, 73.26574f, 50.84252f);
			this.explodePart[261] = Matrix.CreateScale(26.143036f, 14.505119f, 31.818268f) * Matrix.CreateTranslation(-15.899082f, 67.04201f, 55.982624f);
			this.explodePart[262] = Matrix.CreateScale(29.386322f, 15.840176f, 36.768265f) * Matrix.CreateTranslation(-13.340382f, 60.84601f, 62.879925f);
			this.explodePart[263] = Matrix.CreateScale(17.185753f, 11.897919f, 54.82617f) * Matrix.CreateTranslation(14.816152f, 25.978317f, 37.08486f);
			this.explodePart[264] = Matrix.CreateScale(16.071133f, 11.34214f, 57.201965f) * Matrix.CreateTranslation(17.511051f, 22.649164f, 41.514557f);
			this.explodePart[265] = Matrix.CreateScale(14.64164f, 9.176123f, 51.122482f) * Matrix.CreateTranslation(47.843925f, 48.177853f, -50.457035f);
			this.explodePart[266] = Matrix.CreateScale(15.092056f, 9.420319f, 52.49585f) * Matrix.CreateTranslation(48.74552f, 45.787952f, -49.90796f);
			this.explodePart[267] = Matrix.CreateScale(16.391266f, 13.715513f, 54.741013f) * Matrix.CreateTranslation(19.277878f, 16.618685f, 44.758224f);
			this.explodePart[268] = Matrix.CreateScale(15.041901f, 16.667389f, 51.56897f) * Matrix.CreateTranslation(20.958267f, 9.726122f, 47.05005f);
			this.explodePart[269] = Matrix.CreateScale(13.218414f, 9.149948f, 25.82454f) * Matrix.CreateTranslation(12.153353f, 40.25004f, 50.735493f);
			this.explodePart[270] = Matrix.CreateScale(14.308964f, 9.8034935f, 43.99594f) * Matrix.CreateTranslation(12.659679f, 37.139393f, 42.080734f);
			this.explodePart[271] = Matrix.CreateScale(17.243996f, 11.530762f, 67.73486f) * Matrix.CreateTranslation(15.346716f, 30.418304f, 34.381584f);
			this.explodePart[272] = Matrix.CreateScale(1.6127682f, 0.48934174f, 1.6439819f) * Matrix.CreateTranslation(17.012499f, 33.157352f, 2.413669f);
			this.explodePart[273] = Matrix.CreateScale(15.548767f, 10.821093f, 58.922592f) * Matrix.CreateTranslation(13.241896f, 33.931587f, 36.001026f);
			this.explodePart[274] = Matrix.CreateScale(14.896866f, 13.747761f, 52.40547f) * Matrix.CreateTranslation(12.805872f, 25.778646f, -6.5224085f);
			this.explodePart[275] = Matrix.CreateScale(16.965652f, 13.667652f, 61.42038f) * Matrix.CreateTranslation(14.088617f, 20.946564f, -7.286235f);
			this.explodePart[276] = Matrix.CreateScale(17.977882f, 15.122505f, 71.915436f) * Matrix.CreateTranslation(15.940128f, 14.113246f, -6.632375f);
			this.explodePart[277] = Matrix.CreateScale(18.621193f, 17.161743f, 80.421814f) * Matrix.CreateTranslation(17.709635f, 7.0680256f, -3.3547516f);
			this.explodePart[278] = Matrix.CreateScale(16.725449f, 15.854591f, 44.894165f) * Matrix.CreateTranslation(9.453372f, 67.27686f, -24.703104f);
			this.explodePart[279] = Matrix.CreateScale(18.404945f, 16.185818f, 75.79428f) * Matrix.CreateTranslation(11.765973f, 60.872654f, -24.928204f);
			this.explodePart[280] = Matrix.CreateScale(19.021835f, 18.868439f, 95.586914f) * Matrix.CreateTranslation(13.233376f, 52.891582f, -34.824505f);
			this.explodePart[281] = Matrix.CreateScale(17.530441f, 27.630127f, 110.57385f) * Matrix.CreateTranslation(13.941008f, 39.43802f, -55.746464f);
			this.explodePart[282] = Matrix.CreateScale(17.815117f, 13.459633f, 36.33947f) * Matrix.CreateTranslation(0.23382092f, 84.86167f, -60.032494f);
			this.explodePart[283] = Matrix.CreateScale(17.677979f, 13.446976f, 43.69464f) * Matrix.CreateTranslation(3.1078553f, 79.86958f, -50.957123f);
			this.explodePart[284] = Matrix.CreateScale(18.173325f, 15.583534f, 50.95349f) * Matrix.CreateTranslation(5.6127243f, 72.813385f, -43.82808f);
			this.explodePart[285] = Matrix.CreateScale(17.552647f, 18.405746f, 43.58432f) * Matrix.CreateTranslation(8.074933f, 65.1538f, -41.68637f);
			this.explodePart[286] = Matrix.CreateScale(27.120255f, 15.602016f, 30.832985f) * Matrix.CreateTranslation(-16.188725f, 55.518326f, 62.34473f);
			this.explodePart[287] = Matrix.CreateScale(31.22409f, 15.879818f, 30.197021f) * Matrix.CreateTranslation(-14.26668f, 49.682648f, 67.759285f);
			this.explodePart[288] = Matrix.CreateScale(27.040627f, 22.582397f, 32.492325f) * Matrix.CreateTranslation(-8.965259f, 32.75258f, 80.14053f);
			this.explodePart[289] = Matrix.CreateScale(9.319027f, 3.4323196f, 6.278137f) * Matrix.CreateTranslation(-1.8389561f, 42.861496f, 93.22227f);
			this.explodePart[290] = Matrix.CreateScale(29.002373f, 16.587887f, 30.36441f) * Matrix.CreateTranslation(-12.241177f, 43.253147f, 74.033264f);
			this.explodePart[291] = Matrix.CreateScale(22.10379f, 10.601999f, 30.948448f) * Matrix.CreateTranslation(-12.036561f, 17.72539f, 95.82309f);
			this.explodePart[292] = Matrix.CreateScale(21.42044f, 12.161194f, 32.049484f) * Matrix.CreateTranslation(-6.3934493f, 12.701849f, 98.35145f);
			this.explodePart[293] = Matrix.CreateScale(24.784286f, 11.687996f, 20.426155f) * Matrix.CreateTranslation(-4.6934814f, 38.227905f, 96.80124f);
			this.explodePart[294] = Matrix.CreateScale(23.953362f, 12.720734f, 29.85862f) * Matrix.CreateTranslation(-4.047507f, 34.657814f, 97.89841f);
			this.explodePart[295] = Matrix.CreateScale(21.54126f, 14.147335f, 38.76631f) * Matrix.CreateTranslation(-2.837391f, 28.991436f, 99.14063f);
			this.explodePart[296] = Matrix.CreateScale(18.543388f, 17.77774f, 41.386894f) * Matrix.CreateTranslation(-1.4125426f, 21.864405f, 99.591194f);
			this.explodePart[297] = Matrix.CreateScale(9.404152f, 6.980793f, 13.711895f) * Matrix.CreateTranslation(-7.936737f, 23.294392f, 117.647835f);
			this.explodePart[298] = Matrix.CreateScale(14.768234f, 8.163692f, 18.60865f) * Matrix.CreateTranslation(-7.6192017f, 18.92338f, 117.247986f);
			this.explodePart[299] = Matrix.CreateScale(11.479399f, 9.450093f, 8.831047f) * Matrix.CreateTranslation(-18.490976f, 29.10902f, 94.947296f);
			this.explodePart[300] = Matrix.CreateScale(16.328583f, 10.693844f, 17.06247f) * Matrix.CreateTranslation(-16.334795f, 23.506351f, 88.361496f);
			this.explodePart[301] = Matrix.CreateScale(18.57172f, 15.126797f, 42.68152f) * Matrix.CreateTranslation(43.667767f, 53.960747f, -85.80877f);
			this.explodePart[302] = Matrix.CreateScale(18.828178f, 17.575432f, 41.70263f) * Matrix.CreateTranslation(45.780464f, 45.82873f, -85.154015f);
			this.explodePart[303] = Matrix.CreateScale(12.494274f, 13.0299f, 52.12349f) * Matrix.CreateTranslation(39.7224f, 77.80955f, 14.375791f);
			this.explodePart[304] = Matrix.CreateScale(14.735558f, 13.759758f, 65.85361f) * Matrix.CreateTranslation(43.01034f, 71.64808f, 7.872326f);
			this.explodePart[305] = Matrix.CreateScale(16.244305f, 15.957134f, 82.686386f) * Matrix.CreateTranslation(44.88639f, 63.352867f, -6.726181f);
			this.explodePart[306] = Matrix.CreateScale(15.854847f, 20.945831f, 65.11706f) * Matrix.CreateTranslation(46.14355f, 52.869816f, -44.597244f);
			this.explodePart[307] = Matrix.CreateScale(7.573597f, 6.1791344f, 17.556168f) * Matrix.CreateTranslation(43.47756f, 60.448208f, 1.7378135f);
			this.explodePart[308] = Matrix.CreateScale(14.932522f, 11.361305f, 66.398315f) * Matrix.CreateTranslation(36.9505f, 79.94822f, -18.310736f);
			this.explodePart[309] = Matrix.CreateScale(14.769039f, 10.57782f, 63.156433f) * Matrix.CreateTranslation(34.158688f, 84.6526f, -25.447023f);
			this.explodePart[310] = Matrix.CreateScale(1.8764629f, 1.4754791f, 5.6979675f) * Matrix.CreateTranslation(33.84909f, 84.123f, 13.684275f);
			this.explodePart[311] = Matrix.CreateScale(15.245197f, 12.790627f, 51.309814f) * Matrix.CreateTranslation(39.77407f, 74.24559f, -20.848427f);
			this.explodePart[312] = Matrix.CreateScale(16.1185f, 13.608887f, 34.80075f) * Matrix.CreateTranslation(42.127457f, 68.39919f, -25.505173f);
			this.explodePart[313] = Matrix.CreateScale(12.269737f, 13.005768f, 45.78473f) * Matrix.CreateTranslation(46.795197f, 62.554768f, 11.867269f);
			this.explodePart[314] = Matrix.CreateScale(12.8125f, 15.5860405f, 50.430878f) * Matrix.CreateTranslation(47.04791f, 55.54995f, 3.4460316f);
			this.explodePart[315] = Matrix.CreateScale(13.766247f, 15.400444f, 45.36403f) * Matrix.CreateTranslation(49.595913f, 44.342003f, -9.175696f);
			this.explodePart[316] = Matrix.CreateScale(10.7607765f, 23.155212f, 49.2191f) * Matrix.CreateTranslation(50.943462f, 30.249727f, -22.697586f);
			this.explodePart[317] = Matrix.CreateScale(10.319044f, 13.953129f, 30.944283f) * Matrix.CreateTranslation(17.095343f, 78.633385f, -5.764005f);
			this.explodePart[318] = Matrix.CreateScale(16.215809f, 15.065987f, 35.460556f) * Matrix.CreateTranslation(23.744223f, 69.73243f, -1.14709f);
			this.explodePart[319] = Matrix.CreateScale(21.422691f, 16.332703f, 36.204987f) * Matrix.CreateTranslation(30.189276f, 59.480255f, 2.3869762f);
			this.explodePart[320] = Matrix.CreateScale(24.259129f, 21.574287f, 29.895416f) * Matrix.CreateTranslation(35.670864f, 47.199833f, 1.7735596f);
			this.explodePart[321] = Matrix.CreateScale(22.707207f, 15.165401f, 77.41116f) * Matrix.CreateTranslation(19.84553f, 85.92539f, 3.2401562f);
			this.explodePart[322] = Matrix.CreateScale(20.559906f, 15.430046f, 79.448f) * Matrix.CreateTranslation(25.5477f, 80.55085f, 7.163082f);
			this.explodePart[323] = Matrix.CreateScale(17.896858f, 14.726204f, 72.80011f) * Matrix.CreateTranslation(29.87858f, 73.330986f, 9.01767f);
			this.explodePart[324] = Matrix.CreateScale(15.130314f, 21.75592f, 58.060654f) * Matrix.CreateTranslation(35.177956f, 61.32845f, 6.213442f);
			this.explodePart[325] = Matrix.CreateScale(9.317448f, 10.388329f, 44.164764f) * Matrix.CreateTranslation(17.279451f, 83.87123f, 24.817947f);
			this.explodePart[326] = Matrix.CreateScale(12.60273f, 11.61142f, 44.911148f) * Matrix.CreateTranslation(20.800854f, 78.21008f, 26.275013f);
			this.explodePart[327] = Matrix.CreateScale(14.85955f, 12.84087f, 36.144104f) * Matrix.CreateTranslation(24.595266f, 71.22392f, 23.751648f);
			this.explodePart[328] = Matrix.CreateScale(18.490997f, 12.916393f, 20.715271f) * Matrix.CreateTranslation(28.834534f, 63.725803f, 17.567667f);
			this.explodePart[329] = Matrix.CreateScale(14.297546f, 10.739288f, 34.46691f) * Matrix.CreateTranslation(47.761814f, 55.46646f, -14.542532f);
			this.explodePart[330] = Matrix.CreateScale(12.501087f, 9.310886f, 23.957394f) * Matrix.CreateTranslation(47.144016f, 60.173244f, -4.3209167f);
			this.explodePart[331] = Matrix.CreateScale(1.3402557f, 1.0970078f, 3.6485214f) * Matrix.CreateTranslation(52.63345f, 59.645325f, -19.45566f);
			this.explodePart[332] = Matrix.CreateScale(0.6336365f, 0.47517776f, 0.6898041f) * Matrix.CreateTranslation(43.285103f, 54.88142f, -22.40847f);
			this.explodePart[333] = Matrix.CreateScale(14.964256f, 11.024662f, 26.044182f) * Matrix.CreateTranslation(49.10553f, 49.94267f, -17.6106f);
			this.explodePart[334] = Matrix.CreateScale(13.880291f, 14.481522f, 21.251427f) * Matrix.CreateTranslation(50.87748f, 42.3252f, -18.022951f);
			this.explodePart[335] = Matrix.CreateScale(13.989635f, 9.112564f, 38.127876f) * Matrix.CreateTranslation(48.064438f, 42.799667f, -51.601086f);
			this.explodePart[336] = Matrix.CreateScale(13.112247f, 9.581547f, 43.44069f) * Matrix.CreateTranslation(49.67265f, 39.802807f, -44.949257f);
			this.explodePart[337] = Matrix.CreateScale(11.52277f, 10.477566f, 38.361908f) * Matrix.CreateTranslation(50.44282f, 34.933403f, -41.77121f);
			this.explodePart[338] = Matrix.CreateScale(9.278172f, 14.936214f, 34.650497f) * Matrix.CreateTranslation(51.05473f, 27.832378f, -39.88987f);
			this.explodePart[339] = Matrix.CreateScale(13.437435f, 7.8151894f, 31.489304f) * Matrix.CreateTranslation(47.285053f, 50.916756f, -41.199284f);
			this.explodePart[340] = Matrix.CreateScale(0.546402f, 0.16745377f, 0.9639435f) * Matrix.CreateTranslation(51.530617f, 52.99216f, -47.86547f);
			this.explodePart[341] = Matrix.CreateScale(12.538876f, 7.950783f, 17.96243f) * Matrix.CreateTranslation(46.835217f, 52.83833f, -34.910946f);
			this.explodePart[342] = Matrix.CreateScale(0.81292725f, 0.5229912f, 0.97180176f) * Matrix.CreateTranslation(42.795273f, 48.800446f, -48.812237f);
			this.explodePart[343] = Matrix.CreateScale(14.637314f, 11.247715f, 37.331726f) * Matrix.CreateTranslation(47.7406f, 42.272675f, -78.37468f);
			this.explodePart[344] = Matrix.CreateScale(14.551224f, 11.004181f, 45.00654f) * Matrix.CreateTranslation(47.68757f, 38.766865f, -78.23061f);
			this.explodePart[345] = Matrix.CreateScale(13.039524f, 11.284271f, 45.02868f) * Matrix.CreateTranslation(48.3209f, 33.33625f, -74.26215f);
			this.explodePart[346] = Matrix.CreateScale(10.505737f, 15.109894f, 44.14154f) * Matrix.CreateTranslation(49.479153f, 25.763039f, -66.11387f);
			this.explodePart[347] = Matrix.CreateScale(15.501465f, 18.076729f, 55.59584f) * Matrix.CreateTranslation(17.24342f, 47.213493f, -18.728651f);
			this.explodePart[348] = Matrix.CreateScale(19.07344f, 23.956207f, 111.906494f) * Matrix.CreateTranslation(18.885986f, 36.017246f, -53.785175f);
			this.explodePart[349] = Matrix.CreateScale(21.872597f, 23.643156f, 97.74243f) * Matrix.CreateTranslation(20.703297f, 22.345802f, -60.867188f);
			this.explodePart[350] = Matrix.CreateScale(21.357876f, 32.860146f, 75.089294f) * Matrix.CreateTranslation(24.401028f, 4.362201f, -66.28195f);
			this.explodePart[351] = Matrix.CreateScale(39.391464f, 21.147896f, 21.220957f) * Matrix.CreateTranslation(-26.055788f, 65.278496f, 18.181376f);
			this.explodePart[352] = Matrix.CreateScale(44.657623f, 24.156128f, 40.813293f) * Matrix.CreateTranslation(-26.05249f, 57.957508f, 10.055077f);
			this.explodePart[353] = Matrix.CreateScale(46.666214f, 24.81445f, 42.42134f) * Matrix.CreateTranslation(-24.583334f, 49.98239f, 10.817696f);
			this.explodePart[354] = Matrix.CreateScale(47.11537f, 29.04306f, 29.121216f) * Matrix.CreateTranslation(-22.836813f, 40.694653f, 19.14212f);
			this.explodePart[355] = Matrix.CreateScale(7.741117f, 17.169586f, 29.364677f) * Matrix.CreateTranslation(-5.892443f, 69.97655f, 50.818665f);
			this.explodePart[356] = Matrix.CreateScale(13.02623f, 21.462921f, 41.78871f) * Matrix.CreateTranslation(-1.9605258f, 55.109375f, 63.46292f);
			this.explodePart[357] = Matrix.CreateScale(18.837498f, 24.121225f, 57.344543f) * Matrix.CreateTranslation(2.459224f, 37.1668f, 83.61486f);
			this.explodePart[358] = Matrix.CreateScale(26.28875f, 32.921654f, 50.452896f) * Matrix.CreateTranslation(7.6025414f, 17.14613f, 88.26848f);
			this.explodePart[359] = Matrix.CreateScale(21.785652f, 12.76709f, 19.822853f) * Matrix.CreateTranslation(-18.675163f, 78.156784f, 46.27141f);
			this.explodePart[360] = Matrix.CreateScale(23.621552f, 13.5829315f, 28.694511f) * Matrix.CreateTranslation(-17.014654f, 73.26574f, 50.84252f);
			this.explodePart[361] = Matrix.CreateScale(26.143036f, 14.505119f, 31.818268f) * Matrix.CreateTranslation(-15.899082f, 67.04201f, 55.982624f);
			this.explodePart[362] = Matrix.CreateScale(29.386322f, 15.840176f, 36.768265f) * Matrix.CreateTranslation(-13.340382f, 60.84601f, 62.879925f);
			this.explodePart[363] = Matrix.CreateScale(17.185753f, 11.897919f, 54.82617f) * Matrix.CreateTranslation(14.816152f, 25.978317f, 37.08486f);
			this.explodePart[364] = Matrix.CreateScale(16.071133f, 11.34214f, 57.201965f) * Matrix.CreateTranslation(17.511051f, 22.649164f, 41.514557f);
			this.explodePart[365] = Matrix.CreateScale(14.64164f, 9.176123f, 51.122482f) * Matrix.CreateTranslation(47.843925f, 48.177853f, -50.457035f);
			this.explodePart[366] = Matrix.CreateScale(15.092056f, 9.420319f, 52.49585f) * Matrix.CreateTranslation(48.74552f, 45.787952f, -49.90796f);
			this.explodePart[367] = Matrix.CreateScale(16.391266f, 13.715513f, 54.741013f) * Matrix.CreateTranslation(19.277878f, 16.618685f, 44.758224f);
			this.explodePart[368] = Matrix.CreateScale(15.041901f, 16.667389f, 51.56897f) * Matrix.CreateTranslation(20.958267f, 9.726122f, 47.05005f);
			this.explodePart[369] = Matrix.CreateScale(13.218414f, 9.149948f, 25.82454f) * Matrix.CreateTranslation(12.153353f, 40.25004f, 50.735493f);
			this.explodePart[370] = Matrix.CreateScale(14.308964f, 9.8034935f, 43.99594f) * Matrix.CreateTranslation(12.659679f, 37.139393f, 42.080734f);
			this.explodePart[371] = Matrix.CreateScale(17.243996f, 11.530762f, 67.73486f) * Matrix.CreateTranslation(15.346716f, 30.418304f, 34.381584f);
			this.explodePart[372] = Matrix.CreateScale(1.6127682f, 0.48934174f, 1.6439819f) * Matrix.CreateTranslation(17.012499f, 33.157352f, 2.413669f);
			this.explodePart[373] = Matrix.CreateScale(15.548767f, 10.821093f, 58.922592f) * Matrix.CreateTranslation(13.241896f, 33.931587f, 36.001026f);
			this.explodePart[374] = Matrix.CreateScale(14.896866f, 13.747761f, 52.40547f) * Matrix.CreateTranslation(12.805872f, 25.778646f, -6.5224085f);
			this.explodePart[375] = Matrix.CreateScale(16.965652f, 13.667652f, 61.42038f) * Matrix.CreateTranslation(14.088617f, 20.946564f, -7.286235f);
			this.explodePart[376] = Matrix.CreateScale(17.977882f, 15.122505f, 71.915436f) * Matrix.CreateTranslation(15.940128f, 14.113246f, -6.632375f);
			this.explodePart[377] = Matrix.CreateScale(18.621193f, 17.161743f, 80.421814f) * Matrix.CreateTranslation(17.709635f, 7.0680256f, -3.3547516f);
			this.explodePart[378] = Matrix.CreateScale(16.725449f, 15.854591f, 44.894165f) * Matrix.CreateTranslation(9.453372f, 67.27686f, -24.703104f);
			this.explodePart[379] = Matrix.CreateScale(18.404945f, 16.185818f, 75.79428f) * Matrix.CreateTranslation(11.765973f, 60.872654f, -24.928204f);
			this.explodePart[380] = Matrix.CreateScale(19.021835f, 18.868439f, 95.586914f) * Matrix.CreateTranslation(13.233376f, 52.891582f, -34.824505f);
			this.explodePart[381] = Matrix.CreateScale(17.530441f, 27.630127f, 110.57385f) * Matrix.CreateTranslation(13.941008f, 39.43802f, -55.746464f);
			this.explodePart[382] = Matrix.CreateScale(17.815117f, 13.459633f, 36.33947f) * Matrix.CreateTranslation(0.23382092f, 84.86167f, -60.032494f);
			this.explodePart[383] = Matrix.CreateScale(17.677979f, 13.446976f, 43.69464f) * Matrix.CreateTranslation(3.1078553f, 79.86958f, -50.957123f);
			this.explodePart[384] = Matrix.CreateScale(18.173325f, 15.583534f, 50.95349f) * Matrix.CreateTranslation(5.6127243f, 72.813385f, -43.82808f);
			this.explodePart[385] = Matrix.CreateScale(17.552647f, 18.405746f, 43.58432f) * Matrix.CreateTranslation(8.074933f, 65.1538f, -41.68637f);
			this.explodePart[386] = Matrix.CreateScale(27.120255f, 15.602016f, 30.832985f) * Matrix.CreateTranslation(-16.188725f, 55.518326f, 62.34473f);
			this.explodePart[387] = Matrix.CreateScale(31.22409f, 15.879818f, 30.197021f) * Matrix.CreateTranslation(-14.26668f, 49.682648f, 67.759285f);
			this.explodePart[388] = Matrix.CreateScale(27.040627f, 22.582397f, 32.492325f) * Matrix.CreateTranslation(-8.965259f, 32.75258f, 80.14053f);
			this.explodePart[389] = Matrix.CreateScale(9.319027f, 3.4323196f, 6.278137f) * Matrix.CreateTranslation(-1.8389561f, 42.861496f, 93.22227f);
			int num2 = this.randomSeed;
			for (int i = 0; i < num; i++)
			{
				this.bloody.dupe[i] = new explodeDupe(num2);
				num2++;
			}
			this.bloody.max = num;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000FC5F4 File Offset: 0x000FA7F4
		public void initExplode()
		{
			float num = -100f;
			Random random = new Random();
			if (this.sc.bossexplodechoice == 1)
			{
				num = (float)random.Next(-96000, -36000) / 100f;
			}
			if (this.sc.bossexplodechoice == 2)
			{
				num = (float)random.Next(-21000, -3000) / 100f;
			}
			if (this.sc.bossexplodechoice == 3)
			{
				num = (float)random.Next(1500, 26000) / 100f;
			}
			this.sc.bossexplodechoice++;
			if (this.sc.bossexplodechoice > 3)
			{
				this.sc.bossexplodechoice = 1;
			}
			float num2 = (float)random.Next(90, 195) / 100f;
			num2 = 3f;
			float num3 = (float)random.Next(250, 400) / 100f;
			Vector3 vector = new Vector3((float)random.Next(-40, 40) / 100f, (float)random.Next(-20, 20) / 100f, (float)random.Next(-40, 40) / 100f);
			for (int i = 0; i < this.explodePart.Length; i++)
			{
				num3 = (float)random.Next(410, 490) / 100f;
				this.explodePart[i] = this.explodePart[i] * Matrix.CreateScale(this.cuttyScale * 1f) * Matrix.CreateRotationY(this.cuttyRot) * Matrix.CreateTranslation(new Vector3(this.cuttyPos.X, this.cuttyPos.Y + 20f, this.cuttyPos.Z));
				Vector3 vector2 = Vector3.Transform(Vector3.Zero, this.explodePart[i]) - new Vector3(this.cuttyPos.X, num, this.cuttyPos.Z);
				this.bloody.dupe[i].createState(this.explodePart[i], (vector + Vector3.Normalize(vector2)) * num3, this.cuttyScale, num2);
			}
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000FC85C File Offset: 0x000FAA5C
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
					if (this.sc.myTimer % 4f == 0f && this.bloody.dupe[i].age > 2300)
					{
						if (Math.Abs(this.bloody.dupe[i].mypos.X) > 7000f || Math.Abs(this.bloody.dupe[i].mypos.Z) > 7000f)
						{
							this.bloody.dupe[i].move = 0;
						}
						this.bloody.dupe[i].scalePart *= 0.97f;
						this.bloody.dupe[i].myscale *= 0.97f;
						if (this.bloody.dupe[i].age > 2750)
						{
							this.bloody.dupe[i].move = 0;
						}
					}
				}
			}
			if (this.bloody.tempindex < 4)
			{
				this.bloody.index = 0;
				this.bloody.max = 0;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000FCA50 File Offset: 0x000FAC50
		private void drawExplode()
		{
			int tempindex = this.bloody.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = this.bloody.model.Meshes[0].MeshParts[0];
			this.bloody.buffer.SetData<Princess.instancedObject>(this.bloody.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["fastShader"];
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(this.bloody.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000FCBB0 File Offset: 0x000FADB0
		private void energize()
		{
			Vector3 vector = new Vector3((float)this.rr.Next(-2900, 2900), (float)this.rr.Next(20, 60), (float)this.rr.Next(-2900, 2900)) + new Vector3(3000f, 10f, 3000f);
			Vector3 vector2 = Vector3.Normalize(this.cuttyPos - vector) * Vector3.Distance(this.cuttyPos, vector) * (float)this.rr.Next(10, 60) / 100f;
			this.dots.AddParticle(vector, vector2);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000FCC68 File Offset: 0x000FAE68
		private void drawPrincess(ref Texture2D myTexture, Matrix viewWorld, int tech)
		{
			if (!this.pigModelLoaded)
			{
				return;
			}
			this.pigModel.Meshes[this.sc.bossIndex].MeshParts[0].Effect = this.pigSkin;
			this.pigSkin.Parameters["darkness"].SetValue(MathHelper.Lerp(0f, 1.2f, this.sc.darkness));
			this.pigSkin.Parameters["gDiffuse"].SetValue(myTexture);
			this.pigSkin.Parameters["projectorView"].SetValue(viewWorld);
			this.pigSkin.Parameters["amb"].SetValue(new Vector3(0.4f, 0.4f, 0.4f));
			this.pigSkin.Parameters["diff"].SetValue(new Vector3(0.7f, 0.7f, 0.7f));
			this.pigSkin.Parameters["LightDirection"].SetValue(new Vector3(0.7f, -0.7f, 0f));
			this.pigSkin.Parameters["View"].SetValue(this.view);
			this.pigSkin.Parameters["Projection"].SetValue(this.proj);
			this.pigSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
			this.pigSkin.CurrentTechnique = this.pigSkin.Techniques[tech];
			this.pigModel.Meshes[this.sc.bossIndex].Draw();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000FCE48 File Offset: 0x000FB048
		private void drawCuttySkel()
		{
			this.sc.bossAll.Meshes[0].MeshParts[0].Effect = this.pigSkin;
			this.pigSkin.Parameters["View"].SetValue(this.view);
			this.pigSkin.Parameters["Projection"].SetValue(this.proj);
			this.pigSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
			this.pigSkin.CurrentTechnique = this.pigSkin.Techniques["skeleton"];
			this.sc.bossAll.Meshes[0].Draw();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000FCF24 File Offset: 0x000FB124
		private void drawHeart(float dist)
		{
			float num = ((float)Math.Cos((double)(this.sc.myTimer / 60f)) * 3f + 4f) / 100f;
			float num2 = 1.8f;
			if (this.death1 || this.heartattack > 0f)
			{
				num = 0.16f;
				num2 = 2.2f;
			}
			this.heartTimer += 0.08f + num;
			float num3 = MathHelper.Lerp(0.1f, num2, 1f - (float)Math.Abs(Math.Sin((double)this.heartTimer)));
			this.a.boneTransforms[17] = Matrix.CreateScale(num3) * this.heartMatrix;
			this.a.UpdateWorldTransforms(this.cuttyTrans, this.a.boneTransforms);
			this.heartattack -= 1f;
			if (this.heartattack < 60f)
			{
				this.heartVol = false;
			}
			if (this.heartattack <= 0f && !this.death1)
			{
				this.sc.bossAll.Meshes[8].MeshParts[0].Effect = this.pigSkin;
				this.pigSkin.Parameters["fade"].SetValue(dist);
				this.pigSkin.Parameters["slide"].SetValue(this.sc.myTimer * 0.006f);
				this.pigSkin.Parameters["View"].SetValue(this.view);
				this.pigSkin.Parameters["Projection"].SetValue(this.proj);
				this.pigSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
				this.pigSkin.CurrentTechnique = this.pigSkin.Techniques["heart"];
				this.sc.bossAll.Meshes[8].Draw();
				return;
			}
			this.sc.bossAll.Meshes[8].MeshParts[0].Effect = this.pigSkin;
			this.pigSkin.Parameters["slide"].SetValue(this.sc.myTimer * 0.01f);
			this.pigSkin.Parameters["pulse"].SetValue(0.2f + (float)Math.Abs(Math.Sin((double)(this.sc.myTimer / 4f))));
			this.pigSkin.Parameters["View"].SetValue(this.view);
			this.pigSkin.Parameters["Projection"].SetValue(this.proj);
			this.pigSkin.Parameters["Bones"].SetValue(this.a.skinTransforms);
			this.pigSkin.CurrentTechnique = this.pigSkin.Techniques["heartattack"];
			this.sc.bossAll.Meshes[8].Draw();
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000FD288 File Offset: 0x000FB488
		private void DrawModel(Model model, Matrix world)
		{
			Vector3 vector = new Vector3(0.52f, 0.52f, 0.52f);
			Vector3 vector2 = new Vector3(0.85f, 0.85f, 0.85f);
			foreach (Effect effect in model.Meshes[0].Effects)
			{
				BasicEffect basicEffect = (BasicEffect)effect;
				basicEffect.World = world;
				basicEffect.View = this.view;
				basicEffect.Projection = this.proj;
				basicEffect.LightingEnabled = true;
				basicEffect.DirectionalLight0.Enabled = true;
				basicEffect.DirectionalLight0.Direction = new Vector3(0.2f, -0.5f, 0.1f);
				basicEffect.AmbientLightColor = vector2;
				basicEffect.DirectionalLight0.DiffuseColor = vector;
				basicEffect.PreferPerPixelLighting = false;
			}
			model.Meshes[0].Draw();
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000FD390 File Offset: 0x000FB590
		private void shockWave(ref float[,] heights)
		{
			this.shockTimer -= 1f;
			if (this.shockTimer <= 0f)
			{
				this.jumpVol = false;
			}
			this.shockRadius += (float)this.rr.Next(8, 15);
			if (!this.shockHasHit && Vector3.Distance(this.playerpos, this.cuttyPos) < this.shockRadius)
			{
				this.shockHasHit = true;
				this.shockHit = true;
			}
			Vector3 up = Vector3.Up;
			float num = 2f * this.shockRadius * 3.14f;
			float num2 = 120f / num;
			if (num2 < 0.02f)
			{
				num2 = 0.02f;
			}
			float num3 = (float)(this.rr.Next(2100, 9000) / 5);
			Vector3 vector = new Vector3(this.shockRadius, 0f, 0f);
			for (float num4 = 0f; num4 < 6.28f; num4 += num2 + (float)this.rr.Next(0, 30) / 500f)
			{
				Matrix.CreateRotationY(num4, out this.m1);
				Vector3.Transform(ref vector, ref this.m1, out this.v1);
				this.v1.X = this.v1.X + (this.cuttyPos.X + (float)this.rr.Next(-12, 12));
				this.v1.Z = this.v1.Z + (this.cuttyPos.Z + (float)this.rr.Next(-12, 12));
				Princess.GetHeightFast(ref heights, new Vector2(this.v1.X, this.v1.Z), out this.v1.Y);
				Vector3 vector2 = new Vector3(this.v1.X, 0f, this.v1.Z) - new Vector3(this.cuttyPos.X, 0f, this.cuttyPos.Z);
				Vector3.Normalize(ref vector2, out vector2);
				int num5 = this.rr.Next(1, 3);
				vector2.Y = (float)this.rr.Next(80, 130) / 100f;
				for (int i = 0; i < num5; i++)
				{
					int num6 = this.rr.Next(0, 90);
					Vector3 vector3 = new Vector3((float)this.rr.Next(-num6, num6) / 100f, 0f, (float)this.rr.Next(-num6, num6) / 100f);
					this.rocks.AddParticle(this.v1 + vector3, vector2 * (float)this.rr.Next(0, (int)num3) / 10f);
				}
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000FD668 File Offset: 0x000FB868
		public void drawCanvas()
		{
			this.canvas.Meshes[0].MeshParts[0].Effect = this.simple;
			this.simple.Parameters["world"].SetValue(Matrix.CreateTranslation(3000f, 0.5f, 3000f));
			this.simple.Parameters["Projection"].SetValue(this.proj);
			this.simple.Parameters["View"].SetValue(this.view);
			this.simple.CurrentTechnique = this.simple.Techniques[0];
			this.canvas.Meshes[0].Draw();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000FD73C File Offset: 0x000FB93C
		public void drawBurst()
		{
			if (this.burstfade < 0f)
			{
				return;
			}
			Vector3 vector = new Vector3(this.cuttyPos.X, this.burstY, this.cuttyPos.Z);
			if (this.playerpos != vector)
			{
				vector = Vector3.Normalize(this.playerpos - vector);
				vector.Y = 0f;
				vector = vector * 250f + new Vector3(this.cuttyPos.X, this.burstY, this.cuttyPos.Z);
			}
			Matrix matrix = Matrix.CreateBillboard(vector, this.campos, this.view.Up, new Vector3?(this.view.Forward));
			if (this.explodeTimer <= 5)
			{
				if (this.explodeTimer >= -60)
				{
					this.burstscale += 0.03f;
					this.burstfade += 0.05f;
					if (this.burstfade > 1f)
					{
						this.burstfade = 1f;
					}
					if (this.burstscale > 1f)
					{
						this.burstscale = 1f;
					}
				}
				if (this.explodeTimer < -60)
				{
					this.burstY -= 0.15f;
					this.burstfade *= 0.99f;
					this.burstfade -= 0.0001f;
					this.burstscale -= 0.008f;
					if (this.burstscale < 0f)
					{
						this.burstscale = 0f;
					}
				}
			}
			this.burstMatrix *= Matrix.CreateRotationZ(0.02f);
			this.grid.Meshes[0].MeshParts[0].Effect = this.burst;
			this.burst.Parameters["fader"].SetValue(this.burstfade);
			this.burst.Parameters["world"].SetValue(Matrix.CreateScale(this.burstscale) * this.burstMatrix * matrix);
			this.burst.Parameters["Projection"].SetValue(this.proj);
			this.burst.Parameters["View"].SetValue(this.view);
			this.burst.CurrentTechnique = this.burst.Techniques[0];
			this.grid.Meshes[0].Draw();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000FD9E0 File Offset: 0x000FBBE0
		public void drawGuts()
		{
			this.gutsModel.Meshes[0].MeshParts[0].Effect = this.gutsEffect;
			this.gutsEffect.Parameters["world"].SetValue(Matrix.CreateTranslation(3000f, 0f, 3000f));
			this.gutsEffect.Parameters["Projection"].SetValue(this.proj);
			this.gutsEffect.Parameters["View"].SetValue(this.view);
			this.gutsEffect.CurrentTechnique = this.gutsEffect.Techniques[0];
			this.gutsModel.Meshes[0].Draw();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000FDAB4 File Offset: 0x000FBCB4
		public void drawTunnel()
		{
			this.tunnel.Meshes[0].MeshParts[0].Effect = this.tunnelfx;
			this.tunnelfx.Parameters["u"].SetValue(this.sc.myTimer * 0.011f);
			this.tunnelfx.Parameters["fade"].SetValue(this.cuttyRollFade);
			this.tunnelfx.Parameters["world"].SetValue(Matrix.CreateRotationY(this.cuttyRot - 1.57f) * Matrix.CreateTranslation(3000f, 0f, 3000f));
			this.tunnelfx.Parameters["Projection"].SetValue(this.proj);
			this.tunnelfx.Parameters["View"].SetValue(this.view);
			this.tunnelfx.CurrentTechnique = this.tunnelfx.Techniques[0];
			this.tunnel.Meshes[0].Draw();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000FDBEC File Offset: 0x000FBDEC
		private void DrawInstance(Princess.shell shell, string tech)
		{
			int tempindex = shell.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = shell.model.Meshes[0].MeshParts[0];
			shell.buffer.SetData<Princess.instancedObject>(shell.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.Parameters["darkness"].SetValue(MathHelper.Lerp(0.3f, 1.2f, this.sc.darkness));
			effect.Parameters["amb"].SetValue(new Vector3(0.4f, 0.4f, 0.4f));
			effect.Parameters["diff"].SetValue(new Vector3(0.7f, 0.7f, 0.7f));
			effect.Parameters["LightDirection"].SetValue(new Vector3(0.7f, -0.8f, 0f));
			effect.CurrentTechnique = effect.Techniques[tech];
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(shell.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000FDDE0 File Offset: 0x000FBFE0
		public void DrawPuke(float fader)
		{
			int tempindex = this.puke1.tempindex;
			if (tempindex < 1)
			{
				return;
			}
			ModelMeshPart modelMeshPart = this.puke1.model.Meshes[0].MeshParts[0];
			this.puke1.buffer.SetData<Princess.instancedObject>(this.puke1.displayList, 0, tempindex, SetDataOptions.Discard);
			Effect effect = modelMeshPart.Effect;
			effect.CurrentTechnique = effect.Techniques["fastShader2"];
			effect.Parameters["fader"].SetValue(fader);
			effect.Parameters["View"].SetValue(this.view);
			effect.Parameters["Projection"].SetValue(this.proj);
			effect.CurrentTechnique.Passes[0].Apply();
			this._vertexBufferBindings[0] = new VertexBufferBinding(modelMeshPart.VertexBuffer, modelMeshPart.VertexOffset, 0);
			this._vertexBufferBindings[1] = new VertexBufferBinding(this.puke1.buffer, 0, 1);
			this.sc.GraphicsDevice.SetVertexBuffers(this._vertexBufferBindings);
			this.sc.GraphicsDevice.Indices = modelMeshPart.IndexBuffer;
			this.sc.GraphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, 0, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount, tempindex);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000FDF58 File Offset: 0x000FC158
		private static void GetHeightFast(ref float[,] heights, Vector2 position, out float height)
		{
			int num = (int)MathHelper.Clamp(position.X / Princess.unit, 0f, (float)(Princess.bitmap - 2));
			int num2 = (int)MathHelper.Clamp(position.Y / Princess.unit, 0f, (float)(Princess.bitmap - 2));
			height = heights[num, num2];
		}

		// Token: 0x04001085 RID: 4229
		public ParticleSystem rocks;

		// Token: 0x04001086 RID: 4230
		public ParticleSystem dots;

		// Token: 0x04001087 RID: 4231
		public ushort heartHit;

		// Token: 0x04001088 RID: 4232
		public static int speeches = 0;

		// Token: 0x04001089 RID: 4233
		public static int xcoord;

		// Token: 0x0400108A RID: 4234
		public static int ycoord;

		// Token: 0x0400108B RID: 4235
		public static int cuttyCount = 0;

		// Token: 0x0400108C RID: 4236
		public static bool allplayersReady = false;

		// Token: 0x0400108D RID: 4237
		public static bool cuttyDoneSpeech = false;

		// Token: 0x0400108E RID: 4238
		public static byte bit = 0;

		// Token: 0x0400108F RID: 4239
		public static byte uvIndex;

		// Token: 0x04001090 RID: 4240
		public static int whichPigTalks;

		// Token: 0x04001091 RID: 4241
		public static bool someoneTalking = false;

		// Token: 0x04001092 RID: 4242
		public static float shortestDistance = 20000f;

		// Token: 0x04001093 RID: 4243
		public static int bitmap;

		// Token: 0x04001094 RID: 4244
		public static float unit;

		// Token: 0x04001095 RID: 4245
		public static float Grid;

		// Token: 0x04001096 RID: 4246
		public static int seed = 5;

		// Token: 0x04001097 RID: 4247
		public int explodeSpeed;

		// Token: 0x04001098 RID: 4248
		public int randomSeed = 10;

		// Token: 0x04001099 RID: 4249
		public int explodeReal = 1;

		// Token: 0x0400109A RID: 4250
		public Vector3 campos;

		// Token: 0x0400109B RID: 4251
		public bool pigModelLoaded;

		// Token: 0x0400109C RID: 4252
		private Vector3 assScale = Vector3.Zero;

		// Token: 0x0400109D RID: 4253
		private Vector3 assScale2 = Vector3.Zero;

		// Token: 0x0400109E RID: 4254
		private float assRamp;

		// Token: 0x0400109F RID: 4255
		private float myTimer1;

		// Token: 0x040010A0 RID: 4256
		private float myTimer2;

		// Token: 0x040010A1 RID: 4257
		private float burstscale = 0.2f;

		// Token: 0x040010A2 RID: 4258
		private float burstfade;

		// Token: 0x040010A3 RID: 4259
		private float burstY = 30f;

		// Token: 0x040010A4 RID: 4260
		public bool volumeFade;

		// Token: 0x040010A5 RID: 4261
		private AnimationPlayer a;

		// Token: 0x040010A6 RID: 4262
		private AnimationPlayer b;

		// Token: 0x040010A7 RID: 4263
		private int myClip;

		// Token: 0x040010A8 RID: 4264
		private Matrix heartMatrix;

		// Token: 0x040010A9 RID: 4265
		private float heartTimer;

		// Token: 0x040010AA RID: 4266
		private float heartattack;

		// Token: 0x040010AB RID: 4267
		public bool heartExposed;

		// Token: 0x040010AC RID: 4268
		public int heartIndex = -1;

		// Token: 0x040010AD RID: 4269
		private Matrix m1;

		// Token: 0x040010AE RID: 4270
		private Matrix m2;

		// Token: 0x040010AF RID: 4271
		private Matrix m3;

		// Token: 0x040010B0 RID: 4272
		private Matrix m4;

		// Token: 0x040010B1 RID: 4273
		private Matrix m5;

		// Token: 0x040010B2 RID: 4274
		private Vector3 v1 = Vector3.Zero;

		// Token: 0x040010B3 RID: 4275
		private Vector3 myZero = Vector3.Zero;

		// Token: 0x040010B4 RID: 4276
		private NetworkSession networkSession;

		// Token: 0x040010B5 RID: 4277
		public int break1 = 510;

		// Token: 0x040010B6 RID: 4278
		public int break2 = 790;

		// Token: 0x040010B7 RID: 4279
		private int timeframe;

		// Token: 0x040010B8 RID: 4280
		public bool spectator;

		// Token: 0x040010B9 RID: 4281
		public int df;

		// Token: 0x040010BA RID: 4282
		private float playerHealth;

		// Token: 0x040010BB RID: 4283
		private float remoteHealth;

		// Token: 0x040010BC RID: 4284
		public SoundEffect bossTheme;

		// Token: 0x040010BD RID: 4285
		private SoundEffects burps;

		// Token: 0x040010BE RID: 4286
		private SoundEffects explode;

		// Token: 0x040010BF RID: 4287
		private SoundEffects splats;

		// Token: 0x040010C0 RID: 4288
		private SoundEffects dogroll;

		// Token: 0x040010C1 RID: 4289
		private SoundEffects windy;

		// Token: 0x040010C2 RID: 4290
		private SoundEffects heartbeat;

		// Token: 0x040010C3 RID: 4291
		private SoundEffects bucking;

		// Token: 0x040010C4 RID: 4292
		private SoundEffects fart;

		// Token: 0x040010C5 RID: 4293
		public float shockRadius;

		// Token: 0x040010C6 RID: 4294
		public float shockTimer;

		// Token: 0x040010C7 RID: 4295
		public bool shockHit;

		// Token: 0x040010C8 RID: 4296
		public bool groundhit;

		// Token: 0x040010C9 RID: 4297
		public bool shockHasHit;

		// Token: 0x040010CA RID: 4298
		public static StringBuilder nameBuild = new StringBuilder(32, 32);

		// Token: 0x040010CB RID: 4299
		private Model canvas;

		// Token: 0x040010CC RID: 4300
		private Model tunnel;

		// Token: 0x040010CD RID: 4301
		private Effect simple;

		// Token: 0x040010CE RID: 4302
		private Effect simple2;

		// Token: 0x040010CF RID: 4303
		private Effect tunnelfx;

		// Token: 0x040010D0 RID: 4304
		public int myIndex;

		// Token: 0x040010D1 RID: 4305
		public int pigIndex;

		// Token: 0x040010D2 RID: 4306
		private int bodyState;

		// Token: 0x040010D3 RID: 4307
		public bool boneStrike;

		// Token: 0x040010D4 RID: 4308
		private int showSkelTimer;

		// Token: 0x040010D5 RID: 4309
		public int seizureTimer;

		// Token: 0x040010D6 RID: 4310
		private int faceseizureTimer;

		// Token: 0x040010D7 RID: 4311
		public float glowTimer;

		// Token: 0x040010D8 RID: 4312
		public Vector3 playerpos;

		// Token: 0x040010D9 RID: 4313
		public Vector3 remotepos;

		// Token: 0x040010DA RID: 4314
		public bool cuttyCollide;

		// Token: 0x040010DB RID: 4315
		public bool cuttyAssPush;

		// Token: 0x040010DC RID: 4316
		public bool cuttyRoll;

		// Token: 0x040010DD RID: 4317
		public bool cuttyBuck;

		// Token: 0x040010DE RID: 4318
		public bool cuttyShit;

		// Token: 0x040010DF RID: 4319
		private float cuttyRollFade = 1f;

		// Token: 0x040010E0 RID: 4320
		private int cuttyRollCount;

		// Token: 0x040010E1 RID: 4321
		public int cuttyWait;

		// Token: 0x040010E2 RID: 4322
		private readonly VertexBufferBinding[] _vertexBufferBindings = new VertexBufferBinding[2];

		// Token: 0x040010E3 RID: 4323
		private static VertexDeclaration instanceDec = new VertexDeclaration(new VertexElement[]
		{
			new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
			new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
			new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
			new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
			new VertexElement(64, VertexElementFormat.Vector3, VertexElementUsage.Normal, 1)
		});

		// Token: 0x040010E4 RID: 4324
		private Princess.shell chunk;

		// Token: 0x040010E5 RID: 4325
		private Princess.shell faceChunk;

		// Token: 0x040010E6 RID: 4326
		private Princess.shell assChunk;

		// Token: 0x040010E7 RID: 4327
		public Princess.vomit puke1;

		// Token: 0x040010E8 RID: 4328
		public Princess.finale bloody;

		// Token: 0x040010E9 RID: 4329
		public static RasterizerState wiredOn = new RasterizerState
		{
			FillMode = FillMode.WireFrame
		};

		// Token: 0x040010EA RID: 4330
		public static RasterizerState wiredOff = new RasterizerState
		{
			FillMode = FillMode.Solid
		};

		// Token: 0x040010EB RID: 4331
		public float targetRot;

		// Token: 0x040010EC RID: 4332
		public int turningWait = 900;

		// Token: 0x040010ED RID: 4333
		public bool newAction;

		// Token: 0x040010EE RID: 4334
		public bool jumpVol;

		// Token: 0x040010EF RID: 4335
		public bool rollVol;

		// Token: 0x040010F0 RID: 4336
		public bool vomitVol;

		// Token: 0x040010F1 RID: 4337
		public bool shitVol;

		// Token: 0x040010F2 RID: 4338
		public bool heartVol;

		// Token: 0x040010F3 RID: 4339
		public float cuttyDistance = 20000f;

		// Token: 0x040010F4 RID: 4340
		public float fps;

		// Token: 0x040010F5 RID: 4341
		public ushort assDamage;

		// Token: 0x040010F6 RID: 4342
		public ushort faceDamage;

		// Token: 0x040010F7 RID: 4343
		public ushort spineDamage;

		// Token: 0x040010F8 RID: 4344
		private float[] assBreach = new float[] { 40f, 90f, 110f, 80f, 120f, 140f, 250f };

		// Token: 0x040010F9 RID: 4345
		private ushort[] assDam = new ushort[] { 12, 6, 3, 9, 5, 3, 2 };

		// Token: 0x040010FA RID: 4346
		private float[] faceBreach = new float[] { 40f, 90f, 110f, 80f, 120f, 140f, 250f };

		// Token: 0x040010FB RID: 4347
		private ushort[] faceDam = new ushort[] { 14, 6, 3, 8, 5, 3, 2 };

		// Token: 0x040010FC RID: 4348
		private float[] spineBreach = new float[] { 40f, 90f, 110f, 70f, 120f, 140f, 250f };

		// Token: 0x040010FD RID: 4349
		private ushort[] spineDam = new ushort[] { 14, 6, 3, 10, 6, 4, 2 };

		// Token: 0x040010FE RID: 4350
		private bool assDestroyed;

		// Token: 0x040010FF RID: 4351
		private bool spineDestroyed;

		// Token: 0x04001100 RID: 4352
		private bool faceDestroyed;

		// Token: 0x04001101 RID: 4353
		public float[] minDistance = new float[] { 1440000f, 990000f, 850000f, 1440000f, 990000f, 810000f, 1400000f };

		// Token: 0x04001102 RID: 4354
		private int[] jumpOdds = new int[] { 200, 300, 400, 400, 500, 700, 200 };

		// Token: 0x04001103 RID: 4355
		private float[] angleView = new float[] { 0.3f, 0.9f, 1.4f, 1f, 1.4f, 1.6f, 1.7f };

		// Token: 0x04001104 RID: 4356
		public int[] shitDam = new int[] { 3, 5, 8, 8, 10, 15, 20 };

		// Token: 0x04001105 RID: 4357
		public float[] futureDam = new float[] { 30f, 40f, 60f, 60f, 90f, 90f, 90f };

		// Token: 0x04001106 RID: 4358
		public int[] rollDam = new int[] { 2, 3, 5, 5, 7, 8, 12 };

		// Token: 0x04001107 RID: 4359
		private float[] hitScale = new float[] { 1f, 1f, 1.1f, 1f, 1.1f, 1.2f, 1.6f };

		// Token: 0x04001108 RID: 4360
		public float[] rollPower = new float[] { 0.5f, 0.6f, 0.65f, 0.65f, 0.7f, 0.73f, 0.77f };

		// Token: 0x04001109 RID: 4361
		private int[] damTrigger = new int[] { 6, 5, 2, 6, 3, 1, 2 };

		// Token: 0x0400110A RID: 4362
		private int damcheck = 5;

		// Token: 0x0400110B RID: 4363
		public bool shootHeartMessage;

		// Token: 0x0400110C RID: 4364
		public int heartMessCount;

		// Token: 0x0400110D RID: 4365
		public bool shootWoundMessage;

		// Token: 0x0400110E RID: 4366
		public bool notcloseEnough;

		// Token: 0x0400110F RID: 4367
		public int notcloseCount;

		// Token: 0x04001110 RID: 4368
		public float delay;

		// Token: 0x04001111 RID: 4369
		private int timeDelay;

		// Token: 0x04001112 RID: 4370
		private int lasttimeDelay;

		// Token: 0x04001113 RID: 4371
		public bool actionScheduled;

		// Token: 0x04001114 RID: 4372
		public Princess.conductor tempConduct = default(Princess.conductor);

		// Token: 0x04001115 RID: 4373
		private Princess.conductor cuttyStruct = default(Princess.conductor);

		// Token: 0x04001116 RID: 4374
		private Princess.conductor cuttyStruct_send = default(Princess.conductor);

		// Token: 0x04001117 RID: 4375
		private List<Princess.conductor> scheduleList = new List<Princess.conductor>();

		// Token: 0x04001118 RID: 4376
		private int currentKeyframe;

		// Token: 0x04001119 RID: 4377
		private TimeSpan currentTimeValue;

		// Token: 0x0400111A RID: 4378
		private ScreenManager sc;

		// Token: 0x0400111B RID: 4379
		private ContentManager content;

		// Token: 0x0400111C RID: 4380
		private RenderTarget2D screenTarget1;

		// Token: 0x0400111D RID: 4381
		private RenderTarget2D screenTarget2;

		// Token: 0x0400111E RID: 4382
		private RenderTarget2D canvasTarget2;

		// Token: 0x0400111F RID: 4383
		private RenderTarget2D canvasTarget1;

		// Token: 0x04001120 RID: 4384
		private int targetChoice;

		// Token: 0x04001121 RID: 4385
		private int canvasChoice;

		// Token: 0x04001122 RID: 4386
		private SpriteBatch spriteBatch;

		// Token: 0x04001123 RID: 4387
		private int boneHit = -1;

		// Token: 0x04001124 RID: 4388
		private int[] col_Bone = new int[] { 5, 7, 8, 2, 13, 17, 21, 26 };

		// Token: 0x04001125 RID: 4389
		private float[] col_Scale = new float[8];

		// Token: 0x04001126 RID: 4390
		private Vector3[] col_Pos = new Vector3[8];

		// Token: 0x04001127 RID: 4391
		public bool cuttyisHit;

		// Token: 0x04001128 RID: 4392
		public bool cuttyChunkRelease;

		// Token: 0x04001129 RID: 4393
		public Vector3 hitCenter;

		// Token: 0x0400112A RID: 4394
		public Vector3 hitCenter2;

		// Token: 0x0400112B RID: 4395
		public Vector3 hitEdge;

		// Token: 0x0400112C RID: 4396
		public Vector3 hitEdge2;

		// Token: 0x0400112D RID: 4397
		public string[] pigdialogueName = new string[]
		{
			"yell1", "vomit1", "vomit2", "yell2", "yell3", "yell4", "hurtbaby", "baby4", "baby3", "baby2",
			"empty"
		};

		// Token: 0x0400112E RID: 4398
		public int pigLine = -1;

		// Token: 0x0400112F RID: 4399
		public int pigJawIndex = -1;

		// Token: 0x04001130 RID: 4400
		public int talkIndex = -1;

		// Token: 0x04001131 RID: 4401
		public SoundEffects pigDialog1;

		// Token: 0x04001132 RID: 4402
		public float distanceCutty = 10000f;

		// Token: 0x04001133 RID: 4403
		public float distanceCutty2 = 100000f;

		// Token: 0x04001134 RID: 4404
		public float eyeGlow = 1f;

		// Token: 0x04001135 RID: 4405
		public bool isSpeaking;

		// Token: 0x04001136 RID: 4406
		public bool isWatching;

		// Token: 0x04001137 RID: 4407
		public bool pigDialog1Loaded;

		// Token: 0x04001138 RID: 4408
		public bool lookingatPig = true;

		// Token: 0x04001139 RID: 4409
		public float[] pigJaw;

		// Token: 0x0400113A RID: 4410
		public float talkSmooth;

		// Token: 0x0400113B RID: 4411
		public float piglook;

		// Token: 0x0400113C RID: 4412
		private float tiltOffset;

		// Token: 0x0400113D RID: 4413
		private Vector3 bonePos;

		// Token: 0x0400113E RID: 4414
		public float pigPush;

		// Token: 0x0400113F RID: 4415
		public float pigFrame1;

		// Token: 0x04001140 RID: 4416
		private float bFrame1;

		// Token: 0x04001141 RID: 4417
		private AnimationPlayer[] pig1;

		// Token: 0x04001142 RID: 4418
		private Matrix[] princessBone;

		// Token: 0x04001143 RID: 4419
		private int clipIndexA;

		// Token: 0x04001144 RID: 4420
		private int clipIndexB;

		// Token: 0x04001145 RID: 4421
		private float tween = 1f;

		// Token: 0x04001146 RID: 4422
		public Vector3 cuttyPos;

		// Token: 0x04001147 RID: 4423
		public Vector3 oldcuttyPos;

		// Token: 0x04001148 RID: 4424
		public Vector3 cuttyVeloc;

		// Token: 0x04001149 RID: 4425
		private Vector3 oldvomitpos = Vector3.Zero;

		// Token: 0x0400114A RID: 4426
		private Vector3 vomitpos;

		// Token: 0x0400114B RID: 4427
		private Vector3 vomitveloc;

		// Token: 0x0400114C RID: 4428
		private float cuttyScale;

		// Token: 0x0400114D RID: 4429
		public float cuttyRot;

		// Token: 0x0400114E RID: 4430
		private Matrix cuttyTrans;

		// Token: 0x0400114F RID: 4431
		private Random rr;

		// Token: 0x04001150 RID: 4432
		public Model pigModel;

		// Token: 0x04001151 RID: 4433
		public Model sphere;

		// Token: 0x04001152 RID: 4434
		public Model grid;

		// Token: 0x04001153 RID: 4435
		public Model gutsModel;

		// Token: 0x04001154 RID: 4436
		private Effect pigSkin;

		// Token: 0x04001155 RID: 4437
		private Effect solidSkin;

		// Token: 0x04001156 RID: 4438
		public Effect glowEffect;

		// Token: 0x04001157 RID: 4439
		private Texture2D pigTexture;

		// Token: 0x04001158 RID: 4440
		private Texture2D heartTexture;

		// Token: 0x04001159 RID: 4441
		private Texture2D tunneldust;

		// Token: 0x0400115A RID: 4442
		private Texture2D glow;

		// Token: 0x0400115B RID: 4443
		private Texture2D guts;

		// Token: 0x0400115C RID: 4444
		private Effect burst;

		// Token: 0x0400115D RID: 4445
		private Effect gutsEffect;

		// Token: 0x0400115E RID: 4446
		private Matrix burstMatrix;

		// Token: 0x0400115F RID: 4447
		private Matrix view;

		// Token: 0x04001160 RID: 4448
		private Matrix proj;

		// Token: 0x04001161 RID: 4449
		private Matrix[] explodePart;

		// Token: 0x04001162 RID: 4450
		public bool attack1;

		// Token: 0x04001163 RID: 4451
		private int nextAttack = 900;

		// Token: 0x04001164 RID: 4452
		public int hurtBoss;

		// Token: 0x04001165 RID: 4453
		public bool death1;

		// Token: 0x04001166 RID: 4454
		public bool deathsent;

		// Token: 0x04001167 RID: 4455
		public bool cuttyisDead;

		// Token: 0x04001168 RID: 4456
		public int explodeTimer = 50;

		// Token: 0x04001169 RID: 4457
		public bool cuttyVomitHit;

		// Token: 0x0400116A RID: 4458
		public bool cuttyVomitHit2;

		// Token: 0x0400116B RID: 4459
		public bool cuttyShitHit;

		// Token: 0x0400116C RID: 4460
		public bool cuttyShitHit2;

		// Token: 0x0400116D RID: 4461
		public float cuttyVomitTimer;

		// Token: 0x0400116E RID: 4462
		public float cuttyShitTimer;

		// Token: 0x0400116F RID: 4463
		private float upForce;

		// Token: 0x04001170 RID: 4464
		private float upForceAcc;

		// Token: 0x04001171 RID: 4465
		private float upForceMax;

		// Token: 0x04001172 RID: 4466
		private float grav;

		// Token: 0x04001173 RID: 4467
		private float attackduration;

		// Token: 0x04001174 RID: 4468
		private float attacktimer;

		// Token: 0x04001175 RID: 4469
		private float attackWait1;

		// Token: 0x04001176 RID: 4470
		private float attackWait2;

		// Token: 0x04001177 RID: 4471
		private float gonnaRollDelay = 500f;

		// Token: 0x04001178 RID: 4472
		private float buckDelay = 900f;

		// Token: 0x04001179 RID: 4473
		private float shitDelay = 900f;

		// Token: 0x0400117A RID: 4474
		private bool gonnaShit;

		// Token: 0x0400117B RID: 4475
		private bool gonnaVomit;

		// Token: 0x0400117C RID: 4476
		private float shitTimer = 200f;

		// Token: 0x0400117D RID: 4477
		public ushort startHealth = 2500;

		// Token: 0x0400117E RID: 4478
		public ushort health = 2500;

		// Token: 0x0400117F RID: 4479
		private float healthPerc;

		// Token: 0x04001180 RID: 4480
		public byte damagebit = 1;

		// Token: 0x04001181 RID: 4481
		public int homing;

		// Token: 0x04001182 RID: 4482
		public int homingDirection;

		// Token: 0x04001183 RID: 4483
		private int homingTimer;

		// Token: 0x04001184 RID: 4484
		private int homingStart = 500;

		// Token: 0x04001185 RID: 4485
		public bool cuttyDoneLocalSpeech;

		// Token: 0x04001186 RID: 4486
		private List<int> speechList = new List<int>();

		// Token: 0x04001187 RID: 4487
		private List<int> speechInclude = new List<int>();

		// Token: 0x04001188 RID: 4488
		public bool playingBossMusic;

		// Token: 0x04001189 RID: 4489
		public bool isTrialMode;

		// Token: 0x0400118A RID: 4490
		public Princess.paintBody canvasPaint;

		// Token: 0x0400118B RID: 4491
		public Princess.paintBody bodyPaint;

		// Token: 0x0200007A RID: 122
		public struct instancedObject : IVertexType
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x0600045D RID: 1117 RVA: 0x000FE0A8 File Offset: 0x000FC2A8
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					return Princess.instancedObject.InstanceVertexDeclaration;
				}
			}

			// Token: 0x0400118C RID: 4492
			public Matrix Trans;

			// Token: 0x0400118D RID: 4493
			public Vector3 color;

			// Token: 0x0400118E RID: 4494
			private static readonly VertexDeclaration InstanceVertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 0),
				new VertexElement(16, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 1),
				new VertexElement(32, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 2),
				new VertexElement(48, VertexElementFormat.Vector4, VertexElementUsage.BlendWeight, 3),
				new VertexElement(64, VertexElementFormat.Vector3, VertexElementUsage.Normal, 1)
			});
		}

		// Token: 0x0200007B RID: 123
		public struct shell
		{
			// Token: 0x0400118F RID: 4495
			public int max;

			// Token: 0x04001190 RID: 4496
			public int tempindex;

			// Token: 0x04001191 RID: 4497
			public int index;

			// Token: 0x04001192 RID: 4498
			public int maxCapacity;

			// Token: 0x04001193 RID: 4499
			public Matrix[] offset;

			// Token: 0x04001194 RID: 4500
			public bool startDrop;

			// Token: 0x04001195 RID: 4501
			public float dropTimer;

			// Token: 0x04001196 RID: 4502
			public int[] bone;

			// Token: 0x04001197 RID: 4503
			public Princess.instancedObject[] stream;

			// Token: 0x04001198 RID: 4504
			public DynamicVertexBuffer buffer;

			// Token: 0x04001199 RID: 4505
			public Princess.instancedObject[] displayList;

			// Token: 0x0400119A RID: 4506
			public chunkDupe[] dupe;

			// Token: 0x0400119B RID: 4507
			public Model model;
		}

		// Token: 0x0200007C RID: 124
		public struct vomit
		{
			// Token: 0x0400119C RID: 4508
			public int max;

			// Token: 0x0400119D RID: 4509
			public int tempindex;

			// Token: 0x0400119E RID: 4510
			public int index;

			// Token: 0x0400119F RID: 4511
			public int maxCapacity;

			// Token: 0x040011A0 RID: 4512
			public Princess.instancedObject[] stream;

			// Token: 0x040011A1 RID: 4513
			public DynamicVertexBuffer buffer;

			// Token: 0x040011A2 RID: 4514
			public Princess.instancedObject[] displayList;

			// Token: 0x040011A3 RID: 4515
			public vomitDupe[] dupe;

			// Token: 0x040011A4 RID: 4516
			public Model model;
		}

		// Token: 0x0200007D RID: 125
		public struct finale
		{
			// Token: 0x040011A5 RID: 4517
			public int max;

			// Token: 0x040011A6 RID: 4518
			public int tempindex;

			// Token: 0x040011A7 RID: 4519
			public int index;

			// Token: 0x040011A8 RID: 4520
			public int maxCapacity;

			// Token: 0x040011A9 RID: 4521
			public Princess.instancedObject[] stream;

			// Token: 0x040011AA RID: 4522
			public DynamicVertexBuffer buffer;

			// Token: 0x040011AB RID: 4523
			public Princess.instancedObject[] displayList;

			// Token: 0x040011AC RID: 4524
			public explodeDupe[] dupe;

			// Token: 0x040011AD RID: 4525
			public Model model;
		}

		// Token: 0x0200007E RID: 126
		public struct conductor
		{
			// Token: 0x040011AE RID: 4526
			public int flag;

			// Token: 0x040011AF RID: 4527
			public int time;

			// Token: 0x040011B0 RID: 4528
			public int homing;

			// Token: 0x040011B1 RID: 4529
			public float rot;

			// Token: 0x040011B2 RID: 4530
			public int animType;

			// Token: 0x040011B3 RID: 4531
			public int talkindex;

			// Token: 0x040011B4 RID: 4532
			public ushort dur;
		}

		// Token: 0x0200007F RID: 127
		public struct paintBody
		{
			// Token: 0x040011B5 RID: 4533
			public List<int> index;

			// Token: 0x040011B6 RID: 4534
			public List<float> x;

			// Token: 0x040011B7 RID: 4535
			public List<float> z;
		}
	}
}
