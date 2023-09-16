using Cysharp.Threading.Tasks;
using HarmonyLib;
using LBoL.ConfigData;
using LBoL.Core.Cards;
using LBoL.Core.Stations;
using LBoL.EntityLib.EnemyUnits.Character;
using LBoL.Presentation;
using LBoLEntitySideloader;
using LBoLEntitySideloader.Attributes;
using LBoLEntitySideloader.Entities;
using LBoLEntitySideloader.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using static BgmExamples.BepinexPlugin;

namespace BgmExamples
{
    public class BgmDefinitions
    {

        [OverwriteVanilla]
        public sealed class MainmenuBgm : BgmTemplate
        {

            public override IdContainer GetId()
            {
                return "MainMenu";
            }

            public override UniTask<AudioClip> LoadAudioClipAsync()
            {

                return ResourceLoader.LoadAudioClip("badApple.ogg", AudioType.OGGVORBIS, directorySource);
            }

            [DontOverwrite]
            public override BgmConfig MakeConfig()
            {
                throw new NotImplementedException();
            }
        }


        [OverwriteVanilla]
        public sealed class Elite1Bgm : BgmTemplate
        {

            public override IdContainer GetId()
            {
                return "Elite1";
            }

            public override UniTask<AudioClip> LoadAudioClipAsync()
            {

                // ResourceLoader.LoadAudioClip can fetch a clip from web as it uses UnityWebRequestMultimedia.GetAudioClip
                return ResourceLoader.LoadAudioClip("https://upload.thwiki.cc/2/21/th02_10_FM.ogg", AudioType.OGGVORBIS, null, protocol: "");

            }


            public override BgmConfig MakeConfig()
            {
                var config = BgmConfig.FromID(GetId());
                config.TrackName = "恋色Magic";
                config.LoopStart = null;
                config.LoopEnd = null;
                config.Comment = "deeznuts";
                return config;
            }


        }

        public sealed class SeijaBgm : BgmTemplate
        {
            public override IdContainer GetId()
            {
                return nameof(Seija);
            }

            public override UniTask<AudioClip> LoadAudioClipAsync()
            {
                return ResourceLoader.LoadAudioClip("Seija.ogg", AudioType.OGGVORBIS, directorySource);

            }

            public override BgmConfig MakeConfig()
            {
                var config = new BgmConfig(
                        ID: "",
                        No: 0,
                        Show: true,
                        Name: "",
                        Folder: "",
                        Path: "",
                        LoopStart: null,
                        LoopEnd: null,
                        TrackName: "东方辉针城 <Reverse Ideology>",
                        Artist: "ZUN",
                        Original: "リバースイデオロギー ～ Double Dealing Character",
                        Comment: "　鬼人 正邪のテーマです。\r\n　極めて小物臭のする妖怪をイメージして曲を作りました。結果、何だかやんちゃな曲に……まあボスっぽいノリで楽しいよね。\r\n　他人が嫌がることを率先してやる小物なボスですが、それでも今回の黒幕です。"
                );

                return config;
            }



            [HarmonyPatch(typeof(GameMaster), nameof(GameMaster.PlayMusic))]
            class EnableSeijaBgm_Patch
            {

                static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
                {
                    foreach (var ci in instructions)
                    {
                        if (ci.Is(OpCodes.Ldstr, "Seija"))
                        {
                            yield return new CodeInstruction(OpCodes.Ldstr, "");
                        }
                        else
                        {
                            yield return ci;
                        }
                    }
                }

            }


        }










        public sealed class RinBgm : BgmTemplate
        {
            public override IdContainer GetId()
            {
                return "RinHellBgm";
            }

            public override UniTask<AudioClip> LoadAudioClipAsync()
            {
                return ResourceLoader.LoadAudioClip("hell.ogg", AudioType.OGGVORBIS, directorySource);

            }

            public override BgmConfig MakeConfig()
            {
                var config = new BgmConfig(
                        ID: "",
                        No: 0,
                        Show: true,
                        Name: "",
                        Folder: "",
                        Path: "",
                        LoopStart: null,
                        LoopEnd: null,
                        TrackName: "<Lullaby of Deserted Hell>",
                        Artist: "ZUN",
                        Original: "",
                        Comment: ""
                );

                return config;
            }



            [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.PlayEliteBgm))]
            class AudioManager_Patch
            {

                static bool Prefix()
                {

                    if (GameMaster.Instance?.CurrentGameRun?.CurrentStation is BattleStation bs)
                    {
                        if (bs.EnemyGroup.Alives.Any(e => e.Id == nameof(Rin)))
                        { 
                            AudioManager.PlayInLayer1((new RinBgm()).UniqueId);
                            return false;
                        
                        }
                    }
                    return true;

                }
            }
        }

        [OverwriteVanilla]
        public sealed class BgmConfigOverwriteTest : BgmTemplate
        {
            public override IdContainer GetId() => "Stage1";

            [DontOverwrite]
            public override UniTask<AudioClip> LoadAudioClipAsync()
            {
                throw new NotImplementedException();
            }

            public override BgmConfig MakeConfig()
            {
                var con = BgmConfig.FromID("Stage1");
                con.Name = "deeznuts";
                return con;
            }
        }


    }





}

