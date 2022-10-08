using SRModCore;
using Synth.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace SRPerfectTracker
{
    internal class StageEventHandler
    {
        private SRLogger logger;
        private IStageEvents listener;
        private GameControlManager gameControlManager = null;
        StageEvents stageEvents;

        public StageEventHandler(SRLogger logger, IStageEvents eventListener)
        {
            this.logger = logger;
            this.listener = eventListener;

            stageEvents = new StageEvents();
            stageEvents.OnSongStart = new UnityEvent();
            stageEvents.OnSongEnd = new UnityEvent();
            stageEvents.OnNoteHit = new UnityEvent();
            stageEvents.OnNoteFail = new UnityEvent();
            stageEvents.OnEnterSpecial = new UnityEvent();
            stageEvents.OnCompleteSpecial = new UnityEvent();
            stageEvents.OnFailSpecial = new UnityEvent();
        }

        public void RemoveEvents()
        {
            if (stageEvents != null)
            {
                stageEvents.OnSongStart.RemoveListener(listener.OnSongStart);
                stageEvents.OnSongEnd.RemoveListener(listener.OnSongEnd);
                stageEvents.OnNoteHit.RemoveListener(listener.OnNoteHit);
                stageEvents.OnNoteFail.RemoveListener(listener.OnNoteFail);
                stageEvents.OnEnterSpecial.RemoveListener(listener.OnEnterSpecial);
                stageEvents.OnCompleteSpecial.RemoveListener(listener.OnCompleteSpecial);
                stageEvents.OnFailSpecial.RemoveListener(listener.OnFailSpecial);
            }
        }

        private void AddEvents()
        {
            if (stageEvents != null)
            {
                stageEvents.OnSongStart.AddListener(listener.OnSongStart);
                stageEvents.OnSongEnd.AddListener(listener.OnSongEnd);
                stageEvents.OnNoteHit.AddListener(listener.OnNoteHit);
                stageEvents.OnNoteFail.AddListener(listener.OnNoteFail);
                stageEvents.OnEnterSpecial.AddListener(listener.OnEnterSpecial);
                stageEvents.OnCompleteSpecial.AddListener(listener.OnCompleteSpecial);
                stageEvents.OnFailSpecial.AddListener(listener.OnFailSpecial);
            }
        }

        public void SetupEvents(GameControlManager gcm)
        {
            if (gcm == null)
            {
                logger.Msg("GCM null, skipping setup");
                return;
            }

            try
            {
                logger.Msg("Adding stage events!");
                RemoveEvents();
                AddEvents();
                GameControlManager.UpdateStageEventList(stageEvents);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
            }
        }
    }
}
