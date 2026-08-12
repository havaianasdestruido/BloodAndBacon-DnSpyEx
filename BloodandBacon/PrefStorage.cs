using System;
using System.IO;

namespace Blood
{
	// Token: 0x02000067 RID: 103
	public class PrefStorage
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x000E1EA4 File Offset: 0x000E00A4
		public PrefData LoadPreferences()
		{
			this.status = "";
			PrefData prefData = default(PrefData);
			prefData.fileExists = false;
			prefData.lens = 58f;
			prefData.gore = 0;
			prefData.playername = "";
			prefData.aspectratio = 1f;
			prefData.star1 = false;
			prefData.star2 = false;
			prefData.star3 = false;
			string text = "SAVES//saveprefs";
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(File.Open(text, FileMode.Open)))
				{
					prefData.fileExists = binaryReader.ReadBoolean();
					prefData.fileVersion = binaryReader.ReadSingle();
					prefData.mv = binaryReader.ReadSingle();
					prefData.ev = binaryReader.ReadSingle();
					prefData.vv = binaryReader.ReadSingle();
					prefData.df = binaryReader.ReadInt32();
					prefData.brightness = binaryReader.ReadInt32();
					prefData.contrast = binaryReader.ReadInt32();
					prefData.pad_invertY = binaryReader.ReadSingle();
					prefData.pad_sensitivity = binaryReader.ReadSingle();
					prefData.pad_vibro = binaryReader.ReadBoolean();
					prefData.hud_enemy.X = binaryReader.ReadSingle();
					prefData.hud_enemy.Y = binaryReader.ReadSingle();
					prefData.hud_clock.X = binaryReader.ReadSingle();
					prefData.hud_clock.Y = binaryReader.ReadSingle();
					prefData.hud_day.X = binaryReader.ReadSingle();
					prefData.hud_day.Y = binaryReader.ReadSingle();
					prefData.hud_player1.X = binaryReader.ReadSingle();
					prefData.hud_player1.Y = binaryReader.ReadSingle();
					prefData.hud_player2.X = binaryReader.ReadSingle();
					prefData.hud_player2.Y = binaryReader.ReadSingle();
					prefData.hud_weapons.X = binaryReader.ReadSingle();
					prefData.hud_weapons.Y = binaryReader.ReadSingle();
					prefData.hud_dpad.X = binaryReader.ReadSingle();
					prefData.hud_dpad.Y = binaryReader.ReadSingle();
					prefData.camradianA = binaryReader.ReadSingle();
					prefData.camheightA = binaryReader.ReadSingle();
					prefData.campos3rdA.X = binaryReader.ReadSingle();
					prefData.campos3rdA.Y = binaryReader.ReadSingle();
					prefData.campos3rdA.Z = binaryReader.ReadSingle();
					prefData.camlookpos3rdA.X = binaryReader.ReadSingle();
					prefData.camlookpos3rdA.Y = binaryReader.ReadSingle();
					prefData.camlookpos3rdA.Z = binaryReader.ReadSingle();
					prefData.camradianB = binaryReader.ReadSingle();
					prefData.camheightB = binaryReader.ReadSingle();
					prefData.campos3rdB.X = binaryReader.ReadSingle();
					prefData.campos3rdB.Y = binaryReader.ReadSingle();
					prefData.campos3rdB.Z = binaryReader.ReadSingle();
					prefData.camlookpos3rdB.X = binaryReader.ReadSingle();
					prefData.camlookpos3rdB.Y = binaryReader.ReadSingle();
					prefData.camlookpos3rdB.Z = binaryReader.ReadSingle();
					prefData.curDay = binaryReader.ReadUInt16();
					prefData.FarmerUnlocked = binaryReader.ReadBoolean();
					prefData.pad_reload = true;
					prefData.pad_togglesprint = binaryReader.ReadBoolean();
					prefData.aliasing = binaryReader.ReadInt32();
					prefData.resolution = binaryReader.ReadInt32();
					prefData.fullscreen = binaryReader.ReadInt32();
					prefData.aspectratio = binaryReader.ReadSingle();
					prefData.playername = binaryReader.ReadString();
					prefData.lens = binaryReader.ReadSingle();
					prefData.gore = binaryReader.ReadInt32();
					prefData.fastnades = binaryReader.ReadBoolean();
					prefData.star1 = binaryReader.ReadBoolean();
					prefData.star2 = binaryReader.ReadBoolean();
					prefData.star3 = binaryReader.ReadBoolean();
					prefData.doubleAmmo = binaryReader.ReadBoolean();
					prefData.workshopNum = binaryReader.ReadByte();
					binaryReader.Close();
				}
			}
			catch
			{
				this.status = "fail";
			}
			return prefData;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000E22EC File Offset: 0x000E04EC
		public void SavePreferences(PrefData sg)
		{
			this.status = "";
			this.status = "";
			string text = "SAVES//saveprefs";
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
					binaryWriter.Write(sg.mv);
					binaryWriter.Write(sg.ev);
					binaryWriter.Write(sg.vv);
					binaryWriter.Write(sg.df);
					binaryWriter.Write(sg.brightness);
					binaryWriter.Write(sg.contrast);
					binaryWriter.Write(sg.pad_invertY);
					binaryWriter.Write(sg.pad_sensitivity);
					binaryWriter.Write(sg.pad_vibro);
					binaryWriter.Write(sg.hud_enemy.X);
					binaryWriter.Write(sg.hud_enemy.Y);
					binaryWriter.Write(sg.hud_clock.X);
					binaryWriter.Write(sg.hud_clock.Y);
					binaryWriter.Write(sg.hud_day.X);
					binaryWriter.Write(sg.hud_day.Y);
					binaryWriter.Write(sg.hud_player1.X);
					binaryWriter.Write(sg.hud_player1.Y);
					binaryWriter.Write(sg.hud_player2.X);
					binaryWriter.Write(sg.hud_player2.Y);
					binaryWriter.Write(sg.hud_weapons.X);
					binaryWriter.Write(sg.hud_weapons.Y);
					binaryWriter.Write(sg.hud_dpad.X);
					binaryWriter.Write(sg.hud_dpad.Y);
					binaryWriter.Write(sg.camradianA);
					binaryWriter.Write(sg.camheightA);
					binaryWriter.Write(sg.campos3rdA.X);
					binaryWriter.Write(sg.campos3rdA.Y);
					binaryWriter.Write(sg.campos3rdA.Z);
					binaryWriter.Write(sg.camlookpos3rdA.X);
					binaryWriter.Write(sg.camlookpos3rdA.Y);
					binaryWriter.Write(sg.camlookpos3rdA.Z);
					binaryWriter.Write(sg.camradianB);
					binaryWriter.Write(sg.camheightB);
					binaryWriter.Write(sg.campos3rdB.X);
					binaryWriter.Write(sg.campos3rdB.Y);
					binaryWriter.Write(sg.campos3rdB.Z);
					binaryWriter.Write(sg.camlookpos3rdB.X);
					binaryWriter.Write(sg.camlookpos3rdB.Y);
					binaryWriter.Write(sg.camlookpos3rdB.Z);
					binaryWriter.Write(sg.curDay);
					binaryWriter.Write(sg.FarmerUnlocked);
					binaryWriter.Write(sg.pad_togglesprint);
					binaryWriter.Write(sg.aliasing);
					binaryWriter.Write(sg.resolution);
					binaryWriter.Write(sg.fullscreen);
					binaryWriter.Write(sg.aspectratio);
					binaryWriter.Write(sg.playername);
					binaryWriter.Write(sg.lens);
					binaryWriter.Write(sg.gore);
					binaryWriter.Write(sg.fastnades);
					binaryWriter.Write(sg.star1);
					binaryWriter.Write(sg.star2);
					binaryWriter.Write(sg.star3);
					binaryWriter.Write(sg.doubleAmmo);
					binaryWriter.Write(sg.workshopNum);
					binaryWriter.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x04000FE4 RID: 4068
		public string status = "";
	}
}
