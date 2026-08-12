using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x020000DE RID: 222
	internal class tracerxSystem : ParticleSystem
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x001CAA12 File Offset: 0x001C8C12
		public tracerxSystem(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x001CAA1C File Offset: 0x001C8C1C
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "tracer2";
			settings.MaxParticles = 39000;
			settings.Duration = TimeSpan.FromSeconds(30.0);
			settings.DurationRandomness = 0f;
			settings.EmitterVelocitySensitivity = 3f;
			settings.MinHorizontalVelocity = 0f;
			settings.MaxHorizontalVelocity = 0f;
			settings.MinVerticalVelocity = 0f;
			settings.MaxVerticalVelocity = 0f;
			settings.Gravity = new Vector3(0f, 0f, 0f);
			settings.EndVelocity = 0f;
			settings.MinColor = new Color(170, 170, 170, 170);
			settings.MaxColor = new Color(170, 170, 170, 170);
			settings.MinRotateSpeed = 0f;
			settings.MaxRotateSpeed = 0f;
			settings.MinStartSize = 8f;
			settings.MaxStartSize = 8f;
			settings.MinEndSize = 40f;
			settings.MaxEndSize = 40f;
			settings.BlendState = BlendState.NonPremultiplied;
		}
	}
}
