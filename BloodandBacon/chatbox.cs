using System;

namespace Blood
{
	// Token: 0x020000A5 RID: 165
	public class chatbox
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00142352 File Offset: 0x00140552
		public chatbox()
		{
			this.message2send = false;
			this.message = "";
			this.len1 = 0;
			this.len2 = 0;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0014238C File Offset: 0x0014058C
		public string getMessage(ulong num1, byte len1, ulong num2, byte len2)
		{
			string text = "";
			for (int i = 0; i < (int)len1; i++)
			{
				ulong num3 = this.ReadFromBigfield(ref num1, (ulong)((long)i));
				text += this.getLetter(num3);
			}
			for (int j = 0; j < (int)len2; j++)
			{
				ulong num4 = this.ReadFromBigfield(ref num2, (ulong)((long)j));
				text += this.getLetter(num4);
			}
			return text.ToLower();
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x001423F4 File Offset: 0x001405F4
		public void message2nums(string mess)
		{
			this.bignum1 = 0UL;
			this.bignum2 = 0UL;
			this.len1 = 0;
			this.len2 = 0;
			char[] array = mess.ToCharArray();
			int num = array.Length;
			if (num > 12)
			{
				num = 12;
			}
			for (int i = 0; i < num; i++)
			{
				this.AddToBigfield(ref this.bignum1, (ulong)((long)i), this.getNum(array[i]));
				this.len1 = (byte)num;
			}
			if (array.Length > 12)
			{
				ulong num2 = 0UL;
				for (int j = 12; j < array.Length; j++)
				{
					this.AddToBigfield(ref this.bignum2, num2, this.getNum(array[j]));
					num2 += 1UL;
					this.len2 = (byte)(array.Length - 12);
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x001424A3 File Offset: 0x001406A3
		public void AddToBigfield(ref ulong bitfield, ulong bitCount, ulong value)
		{
			bitfield += value * (ulong)Math.Pow(32.0, bitCount);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x001424C0 File Offset: 0x001406C0
		public ulong getNum(char c)
		{
			ulong num = 1UL;
			string text = c.ToString();
			text = text.ToUpper();
			if (text == "A")
			{
				num = 0UL;
			}
			if (text == "B")
			{
				num = 1UL;
			}
			if (text == "C")
			{
				num = 2UL;
			}
			if (text == "D")
			{
				num = 3UL;
			}
			if (text == "E")
			{
				num = 4UL;
			}
			if (text == "F")
			{
				num = 5UL;
			}
			if (text == "G")
			{
				num = 6UL;
			}
			if (text == "H")
			{
				num = 7UL;
			}
			if (text == "I")
			{
				num = 8UL;
			}
			if (text == "J")
			{
				num = 9UL;
			}
			if (text == "K")
			{
				num = 10UL;
			}
			if (text == "L")
			{
				num = 11UL;
			}
			if (text == "M")
			{
				num = 12UL;
			}
			if (text == "N")
			{
				num = 13UL;
			}
			if (text == "O")
			{
				num = 14UL;
			}
			if (text == "P")
			{
				num = 15UL;
			}
			if (text == "Q")
			{
				num = 16UL;
			}
			if (text == "R")
			{
				num = 17UL;
			}
			if (text == "S")
			{
				num = 18UL;
			}
			if (text == "T")
			{
				num = 19UL;
			}
			if (text == "U")
			{
				num = 20UL;
			}
			if (text == "V")
			{
				num = 21UL;
			}
			if (text == "W")
			{
				num = 22UL;
			}
			if (text == "X")
			{
				num = 23UL;
			}
			if (text == "Y")
			{
				num = 24UL;
			}
			if (text == "Z")
			{
				num = 25UL;
			}
			if (text == " ")
			{
				num = 26UL;
			}
			if (text == "?")
			{
				num = 27UL;
			}
			if (text == "!")
			{
				num = 28UL;
			}
			if (text == ":")
			{
				num = 29UL;
			}
			if (text == ")")
			{
				num = 30UL;
			}
			if (text == "(")
			{
				num = 31UL;
			}
			return num;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x001426F8 File Offset: 0x001408F8
		public ulong ReadFromBigfield(ref ulong bitfield, ulong bitCount)
		{
			ulong num = (ulong)Math.Pow(32.0, bitCount + 1UL);
			ulong num2 = (ulong)Math.Pow(32.0, bitCount);
			return bitfield % num / num2;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00142734 File Offset: 0x00140934
		public string getLetter(ulong val)
		{
			string text = ".";
			if (val == 0UL)
			{
				text = "A";
			}
			if (val == 1UL)
			{
				text = "B";
			}
			if (val == 2UL)
			{
				text = "C";
			}
			if (val == 3UL)
			{
				text = "D";
			}
			if (val == 4UL)
			{
				text = "E";
			}
			if (val == 5UL)
			{
				text = "F";
			}
			if (val == 6UL)
			{
				text = "G";
			}
			if (val == 7UL)
			{
				text = "H";
			}
			if (val == 8UL)
			{
				text = "I";
			}
			if (val == 9UL)
			{
				text = "J";
			}
			if (val == 10UL)
			{
				text = "K";
			}
			if (val == 11UL)
			{
				text = "L";
			}
			if (val == 12UL)
			{
				text = "M";
			}
			if (val == 13UL)
			{
				text = "N";
			}
			if (val == 14UL)
			{
				text = "O";
			}
			if (val == 15UL)
			{
				text = "P";
			}
			if (val == 16UL)
			{
				text = "Q";
			}
			if (val == 17UL)
			{
				text = "R";
			}
			if (val == 18UL)
			{
				text = "S";
			}
			if (val == 19UL)
			{
				text = "T";
			}
			if (val == 20UL)
			{
				text = "U";
			}
			if (val == 21UL)
			{
				text = "V";
			}
			if (val == 22UL)
			{
				text = "W";
			}
			if (val == 23UL)
			{
				text = "X";
			}
			if (val == 24UL)
			{
				text = "Y";
			}
			if (val == 25UL)
			{
				text = "Z";
			}
			if (val == 26UL)
			{
				text = " ";
			}
			if (val == 27UL)
			{
				text = "?";
			}
			if (val == 28UL)
			{
				text = "!";
			}
			if (val == 29UL)
			{
				text = ":";
			}
			if (val == 30UL)
			{
				text = ")";
			}
			if (val == 31UL)
			{
				text = "(";
			}
			return text;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x001428C0 File Offset: 0x00140AC0
		public string getStringfromByte(byte mybyte)
		{
			string text = "*";
			if (mybyte == 0)
			{
				text = "0";
			}
			if (mybyte == 1)
			{
				text = "1";
			}
			if (mybyte == 2)
			{
				text = "2";
			}
			if (mybyte == 3)
			{
				text = "3";
			}
			if (mybyte == 4)
			{
				text = "4";
			}
			if (mybyte == 5)
			{
				text = "5";
			}
			if (mybyte == 6)
			{
				text = "6";
			}
			if (mybyte == 7)
			{
				text = "7";
			}
			if (mybyte == 8)
			{
				text = "8";
			}
			if (mybyte == 9)
			{
				text = "a";
			}
			if (mybyte == 10)
			{
				text = "b";
			}
			if (mybyte == 11)
			{
				text = "c";
			}
			if (mybyte == 12)
			{
				text = "d";
			}
			if (mybyte == 13)
			{
				text = "e";
			}
			if (mybyte == 14)
			{
				text = "f";
			}
			if (mybyte == 15)
			{
				text = "g";
			}
			if (mybyte == 16)
			{
				text = "h";
			}
			if (mybyte == 17)
			{
				text = "i";
			}
			if (mybyte == 18)
			{
				text = "j";
			}
			if (mybyte == 19)
			{
				text = "k";
			}
			if (mybyte == 20)
			{
				text = "l";
			}
			if (mybyte == 21)
			{
				text = "m";
			}
			if (mybyte == 22)
			{
				text = "n";
			}
			if (mybyte == 23)
			{
				text = "o";
			}
			if (mybyte == 24)
			{
				text = "p";
			}
			if (mybyte == 25)
			{
				text = "q";
			}
			if (mybyte == 26)
			{
				text = "r";
			}
			if (mybyte == 27)
			{
				text = "s";
			}
			if (mybyte == 28)
			{
				text = "t";
			}
			if (mybyte == 29)
			{
				text = "u";
			}
			if (mybyte == 30)
			{
				text = "v";
			}
			if (mybyte == 31)
			{
				text = "w";
			}
			if (mybyte == 32)
			{
				text = "x";
			}
			if (mybyte == 33)
			{
				text = "y";
			}
			if (mybyte == 34)
			{
				text = "z";
			}
			if (mybyte == 35)
			{
				text = "A";
			}
			if (mybyte == 36)
			{
				text = "B";
			}
			if (mybyte == 37)
			{
				text = "C";
			}
			if (mybyte == 38)
			{
				text = "D";
			}
			if (mybyte == 39)
			{
				text = "E";
			}
			if (mybyte == 40)
			{
				text = "F";
			}
			if (mybyte == 41)
			{
				text = "G";
			}
			if (mybyte == 42)
			{
				text = "H";
			}
			if (mybyte == 43)
			{
				text = "I";
			}
			if (mybyte == 44)
			{
				text = "J";
			}
			if (mybyte == 45)
			{
				text = "K";
			}
			if (mybyte == 46)
			{
				text = "L";
			}
			if (mybyte == 47)
			{
				text = "M";
			}
			if (mybyte == 48)
			{
				text = "N";
			}
			if (mybyte == 49)
			{
				text = "O";
			}
			if (mybyte == 50)
			{
				text = "P";
			}
			if (mybyte == 51)
			{
				text = "Q";
			}
			if (mybyte == 52)
			{
				text = "R";
			}
			if (mybyte == 53)
			{
				text = "S";
			}
			if (mybyte == 54)
			{
				text = "T";
			}
			if (mybyte == 55)
			{
				text = "U";
			}
			if (mybyte == 56)
			{
				text = "V";
			}
			if (mybyte == 57)
			{
				text = "W";
			}
			if (mybyte == 58)
			{
				text = "X";
			}
			if (mybyte == 59)
			{
				text = "Y";
			}
			if (mybyte == 60)
			{
				text = "Z";
			}
			if (mybyte == 61)
			{
				text = "!";
			}
			if (mybyte == 62)
			{
				text = "@";
			}
			if (mybyte == 63)
			{
				text = "#";
			}
			if (mybyte == 64)
			{
				text = "$";
			}
			if (mybyte == 65)
			{
				text = "%";
			}
			if (mybyte == 66)
			{
				text = "^";
			}
			if (mybyte == 67)
			{
				text = "&";
			}
			if (mybyte == 68)
			{
				text = "*";
			}
			if (mybyte == 69)
			{
				text = "(";
			}
			if (mybyte == 70)
			{
				text = ")";
			}
			if (mybyte == 71)
			{
				text = "<";
			}
			if (mybyte == 72)
			{
				text = ">";
			}
			if (mybyte == 73)
			{
				text = "?";
			}
			if (mybyte == 74)
			{
				text = ":";
			}
			if (mybyte == 75)
			{
				text = "{";
			}
			if (mybyte == 76)
			{
				text = "}";
			}
			if (mybyte == 77)
			{
				text = "_";
			}
			if (mybyte == 78)
			{
				text = "+";
			}
			if (mybyte == 79)
			{
				text = ",";
			}
			if (mybyte == 80)
			{
				text = ".";
			}
			if (mybyte == 81)
			{
				text = "/";
			}
			if (mybyte == 82)
			{
				text = ";";
			}
			if (mybyte == 83)
			{
				text = "[";
			}
			if (mybyte == 84)
			{
				text = "]";
			}
			if (mybyte == 85)
			{
				text = "-";
			}
			if (mybyte == 86)
			{
				text = "=";
			}
			if (mybyte == 87)
			{
				text = " ";
			}
			if (mybyte == 88)
			{
				text = "9";
			}
			if (mybyte == 89)
			{
				text = "'";
			}
			if (mybyte == 90)
			{
				text = "\"";
			}
			if (mybyte == 91)
			{
				text = "|";
			}
			if (mybyte == 92)
			{
				text = "\\";
			}
			return text;
		}

		// Token: 0x0400171A RID: 5914
		private ulong num;

		// Token: 0x0400171B RID: 5915
		public bool message2send;

		// Token: 0x0400171C RID: 5916
		public int messageIndex = -1;

		// Token: 0x0400171D RID: 5917
		public bool sendAgain;

		// Token: 0x0400171E RID: 5918
		public string message = "";

		// Token: 0x0400171F RID: 5919
		public ulong bignum1;

		// Token: 0x04001720 RID: 5920
		public ulong bignum2;

		// Token: 0x04001721 RID: 5921
		public byte len1;

		// Token: 0x04001722 RID: 5922
		public byte len2;
	}
}
