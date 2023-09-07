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
    public class Generation
    {
        public static void InnitGen()
        {
            EntityManager.AddPostLoadAction(() => 
            {
                sfxGen.QueueGen("ReimuSpellLaunch", overwriteVanilla: true, () => (new DummySfxDef()).DefaultConfig(), () => new List<UniTask<AudioClip>>() {
                                        ResourceLoader.LoadAudioClip("deeznuts.ogg", AudioType.OGGVORBIS, sfxDir),
                                        ResourceLoader.LoadAudioClip("gotim.ogg", AudioType.OGGVORBIS, sfxDir)
                                    });


                //pls no
                /*                uiSoundGen.QueueGen("ButtonClick0", overwriteVanilla: true, () => (new DummyUiSoundDef()).DefaultConfig(), () => new List<UniTask<AudioClip>>() {
                                        ResourceLoader.LoadAudioClip("deeznuts.ogg", AudioType.OGGVORBIS, sfxDir),
                                        ResourceLoader.LoadAudioClip("gotim.ogg", AudioType.OGGVORBIS, sfxDir)
                                    });*/


                // quickly load bunch of sfx
                foreach (var fi in sfxDir.dirInfo.GetFiles("*.ogg"))
                { 
                    sfxGen.QueueGen(fi.Name, overwriteVanilla: false, () => (new DummySfxDef()).DefaultConfig(), () => new List<UniTask<AudioClip>>() { ResourceLoader.LoadAudioClip(fi.Name, AudioType.OGGVORBIS, sfxDir) });

                    // loading the same sfx for ui sounds is probably overkill
                    uiSoundGen.QueueGen(fi.Name, overwriteVanilla: false, () => (new DummyUiSoundDef()).DefaultConfig(), () => new List<UniTask<AudioClip>>() { ResourceLoader.LoadAudioClip(fi.Name, AudioType.OGGVORBIS, sfxDir) });
                }



                uiSoundGen.FinalizeGen();
                sfxGen.FinalizeGen();
            });
        
        }



        class DummySfxDef : SfxTemplate
        {
            public override IdContainer GetId()
            {
                throw new NotImplementedException();
            }

            public override List<UniTask<AudioClip>> LoadSfxListAsync()
            {
                throw new NotImplementedException();
            }

            public override SfxConfig MakeConfig()
            {
                throw new NotImplementedException();
            }
        }

        class DummyUiSoundDef : UiSoundTemplate
        {
            public override IdContainer GetId()
            {
                throw new NotImplementedException();
            }

            public override List<UniTask<AudioClip>> LoadSfxListAsync()
            {
                throw new NotImplementedException();
            }

            public override UiSoundConfig MakeConfig()
            {
                throw new NotImplementedException();
            }
        }
    }
}
