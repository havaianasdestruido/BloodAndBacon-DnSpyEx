using System;
using System.Collections.Generic;

namespace Blood
{
	// Token: 0x02000096 RID: 150
	internal class packetSender
	{
		// Token: 0x06000595 RID: 1429 RVA: 0x00136021 File Offset: 0x00134221
		public packetSender()
		{
			this.packet = new List<byte>();
			this.packet.Clear();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00136056 File Offset: 0x00134256
		public void Write(byte val)
		{
			this.packet.Add(val);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00136064 File Offset: 0x00134264
		public void Write(float val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte b in bytes)
			{
				this.packet.Add(b);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00136098 File Offset: 0x00134298
		public void Write(int val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte b in bytes)
			{
				this.packet.Add(b);
			}
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x001360CC File Offset: 0x001342CC
		public void WriteU(ulong val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte b in bytes)
			{
				this.packet.Add(b);
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00136100 File Offset: 0x00134300
		public void Write(uint val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte b in bytes)
			{
				this.packet.Add(b);
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00136134 File Offset: 0x00134334
		public void Write(ushort val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte b in bytes)
			{
				this.packet.Add(b);
			}
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00136168 File Offset: 0x00134368
		public void Write(bool val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte b in bytes)
			{
				this.packet.Add(b);
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0013619C File Offset: 0x0013439C
		public void WriteString(string val)
		{
			for (int i = 0; i < val.Length; i++)
			{
				this.packet.Add(this.getByteFromString(val.Substring(i, 1)));
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x001361D3 File Offset: 0x001343D3
		public byte[] mypackets()
		{
			return this.packet.ToArray();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x001361E0 File Offset: 0x001343E0
		public void clean()
		{
			this.packet.Clear();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x001361F0 File Offset: 0x001343F0
		public byte getByteFromString(string s)
		{
			byte b = 78;
			if (s == "0")
			{
				b = 0;
			}
			if (s == "1")
			{
				b = 1;
			}
			if (s == "2")
			{
				b = 2;
			}
			if (s == "3")
			{
				b = 3;
			}
			if (s == "4")
			{
				b = 4;
			}
			if (s == "5")
			{
				b = 5;
			}
			if (s == "6")
			{
				b = 6;
			}
			if (s == "7")
			{
				b = 7;
			}
			if (s == "8")
			{
				b = 8;
			}
			if (s == "a")
			{
				b = 9;
			}
			if (s == "b")
			{
				b = 10;
			}
			if (s == "c")
			{
				b = 11;
			}
			if (s == "d")
			{
				b = 12;
			}
			if (s == "e")
			{
				b = 13;
			}
			if (s == "f")
			{
				b = 14;
			}
			if (s == "g")
			{
				b = 15;
			}
			if (s == "h")
			{
				b = 16;
			}
			if (s == "i")
			{
				b = 17;
			}
			if (s == "j")
			{
				b = 18;
			}
			if (s == "k")
			{
				b = 19;
			}
			if (s == "l")
			{
				b = 20;
			}
			if (s == "m")
			{
				b = 21;
			}
			if (s == "n")
			{
				b = 22;
			}
			if (s == "o")
			{
				b = 23;
			}
			if (s == "p")
			{
				b = 24;
			}
			if (s == "q")
			{
				b = 25;
			}
			if (s == "r")
			{
				b = 26;
			}
			if (s == "s")
			{
				b = 27;
			}
			if (s == "t")
			{
				b = 28;
			}
			if (s == "u")
			{
				b = 29;
			}
			if (s == "v")
			{
				b = 30;
			}
			if (s == "w")
			{
				b = 31;
			}
			if (s == "x")
			{
				b = 32;
			}
			if (s == "y")
			{
				b = 33;
			}
			if (s == "z")
			{
				b = 34;
			}
			if (s == "A")
			{
				b = 35;
			}
			if (s == "B")
			{
				b = 36;
			}
			if (s == "C")
			{
				b = 37;
			}
			if (s == "D")
			{
				b = 38;
			}
			if (s == "E")
			{
				b = 39;
			}
			if (s == "F")
			{
				b = 40;
			}
			if (s == "G")
			{
				b = 41;
			}
			if (s == "H")
			{
				b = 42;
			}
			if (s == "I")
			{
				b = 43;
			}
			if (s == "J")
			{
				b = 44;
			}
			if (s == "K")
			{
				b = 45;
			}
			if (s == "L")
			{
				b = 46;
			}
			if (s == "M")
			{
				b = 47;
			}
			if (s == "N")
			{
				b = 48;
			}
			if (s == "O")
			{
				b = 49;
			}
			if (s == "P")
			{
				b = 50;
			}
			if (s == "Q")
			{
				b = 51;
			}
			if (s == "R")
			{
				b = 52;
			}
			if (s == "S")
			{
				b = 53;
			}
			if (s == "T")
			{
				b = 54;
			}
			if (s == "U")
			{
				b = 55;
			}
			if (s == "V")
			{
				b = 56;
			}
			if (s == "W")
			{
				b = 57;
			}
			if (s == "X")
			{
				b = 58;
			}
			if (s == "Y")
			{
				b = 59;
			}
			if (s == "Z")
			{
				b = 60;
			}
			if (s == "!")
			{
				b = 61;
			}
			if (s == "@")
			{
				b = 62;
			}
			if (s == "#")
			{
				b = 63;
			}
			if (s == "$")
			{
				b = 64;
			}
			if (s == "%")
			{
				b = 65;
			}
			if (s == "^")
			{
				b = 66;
			}
			if (s == "&")
			{
				b = 67;
			}
			if (s == "*")
			{
				b = 68;
			}
			if (s == "(")
			{
				b = 69;
			}
			if (s == ")")
			{
				b = 70;
			}
			if (s == "<")
			{
				b = 71;
			}
			if (s == ">")
			{
				b = 72;
			}
			if (s == "?")
			{
				b = 73;
			}
			if (s == ":")
			{
				b = 74;
			}
			if (s == "{")
			{
				b = 75;
			}
			if (s == "}")
			{
				b = 76;
			}
			if (s == "_")
			{
				b = 77;
			}
			if (s == "+")
			{
				b = 78;
			}
			if (s == ",")
			{
				b = 79;
			}
			if (s == ".")
			{
				b = 80;
			}
			if (s == "/")
			{
				b = 81;
			}
			if (s == ";")
			{
				b = 82;
			}
			if (s == "[")
			{
				b = 83;
			}
			if (s == "]")
			{
				b = 84;
			}
			if (s == "-")
			{
				b = 85;
			}
			if (s == "=")
			{
				b = 86;
			}
			if (s == " ")
			{
				b = 87;
			}
			if (s == "9")
			{
				b = 88;
			}
			if (s == "'")
			{
				b = 89;
			}
			if (s == "\"")
			{
				b = 90;
			}
			if (s == "|")
			{
				b = 91;
			}
			if (s == "\\")
			{
				b = 92;
			}
			return b;
		}

		// Token: 0x040015CE RID: 5582
		public byte[] packetB = new byte[1];

		// Token: 0x040015CF RID: 5583
		public List<byte> packet = new List<byte>();
	}
}
