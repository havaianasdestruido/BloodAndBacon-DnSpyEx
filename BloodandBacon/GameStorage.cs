using System;
using System.IO;

namespace Blood
{
	// Token: 0x02000054 RID: 84
	public class GameStorage
	{
		// Token: 0x0600033E RID: 830 RVA: 0x000D0D50 File Offset: 0x000CEF50
		public GameData LoadGame()
		{
			this.status = "";
			GameData gameData = default(GameData);
			gameData.fileExists = false;
			gameData.days = new byte[201];
			gameData.hats = 0;
			gameData.mats = 0;
			gameData.flashlight1 = 0;
			gameData.flashlight2 = 0;
			gameData.flashlight3 = 0;
			gameData.goggles = 0;
			gameData.map = new byte[20];
			gameData.ammobox1 = new byte[20];
			gameData.ammobox2 = new byte[20];
			gameData.ammobox3 = new byte[20];
			gameData.cog1 = new byte[20];
			gameData.cog2 = new byte[20];
			gameData.cog3 = new byte[20];
			gameData.exitkey = new byte[20];
			gameData.code1 = new byte[20, 3];
			gameData.code2 = new byte[20, 3];
			gameData.code3 = new byte[20, 3];
			gameData.redskull1 = 0;
			gameData.redskull2 = 0;
			gameData.redskull3 = 0;
			gameData.tusk1 = 0;
			gameData.tusk2 = 0;
			gameData.tusk3 = 0;
			gameData.heirloom = new byte[7];
			gameData.scarh = false;
			string text = "SAVES//savegame";
			if (!File.Exists(text))
			{
				return gameData;
			}
			this.status = "";
			text = "SAVES//savegame";
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(File.Open(text, FileMode.Open)))
				{
					gameData.fileExists = binaryReader.ReadBoolean();
					gameData.fileVersion = binaryReader.ReadSingle();
					for (int i = 0; i < gameData.days.Length; i++)
					{
						gameData.days[i] = binaryReader.ReadByte();
					}
					gameData.weaponsunlocked = binaryReader.ReadInt32();
					gameData.grinderunlocked = binaryReader.ReadInt32();
					gameData.charunlocked = binaryReader.ReadInt32();
					gameData.grenades = binaryReader.ReadByte();
					gameData.milks = binaryReader.ReadByte();
					gameData.bulkify = binaryReader.ReadByte();
					gameData.pills = binaryReader.ReadByte();
					gameData.mirv = binaryReader.ReadByte();
					gameData.hats = binaryReader.ReadByte();
					gameData.mats = binaryReader.ReadByte();
					gameData.man1 = binaryReader.ReadBoolean();
					gameData.man2 = binaryReader.ReadBoolean();
					gameData.man3 = binaryReader.ReadBoolean();
					gameData.man4 = binaryReader.ReadBoolean();
					gameData.flashlight1 = binaryReader.ReadByte();
					gameData.flashlight2 = binaryReader.ReadByte();
					gameData.flashlight3 = binaryReader.ReadByte();
					gameData.goggles = binaryReader.ReadByte();
					for (int j = 0; j < gameData.map.Length; j++)
					{
						gameData.map[j] = binaryReader.ReadByte();
					}
					for (int k = 0; k < gameData.ammobox1.Length; k++)
					{
						gameData.ammobox1[k] = binaryReader.ReadByte();
					}
					for (int l = 0; l < gameData.ammobox2.Length; l++)
					{
						gameData.ammobox2[l] = binaryReader.ReadByte();
					}
					for (int m = 0; m < gameData.ammobox3.Length; m++)
					{
						gameData.ammobox3[m] = binaryReader.ReadByte();
					}
					for (int n = 0; n < gameData.cog1.Length; n++)
					{
						gameData.cog1[n] = binaryReader.ReadByte();
					}
					for (int num = 0; num < gameData.cog2.Length; num++)
					{
						gameData.cog2[num] = binaryReader.ReadByte();
					}
					for (int num2 = 0; num2 < gameData.cog3.Length; num2++)
					{
						gameData.cog3[num2] = binaryReader.ReadByte();
					}
					for (int num3 = 0; num3 < gameData.exitkey.Length; num3++)
					{
						gameData.exitkey[num3] = binaryReader.ReadByte();
					}
					for (int num4 = 0; num4 < 20; num4++)
					{
						gameData.code1[num4, 0] = binaryReader.ReadByte();
						gameData.code1[num4, 1] = binaryReader.ReadByte();
						gameData.code1[num4, 2] = binaryReader.ReadByte();
						gameData.code2[num4, 0] = binaryReader.ReadByte();
						gameData.code2[num4, 1] = binaryReader.ReadByte();
						gameData.code2[num4, 2] = binaryReader.ReadByte();
						gameData.code3[num4, 0] = binaryReader.ReadByte();
						gameData.code3[num4, 1] = binaryReader.ReadByte();
						gameData.code3[num4, 2] = binaryReader.ReadByte();
					}
					gameData.redskull1 = binaryReader.ReadByte();
					gameData.redskull2 = binaryReader.ReadByte();
					gameData.redskull3 = binaryReader.ReadByte();
					gameData.tusk1 = binaryReader.ReadByte();
					gameData.tusk2 = binaryReader.ReadByte();
					gameData.tusk3 = binaryReader.ReadByte();
					gameData.heirloom[0] = binaryReader.ReadByte();
					gameData.heirloom[1] = binaryReader.ReadByte();
					gameData.heirloom[2] = binaryReader.ReadByte();
					gameData.heirloom[3] = binaryReader.ReadByte();
					gameData.heirloom[4] = binaryReader.ReadByte();
					gameData.heirloom[5] = binaryReader.ReadByte();
					gameData.heirloom[6] = binaryReader.ReadByte();
					gameData.scarh = binaryReader.ReadBoolean();
					binaryReader.Close();
				}
			}
			catch
			{
				this.status = "fail";
			}
			return gameData;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000D1320 File Offset: 0x000CF520
		public void SaveGame(GameData sg)
		{
			this.status = "";
			this.status = "";
			string text = "SAVES//savegame";
			if (!Directory.Exists("SAVES"))
			{
				Directory.CreateDirectory("SAVES");
			}
			try
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(text, FileMode.Create)))
				{
					binaryWriter.Write(true);
					binaryWriter.Write(sg.fileVersion);
					for (int i = 0; i < sg.days.Length; i++)
					{
						binaryWriter.Write(sg.days[i]);
					}
					binaryWriter.Write(sg.weaponsunlocked);
					binaryWriter.Write(sg.grinderunlocked);
					binaryWriter.Write(sg.charunlocked);
					binaryWriter.Write(sg.grenades);
					binaryWriter.Write(sg.milks);
					binaryWriter.Write(sg.bulkify);
					binaryWriter.Write(sg.pills);
					binaryWriter.Write(sg.mirv);
					binaryWriter.Write(sg.hats);
					binaryWriter.Write(sg.mats);
					binaryWriter.Write(sg.man1);
					binaryWriter.Write(sg.man2);
					binaryWriter.Write(sg.man3);
					binaryWriter.Write(sg.man4);
					binaryWriter.Write(sg.flashlight1);
					binaryWriter.Write(sg.flashlight2);
					binaryWriter.Write(sg.flashlight3);
					binaryWriter.Write(sg.goggles);
					for (int j = 0; j < sg.map.Length; j++)
					{
						binaryWriter.Write(sg.map[j]);
					}
					for (int k = 0; k < sg.ammobox1.Length; k++)
					{
						binaryWriter.Write(sg.ammobox1[k]);
					}
					for (int l = 0; l < sg.ammobox2.Length; l++)
					{
						binaryWriter.Write(sg.ammobox2[l]);
					}
					for (int m = 0; m < sg.ammobox3.Length; m++)
					{
						binaryWriter.Write(sg.ammobox3[m]);
					}
					for (int n = 0; n < sg.cog1.Length; n++)
					{
						binaryWriter.Write(sg.cog1[n]);
					}
					for (int num = 0; num < sg.cog2.Length; num++)
					{
						binaryWriter.Write(sg.cog2[num]);
					}
					for (int num2 = 0; num2 < sg.cog3.Length; num2++)
					{
						binaryWriter.Write(sg.cog3[num2]);
					}
					for (int num3 = 0; num3 < sg.exitkey.Length; num3++)
					{
						binaryWriter.Write(sg.exitkey[num3]);
					}
					for (int num4 = 0; num4 < 20; num4++)
					{
						binaryWriter.Write(sg.code1[num4, 0]);
						binaryWriter.Write(sg.code1[num4, 1]);
						binaryWriter.Write(sg.code1[num4, 2]);
						binaryWriter.Write(sg.code2[num4, 0]);
						binaryWriter.Write(sg.code2[num4, 1]);
						binaryWriter.Write(sg.code2[num4, 2]);
						binaryWriter.Write(sg.code3[num4, 0]);
						binaryWriter.Write(sg.code3[num4, 1]);
						binaryWriter.Write(sg.code3[num4, 2]);
					}
					binaryWriter.Write(sg.redskull1);
					binaryWriter.Write(sg.redskull2);
					binaryWriter.Write(sg.redskull3);
					binaryWriter.Write(sg.tusk1);
					binaryWriter.Write(sg.tusk2);
					binaryWriter.Write(sg.tusk3);
					binaryWriter.Write(sg.heirloom[0]);
					binaryWriter.Write(sg.heirloom[1]);
					binaryWriter.Write(sg.heirloom[2]);
					binaryWriter.Write(sg.heirloom[3]);
					binaryWriter.Write(sg.heirloom[4]);
					binaryWriter.Write(sg.heirloom[5]);
					binaryWriter.Write(sg.heirloom[6]);
					binaryWriter.Write(sg.scarh);
					binaryWriter.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000DB9 RID: 3513
		public string status = "";
	}
}
