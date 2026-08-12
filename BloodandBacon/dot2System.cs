using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000068 RID: 104
	internal class dot2System : ParticleSystem
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x000E26FB File Offset: 0x000E08FB
		public dot2System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000E2708 File Offset: 0x000E0908
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "dot2";
			settings.MaxParticles = 4000;
			settings.Duration = TimeSpan.FromSeconds(3.200000047683716);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, -5f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(80, 90, 170, 160);
			settings.MaxColor = new Color(255, 255, 255, 250);
			settings.MinRotateSpeed = 6f;
			settings.MaxRotateSpeed = 22f;
			settings.MinStartSize = 10f;
			settings.MaxStartSize = 40f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 6f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
