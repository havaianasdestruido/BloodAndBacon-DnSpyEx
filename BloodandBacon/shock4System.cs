using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Blood
{
	// Token: 0x02000162 RID: 354
	internal class shock4System : ParticleSystem
	{
		// Token: 0x06000CCE RID: 3278 RVA: 0x003768FB File Offset: 0x00374AFB
		public shock4System(Game game, ContentManager content)
			: base(game, content)
		{
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00376908 File Offset: 0x00374B08
		protected override void InitializeSettings(ParticleSettings settings)
		{
			settings.TextureName = "spire4";
			settings.MaxParticles = 24000;
			settings.Duration = TimeSpan.FromSeconds(1.399999976158142);
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
