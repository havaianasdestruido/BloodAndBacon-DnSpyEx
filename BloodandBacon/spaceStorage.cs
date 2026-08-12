using System;
using System.IO;

namespace Blood
{
	// Token: 0x0200001E RID: 30
	public class spaceStorage
	{
		// Token: 0x06000154 RID: 340 RVA: 0x0002FE0C File Offset: 0x0002E00C
		public SpaceData LoadPreferences()
		{
			this.status = "";
			SpaceData spaceData = default(SpaceData);
			spaceData.fileExists = false;
			string text = "SAVES//spaceprefs";
			if (!File.Exists(text))
			{
				this.status = "fail";
				return spaceData;
			}
			try
			{
				using (BinaryReader binaryReader = new BinaryReader(File.Open(text, FileMode.Open)))
				{
					spaceData.fileExists = binaryReader.ReadBoolean();
					spaceData.fileVersion = binaryReader.ReadSingle();
					spaceData.mv = binaryReader.ReadSingle();
					spaceData.ev = binaryReader.ReadSingle();
					spaceData.vv = binaryReader.ReadSingle();
					spaceData.df = binaryReader.ReadInt32();
					spaceData.brightness = binaryReader.ReadInt32();
					spaceData.contrast = binaryReader.ReadInt32();
					spaceData.pad_invertX = binaryReader.ReadInt32();
					spaceData.pad_invertY = binaryReader.ReadInt32();
					spaceData.pad_sensitivityX = binaryReader.ReadSingle();
					spaceData.pad_sensitivityY = binaryReader.ReadSingle();
					spaceData.pad_winvertX = binaryReader.ReadInt32();
					spaceData.pad_winvertY = binaryReader.ReadInt32();
					spaceData.pad_wsensitivityX = binaryReader.ReadSingle();
					spaceData.pad_wsensitivityY = binaryReader.ReadSingle();
					spaceData.pad_vibro = binaryReader.ReadInt32();
					int num = binaryReader.ReadInt32();
					int num2 = binaryReader.ReadInt32();
					int num3 = binaryReader.ReadInt32();
					int num4 = binaryReader.ReadInt32();
					int[] array = new int[] { num, num2, num3, num4 };
					spaceData.allcamsradius = array;
					num = binaryReader.ReadInt32();
					num2 = binaryReader.ReadInt32();
					num3 = binaryReader.ReadInt32();
					num4 = binaryReader.ReadInt32();
					array = new int[] { num, num2, num3, num4 };
					spaceData.allcamsorbit = array;
					num = binaryReader.ReadInt32();
					num2 = binaryReader.ReadInt32();
					num3 = binaryReader.ReadInt32();
					num4 = binaryReader.ReadInt32();
					array = new int[] { num, num2, num3, num4 };
					spaceData.allcamsaltitude = array;
					num = binaryReader.ReadInt32();
					num2 = binaryReader.ReadInt32();
					num3 = binaryReader.ReadInt32();
					num4 = binaryReader.ReadInt32();
					array = new int[] { num, num2, num3, num4 };
					spaceData.allcamslens = array;
					spaceData.pad_rinvertX = binaryReader.ReadInt32();
					spaceData.pad_rinvertY = binaryReader.ReadInt32();
					spaceData.pad_rsensitivityX = binaryReader.ReadSingle();
					spaceData.pad_rsensitivityY = binaryReader.ReadSingle();
					spaceData.roverindex = binaryReader.ReadInt32();
					spaceData.roverrotlock = binaryReader.ReadInt32();
					spaceData.roverhitelock = binaryReader.ReadInt32();
					float num5 = binaryReader.ReadSingle();
					float num6 = binaryReader.ReadSingle();
					float num7 = binaryReader.ReadSingle();
					float[] array2 = new float[] { num5, num6, num7 };
					spaceData.roverdist = array2;
					num5 = binaryReader.ReadSingle();
					num6 = binaryReader.ReadSingle();
					num7 = binaryReader.ReadSingle();
					array2 = new float[] { num5, num6, num7 };
					spaceData.roverheight = array2;
					num5 = binaryReader.ReadSingle();
					num6 = binaryReader.ReadSingle();
					num7 = binaryReader.ReadSingle();
					array2 = new float[] { num5, num6, num7 };
					spaceData.roverradian = array2;
					spaceData.landerindex = binaryReader.ReadInt32();
					spaceData.landerrotlock = binaryReader.ReadInt32();
					spaceData.landerhitelock = binaryReader.ReadInt32();
					num5 = binaryReader.ReadSingle();
					num6 = binaryReader.ReadSingle();
					num7 = binaryReader.ReadSingle();
					array2 = new float[] { num5, num6, num7 };
					spaceData.landerdist = array2;
					num5 = binaryReader.ReadSingle();
					num6 = binaryReader.ReadSingle();
					num7 = binaryReader.ReadSingle();
					array2 = new float[] { num5, num6, num7 };
					spaceData.landerheight = array2;
					num5 = binaryReader.ReadSingle();
					num6 = binaryReader.ReadSingle();
					num7 = binaryReader.ReadSingle();
					array2 = new float[] { num5, num6, num7 };
					spaceData.landerradian = array2;
					binaryReader.Close();
					this.status = "success";
				}
			}
			catch
			{
				this.status = "fail";
			}
			return spaceData;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000302C4 File Offset: 0x0002E4C4
		public void SaveSpacePreferences(SpaceData sg)
		{
			this.status = "";
			string text = "SAVES//spaceprefs";
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
					binaryWriter.Write(sg.pad_invertX);
					binaryWriter.Write(sg.pad_invertY);
					binaryWriter.Write(sg.pad_sensitivityX);
					binaryWriter.Write(sg.pad_sensitivityY);
					binaryWriter.Write(sg.pad_winvertX);
					binaryWriter.Write(sg.pad_winvertY);
					binaryWriter.Write(sg.pad_wsensitivityX);
					binaryWriter.Write(sg.pad_wsensitivityY);
					binaryWriter.Write(sg.pad_vibro);
					binaryWriter.Write(sg.allcamsradius[0]);
					binaryWriter.Write(sg.allcamsradius[1]);
					binaryWriter.Write(sg.allcamsradius[2]);
					binaryWriter.Write(sg.allcamsradius[3]);
					binaryWriter.Write(sg.allcamsorbit[0]);
					binaryWriter.Write(sg.allcamsorbit[1]);
					binaryWriter.Write(sg.allcamsorbit[2]);
					binaryWriter.Write(sg.allcamsorbit[3]);
					binaryWriter.Write(sg.allcamsaltitude[0]);
					binaryWriter.Write(sg.allcamsaltitude[1]);
					binaryWriter.Write(sg.allcamsaltitude[2]);
					binaryWriter.Write(sg.allcamsaltitude[3]);
					binaryWriter.Write(sg.allcamslens[0]);
					binaryWriter.Write(sg.allcamslens[1]);
					binaryWriter.Write(sg.allcamslens[2]);
					binaryWriter.Write(sg.allcamslens[3]);
					binaryWriter.Write(sg.pad_rinvertX);
					binaryWriter.Write(sg.pad_rinvertY);
					binaryWriter.Write(sg.pad_rsensitivityX);
					binaryWriter.Write(sg.pad_rsensitivityY);
					binaryWriter.Write(sg.roverindex);
					binaryWriter.Write(sg.roverrotlock);
					binaryWriter.Write(sg.roverhitelock);
					binaryWriter.Write(sg.roverdist[0]);
					binaryWriter.Write(sg.roverdist[1]);
					binaryWriter.Write(sg.roverdist[2]);
					binaryWriter.Write(sg.roverheight[0]);
					binaryWriter.Write(sg.roverheight[1]);
					binaryWriter.Write(sg.roverheight[2]);
					binaryWriter.Write(sg.roverradian[0]);
					binaryWriter.Write(sg.roverradian[1]);
					binaryWriter.Write(sg.roverradian[2]);
					binaryWriter.Write(sg.landerindex);
					binaryWriter.Write(sg.landerrotlock);
					binaryWriter.Write(sg.landerhitelock);
					binaryWriter.Write(sg.landerdist[0]);
					binaryWriter.Write(sg.landerdist[1]);
					binaryWriter.Write(sg.landerdist[2]);
					binaryWriter.Write(sg.landerheight[0]);
					binaryWriter.Write(sg.landerheight[1]);
					binaryWriter.Write(sg.landerheight[2]);
					binaryWriter.Write(sg.landerradian[0]);
					binaryWriter.Write(sg.landerradian[1]);
					binaryWriter.Write(sg.landerradian[2]);
					binaryWriter.Close();
				}
			}
			catch
			{
			}
		}

		// Token: 0x040006C7 RID: 1735
		public string status = "";
	}
}
