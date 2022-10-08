using HarmonyLib;
using MelonLoader;
using SRModCore;
using Synth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SRPerfectTracker
{
    public class SRPerfectTracker : MelonMod, IStageEvents
    {
        public static SRPerfectTracker Instance { get; private set; }

        private SRLogger logger;
        private StageEventHandler stageEventHandler;
        private TMPro.TextMeshPro scoreTextField;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            logger = new MelonLoggerWrapper(LoggerInstance);
            stageEventHandler = new StageEventHandler(logger, this);
            Instance = this;
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            base.OnSceneWasInitialized(buildIndex, sceneName);

            var scene = new SRScene(sceneName);
            logger.Msg($"IsStage? {scene.IsStage()}. Type: {scene.SceneType}");
            if (scene.IsStage())
            {
                var gcm = GameControlManager.s_instance;

                stageEventHandler.SetupEvents(gcm);

                var scoreManagerTraverse = Traverse.Create(gcm?.ScoreManager);
                scoreTextField = scoreManagerTraverse?.Field<TMPro.TextMeshPro>("m_scoreTextField")?.Value;
            }
        }

        private void UpdatePerfectIndicator()
        {
            var mgr = GameControlManager.s_instance;
            if (mgr == null)
            {
                return;
            }

            if (mgr.TotalNormalNotes > 0 || mgr.TotalBadNotes > 0 || mgr.TotalFailNotes > 0 || mgr.TotalFailWalls > 0)
            {
                RemovePerfectIndicator();
            }
        }

        private void AddPerfectIndicator()
        {
            if (scoreTextField != null)
            {
                scoreTextField.outlineColor = Color.yellow;
                scoreTextField.outlineWidth = 0.2f;
            }
        }

        private void RemovePerfectIndicator()
        {
            if (scoreTextField != null)
            {
                scoreTextField.outlineColor = Color.white;
                scoreTextField.outlineWidth = 0;
                stageEventHandler.RemoveEvents();
            }
        }

        public void OnSongStart()
        {
            logger.Msg("Song start. Setting up perfect indicator");
            AddPerfectIndicator();
        }

        public void OnSongEnd()
        {
            logger.Msg("SongEnd");
        }

        public void OnNoteHit()
        {
            //logger.Msg("NoteHit");
            UpdatePerfectIndicator();
        }

        public void OnNoteFail()
        {
            //logger.Msg("NoteFail");
            UpdatePerfectIndicator();
        }

        public void OnEnterSpecial()
        {
            //logger.Msg("EnterSpecial");
        }

        public void OnCompleteSpecial()
        {
            //logger.Msg("CompleteSpecial");
            UpdatePerfectIndicator();
        }

        public void OnFailSpecial()
        {
            //logger.Msg("FailSpecial");
            UpdatePerfectIndicator();
        }
    }
}
