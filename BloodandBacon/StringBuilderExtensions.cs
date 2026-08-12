using System;
using System.Text;

namespace Blood
{
	// Token: 0x02000023 RID: 35
	public static class StringBuilderExtensions
	{
		// Token: 0x06000165 RID: 357 RVA: 0x000313FC File Offset: 0x0002F5FC
		public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount, char pad_char, uint base_val)
		{
			uint num = 0U;
			uint num2 = uint_val;
			do
			{
				num2 /= base_val;
				num += 1U;
			}
			while (num2 > 0U);
			string_builder.Append(pad_char, (int)Math.Max(pad_amount, num));
			int num3 = string_builder.Length;
			while (num > 0U)
			{
				num3--;
				string_builder[num3] = StringBuilderExtensions.ms_digits[(int)((UIntPtr)(uint_val % base_val))];
				uint_val /= base_val;
				num -= 1U;
			}
			return string_builder;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00031457 File Offset: 0x0002F657
		public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val)
		{
			string_builder.Concat(uint_val, 0U, StringBuilderExtensions.ms_default_pad_char, 10U);
			return string_builder;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0003146A File Offset: 0x0002F66A
		public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount)
		{
			string_builder.Concat(uint_val, pad_amount, StringBuilderExtensions.ms_default_pad_char, 10U);
			return string_builder;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0003147D File Offset: 0x0002F67D
		public static StringBuilder Concat(this StringBuilder string_builder, uint uint_val, uint pad_amount, char pad_char)
		{
			string_builder.Concat(uint_val, pad_amount, pad_char, 10U);
			return string_builder;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0003148C File Offset: 0x0002F68C
		public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount, char pad_char, uint base_val)
		{
			if (int_val < 0)
			{
				string_builder.Append('-');
				uint num = (uint)(-1 - int_val + 1);
				string_builder.Concat(num, pad_amount, pad_char, base_val);
			}
			else
			{
				string_builder.Concat((uint)int_val, pad_amount, pad_char, base_val);
			}
			return string_builder;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000314C7 File Offset: 0x0002F6C7
		public static StringBuilder Concat(this StringBuilder string_builder, int int_val)
		{
			string_builder.Concat(int_val, 0U, StringBuilderExtensions.ms_default_pad_char, 10U);
			return string_builder;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000314DA File Offset: 0x0002F6DA
		public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount)
		{
			string_builder.Concat(int_val, pad_amount, StringBuilderExtensions.ms_default_pad_char, 10U);
			return string_builder;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000314ED File Offset: 0x0002F6ED
		public static StringBuilder Concat(this StringBuilder string_builder, int int_val, uint pad_amount, char pad_char)
		{
			string_builder.Concat(int_val, pad_amount, pad_char, 10U);
			return string_builder;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000314FC File Offset: 0x0002F6FC
		public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places, uint pad_amount, char pad_char)
		{
			if (decimal_places == 0U)
			{
				int num;
				if (float_val >= 0f)
				{
					num = (int)(float_val + 0.5f);
				}
				else
				{
					num = (int)(float_val - 0.5f);
				}
				string_builder.Concat(num, pad_amount, pad_char, 10U);
			}
			else
			{
				int num2 = (int)float_val;
				string_builder.Concat(num2, pad_amount, pad_char, 10U);
				string_builder.Append('.');
				float num3 = Math.Abs(float_val - (float)num2);
				do
				{
					num3 *= 10f;
					decimal_places -= 1U;
				}
				while (decimal_places > 0U);
				num3 += 0.5f;
				string_builder.Concat((uint)num3, 0U, '0', 10U);
			}
			return string_builder;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00031582 File Offset: 0x0002F782
		public static StringBuilder Concat(this StringBuilder string_builder, float float_val)
		{
			string_builder.Concat(float_val, StringBuilderExtensions.ms_default_decimal_places, 0U, StringBuilderExtensions.ms_default_pad_char);
			return string_builder;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00031598 File Offset: 0x0002F798
		public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places)
		{
			string_builder.Concat(float_val, decimal_places, 0U, StringBuilderExtensions.ms_default_pad_char);
			return string_builder;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000315AA File Offset: 0x0002F7AA
		public static StringBuilder Concat(this StringBuilder string_builder, float float_val, uint decimal_places, uint pad_amount)
		{
			string_builder.Concat(float_val, decimal_places, pad_amount, StringBuilderExtensions.ms_default_pad_char);
			return string_builder;
		}

		// Token: 0x040006F7 RID: 1783
		private static readonly char[] ms_digits = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		// Token: 0x040006F8 RID: 1784
		private static readonly uint ms_default_decimal_places = 5U;

		// Token: 0x040006F9 RID: 1785
		private static readonly char ms_default_pad_char = '0';
	}
}
