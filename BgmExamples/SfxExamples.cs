using Cysharp.Threading.Tasks;
using LBoL.ConfigData;
using LBoL.Core;
using LBoL.Core.Units;
using LBoL.EntityLib.Exhibits.Shining;
using LBoL.Presentation;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static BgmExamples.BepinexPlugin;

namespace BgmExamples
{
    /*    [OverwriteVanilla]
        public sealed class ReimuUltDeeznutsSfxDef : SfxTemplate
        {
            // sfx on Reimu bomb use
            public override IdContainer GetId() => "ReimuSpellLaunch";

            public override List<UniTask<AudioClip>> LoadSfxListAsync()
            {
                // on AudioManger.PlaySfx() ones of these sfx will be played randomly
                return  new List<UniTask<AudioClip>>() { 
                    ResourceLoader.LoadAudioClip("deeznuts.ogg", AudioType.OGGVORBIS, sfxDir),
                    ResourceLoader.LoadAudioClip("gotim.ogg", AudioType.OGGVORBIS, sfxDir)
                };
            }

            public override SfxConfig MakeConfig()
            {
                return DefaultConfig();
            }
        }*/



    public sealed class CaCawSfxDef : SfxTemplate
    {
        public override IdContainer GetId() => "Ca-Caw";

        public override List<UniTask<AudioClip>> LoadSfxListAsync()
        {
            return new List<UniTask<AudioClip>>() {
                ResourceLoader.LoadAudioClip("ca-caw.ogg", AudioType.OGGVORBIS, sfxDir)
            };
        }

        public override SfxConfig MakeConfig()
        {
            return DefaultConfig();
        }
    }


    public sealed class CaCawExhibitDef : ExhibitTemplate
    {
        public override IdContainer GetId() => nameof(CaCawExhibit);

        public override LocalizationOption LoadLocalization()
        {
            return null;
        }

        public override ExhibitSprites LoadSprite()
        {
            return null;
        }

        public override ExhibitConfig MakeConfig()
        {
            return DefaultConfig();
        }

        [EntityLogic(typeof(CaCawExhibitDef))]
        public sealed class CaCawExhibit : Exhibit
        {
            protected override void OnEnterBattle()
            {
                HandleBattleEvent(Battle.BattleStarted, (GameEventArgs args) => { AudioManager.PlaySfx((new CaCawSfxDef()).UniqueId); });
            }

            
        }
    }

    // disabled this def cuz it was too awful
    [OverwriteVanilla]
    public /*sealed*/ class ButtonClick0UiSfxDef : UiSoundTemplate
    {
        public override IdContainer GetId() => "ButtonClick0";

        public override List<UniTask<AudioClip>> LoadSfxListAsync()
        {
            return new List<UniTask<AudioClip>>() {
                ResourceLoader.LoadAudioClip("deeznuts.ogg", AudioType.OGGVORBIS, sfxDir),
                ResourceLoader.LoadAudioClip("gotim.ogg", AudioType.OGGVORBIS, sfxDir)
            };
        }

        public override UiSoundConfig MakeConfig()
        {
            return DefaultConfig();
        }
    }
}
