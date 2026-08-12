using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000AF RID: 175
	internal class shock3System : ParticleSystem
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x00147DB5 File Offset: 0x00145FB5
		public shock3System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00147DC0 File Offset: 0x00145FC0
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spire3";
			settings.MaxParticles = 24000;
			settings.Duration = TimeSpan.FromSeconds(1.100000023841858);
			settings.DurationRandomness = 0.1f;
			settings.EmitterVelocitySensitivity = 2f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 2f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 2f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(250, 170, 205, 120);
			settings.MaxColor = new Color(255, 255, 255, 200);
			settings.MinRotateSpeed = 2f;
			settings.MaxRotateSpeed = 8f;
			settings.MinStartSize = 25f;
			settings.MaxStartSize = 40f;
			settings.MinEndSize = 2f;
			settings.MaxEndSize = 6f;
			settings.BlendState = BlendState.Additive;
		}
	}
}
